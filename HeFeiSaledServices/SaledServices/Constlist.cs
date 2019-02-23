using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using NPOI.HSSF.UserModel;

namespace SaledServices
{

    public class stationInfoRecord
    {
        public string id { set; get; }
        public string trackno{set;get;}
        public string station { set; get; }
        public string recordstatus { set; get; }//ok, fail
        public string recorddate { set; get; }

        //1 如果是维修，维修位置，数量，与料号，2  如果是bga则位置，数量为1， 与料号， 3 如果是mylar，则位置，数量，与料号
        public string repairplace1 { set; get; }
        public string repairnum1 { set; get; }
        public string repairMaterial1 { set; get; }

        public string inputer { set; get; }
        //public string repairplace2 { set; get; }
        //public string repairnum2 { set; get; }
        //public string repairMaterial2 { set; get; }

        //public string repairplace3 { set; get; }
        //public string repairnum3 { set; get; }
        //public string repairMaterial3 { set; get; }

        //public string repairplace4 { set; get; }
        //public string repairnum4 { set; get; }
        //public string repairMaterial4 { set; get; }

        //public string repairplace5 { set; get; }
        //public string repairnum5 { set; get; }
        //public string repairMaterial5 { set; get; }

    }

    public class StockCheck
    {
        public string material_no;
        public string num;
        public string house;
        public string place;
        public string describe;
    }

    class ReportCustomInfo
    {
        public string track_no;
        public string material_no;
        public string declear_number;
        public string date;
    }

    public class ChooseStock
    {
        public string Id;
        public string house;
        public string place;
        public string number;
    }

    public class PrepareUseDetail
    {
        public string Id;
        public string mb_brief;
        public string material_mpn;
        public string stock_place;
        public string thisUseNumber;
        public string totalUseNumber;
    }

    public class Constlist
    {
        public static string ipConst = "192.168.1.1";//inner server
        
       // public static string ipConst = "192.168.2.121";//huadong server
        
        
        //public static string ConStr = "server=.;database=SaledService;uid=admin;pwd=admin";
        //public static string ConStr = "server=" + ipConst + ";database=SaledService;uid=admin;pwd=admin";
        public static string ConStr = "server=" + ipConst + ";database=SaledService;uid=admin;pwd=admin";

        public static string table_MBMaterialCompare = "MB物料对照表";
        public static string table_name_MBMaterialCompare = "MBMaterialCompare";

        public static string table_receiveOrder = "RMA";//sheet 名字
        public static string table_name_ReceiveOrder = "ReceiveOrder";

        public static string table_LCFC_MBBOM = "LCFC_MBBOM";
        public static string table_name_LCFC_MBBOM = "LCFC_MBBOM_table";

        //public static string table_COMPAL_MBBOM = "COMPAL_MBBOM";
        //public static string table_name_COMPAL_MBBOM = "COMPAL_MBBOM_table";

        public static string table_LCFC71BOM = "LCFC71BOM表";
        public static string table_name_LCFC71BOM = "LCFC71BOM_table";

        public static string table_DPK = "DPK";
        public static string table_name_DPK = "DPK_table";

        public static string table_customFault = "故障代码表";
        public static string table_name_customFault = "customFault";

        public static string table_stock_in_sheet = "材料入库单";
        public static string table_name_stock_in_sheet = "stock_in_sheet";

        public static string table_stock_house = "库房";
        public static string table_name_store_house_sheet = "store_house";

        public static string table_users = "用户导入";
        public static string table_name_users_sheet = "users";

        public static string table_flexId8s = "flexid对照表";
        public static string table_name_flexId8s = "flexid_8s_mpn_table";

        public static string table_8scodes = "8s表格";
        public static string table_to_analysis_8s_sheet = "to_analysis_8s_table";
    }

