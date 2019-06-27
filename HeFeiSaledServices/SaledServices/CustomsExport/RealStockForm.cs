using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class RealStockForm : Form
    {
        public RealStockForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            RealStockClass realstock = new RealStockClass();
            List<StoreAmount> storeAmountList = new List<StoreAmount>();

            string seq_no = DateTime.Now.ToString("yyyyMMdd") + "2006" + "1";//日期+类型+序号
            string boxtype = "2006";//代码
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
                    if (_71bomDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        _71bomDic.Add(querySdr[0].ToString().Trim(), querySdr[1].ToString().Trim());
                    }
                }
                querySdr.Close();
                //查询物料对照表
                Dictionary<string, string> materialbomDic = new Dictionary<string, string>();
                cmd.CommandText = "select distinct custommaterialNo,vendormaterialNo from MBMaterialCompare";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (materialbomDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        materialbomDic.Add(querySdr[0].ToString().Trim(), querySdr[1].ToString().Trim());
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

                if (newBankNo.Checked)
                {
                    cmd.CommandText = "select indentifier, book_number from company_fixed_table_new";
                    querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        trade_code = querySdr[0].ToString();
                        ems_no = querySdr[1].ToString();
                    }
                    querySdr.Close();
                }

                Dictionary<string, int> receiveOrderDic = new Dictionary<string, int>();
                //1 从收货表中查询信息
                cmd.CommandText = "select custom_materialNo, receivedNum,returnNum,cid_number from receiveOrder where _status !='return'";
                querySdr = cmd.ExecuteReader();
                int receiveNum = 0, returnNum = 0, cidNum = 0;
                while (querySdr.Read())
                {
                    receiveNum = Int32.Parse(querySdr[1].ToString());
                    try
                    {
                        returnNum = Int32.Parse(querySdr[2].ToString());
                    }
                    catch (Exception ex)
                    {
                        returnNum = 0;
                    }
                    try
                    {
                        cidNum = Int32.Parse(querySdr[3].ToString());
                    }
                    catch (Exception ex)
                    {
                        cidNum = 0;
                    }

                    if (receiveOrderDic.ContainsKey(querySdr[0].ToString()))
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
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string temp =  kvp.Key;
                    if (temp.Length == 10 && temp.StartsWith("000"))
                    {
                        temp = temp.Substring(3);
                    }

                    init1.cop_g_no =temp;//正常使用客户料号
                    init1.qty = kvp.Value.ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                  //  storeAmountList.Add(init1);
                }

                //2 读取库房信息,良品
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
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string currentDeclear = "";
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();
                        if (currentDeclear.Length == 10 && currentDeclear.StartsWith("000"))
                        {
                            currentDeclear = currentDeclear.Substring(3);
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
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeAmountList.Add(init1);
                }
                querySdr.Close();

                //3 读取MB/SMT/BGA不良品信息
                cmd.CommandText = "select mpn, number from store_house_ng where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string currentDeclear = "";
                    bool isMB = false;
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()] + "-1";//海关要求料号不一样，加-1
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();
                        if (currentDeclear.Length == 10 && currentDeclear.StartsWith("000"))
                        {
                            currentDeclear = currentDeclear.Substring(3);
                            isMB = true;
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
                    init1.goods_nature = isMB?"E": "I";//代码
                    init1.bom_version = "";
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeAmountList.Add(init1);
                }
                querySdr.Close();

                //3-1 读取MB Buffer不良品信息，此处的MB是由良品库过来的，所以直接用原始料号71即可
                cmd.CommandText = "select mpn, number from store_house_ng_buffer_mb where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string currentDeclear = querySdr[0].ToString();                       

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
                    init1.goods_nature = "E";//代码
                    init1.bom_version = "";
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeAmountList.Add(init1);
                }
                querySdr.Close();

                //4 读取MB待维修库信息
                cmd.CommandText = "select custom_materialNo,leftNumber from wait_repair_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string temp = querySdr[0].ToString();
                    if (temp.Length == 10 && temp.StartsWith("000"))
                    {
                        temp = temp.Substring(3);
                    }

                    init1.cop_g_no = temp;//正常使用客户料号
                    init1.qty = querySdr[1].ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeAmountList.Add(init1);
                }
                querySdr.Close();

                //5 读取MB良品库信息
                cmd.CommandText = "select custom_materialNo, leftNumber from repaired_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreAmount init1 = new StoreAmount();
                    init1.ems_no = ems_no;

                    string temp = querySdr[0].ToString();
                    if (temp.Length == 10 && temp.StartsWith("000"))
                    {
                        temp = temp.Substring(3);
                    }

                    init1.cop_g_no = temp;//正常使用客户料号
                    init1.qty = querySdr[1].ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "E";//代码
                    init1.bom_version = "";
                    init1.stock_date = Untils.getCustomCurrentDate();
                    init1.date_type = "B";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeAmountList.Add(init1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            realstock.seq_no = seq_no;
            realstock.boxtype = boxtype;
            realstock.flowstateg = flowstateg;
            realstock.trade_code = trade_code;
            realstock.ems_no = ems_no;
            realstock.status = status;

            realstock.storeAmountList = storeAmountList;
            if(storeAmountList.Count>0)
            {
                string fileName = seq_no;

                if (newBankNo.Checked)
                {
                    fileName = seq_no + "_新账册号";
                }

                Untils.createRealStockXML(realstock, "D:\\STORE_AMOUNT" + fileName + ".xml");
                MessageBox.Show("海关实盘库存信息产生成功！");
            }
            else
            {
                MessageBox.Show("没有实盘库存信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
