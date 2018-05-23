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
    public partial class FaultMBStoreForm : Form
    {
        private string currentTableName = "";
        private ChooseStock chooseStock = new ChooseStock();
        private PrepareUseDetail mPrepareUseDetail;
        public FaultMBStoreForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            inputertextBox.Text = LoginForm.currentUser;
            this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            mPrepareUseDetail = new PrepareUseDetail();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void loadAdditionInfomation()
        {
            this.typecomboBox.SelectedIndex = 0;
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
                    
                    cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
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

                    //首先查询本库中是否已经出过此块板子
                    cmd.CommandText = "select track_serial_no from fault_mb_enter_record_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
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
                        this.custommaterialNoTextBox.Text = customMaterialNo;

                        //确定库位
                        cmd.CommandText = "select Id,house, place where custom_materialNo='" + customMaterialNo + "'";
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
                    }
                    else
                    {  
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        error = true;
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
            string source_txt = this.sourcetextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string mb_describe_txt = this.mb_describetextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string mb_make_date_txt = this.mb_make_dateTextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
            string ECO_txt = this.ECOtextBox.Text.Trim();          
            string inputer_txt = this.inputertextBox.Text.Trim();
            string input_date_txt = this.input_datetextBox.Text.Trim();
            string custommaterialNo = this.custommaterialNoTextBox.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from " + currentTableName + " where track_serial_no_txt='" + this.track_serial_noTextBox.Text + "'";
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
                        + source_txt + "','"
                        + orderno_txt + "','"
                        + receivedate_txt + "','"
                        + mb_describe_txt + "','"
                        + mb_brief_txt + "','"
                        + custom_serial_no_txt + "','"
                        + vendor_serail_no_txt + "','"
                        + mpn_txt + "','"
                        + mb_make_date_txt + "','"
                        + customFault_txt + "','"
                        + ECO_txt + "','"                     
                        + inputer_txt + "','"
                        + input_date_txt + "','"
                        + custommaterialNo + "')";
                    
                    cmd.ExecuteNonQuery();

                    //更新维修站别
                    cmd.CommandText = "update stationInformation set station = '不良品库', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                               + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
                    
                    //更新数量
                    cmd.CommandText = "select Id,left_number from store_house_ng where custom_materialNo='" + custommaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    exist = "";
                    string left_number = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                        left_number = querySdr[1].ToString();
                        break;
                    }

                    if (exist == "")
                    {
                        cmd.CommandText = "INSERT INTO store_house_ng VALUES('"
                            + chooseStock.house + "','"
                            + chooseStock.place + "','"
                            + custommaterialNo + "','"
                            + "1" + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        try 
                        {
                            int totalLeft = Int32.Parse(left_number);
                            int thistotal = totalLeft +1;

                            cmd.CommandText = "update store_house_ng set left_number = '" + thistotal + "'"
                                   + "where mpn = '" + custommaterialNo + "'";
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
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
             
                //this.repairertextBox.Text = "";
                this.input_datetextBox.Text = "";
                this.custommaterialNoTextBox.Text = "";

                this.track_serial_noTextBox.Focus();
                query_Click(null, null);
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

            string[] hTxt = {"ID", "跟踪条码", "厂商","客户别","来源","订单编号",
                             "收货日期","MB描述","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","ECO","录入人", "录入日期","客户料号"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }
       
        private void typecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTableName = "fault_mb_enter_record_table";
        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;

            this.stockplacetextBox.Text = chooseStock.house + "," + chooseStock.place;
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
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this, "store_house_ng");
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }
    }
}
