using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class OpeningStockForm : Form
    {
        public OpeningStockForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));   

            if (DateTime.Compare(time1,time2)>0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            OpeningStockClass openingstock = new OpeningStockClass();
            List<StoreInit> storeInitList = new List<StoreInit>();

            string seq_no = DateTime.Now.ToString("yyyyMMdd") + "2005" + "1";//日期+类型+序号

            string boxtype = "2005";//代码 
            string flowstateg = "";
            string trade_code = "";
            string ems_no = "";

            string status = "A";
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //查询71bom
                Dictionary<string, string> _71bomDic = new Dictionary<string, string>();
                cmd.CommandText = "select distinct material_mpn,material_vendor_pn from LCFC71BOM_table";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()) == false)
                    {
                        _71bomDic.Add(querySdr[0].ToString(), querySdr[1].ToString());
                    }
                }
                querySdr.Close();
                //查询物料对照表
                Dictionary<string, string> materialbomDic = new Dictionary<string, string>();
                cmd.CommandText = "select distinct vendormaterialNo,custommaterialNo from MBMaterialCompare";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (materialbomDic.ContainsKey(querySdr[0].ToString()) == false)
                    {
                        materialbomDic.Add(querySdr[0].ToString(), querySdr[1].ToString());
                    }
                }
                querySdr.Close();


                cmd.CommandText = "select indentifier, book_number from company_fixed_table";
                querySdr = cmd.ExecuteReader();
               
                while (querySdr.Read())
                {
                    trade_code = querySdr[0].ToString();
                    ems_no = querySdr[1].ToString();
                }
                querySdr.Close();

                Dictionary<string, int> receiveOrderDic = new Dictionary<string, int>();
                //1 从收货表中查询信息
                cmd.CommandText = "select custom_materialNo, receivedNum,returnNum,cid_number from receiveOrder where _status !='return'";
                querySdr = cmd.ExecuteReader();
                int receiveNum=0, returnNum=0, cidNum = 0;
                while (querySdr.Read())
                {
                    receiveNum = Int32.Parse(querySdr[1].ToString());
                    try
                    {
                       returnNum = Int32.Parse(querySdr[2].ToString());
                    }
                    catch(Exception ex)
                    {
                        returnNum = 0;
                    }
                    try
                    {
                       cidNum = Int32.Parse(querySdr[3].ToString());
                    }
                    catch(Exception ex)
                    {
                        cidNum = 0;
                    }

                    if(receiveOrderDic.ContainsKey(querySdr[0].ToString()))
                    {
                        //加上原来的数量
                        receiveOrderDic[querySdr[0].ToString()] = receiveOrderDic[querySdr[0].ToString()] + receiveNum - returnNum - cidNum;
                    }
                    else
                    {
                        receiveOrderDic.Add(querySdr[0].ToString(), receiveNum - returnNum - cidNum);
                    }                    
                }
                querySdr.Close();

                foreach (KeyValuePair<string, int> kvp in receiveOrderDic)
                {                    
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    init1.cop_g_no = kvp.Key;//维修的板子，使用客户料号
                    init1.qty = kvp.Value.ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }

                //2 读取良品库房信息
                Dictionary<string, string> mpn_unit = new Dictionary<string, string>();
                cmd.CommandText = "select distinct mpn,declare_unit from stock_in_sheet where mpn !=''";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (mpn_unit.ContainsKey(querySdr[0].ToString()) == false)
                    {
                        mpn_unit.Add(querySdr[0].ToString(), querySdr[1].ToString());
                    }

                }
                querySdr.Close();

                cmd.CommandText = "select mpn, number from store_house where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();                
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear ="";
                    if(_71bomDic.ContainsKey( querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if(currentDeclear == "")
                    {
                        if(materialbomDic.ContainsKey( querySdr[0].ToString()))
                        {
                            currentDeclear = materialbomDic[querySdr[0].ToString()];
                        }
                        else
                        {
                            MessageBox.Show("良品库房 库存的物料" + querySdr[0].ToString() + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                    }

                    init1.cop_g_no = currentDeclear;//因为报关原因，需要改成71料号（联想料号）TODO，包括材料与买的MB，物料对照表与71bom
                    init1.qty = querySdr[1].ToString();
                    try
                    {
                        init1.unit = Untils.getCustomCode(mpn_unit[querySdr[0].ToString()]);
                    }
                    catch (Exception ex)
                    {
                        init1.unit = "007";
                    }
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();

                //3 读取MB/SMT/BGA不良品信息
                cmd.CommandText = "select mpn, number from store_house_ng where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    
                    string currentDeclear = "";
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if (currentDeclear == "")
                    {
                        if (materialbomDic.ContainsKey(querySdr[0].ToString()))
                        {
                            currentDeclear = materialbomDic[querySdr[0].ToString()];
                        }
                        else
                        {
                            MessageBox.Show("MB/SMT/BGA不良品库存的物料" + querySdr[0].ToString() + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                    }

                    init1.cop_g_no = currentDeclear;//因为报关原因，需要改成71料号（联想料号）TODO
                    init1.qty = querySdr[1].ToString();                   

                    try
                    {
                        init1.unit = Untils.getCustomCode(mpn_unit[querySdr[0].ToString()]);
                    }
                    catch (Exception ex)
                    {
                        init1.unit = "007";
                    }
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();               

                //4 读取MB待维修库信息
                cmd.CommandText = "select custom_materialNo,leftNumber from wait_repair_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    init1.cop_g_no = querySdr[0].ToString();//正常使用客户料号
                    init1.qty = querySdr[1].ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();

                //5 读取MB良品库信息
                cmd.CommandText = "select custom_materialNo, leftNumber from repaired_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    init1.cop_g_no = querySdr[0].ToString();//正常使用客户料号
                    init1.qty = querySdr[1].ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            openingstock.seq_no = seq_no;
            openingstock.boxtype = boxtype;
            openingstock.flowstateg = flowstateg;
            openingstock.trade_code = trade_code;
            openingstock.ems_no = ems_no;
            openingstock.status = status;

            openingstock.storeInitList = storeInitList;

            if (storeInitList.Count > 0)
            {
                Untils.createOpeningStockXML(openingstock, "D:\\STORE_INIT" + seq_no + ".xml");
                MessageBox.Show("海关期初库存信息产生成功！");
            }
            else
            {
                MessageBox.Show("没有期初库存信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