    public class PrintUtils
    {
        static LabelManager2.Application labApp = null;
        static LabelManager2.Document doc = null;
        public static void InitCodesoftForReturn()
        {
            try
            {
                labApp = new LabelManager2.Application();
                string labFileName = @"D:\printLab\test.Lab";
                if (!File.Exists(labFileName))
                {
                    MessageBox.Show("沒有找到標簽模板文件：" + labFileName + ",請聯系系統管理員", "溫馨提示");
                    return;
                }

                labApp.Documents.Open(labFileName, false);// 调用设计好的label文件
                doc = labApp.ActiveDocument;
                doc.Printer.SwitchTo("codesoft");//打印机名字，自定义，可以修改               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
        }

        public static void  printCustomMaterialNo(string rma, string _71material, string customMaterial, string flxid, string _8sCode)
        {
            if (labApp == null)
            {
                InitCodesoftForReturn();
            }

            doc.Variables.FormVariables.Item("RMA").Value = rma;
            doc.Variables.FormVariables.Item("PN").Value = _71material;
            doc.Variables.FormVariables.Item("LPN").Value = customMaterial;
            doc.Variables.FormVariables.Item("ONO").Value = flxid;
            doc.Variables.FormVariables.Item("SNN").Value = _8sCode;
            
            doc.PrintDocument(); //打印一次
            doc.FormFeed(); //结束打印
        }

        public static void disposePrinter()
        {
            if (doc != null)
            {
                doc.Close();
                labApp.Quit();
            }
        }
    }

    public class allContent
    {
        public string sheetName;
        public List<string> titleList;//= new List<string>();
        public List<Object> contentList;// = new List<object>();
    }

    public class ExportExcelContent
    {
        public List<string> contentArray;       
    }

    public class Untils
    {
        public static string modifyDataFormat(string date)
        {
            return (date != null && date != "") ? date.Substring(0, date.IndexOf(" ")).Trim() : "";
        }

