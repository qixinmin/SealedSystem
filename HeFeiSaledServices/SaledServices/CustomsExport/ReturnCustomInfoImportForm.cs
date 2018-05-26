using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SaledServices
{
  

    public partial class ReturnCustomInfoImportForm : Form
    {
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;

        public ReturnCustomInfoImportForm()
        {
            InitializeComponent();
        }

        private void findFile_Click(object sender, EventArgs e)
        {  
            openFileDialog.ShowDialog();
            
            string fileName = openFileDialog.FileName;
            if (fileName.EndsWith("xls") || fileName.EndsWith("xlsx"))
            {
                filePath.Text = fileName;
                importButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("请输入正确的xls文件 并选择正确的import目标!");
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            this.importButton.Enabled = false;
            string sheetName = "海关上传";
           
            app = new Microsoft.Office.Interop.Excel.Application();
            app.DisplayAlerts = false;
            wbs = app.Workbooks;
            wb = wbs.Add(filePath.Text);            
            
            wb = wbs.Open(filePath.Text, 0, false, 5, string.Empty, string.Empty, 
                false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, 
                string.Empty, true, false, 0, true, 1, 0);
            
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[sheetName];
            int rowLength = ws.UsedRange.Rows.Count;
            int columnLength = ws.UsedRange.Columns.Count;

            List<ReportCustomInfo> reportList = new List<ReportCustomInfo>();
            try
            {
                //读取数据到数据结构中
                for (int i = 2; i <= rowLength; i++)
                {
                    ReportCustomInfo info = new ReportCustomInfo();

                    info.track_no = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 1]).Value2.ToString();
                    info.material_no = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 2]).Value2.ToString();
                    info.declear_number = ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Value2.ToString();

                    DateTime strDate = DateTime.FromOADate(double.Parse(((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Value2.ToString()));
                    info.date = strDate.ToString("yyyy/MM/dd");
                    reportList.Add(info);
                }

                SqlConnection queryConn = new SqlConnection(Constlist.ConStr);
                queryConn.Open();

               // string today = DateTime.Now.ToString("yyyy/MM/dd");
                if (queryConn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = queryConn;
                    cmd.CommandType = CommandType.Text;
                    if (reportList.Count > 0)
                    {
                        //首先判断是否在repaired_out_house_table表中，一一对应
                        foreach (ReportCustomInfo temp in reportList)
                        {
                            cmd.CommandText = "select Id from repaired_out_house_table where track_serial_no='" + temp.track_no.Trim() + "'";
                            SqlDataReader sqlReader = cmd.ExecuteReader();
                            if (sqlReader.HasRows == false)
                            {
                                MessageBox.Show("此序列号"+temp.track_no+"不存在良品出库表中");
                                queryConn.Close();
                                return;
                            }
                        }

                        foreach(ReportCustomInfo temp in reportList)
                        {
                            cmd.CommandText = "INSERT INTO repaired_out_house_excel_table VALUES('"
                              + temp.track_no.Trim() + "','"
                              + temp.material_no.Trim() + "','"
                              + temp.declear_number.Trim() + "','"
                              + temp.date.Trim()
                              + "')";

                            cmd.ExecuteNonQuery();

                            //同时更新成品出库的时间，这个时候才能拿到数据给海关
                            cmd.CommandText = "update repaired_out_house_table set input_date='"+temp.date.Trim()+"'";
                            cmd.ExecuteNonQuery();
                        }

                        //cmd.CommandText = "select track_serial_no,custom_materialNo from repaired_out_house_table where input_date between '" + today + "' and '" + today + "'";
                        //SqlDataReader querySdr = cmd.ExecuteReader();

                        //string exist = "";
                        //string left_number = "";
                        //while (querySdr.Read())
                        //{
                        //    exist = querySdr[0].ToString();
                        //    left_number = querySdr[1].ToString();
                        //    break;
                        //}
                        //querySdr.Close();
                    }
                }

                queryConn.Close();
                MessageBox.Show("导入完毕");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                closeAndKillApp();
            }
            

            this.importButton.Enabled = true;
        }
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        private void closeAndKillApp()
        {
            try
            {
                wbs.Close();
                app.Quit();
                IntPtr intptr = new IntPtr(app.Hwnd);
                int id;
                GetWindowThreadProcessId(intptr, out id);
                var p = Process.GetProcessById(id);
                //if (p != null)
                p.Kill();
            }
            catch (Exception ex)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
