using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Export
{
    public partial class ExportDatabaseToExcel : Form
    {
        string currentTableName = "";
        public ExportDatabaseToExcel()
        {
            InitializeComponent();
        }

        private void chooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.pathTextBox.Text = path.SelectedPath;
        }

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            if (this.pathTextBox.Text == "")
            {
                MessageBox.Show("目的文件夹为空");
                return;
            }

            if (this.datasourceComboBox.Text == "")
            {
                MessageBox.Show("源数据库为空！");
                return;
            }

            DateTime start = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime end = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(start, end) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            string sqlCmd = "select * from " + currentTableName;
            if (currentTableName == "receiveOrder")
            {
                sqlCmd += " where receivedate between '" + start + "' and '" + end + "'";

                titleList.Add("ID");
                titleList.Add("厂商");
                titleList.Add("客户别");
                titleList.Add("订单编号");
                titleList.Add("客户料号");
                titleList.Add("客户物料描述");
                titleList.Add("订单数量");
                titleList.Add("MB简称");
                titleList.Add("厂商料号");
                titleList.Add("制单人");
                titleList.Add("制单时间");
                titleList.Add("收货数量");
                titleList.Add("收货日期");
                titleList.Add("订单状态");
                titleList.Add("仓库别");
                titleList.Add("还货数量");
                titleList.Add("申报单位");
                titleList.Add("报关单号");
                titleList.Add("申请单号");
                titleList.Add("Cid数量");
            }
            else if (currentTableName == "cidRecord")
            {
                sqlCmd += " where inputdate between '" + start + "' and '" + end + "'";
                titleList.Add("ID");
                titleList.Add("跟踪条码");
                titleList.Add("厂商");
                titleList.Add("客户别");
                titleList.Add("订单编号");
                titleList.Add("客户料号");
                titleList.Add("客户序号");
                titleList.Add("MB简称");
                titleList.Add("厂商料号");
                titleList.Add("收货日期");
                titleList.Add("客户故障");
                titleList.Add("客责类别");
                titleList.Add("客责描述");
                titleList.Add("短路电压");
                titleList.Add("录入人");
                titleList.Add("录入日期");
            }
            else if (currentTableName == "LCFC_MBBOM_table")
            {
                //sqlCmd = "select top 1000 * from " + currentTableName;

                titleList.Add("ID");
                titleList.Add("日期");
                titleList.Add("厂商");
                titleList.Add("客户别");
                titleList.Add("MB简称");
                titleList.Add("MPN");
                titleList.Add("材料MPN");
                titleList.Add("料盒位置");
                titleList.Add("物料描述");
                titleList.Add("用料数量");
                titleList.Add("L1");
                titleList.Add("L2");
                titleList.Add("L3");
                titleList.Add("L4");
                titleList.Add("L5");
                titleList.Add("L6");
                titleList.Add("L7");
                titleList.Add("L8");
            }
            else
            {
                MessageBox.Show("报表还没有制作！");
                this.Cursor = Cursors.Default;
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlCmd;
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        ExportExcelContent ctest1 = new ExportExcelContent();
                        List<string> ct1 = new List<string>();

                        if (currentTableName == "receiveOrder")
                        {
                            ct1.Add(querySdr[0].ToString());
                            ct1.Add(querySdr[1].ToString());
                            ct1.Add(querySdr[2].ToString());
                            ct1.Add(querySdr[3].ToString());
                            ct1.Add(querySdr[4].ToString());
                            ct1.Add(querySdr[5].ToString());
                            ct1.Add(querySdr[6].ToString());
                            ct1.Add(querySdr[7].ToString());
                            ct1.Add(querySdr[8].ToString());
                            ct1.Add(querySdr[9].ToString());
                            ct1.Add(querySdr[10].ToString());
                            ct1.Add(querySdr[11].ToString());
                            ct1.Add(querySdr[12].ToString());
                            ct1.Add(querySdr[13].ToString());
                            ct1.Add(querySdr[14].ToString());
                            ct1.Add(querySdr[15].ToString());
                            ct1.Add(querySdr[16].ToString());
                            ct1.Add(querySdr[17].ToString());
                            ct1.Add(querySdr[18].ToString());
                            ct1.Add(querySdr[19].ToString());
                        }
                        else if (currentTableName == "cidRecord")
                        {
                            ct1.Add(querySdr[0].ToString());
                            ct1.Add(querySdr[1].ToString());
                            ct1.Add(querySdr[2].ToString());
                            ct1.Add(querySdr[3].ToString());
                            ct1.Add(querySdr[4].ToString());
                            ct1.Add(querySdr[5].ToString());
                            ct1.Add(querySdr[6].ToString());
                            ct1.Add(querySdr[7].ToString());
                            ct1.Add(querySdr[8].ToString());
                            ct1.Add(querySdr[9].ToString());
                            ct1.Add(querySdr[10].ToString());
                            ct1.Add(querySdr[11].ToString());
                            ct1.Add(querySdr[12].ToString());
                            ct1.Add(querySdr[13].ToString());
                            ct1.Add(querySdr[14].ToString());
                            ct1.Add(querySdr[15].ToString());
                        }
                        else if (currentTableName == "LCFC_MBBOM_table")
                        {
                            ct1.Add(querySdr[0].ToString());
                            ct1.Add(querySdr[1].ToString());
                            ct1.Add(querySdr[2].ToString());
                            ct1.Add(querySdr[3].ToString());
                            ct1.Add(querySdr[4].ToString());
                            ct1.Add(querySdr[5].ToString());
                            ct1.Add(querySdr[6].ToString());
                            ct1.Add(querySdr[7].ToString());
                            ct1.Add(querySdr[8].ToString());
                            ct1.Add(querySdr[9].ToString());
                            ct1.Add(querySdr[10].ToString());
                            ct1.Add(querySdr[11].ToString());
                            ct1.Add(querySdr[12].ToString());
                            ct1.Add(querySdr[13].ToString());
                            ct1.Add(querySdr[14].ToString());
                            ct1.Add(querySdr[15].ToString());
                            ct1.Add(querySdr[16].ToString());
                            ct1.Add(querySdr[17].ToString());
                        }

                        ctest1.contentArray = ct1;
                        contentList.Add(ctest1);
                    }
                    querySdr.Close();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                string path = this.pathTextBox.Text;
                if(path.EndsWith("\\") == false)
                {
                    path += "\\";
                }
                Untils.createExcel(path + this.datasourceComboBox.Text + ".xlsx", titleList, contentList);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }
        }

        private void datasourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.datasourceComboBox.Text.Trim() =="收货单")
            {
                currentTableName = "receiveOrder";
            }
            else if(this.datasourceComboBox.Text.Trim() =="CID记录")
            {
                currentTableName = "cidRecord";
            }
            else if(this.datasourceComboBox.Text.Trim() =="Flexid对应表")
            {
                currentTableName = "flexidRecord";
            }
            else if(this.datasourceComboBox.Text.Trim() =="MB物料对照表")
            {
                currentTableName = "MBMaterialCompare";
            }
            else if(this.datasourceComboBox.Text.Trim() =="用户表")
            {
                currentTableName = "users";
            }
            else if(this.datasourceComboBox.Text.Trim() =="收货表")
            {
                currentTableName = "DeliveredTable";
            }
            else if(this.datasourceComboBox.Text.Trim() =="还货表")
            {
                currentTableName = "returnStore";
            }
            else if(this.datasourceComboBox.Text.Trim() =="LCFC_MBBOM")
            {
                currentTableName = "LCFC_MBBOM_table";
            }
            else if(this.datasourceComboBox.Text.Trim() =="LCFC71BOM")
            {
                currentTableName = "LCFC71BOM_table";
            }
            else if(this.datasourceComboBox.Text.Trim() =="DPK表")
            {
                currentTableName = "DPK_table";
            }
            else if(this.datasourceComboBox.Text.Trim() =="良品库房")
            {
                currentTableName = "store_house";
            }            
            else if(this.datasourceComboBox.Text.Trim() =="MB良品入库表")
            {
                currentTableName = "repaired_in_house_table";
            }
            else if (this.datasourceComboBox.Text.Trim() == "MB良品出库表")
            {
                currentTableName = "repaired_out_house_table";
            }
        }
    }
}
