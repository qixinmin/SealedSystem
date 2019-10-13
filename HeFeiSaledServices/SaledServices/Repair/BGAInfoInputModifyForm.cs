using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class BGAInfoInputModifyForm : Form
    {
        public BGAInfoInputModifyForm()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
            }

            track_serial_noTextBox.Focus();            
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    this.track_serial_noTextBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    error = true;
                    return;
                }
                this.track_serial_noTextBox.Text = this.track_serial_noTextBox.Text.ToUpper();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select _8sCode from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and isLock='true'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此序列号需要分析但是已经锁定，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        return;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select Id from cidRecord where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string cidExist = "";
                    while (querySdr.Read())
                    {
                        cidExist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (cidExist != "")
                    {
                        MessageBox.Show("此序列号已经在CID中，不能走下面的流程！");

                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string stationInfo = "";
                    while (querySdr.Read())
                    {
                        stationInfo = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (stationInfo == "维修" || stationInfo == "ECO" || stationInfo == "BGA"/* || stationInfo == "收货"*/)
                    {

                    }
                    else
                    {
                        MessageBox.Show("此序列号的站别已经在:【" + stationInfo + "】，不能走下面的流程！");
                        mConn.Close();

                        return;
                    }

                    cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    string sourceBrief = "", customOrder = "", order_receive_date = "", custom_serial_no = "", vendor_serial_no = "", mb_make_date = "", custom_fault = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                        sourceBrief = querySdr[1].ToString();
                        customOrder = querySdr[2].ToString();
                        order_receive_date = querySdr[3].ToString();
                        custom_serial_no = querySdr[4].ToString();
                        vendor_serial_no = querySdr[5].ToString();
                        mb_make_date = DateTime.Parse(querySdr[6].ToString()).ToString("yyyy/MM/dd");
                        custom_fault = querySdr[7].ToString();

                    }
                    querySdr.Close();

                    if (customMaterialNo != "")
                    {
                        string vendor = "", product = "", mb_describe = "", mb_brief = "", mpn = "", eco = "";
                        cmd.CommandText = "select vendor,product, mb_descripe, mb_brief,mpn,eco from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                        querySdr = cmd.ExecuteReader();

                        while (querySdr.Read())
                        {
                            vendor = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                            mb_describe = querySdr[2].ToString();
                            mb_brief = querySdr[3].ToString();
                            mpn = querySdr[4].ToString();
                            eco = querySdr[5].ToString();
                        }
                        querySdr.Close();


                    }
                    else
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        error = true;
                        mConn.Close();
                        return;
                    }

                    //查询bga的维修记录，如果有bga的维修记录，则取最后一条，判断状态是否是不是“BGA待换”，否则提示报错，然后把相关bga的内容填入相应内容中去
                    cmd.CommandText = "select top 1 bga_repair_result,bgatype,BGAPN,BGA_place,bga_brief,mbfa1 from bga_repair_record_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'  order by Id desc";

                    querySdr = cmd.ExecuteReader();
                    string bga_repair_result = "", bgatype = "", BGAPN = "", BGA_place = "", bga_brief = "", mbfa1 = "";
                    while (querySdr.Read())
                    {
                        bga_repair_result = querySdr[0].ToString();
                        bgatype = querySdr[1].ToString();
                        BGAPN = querySdr[2].ToString();
                        BGA_place = querySdr[3].ToString();
                        bga_brief = querySdr[4].ToString();
                        mbfa1 = querySdr[5].ToString();
                    }
                    querySdr.Close();

                    if (bga_repair_result != "" && bga_repair_result == "BGA待换")
                    {
                        MessageBox.Show("BGA的状态不应该是BGA待换，BGA的维修状态还没有完成！！！");

                        mConn.Close();
                        return;
                    }
                    else
                    {
                        this.bga_brieftextBox.Text = bga_brief;
                        this.BGAPNtextBox.Text = BGAPN;
                        this.BGA_placetextBox.Text = BGA_place;
                       
                    }

                    mConn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;              

                string sqlStr = "select top 20 Id,track_serial_no,bgatype,BGAPN,BGA_place,bga_brief from bga_wait_record_table";

                if (this.track_serial_noTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where track_serial_no= '" + track_serial_noTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and track_serial_no= '" + track_serial_noTextBox.Text.Trim() + "' ";
                    }
                }

                sqlStr += " order by Id desc";

                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "bga_wait_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "BGA类型", "BGAPN","BGA位置","BGA简述"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
            MessageBox.Show("查询完毕");
        }

        private void BGAInfoInputForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
            GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
            SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel2, true, null);
            tableLayoutPanel3.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel3, true, null);
           
        }
        string tobedeletedId = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            tobedeletedId = dataGridView1.SelectedCells[0].Value.ToString();
            this.track_serial_noTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.bgatypetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.BGAPNtextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.BGA_placetextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.bga_brieftextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
        }

        private void modify_Click(object sender, EventArgs e)
        {
             try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "update bga_wait_record_table set BGAPN='" + this.BGAPNtextBox.Text.Trim() + "', bga_brief='" + this.bga_brieftextBox.Text.Trim() + "' where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and _status='BGA不良'";
                    cmd.ExecuteNonQuery();

                    this.track_serial_noTextBox.Text = "";
                    this.bgatypetextBox.Text = "";
                    this.BGAPNtextBox.Text = "";
                    this.BGA_placetextBox.Text = "";
                    this.bga_brieftextBox.Text = "";
                    query_Click(null, null);
                    MessageBox.Show("修改完毕");
                }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
        }
    }
}
