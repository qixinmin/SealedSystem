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
    public partial class BufferFaultMBStoreForm : Form
    {
        private string currentTableName = "fault_mb_enter_record_table_buffer";
        private ChooseStock chooseStock = new ChooseStock();
        private PrepareUseDetail mPrepareUseDetail;
        public BufferFaultMBStoreForm()
        {
            InitializeComponent();

            inputertextBox.Text = LoginForm.currentUser;
            this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            mPrepareUseDetail = new PrepareUseDetail();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
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

                    cmd.CommandText = "select mpn, custom_serial_no,vendor_serial_no,mb_brief,vendormaterialNo,vendor,product from mb_in_stock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();                    
                    string mpn = "", custom_serial_no = "", vendor_serial_no = "", mb_brief = "", vendormaterialNo = "", vendor = "", product = "";
                    while (querySdr.Read())
                    {
                        mpn = querySdr[0].ToString();
                        custom_serial_no = querySdr[1].ToString();
                        vendor_serial_no = querySdr[2].ToString();
                        mb_brief = querySdr[3].ToString();
                        vendormaterialNo = querySdr[4].ToString();
                        vendor = querySdr[5].ToString();
                        product = querySdr[6].ToString();
                    }
                    querySdr.Close();

                    if (mpn == "")
                    {
                        MessageBox.Show("此序列号的板子不在主板入库表中！");
                        mConn.Close();
                        return;
                    }

                    //首先查询本库中是否已经出过此块板子
                    cmd.CommandText = "select track_serial_no from "+currentTableName+" where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string exist = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();                        
                    }
                    querySdr.Close();
                    if (exist != "")
                    {
                        MessageBox.Show("本块MB已经在不良品库里了，请检查！");
                        this.track_serial_noTextBox.Clear();
                        this.track_serial_noTextBox.Focus();
                        mConn.Close();
                        return;
                    }

                    this.vendorTextBox.Text = vendor;
                    this.producttextBox.Text = product;
                    this.mb_brieftextBox.Text = mb_brief;
                    this.custom_serial_notextBox.Text = custom_serial_no;
                    this.vendor_serail_notextBox.Text = vendor_serial_no;
                    this.mpntextBox.Text = mpn;
                    this.vendormaterialNoTextBox.Text = vendormaterialNo;
                    this.inputertextBox.Text = LoginForm.currentUser;
                    this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                    //确定库位
                    cmd.CommandText = "select Id,house, place from store_house_ng_buffer_mb where mpn='" + vendormaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    string id="",house="",place="";
                    while (querySdr.Read())
                    {
                        id = querySdr[0].ToString();
                        house = querySdr[1].ToString();
                        place = querySdr[2].ToString();
                    }
                    querySdr.Close();
                    if(id !="")
                    {
                        setChooseStock(id, house, place);
                    }
                   
                    mConn.Close();
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
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();

            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();          
            string inputer_txt = this.inputertextBox.Text.Trim();
            string input_date_txt = this.input_datetextBox.Text.Trim();
            string vendormaterialNo = this.vendormaterialNoTextBox.Text.Trim();

            if(track_serial_no_txt == "" ||mpn_txt == "")
            {
                MessageBox.Show("输入的内容为空，请检查！");               
                return;
            }

            if (chooseStock.house == "" || chooseStock.place == "")
            {
                MessageBox.Show("输入的库房为空，请检查！");
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

                    cmd.CommandText = "select Id from " + currentTableName + " where track_serial_no='" + this.track_serial_noTextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string exist = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (exist != "")
                    {
                        MessageBox.Show("此序列号已经存在库中了，请检查！");
                        conn.Close();
                        return;
                    }
                    
                    cmd.CommandText = "INSERT INTO " + currentTableName + " VALUES('"
                        + track_serial_no_txt + "','"
                        + vendor_txt + "','"
                        + product_txt + "','"                      
                        + mb_brief_txt + "','"
                        + custom_serial_no_txt + "','"
                        + vendor_serail_no_txt + "','"
                        + vendormaterialNo + "','"
                        + mpn_txt + "','"    
                        + inputer_txt + "','"
                        + input_date_txt + "')";
                    
                    cmd.ExecuteNonQuery();
                    
                    //更新数量
                    cmd.CommandText = "select Id,number from store_house_ng_buffer_mb where house='" + chooseStock.house + "' and place='" + chooseStock.place + "'";
                    querySdr = cmd.ExecuteReader();
                    exist = "";
                    string left_number = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                        left_number = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (left_number == null || left_number == "")
                    {
                        left_number = "0";
                    }
                    
                    try 
                    {
                        int totalLeft = Int32.Parse(left_number);
                        int thistotal = totalLeft +1;

                        cmd.CommandText = "update store_house_ng_buffer_mb set number = '" + thistotal + "', mpn='" + this.vendormaterialNoTextBox.Text.Trim() + "'"
                                + " where house='" + chooseStock.house + "' and place='" + chooseStock.place + "'";
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    //原良品库房的数量要减去1
                    //需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.vendormaterialNoTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "";
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "update store_house set number = '" + (Int32.Parse(number) - 1) + "'  where house='" + house + "' and place='" + place + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select mpn from store_house where mpn != '' group by mpn having COUNT(*) > 1 ";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("请关闭窗口之前上报管理员并拍照");
                    }
                    querySdr.Close();
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
                MessageBox.Show("添不良品库数据成功");

                this.track_serial_noTextBox.Text = "";
                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
              
                this.mb_brieftextBox.Text = "";
                this.custom_serial_notextBox.Text = "";
                this.vendor_serail_notextBox.Text = "";
                this.mpntextBox.Text = "";               
             
                //this.repairertextBox.Text = "";
                this.input_datetextBox.Text = "";
                this.vendormaterialNoTextBox.Text = "";

                this.track_serial_noTextBox.Focus();
                query_Click(null, null);

                stockplacetextBox.Text = "";
                stockplacetextBox.Enabled = true;
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from "+currentTableName;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, currentTableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "厂商","客户别","MB简称","客户序号","厂商序号","厂商料号","MPN",
                             "录入人", "录入日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;

            this.stockplacetextBox.Text = chooseStock.house + "," + chooseStock.place;
            this.stockplacetextBox.Enabled = false;
        }

        private void stockplacetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //if (this.typecomboBox.Text == "")
                //{
                //    MessageBox.Show("请输入操作类型:出库还是入库");
                //    return;
                //}
                //打开选择界面，并把结果返回到本界面来
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this, "store_house_ng_buffer_mb");
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }
    }
}
