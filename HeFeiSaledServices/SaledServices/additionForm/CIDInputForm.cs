using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Repair;

namespace SaledServices
{
    public partial class CIDInputForm : Form
    {
        private ChooseStock chooseStock = new ChooseStock();
        public CIDInputForm()
        {
            InitializeComponent();

            inputertextBox.Text = LoginForm.currentUser;
            this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            this.add.Enabled = false;
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

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from Packagetable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                   if(querySdr.HasRows)
                    {
                        querySdr.Close();
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的已经在包装站别了，不能入CID！");
                        error = true;
                        mConn.Close();
                        return;
                    }
                    querySdr.Close();

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
                        mb_make_date = querySdr[6].ToString();
                        custom_fault = querySdr[7].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select flex_id from flexidRecord where track_serial_no='" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.flexidtextBox.Text = querySdr[0].ToString();
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

                        this.custommaterialNoTextBox.Text = customMaterialNo;
                        this.vendorTextBox.Text = vendor;
                        this.producttextBox.Text = product;
                        this.sourcetextBox.Text = sourceBrief;
                        this.ordernotextBox.Text = customOrder;
                        this.receivedatetextBox.Text = order_receive_date;
                        this.mb_describetextBox.Text = mb_describe;
                        this.mb_brieftextBox.Text = mb_brief;
                        this.custom_serial_notextBox.Text = custom_serial_no;
                        this.vendor_serail_notextBox.Text = vendor_serial_no;
                        this.mpntextBox.Text = mpn;
                        this.mb_make_dateTextBox.Text = mb_make_date;
                        this.customFaulttextBox.Text = custom_fault;
                        this.ECOtextBox.Text = eco;
                        this.inputertextBox.Text = LoginForm.currentUser;
                        this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
                    mConn.Close();
                    this.add.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void RepairOperationForm_Load(object sender, EventArgs e)
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

        private void add_Click(object sender, EventArgs e)
        {
            bool error = false;

            string custommaterialNo_txt = this.custommaterialNoTextBox.Text.Trim();
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();
            string source_txt = this.sourcetextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string mb_describe_txt = this.mb_describetextBox.Text.Trim().Replace('\'', '_'); ;
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string mb_make_date_txt = this.mb_make_dateTextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
            string ECO_txt = this.ECOtextBox.Text.Trim();          
            string inputer_txt = this.inputertextBox.Text.Trim();
            string input_date_txt = this.input_datetextBox.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from cidRecord where track_serial_no = '" + track_serial_no_txt + "'";
                    string id = "";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        id = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (id != "")
                    {
                        MessageBox.Show("本序列号已经被CID了，不能重复输入！");
                        clearInput();
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "select cid_number from receiveOrder where orderno = '" + orderno_txt
                           + "' and custom_materialNo = '" + custommaterialNo_txt + "'";

                    int cidNumber = -1;                  
                    querySdr = cmd.ExecuteReader();                    
                    while (querySdr.Read())
                    {
                        cidNumber = Int32.Parse(querySdr[0].ToString());                       
                    }
                    querySdr.Close();
                    if (cidNumber < 0)
                    {
                        MessageBox.Show("收货单里面有问题，请查看收货单！");
                        clearInput();
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "insert into cidRecord values('" + track_serial_no_txt + "','" + orderno_txt + "','" + custommaterialNo_txt + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
            "','CID','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','"+inputer_txt+"')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update receiveOrder set cid_number = '"+ (cidNumber+1)+"'"
                               + " where orderno = '" + orderno_txt + "' and custom_materialNo = '" + custommaterialNo_txt + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    error = true;
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(ex.ToString());
            }

            if (error == false)
            {
                MessageBox.Show("添加CID数据成功");

                clearInput(); 
            }
        }

        private void clearInput()
        {
            this.track_serial_noTextBox.Text = "";
            this.vendorTextBox.Text = "";
            this.producttextBox.Text = "";
            this.sourcetextBox.Text = "";
            this.ordernotextBox.Text = "";
            this.receivedatetextBox.Text = "";
            this.mb_describetextBox.Text = "";
            this.mb_brieftextBox.Text = "";
            this.custom_serial_notextBox.Text = "";
            this.vendor_serail_notextBox.Text = "";
            this.mpntextBox.Text = "";
            this.mb_make_dateTextBox.Text = "";
            this.customFaulttextBox.Text = "";

            this.ECOtextBox.Text = "";

            this.input_datetextBox.Text = "";

            this.track_serial_noTextBox.Focus();     
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select orderno,custom_materialNo,_status,receivedNum,cid_number from receiveOrder";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = { "订单编号", "客户料号", "状态", "收货数量", "CID数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
