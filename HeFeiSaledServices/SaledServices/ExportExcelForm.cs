using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class ExportExcelForm : Form
    {
        public ExportExcelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string tablename = "receiveOrder";
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select receiveOrder.vendor,receiveOrder.product,receiveOrder.storehouse,receiveOrder.orderno,receiveOrder.custom_materialNo,receiveOrder.mb_brief,mpn, dpk_type, receiveOrder.receivedNum,receiveOrder.returnNum from " + tablename +
                    " inner join MBMaterialCompare on custom_materialNo = MBMaterialCompare.custommaterialNo";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, tablename);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = {"厂商", "客户别","仓库别","订单编号",
                                "客户料号","MB简称","MPN", "DPK类型","收货数量","还货数量"};
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                    dataGridView1.Columns[i].Name = hTxt[i];
                }

                string owe = "欠货数量";
                DataGridViewColumn dc = new DataGridViewColumn();
                dc.DefaultCellStyle.BackColor = Color.Red;
                dc.Name = owe;
                //dc.DataPropertyName = "FID";

                DataTable dataTable = ds.Tables[0];
                dataTable.Columns.Add(owe);

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = owe;
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridView1.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    try
                    {
                        int oNum = Int32.Parse(dr.Cells["收货数量"].Value.ToString());
                        int rNum = Int32.Parse(dr.Cells["还货数量"].Value.ToString());

                        if (oNum - rNum == 0)
                        {
                            dr.Cells[owe].Style.BackColor = Color.Green;
                        }
                        dr.Cells[owe].Value = (oNum - rNum) + " ";
                    }
                    catch (Exception ex)
                    { }
                }

                DataSetToExcel(ds, "test", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool DataSetToExcel(DataSet dataSet, string fileName, bool isShowExcle)
        {            
            DataTable dataTable = dataSet.Tables[0];
            DataRow dr = dataTable.NewRow();

            int columnNumber = dataTable.Columns.Count;

            string[] hTxt = {"厂商", "客户别","仓库别","订单编号",
                                "客户料号","MB简称","MPN", "DPK类型","收货数量","还货数量", "欠货数量"};
            dr.ItemArray = hTxt;
            
            dataTable.Rows.InsertAt(dr, 0);

            int rowNumber = dataTable.Rows.Count;//不包括字段名
           
            int colIndex = 0;

            if (rowNumber == 0)
            {
                MessageBox.Show("没有任何数据可以导入到Excel文件！");
                return false;
            }

            //建立Excel对象 
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            excel.Visible = false;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;

            //生成字段名称 
            foreach (DataColumn col in dataTable.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            object[,] objData = new object[rowNumber, columnNumber];

            for (int r = 0; r < rowNumber; r++)
            {
                for (int c = 0; c < columnNumber; c++)
                {
                    objData[r, c] = dataTable.Rows[r][c];
                }
            }

            // 写入Excel 
            range = worksheet.Range[excel.Cells[1, 1], excel.Cells[rowNumber, columnNumber]];
            range.NumberFormat = "@";//设置单元格为文本格式
            range.Value2 = objData;
            range.EntireColumn.AutoFit();//自动调整列宽
            string fileNameFinally = "D:\\" + fileName + DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss").Replace(':', '_') + ".xlsx";
            workbook.SaveAs(fileNameFinally, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            try
            {
                workbook.Saved = true;
                excel.UserControl = false;
                //excelapp.Quit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                workbook.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlSaveChanges, Missing.Value, Missing.Value);
                excel.Quit();
            }

            if (isShowExcle)
            {
                System.Diagnostics.Process.Start(fileNameFinally);
            }
            return true;
        }
    }
}