        public static bool isTimeError(string nowDate)
        {
            try
            {
                DateTime timeStart = Convert.ToDateTime(nowDate);

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select top 1 receivedate from receiveOrder order by receivedate desc ";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string oldTime = "";
                while (querySdr.Read())
                {
                    oldTime = querySdr[0].ToString();
                }

                querySdr.Close();

                mConn.Close();

                DateTime timeOld = Convert.ToDateTime(oldTime);

                if (DateTime.Compare(timeStart, timeOld) < 0) //判断日期大小
                {
                    MessageBox.Show("当前日期不对，请检查");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           

            return false;
        }


        public static void setValue(ref Microsoft.Office.Interop.Excel.Worksheet xSheet, int column, int row,Object content)
        {
            if(content is ExportExcelContent)
            {
                ExportExcelContent temp = (ExportExcelContent)content;
                xSheet.Cells[column][row] = temp.contentArray[column - 1];
            }
        }

        public static void createMulitSheetsUsingNPOI(string filepathname, List<allContent> allcontentList)
        {
          //  string path = "D:\\导出文件汇总\\";
            //if (Directory.Exists(path) == false)
            //{
            //    Directory.CreateDirectory(path);
            //}
           // filepathname = path + filepathname;

            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            //内容表格
            foreach (allContent temp in allcontentList)
            {
                if (temp.contentList.Count <= 0)
                {
                    continue;
                }
                HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet(temp.sheetName);
                int row = temp.contentList.Count + 1;
                int column = ((ExportExcelContent)(temp.contentList[0])).contentArray.Count;

                for (int ri = 0; ri < row; ri++)
                {
                    sheet.CreateRow(ri);
                }
                for (int ri = 0; ri < row; ri++)
                {
                    for (int ci = 0; ci < column; ci++)
                    {
                        if (ri == 0)
                        {
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(temp.titleList[ci]);
                        }
                        else
                        {
                            string content = ((ExportExcelContent)(temp.contentList[ri - 1])).contentArray[ci];
                            sheet.GetRow(ri).CreateCell(ci).SetCellValue(content);
                        }
                    }
                }
                for (int ci = 0; ci < column; ci++)
                {
                    sheet.AutoSizeColumn(ci);
                }
            }

            FileStream file = new FileStream(filepathname, FileMode.Create);

            hssfworkbook.Write(file);
            file.Close();
            MessageBox.Show(filepathname + "导出成功");
        }

        //title list的长度要保证与内容contentArray的长度一致
        public static void createExcel(string filepathname, List<string> titleList, List<Object> contentList)
        {
            //1.创建Applicaton对象
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook;

            xApp.Visible = false;
            xApp.DisplayAlerts = false;  

            if (File.Exists(filepathname))
            {
                workBook = xApp.Workbooks.Open(filepathname, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            else
            {
                workBook = xApp.Workbooks.Add(true);
            }

            //3.指定要操作的Sheet
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;

            //4.向相应对位置写入相应的数据
            for (int column = 1; column <= titleList.Count; column++)
            {
                xSheet.Cells[column][1] = titleList[column - 1];
            }            
           
            object[,] saRet = new object[contentList.Count , titleList.Count]; //row,column

            for (int row = 0; row < contentList.Count ; row++)
            {
                for (int column = 0; column < titleList.Count; column++)
                {
                    saRet[row, column] = ((ExportExcelContent)contentList[row]).contentArray[column];
                }
            }

            //for (int row = 2; row <= contentList.Count + 1; row++)           
            //{
            //    for (int column = 1; column <= titleList.Count; column++)
            //    {
            //        setValue(ref xSheet,column, row, contentList[row - 2]);
            //    }
            //}

            //range = xSheet.get_Range(xSheet.Cells[2, 1], xSheet.Cells[contentList.Count+1, titleList.Count]);//此方案有问题，改成下面的方案range[,]方案

            range = xSheet.Range[xSheet.Cells[2, 1], xSheet.Cells[contentList.Count + 1, titleList.Count]];

           
           
            range.NumberFormatLocal = "@";
            range.Value2 = saRet;

            xSheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //5.保存保存WorkBook
            workBook.SaveAs(filepathname);
            //6.从内存中关闭Excel对象
            xSheet = null;
            //关闭EXCEL的提示框
            xApp.DisplayAlerts = false;
            //Excel从内存中退出        
            try
            {
                workBook.Close();
                xApp.Quit();
                IntPtr intptr = new IntPtr(xApp.Hwnd);
                int id;
                GetWindowThreadProcessId(intptr, out id);
                var p = Process.GetProcessById(id);
                if (p != null)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            { }
            MessageBox.Show("导出" + filepathname + "成功！");
        }

        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        public static string getCustomDate(string inputDate)
        {
            return DateTime.Parse(inputDate).ToString("yyyy-MM-dd"); // DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string getCustomCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static void createOpeningStockXML(OpeningStockClass openingStockClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_INIT\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", openingStockClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", openingStockClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", openingStockClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", openingStockClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", openingStockClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", openingStockClass.status);
            xmlResult.Append("</TRANSMIT>");

            foreach (StoreInit storeInit in openingStockClass.storeInitList)
            {
                xmlResult.Append("<STORE_INIT>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeInit.ems_no);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeInit.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeInit.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeInit.unit);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeInit.goods_nature);
                xmlResult.AppendFormat("<BOM_VERSION>{0}</BOM_VERSION>\n", storeInit.bom_version);
                xmlResult.AppendFormat("<CHECK_DATE>{0}</CHECK_DATE>\n", storeInit.check_date);
                xmlResult.AppendFormat("<DATA_TYPE>{0}</DATA_TYPE>\n", storeInit.date_type);

                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeInit.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeInit.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeInit.note);
                xmlResult.Append("</STORE_INIT>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");            

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }        
        }

        public static void createRealStockXML(RealStockClass realStockClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_AMOUNT\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", realStockClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", realStockClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", realStockClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", realStockClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", realStockClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", realStockClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (StoreAmount storeInit in realStockClass.storeAmountList)
            {
                xmlResult.Append("<STORE_AMOUNT>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeInit.ems_no);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeInit.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeInit.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeInit.unit);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeInit.goods_nature);
                xmlResult.AppendFormat("<BOM_VERSION>{0}</BOM_VERSION>\n", storeInit.bom_version);
                xmlResult.AppendFormat("<STOCK_DATE>{0}</STOCK_DATE>\n", storeInit.stock_date);
                xmlResult.AppendFormat("<DATA_TYPE>{0}</DATA_TYPE>\n", storeInit.date_type);

                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeInit.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeInit.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeInit.note);
                xmlResult.Append("</STORE_AMOUNT>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createStockInOutXML(StockInOutClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.STORE_TRANS\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (StoreTrans storeTrans in stockInOutClass.storeTransList)
            {
                xmlResult.Append("<STORE_TRANS>\n");
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.ems_no);
                xmlResult.AppendFormat("<IO_NO>{0}</IO_NO>\n", storeTrans.io_no);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE>\n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<IO_DATE>{0}</IO_DATE>\n", storeTrans.io_date);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.unit);
                xmlResult.AppendFormat("<TYPE>{0}</TYPE>\n", storeTrans.type);
                xmlResult.AppendFormat("<CHK_CODE>{0}</CHK_CODE>\n", storeTrans.chk_code);
                xmlResult.AppendFormat("<ENTRY_ID>{0}</ENTRY_ID>\n", storeTrans.entry_id);
                xmlResult.AppendFormat("<GATEJOB_NO>{0}</GATEJOB_NO>\n", storeTrans.gatejob_no);
                xmlResult.AppendFormat("<WHS_CODE>{0}</WHS_CODE>\n", storeTrans.whs_code);
                xmlResult.AppendFormat("<LOCATION_CODE>{0}</LOCATION_CODE>\n", storeTrans.location_code);
                xmlResult.AppendFormat("<NOTE>{0}</NOTE>\n", storeTrans.note);
                xmlResult.Append("</STORE_TRANS>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createWorkListHeadXML(WorkListHeadClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.WORK_ORDER_HEAD\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (WorkOrderHead storeTrans in stockInOutClass.workOrderHeadList)
            {
                xmlResult.Append("<WORK_ORDER_HEAD>\n");

                xmlResult.AppendFormat("<WO_NO>{0}</WO_NO>\n", storeTrans.wo_no);
                xmlResult.AppendFormat("<WO_DATE>{0}</WO_DATE>\n", storeTrans.wo_date);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE> \n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.unit);
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.emo_no);

                xmlResult.Append("</WORK_ORDER_HEAD>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static void createWorkListBodyXML(WorkListBodyClass stockInOutClass, string fileName)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlResult.AppendFormat("<HCHX_DATA xmlns=\"http://HCHX.Schemas.WORK_ORDER_LIST\">\n");
            xmlResult.Append("<TRANSMIT>\n");
            xmlResult.AppendFormat("<SEQ_NO>{0}</SEQ_NO>\n", stockInOutClass.seq_no);
            xmlResult.AppendFormat("<BOXTYPE>{0}</BOXTYPE>\n", stockInOutClass.boxtype);

            xmlResult.AppendFormat("<FLOWSTATEG>{0}</FLOWSTATEG>\n", stockInOutClass.flowstateg);
            xmlResult.AppendFormat("<TRADE_CODE>{0}</TRADE_CODE>\n", stockInOutClass.trade_code);

            xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", stockInOutClass.ems_no);
            xmlResult.AppendFormat("<STATUS>{0}</STATUS>\n", stockInOutClass.status);
            xmlResult.Append("</TRANSMIT>\n");

            foreach (WorkOrderList storeTrans in stockInOutClass.workOrderList)
            {
                xmlResult.Append("<WORK_ORDER_LIST>\n");

                xmlResult.AppendFormat("<WO_NO>{0}</WO_NO>\n", storeTrans.wo_no);
                xmlResult.AppendFormat("<TAKE_DATE>{0}</TAKE_DATE>\n", storeTrans.take_date);
                xmlResult.AppendFormat("<GOODS_NATURE>{0}</GOODS_NATURE> \n", storeTrans.goods_nature);
                xmlResult.AppendFormat("<COP_G_NO>{0}</COP_G_NO>\n", storeTrans.cop_g_no);
                xmlResult.AppendFormat("<QTY>{0}</QTY>\n", storeTrans.qty);
                xmlResult.AppendFormat("<UNIT>{0}</UNIT>\n", storeTrans.unit);
                xmlResult.AppendFormat("<EMS_NO>{0}</EMS_NO>\n", storeTrans.emo_no);

                xmlResult.Append("</WORK_ORDER_LIST>\n");
            }
            xmlResult.Append("</HCHX_DATA>\n");

            //写入文件  
            try
            {
                //1.创建文件流    
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                //2.创建写入器    
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //3.将内容写入文件    
                streamWriter.WriteLine(xmlResult);
                //4.关闭写入器    
                streamWriter.Close();
                //5.关闭文件流    
                fileStream.Close();
            }
            catch (Exception e)
            { }
        }

        public static  bool in90Days(string burn_date)
        {
            DateTime dt1 = Convert.ToDateTime(burn_date);
            DateTime dt2 = DateTime.Now;

            TimeSpan ts = dt2.Subtract(dt1);
            int overdays = ts.Days;
            if (overdays > 90)
            {
                return false;
            }
            return true;
        }

        public static void createFile(string path, string filename, string content)
        {
            try
            {
                FileStream fs1 = new FileStream(path+filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(content);
                sw.Close();
                fs1.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
            }
        }

        public static void deleteFile(string path, string filename)
        {
            try
            {
                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //规则是0-9，a-z 累加， a =10
        public static string getTimeByChar(bool isYear, char ch)
        {
            ch = Char.ToLower(ch);
            string ret = ch.ToString();

            switch (ch)
            {
                case '0':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "0";
                    }
                    break;
                case '1':
                    if (isYear)
                    {
                        ret = "21";
                    }
                    else
                    {
                        ret = "1";
                    }
                    break;
                case '2':
                    if (isYear)
                    {
                        ret = "22";
                    }
                    else
                    {
                        ret = "2";
                    }
                    break;
                case '3':
                    if (isYear)
                    {
                        ret = "23";
                    }
                    else
                    {
                        ret = "3";
                    }
                    break;
                case '4':
                    if (isYear)
                    {
                        ret = "24";
                    }
                    else
                    {
                        ret = "4";
                    }
                    break;
                case '5':
                    if (isYear)
                    {
                        ret = "15";
                    }
                    else
                    {
                        ret = "5";
                    }
                    break;
                case '6':
                    if (isYear)
                    {
                        ret = "16";
                    }
                    else
                    {
                        ret = "6";
                    }
                    break;
                case '7':
                    if (isYear)
                    {
                        ret = "17";
                    }
                    else
                    {
                        ret = "7";
                    }
                    break;
                case '8':
                    if (isYear)
                    {
                        ret = "18";
                    }
                    else
                    {
                        ret = "8";
                    }
                    break;
                case '9':
                    if (isYear)
                    {
                        ret = "19";
                    }
                    else
                    {
                        ret = "9";
                    }
                    break;
                case 'a':
                    if (isYear)
                    {
                        ret = "20";
                    }
                    else
                    {
                        ret = "10";
                    }
                    break;
                case 'b':
                    ret = "11";
                    break;
                case 'c':
                    ret = "12";
                    break;
                case 'd':
                    ret = "13";
                    break;
                case 'e':
                    ret = "14";
                    break;
                case 'f':
                    ret = "15";
                    break;
                case 'g':
                    ret = "16";
                    break;
                case 'h':
                    ret = "17";
                    break;
                //case 'i':
                //    ret = "18";
                //    break;
                case 'j':
                    ret = "18";
                    break;
                case 'k':
                    ret = "19";
                    break;
                case 'l':
                    ret = "20";
                    break;
                case 'm':
                    ret = "21";
                    break;
                case 'n':
                    ret = "22";
                    break;
                //case 'o':
                //    ret = "24";
                //    break;
                case 'p':
                    ret = "23";
                    break;
                //case 'q':
                //    ret = "24";
                //    break;
                case 'r':
                    ret = "24";
                    break;
                case 's':
                    ret = "25";
                    break;
                case 't':
                    ret = "26";
                    break;
                //case 'u':
                //    ret = "28";
                //    break;
                case 'v':
                    ret = "27";
                    break;
                case 'w':
                    ret = "28";
                    break;
                case 'x':
                    ret = "29";
                    break;
                case 'y':
                    ret = "30";
                    break;
                case 'z':
                    ret = "31";
                    break;
            }

            return ret;
        }

        public static Hashtable customCodeTable = new Hashtable();
        private static void initCustomCode()
        {
            customCodeTable.Add("台", "001");
            customCodeTable.Add("座", "002");
            customCodeTable.Add("辆", "003");
            customCodeTable.Add("艘", "004");
            customCodeTable.Add("架", "005");
            customCodeTable.Add("套", "006");
            customCodeTable.Add("个", "007");
            customCodeTable.Add("只", "008");
            customCodeTable.Add("头", "009");
            customCodeTable.Add("张", "010");
            customCodeTable.Add("件", "011");
            customCodeTable.Add("支", "012");
            customCodeTable.Add("枝", "013");
            customCodeTable.Add("根", "014");
            customCodeTable.Add("条", "015");
            customCodeTable.Add("把", "016");
            customCodeTable.Add("块", "017");
            customCodeTable.Add("卷", "018");
            customCodeTable.Add("副", "019");
            customCodeTable.Add("片", "020");
            customCodeTable.Add("组", "021");
            customCodeTable.Add("份", "022");
            customCodeTable.Add("幅", "023");
            customCodeTable.Add("双", "025");
            customCodeTable.Add("对", "026");
            customCodeTable.Add("棵", "027");
            customCodeTable.Add("株", "028");
            customCodeTable.Add("井", "029");
            customCodeTable.Add("米", "030");
            customCodeTable.Add("盘", "031");
            customCodeTable.Add("平方米", "032");
            customCodeTable.Add("立方米", "033");
            customCodeTable.Add("筒", "034");
            customCodeTable.Add("千克", "035");
            customCodeTable.Add("克", "036");
            customCodeTable.Add("盆", "037");
            customCodeTable.Add("万个", "038");
            customCodeTable.Add("具", "039");
            customCodeTable.Add("百副", "040");
            customCodeTable.Add("百支", "041");
            customCodeTable.Add("百把", "042");
            customCodeTable.Add("百个", "043");
            customCodeTable.Add("百片", "044");
            customCodeTable.Add("刀", "045");
            customCodeTable.Add("疋", "046");
            customCodeTable.Add("公担", "047");
            customCodeTable.Add("扇", "048");
            customCodeTable.Add("百枝", "049");
            customCodeTable.Add("千只", "050");
            customCodeTable.Add("千块", "051");
            customCodeTable.Add("千盒", "052");
            customCodeTable.Add("千枝", "053");
            customCodeTable.Add("千个", "054");
            customCodeTable.Add("亿支", "055");
            customCodeTable.Add("亿个", "056");
            customCodeTable.Add("万套", "057");
            customCodeTable.Add("千张", "058");
            customCodeTable.Add("万张", "059");
            customCodeTable.Add("千伏安", "060");
            customCodeTable.Add("千瓦", "061");
            customCodeTable.Add("千瓦时", "062");
            customCodeTable.Add("千升", "063");
            customCodeTable.Add("英尺", "067");
            customCodeTable.Add("吨", "070");
            customCodeTable.Add("长吨", "071");
            customCodeTable.Add("短吨", "072");
            customCodeTable.Add("司马担", "073");
            customCodeTable.Add("司马斤", "074");
            customCodeTable.Add("斤", "075");
            customCodeTable.Add("磅", "076");
            customCodeTable.Add("担", "077");
            customCodeTable.Add("英担", "078");
            customCodeTable.Add("短担", "079");
            customCodeTable.Add("两", "080");
            customCodeTable.Add("市担", "081");
            customCodeTable.Add("盎司", "083");
            customCodeTable.Add("克拉", "084");
            customCodeTable.Add("市尺", "085");
            customCodeTable.Add("码", "086");
            customCodeTable.Add("英寸", "088");
            customCodeTable.Add("寸", "089");
            customCodeTable.Add("升", "095");
            customCodeTable.Add("毫升", "096");
            customCodeTable.Add("英加仑", "097");
            customCodeTable.Add("美加仑", "098");
            customCodeTable.Add("立方英尺", "099");
            customCodeTable.Add("立方尺", "101");
            customCodeTable.Add("平方码", "110");
            customCodeTable.Add("平方英尺", "111");
            customCodeTable.Add("平方尺", "112");
            customCodeTable.Add("英制马力", "115");
            customCodeTable.Add("公制马力", "116");
            customCodeTable.Add("令", "118");
            customCodeTable.Add("箱", "120");
            customCodeTable.Add("批", "121");
            customCodeTable.Add("罐", "122");
            customCodeTable.Add("桶", "123");
            customCodeTable.Add("扎", "124");
            customCodeTable.Add("包", "125");
            customCodeTable.Add("箩", "126");
            customCodeTable.Add("打", "127");
            customCodeTable.Add("筐", "128");
            customCodeTable.Add("罗", "129");
            customCodeTable.Add("匹", "130");
            customCodeTable.Add("册", "131");
            customCodeTable.Add("本", "132");
            customCodeTable.Add("发", "133");
            customCodeTable.Add("枚", "134");
            customCodeTable.Add("捆", "135");
            customCodeTable.Add("袋", "136");
            customCodeTable.Add("粒", "139");
            customCodeTable.Add("盒", "140");
            customCodeTable.Add("合", "141");
            customCodeTable.Add("瓶", "142");
            customCodeTable.Add("千支", "143");
            customCodeTable.Add("万双", "144");
            customCodeTable.Add("万粒", "145");
            customCodeTable.Add("千粒", "146");
            customCodeTable.Add("千米", "147");
            customCodeTable.Add("千英尺", "148");
            customCodeTable.Add("百万贝可", "149");
            customCodeTable.Add("部", "163");
        }
        public static string getCustomCode(string unitStr)
        {
            if (customCodeTable.Count == 0)
            {
                initCustomCode();
            }

            try
            {
                return customCodeTable[unitStr].ToString();
            }
            catch (Exception ex)
            {
                return "007";//默认单位个
            }
        }
    }
}
