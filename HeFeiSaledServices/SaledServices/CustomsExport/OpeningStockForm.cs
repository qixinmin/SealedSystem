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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SaledServices.CustomsExport
{
    public partial class OpeningStockForm : Form
    {
        public OpeningStockForm()
        {
            InitializeComponent();
        }

        public void exportxmlbutton_Click(object sender, EventArgs e)
        {
            //DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            //DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));   

            //if (DateTime.Compare(time1,time2)>0) //判断日期大小
            //{
            //    MessageBox.Show("开始日期大于结束");
            //    return;
            //}

            //string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            //string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            OpeningStockClass openingstock = new OpeningStockClass();
            List<StoreInit> storeInitList = new List<StoreInit>();

            List<StockCheck> StockCheckList = new List<StockCheck>();

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

                Dictionary<string, string> _71bomDescribeDic = new Dictionary<string, string>();//料号与描述对应
                cmd.CommandText = "select distinct material_mpn,material_vendor_pn,_description from LCFC71BOM_table";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (_71bomDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        _71bomDic.Add(querySdr[0].ToString().Trim(), querySdr[1].ToString().Trim());
                    }

                    if (_71bomDescribeDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        _71bomDescribeDic.Add(querySdr[0].ToString().Trim(), querySdr[2].ToString().Trim());
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

                // if (newBankNo.Checked) //调整阶段，期初都用新账册
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

                    string temp = kvp.Key;
                    if (temp.Length == 10 && temp.StartsWith("000"))
                    {
                        temp = temp.Substring(3);
                    }

                    init1.cop_g_no = temp;//维修的板子，使用客户料号
                    init1.qty = kvp.Value.ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                   // storeInitList.Add(init1);//在待维修库里面存在  

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = temp;
                    stockcheck.num = kvp.Value.ToString();
                    stockcheck.house = "成品之前";
                    stockcheck.place = "";
                    stockcheck.describe = "维修中主板";
                    if (stockcheck.num != "0")
                    {
                        //  StockCheckList.Add(stockcheck);//在待维修库里面存在  
                    }
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

                cmd.CommandText = "select mpn, number,house,place from store_house where mpn !='' and number!='0'";
                querySdr = cmd.ExecuteReader();                
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear ="";
                    bool isMB = false;
                    if(_71bomDic.ContainsKey( querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if(currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();//buffer主板直接用71料号存储的                       
                        if (currentDeclear.Length == 10 && currentDeclear.StartsWith("000"))
                        {
                            currentDeclear = currentDeclear.Substring(3);
                            isMB = true;
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

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no =isMB ? currentDeclear:querySdr[0].ToString();
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = querySdr[2].ToString();
                    stockcheck.place = querySdr[3].ToString();
                    if (_71bomDescribeDic.ContainsKey(stockcheck.material_no))
                    {
                        stockcheck.describe = _71bomDescribeDic[stockcheck.material_no];
                    }
                    //if (stockcheck.num != "0")
                    //{
                    //    StockCheckList.Add(stockcheck);
                    //}
                }
                querySdr.Close();

                //单独读取为了生存excel文件
                cmd.CommandText = "select mpn, number,house,place from store_house where mpn !=''";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string currentDeclear = "";
                    bool isMB = false;
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();//buffer主板直接用71料号存储的                       
                        if (currentDeclear.Length == 10 && currentDeclear.StartsWith("000"))
                        {
                            currentDeclear = currentDeclear.Substring(3);
                            isMB = true;
                        }
                    }

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = isMB ? currentDeclear : querySdr[0].ToString();
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = querySdr[2].ToString();
                    stockcheck.place = querySdr[3].ToString();
                    if (_71bomDescribeDic.ContainsKey(stockcheck.material_no))
                    {
                        stockcheck.describe = _71bomDescribeDic[stockcheck.material_no];
                    }
                  //  if (stockcheck.num != "0")是否为零都要导出
                    {
                        StockCheckList.Add(stockcheck);
                    }
                }
                querySdr.Close();

                //3 读取MB/SMT/BGA不良品信息，此处的MB是由CID过来的，所以直接用原始料号即可
                cmd.CommandText = "select mpn, number,house,place from store_house_ng where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;
                    
                    string currentDeclear = "";
                    bool isMB = false;
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()] + "-1";//海关要求料号不一样，加-1
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();//buffer主板直接用71料号存储的                       
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
                    init1.goods_nature = isMB? "E" :"I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = isMB ? currentDeclear : querySdr[0].ToString();
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = querySdr[2].ToString();
                    stockcheck.place = querySdr[3].ToString();
                    if (_71bomDescribeDic.ContainsKey(stockcheck.material_no))
                    {
                        stockcheck.describe = _71bomDescribeDic[stockcheck.material_no];
                    }
                    else
                    {
                        stockcheck.describe = isMB ? "不良品主板" : "";
                    }
                    stockcheck.material_no += "_1";//区分良品与不良品信息
                    if (stockcheck.num != "0")
                    {
                        StockCheckList.Add(stockcheck);
                    }
                }
                querySdr.Close();

                //3-1 读取MB Buffer不良品信息，此处的MB是由良品库过来的，所以直接用原始料号71即可
                cmd.CommandText = "select mpn, number,house,place from store_house_ng_buffer_mb where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear = querySdr[0].ToString().Trim();

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
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = currentDeclear;
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = querySdr[2].ToString();
                    stockcheck.place = querySdr[3].ToString();
                    if (_71bomDescribeDic.ContainsKey(stockcheck.material_no))
                    {
                        stockcheck.describe = _71bomDescribeDic[stockcheck.material_no];
                    }
                    //stockcheck.material_no += "_1";//区分良品与不良品信息
                    if (stockcheck.num != "0")
                    {
                        StockCheckList.Add(stockcheck);
                    }
                }
                querySdr.Close();    

                //4 读取MB待维修库信息
                cmd.CommandText = "select custom_materialNo,leftNumber from wait_repair_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
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
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = temp;
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = "待维修库";
                    stockcheck.place = "";
                    stockcheck.describe = "待维修主板";
                    if (stockcheck.num != "0")
                    {
                        StockCheckList.Add(stockcheck);
                    }
                }
                querySdr.Close();

                //5 读取MB良品库信息
                cmd.CommandText = "select custom_materialNo, leftNumber from repaired_left_house_table where leftNumber !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
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
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                    storeInitList.Add(init1);

                    StockCheck stockcheck = new StockCheck();
                    stockcheck.material_no = temp;
                    stockcheck.num = querySdr[1].ToString();
                    stockcheck.house = "良品库";
                    stockcheck.place = "";
                    stockcheck.describe = "维修后良品主板";
                    if (stockcheck.num != "0")
                    {
                        StockCheckList.Add(stockcheck);
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(StockCheckList);

            openingstock.seq_no = seq_no;
            openingstock.boxtype = boxtype;
            openingstock.flowstateg = flowstateg;
            openingstock.trade_code = trade_code;
            openingstock.ems_no = ems_no;
            openingstock.status = status;

            openingstock.storeInitList = storeInitList;

            //导出xml的逻辑变成excel上传了
            if (storeInitList.Count > 0)
            {
                seq_no += "_新账册号";
                Untils.createOpeningStockXML(openingstock, "D:\\STORE_INIT" + seq_no + "test.xml");
                MessageBox.Show("海关期初库存信息产生成功！");
            }
            else
            {
                MessageBox.Show("没有期初库存信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void generateExcelToCheck(List<StockCheck> StockCheckList)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("料号");
            titleList.Add("数量");
            titleList.Add("库房");
            titleList.Add("储位");
            titleList.Add("描述");
            titleList.Add("真实数量");

            foreach (StockCheck stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();    
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.material_no);
                ct1.Add(stockcheck.num);
                ct1.Add(stockcheck.house);
                ct1.Add(stockcheck.place);
                ct1.Add(stockcheck.describe);
                ct1.Add("");

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\期初盘点表格" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx", titleList, contentList);
        }

       

        //Microsoft.Office.Interop.Excel.Application app = null;// new Microsoft.Office.Interop.Excel.Application();
        //Microsoft.Office.Interop.Excel.Workbooks wbs = null;// app.Workbooks;
        private void uploadExcelButton_Click(object sender, EventArgs e)
        {
            //Dictionary<string, string> realMaterialNum = new Dictionary<string, string>();


            //解析xml，并把所以的料号与数量对上，其他类似之前的做法
            //try
            //{
            //    app = new Microsoft.Office.Interop.Excel.Application();
            //    wbs = app.Workbooks;
            //    Microsoft.Office.Interop.Excel.Workbook wb  = wbs.Open(pathTextBox.Text, 0, false, 5, string.Empty, string.Empty,
            //    false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
            //    string.Empty, true, false, 0, true, 1, 0);

            //    app.DisplayAlerts = false;

            //    Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets["Sheet1"];

            //    int rowLength = ws.UsedRange.Rows.Count;
            //    int columnLength = ws.UsedRange.Columns.Count;

                
            //    for (int i = 2; i <= rowLength; i++)
            //    {
            //        string mpn = "", number = "";
            //            //有可能有空值
            //        mpn = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 1]).Value2.ToString();
            //        number = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 6]).Value2.ToString();

            //        if (mpn.Trim() == "" || number.Trim() == "")
            //        {
            //            MessageBox.Show("数量或料号有空值");
            //            break;
            //        }

            //        realMaterialNum.Add(mpn.Trim(), number.Trim());
            //    }
            //}catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    closeAndKillApp();
            //}

            //下面把内容按之前的方式生成
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            OpeningStockClass openingstock = new OpeningStockClass();
            List<StoreInit> storeInitList = new List<StoreInit>();

            List<StockCheck> StockCheckList = new List<StockCheck>();

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

                Dictionary<string, string> _71bomDescribeDic = new Dictionary<string, string>();//料号与描述对应
                cmd.CommandText = "select distinct material_mpn,material_vendor_pn,_description from LCFC71BOM_table";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (_71bomDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        _71bomDic.Add(querySdr[0].ToString().Trim(), querySdr[1].ToString().Trim());
                    }

                    if (_71bomDescribeDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        _71bomDescribeDic.Add(querySdr[0].ToString().Trim(), querySdr[2].ToString().Trim());
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

                //if (newBankNo.Checked)
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
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string temp = kvp.Key;
                    if (temp.Length == 10 && temp.StartsWith("000"))
                    {
                        temp = temp.Substring(3);
                    }

                    init1.cop_g_no = temp;//维修的板子，使用客户料号
                    init1.qty = kvp.Value.ToString();
                    init1.unit = "007";//固定单位
                    init1.goods_nature = "I";//代码
                    init1.bom_version = "";
                    init1.check_date = Untils.getCustomCurrentDate();
                    init1.date_type = "C";//代码
                    init1.whs_code = "";
                    init1.location_code = "";
                    init1.note = "";
                  //  storeInitList.Add(init1);
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

                cmd.CommandText = "select mpn, number,house,place from store_house where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear = "";
                    bool isMB = false;
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()];
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();//buffer主板直接用71料号存储的                       
                        if (currentDeclear.Length == 10 && currentDeclear.StartsWith("000"))
                        {
                            currentDeclear = currentDeclear.Substring(3);
                            isMB = true;
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

                //3 读取MB/SMT/BGA不良品信息，此处的MB是由CID过来的，所以直接用原始料号即可
                cmd.CommandText = "select mpn, number,house,place from store_house_ng where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear = "";
                    bool isMB = false;
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()))
                    {
                        currentDeclear = _71bomDic[querySdr[0].ToString()] + "-1";//海关要求料号不一样，加-1
                    }
                    else if (currentDeclear == "")
                    {
                        currentDeclear = querySdr[0].ToString();//buffer主板直接用71料号存储的                       
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

                //3-1 读取MB Buffer不良品信息，此处的MB是由良品库过来的，所以直接用原始料号71即可
                cmd.CommandText = "select mpn, number,house,place from store_house_ng_buffer_mb where mpn !='' and number !='0'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    StoreInit init1 = new StoreInit();
                    init1.ems_no = ems_no;

                    string currentDeclear = querySdr[0].ToString().Trim();

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
                string fileName = seq_no;

                if (newBankNo.Checked)
                {
                    fileName = seq_no + "_新账册号";
                }
                Untils.createOpeningStockXML(openingstock, "D:\\STORE_INIT" + fileName + ".xml");
                MessageBox.Show("海关期初库存信息产生成功！");
            }
            else
            {
                MessageBox.Show("没有期初库存信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
