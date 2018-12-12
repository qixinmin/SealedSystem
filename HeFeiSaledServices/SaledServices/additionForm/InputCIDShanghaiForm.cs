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
    public partial class InputCIDShanghaiForm : Form
    {
        public InputCIDShanghaiForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            inputertextBox.Text = LoginForm.currentUser;
            this.inputdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }

            track_serial_noTextBox.Focus();
            this.add.Enabled = false;
        }

        private void loadAdditionInfomation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct _type from customResponsibilityType";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_res_typecomboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                    cmd.CommandText = "select Id from Packagetable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
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

                    cmd.CommandText = "select flex_id from flexidRecord where track_serial_no='" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.flexidtextBox.Text = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select vendor, product,custom_order,custommaterialNo,custom_serial_no,mb_brief,mpn,order_receive_date from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.vendorTextBox.Text = querySdr[0].ToString();
                        this.producttextBox.Text = querySdr[1].ToString();
                        this.ordernotextBox.Text = querySdr[2].ToString();
                        this.custom_matertial_notextBox.Text = querySdr[3].ToString();
                        this.custom_serial_notextBox.Text = querySdr[4].ToString();
                        this.mb_brieftextBox.Text = querySdr[5].ToString();
                        this.mpntextBox.Text = querySdr[6].ToString();
                        this.receivedatetextBox.Text = DateTime.Parse(querySdr[7].ToString()).ToString("yyyy/MM/dd");
                    }
                    querySdr.Close();

                    if (this.vendorTextBox.Text == "")
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

        private void uncheckShortCut()
        {
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;
            this.checkBox6.Checked = false;
            this.checkBox7.Checked = false;
            this.checkBox8.Checked = false;
            this.checkBox9.Checked = false;
            this.checkBox10.Checked = false;
            this.checkBox11.Checked = false;
            this.checkBox12.Checked = false;
            this.checkBox13.Checked = false;
            this.checkBox14.Checked = false;
            this.checkBox15.Checked = false;
            this.checkBox16.Checked = false;
            this.checkBox17.Checked = false;
            this.checkBox18.Checked = false;
            this.checkBox19.Enabled = false;
            this.checkBox20.Enabled = false;
            this.checkBox21.Enabled = false;
            this.textBox1.Text = "";
        }

        private string getShortCutText()
        {
            string retStr = "";
            if (checkBox1.Checked)
            {
                retStr += checkBox1.Text.Trim() + ",";
            }
            if (checkBox2.Checked)
            {
                retStr += checkBox2.Text.Trim() + ",";
            }
            if (checkBox3.Checked)
            {
                retStr += checkBox3.Text.Trim() + ",";
            }

            if (checkBox4.Checked)
            {
                retStr += checkBox4.Text.Trim() + ",";
            }

            if (checkBox5.Checked)
            {
                retStr += checkBox5.Text.Trim() + ",";
            }
            if (checkBox6.Checked)
            {
                retStr += checkBox6.Text.Trim() + ",";
            }
            if (checkBox7.Checked)
            {
                retStr += checkBox7.Text.Trim() + ",";
            }
            if (checkBox8.Checked)
            {
                retStr += checkBox8.Text.Trim() + ",";
            }
            if (checkBox9.Checked)
            {
                retStr += checkBox9.Text.Trim() + ",";
            }
            if (checkBox10.Checked)
            {
                retStr += checkBox10.Text.Trim() + ",";
            }
            if (checkBox11.Checked)
            {
                retStr += checkBox11.Text.Trim() + ",";
            } if (checkBox12.Checked)
            {
                retStr += checkBox12.Text.Trim() + ",";
            } if (checkBox13.Checked)
            {
                retStr += checkBox13.Text.Trim() + ",";
            } if (checkBox14.Checked)
            {
                retStr += checkBox14.Text.Trim() + ",";
            } if (checkBox15.Checked)
            {
                retStr += checkBox15.Text.Trim() + ",";
            } if (checkBox16.Checked)
            {
                retStr += checkBox16.Text.Trim() + ",";
            } if (checkBox17.Checked)
            {
                retStr += checkBox17.Text.Trim() + ",";
            } if (checkBox18.Checked)
            {
                retStr += checkBox18.Text.Trim() + ",";
            }
            if (checkBox19.Checked)
            {
                retStr += checkBox19.Text.Trim() + ",";
            } if (checkBox20.Checked)
            {
                retStr += checkBox20.Text.Trim() + ",";
            } if (checkBox21.Checked)
            {
                retStr += checkBox21.Text.Trim() + ",";
            } 
            if (this.textBox1.Text != "")
            {
                retStr += textBox1.Text.Trim() + ",";
            }

            return retStr;
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

            tableLayoutPanel4.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel4, true, null);
        }       

        private void add_Click(object sender, EventArgs e)
        {
            if (custom_res_typecomboBox.Text.Trim() == "")
            {
                MessageBox.Show("客责类别的内容为空，请检查！");
                this.custom_res_typecomboBox.Focus();
                return;
            }
            if (customResponsibilityrichTextBox.Text.Trim() == "")
            {
                MessageBox.Show("客责描述的内容为空，请检查！");
                this.customResponsibilityrichTextBox.Focus();
                return;
            }

            if (custom_matertial_notextBox.Text.Trim() == "")
            {
                MessageBox.Show("客户料号为空，请回车！");
                this.custom_matertial_notextBox.Focus();
                return;
            }

            bool error = false;
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string custom_material_no_txt = this.custom_matertial_notextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
            string custom_res_type_txt = this.custom_res_typecomboBox.Text.Trim();
            string customResponsibility_txt = this.customResponsibilityrichTextBox.Text.Trim();            
            string short_cut_txt = getShortCutText();
            string inputer_txt = this.inputertextBox.Text.Trim();
            string repair_date_txt = this.inputdatetextBox.Text.Trim();            

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO cidRecord VALUES('"
                        + track_serial_no_txt + "','"
                        + vendor_txt + "','"
                        + product_txt + "','"
                        + orderno_txt + "','"
                        + custom_material_no_txt + "','"
                        + custom_serial_no_txt + "','"
                        + mb_brief_txt + "','"
                        + mpn_txt + "','"
                        + receivedate_txt + "','"
                        + customFault_txt + "','"
                        + custom_res_type_txt + "','"
                        + customResponsibility_txt + "','"
                        + short_cut_txt + "','"
                        + inputer_txt + "','"
                        + repair_date_txt + "')";
                    
                    cmd.ExecuteNonQuery();

                    //更新维修站别
                    cmd.CommandText = "update stationInformation set station = 'CID', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                               + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //更新flexid状态
                    cmd.CommandText = "update flexidRecord set _status = 'cid' where track_serial_no = '" 
                        + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //把订单变化里面对应的 cid数量 设置为+1
                    cmd.CommandText = "select cid_number from receiveOrder where orderno='" + this.ordernotextBox.Text + "' and custom_materialNo='"+this.custom_matertial_notextBox.Text+"'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    int cidNum = 0;
                    while (querySdr.Read())
                    {
                        cidNum = Int32.Parse(querySdr[0].ToString());
                    }
                    querySdr.Close();

                    //更新CID数量
                    cmd.CommandText = "update receiveOrder set cid_number = '" + (cidNum+1) + "' where orderno='" + this.ordernotextBox.Text + "' and custom_materialNo='" + this.custom_matertial_notextBox.Text + "'";
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

                this.track_serial_noTextBox.Text = "";
                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
                
                this.ordernotextBox.Text = "";
                this.receivedatetextBox.Text = "";
                this.mb_brieftextBox.Text = "";
                this.custom_serial_notextBox.Text = "";
               
                this.mpntextBox.Text = "";
               
                this.customFaulttextBox.Text = "";
               
                this.customResponsibilityrichTextBox.Text = "";
                uncheckShortCut();

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
                cmd.CommandText = "select * from cidRecord";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "repair_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID","跟踪条码","收货日期","厂商","客户别","订单编号","客户料号","客户序号","MB简称","MPN","客户故障","客责类别","客责描述","短路电压"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void repair_resultcomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.customResponsibilityrichTextBox.Enabled = true;

            this.checkBox1.Enabled = true;
            this.checkBox2.Enabled = true;
            this.checkBox3.Enabled = true;
            this.checkBox4.Enabled = true;
            this.checkBox5.Enabled = true;
            this.checkBox6.Enabled = true;
            this.checkBox7.Enabled = true;
            this.checkBox8.Enabled = true;
            this.checkBox9.Enabled = true;
            this.checkBox10.Enabled = true;
            this.checkBox11.Enabled = true;
            this.checkBox12.Enabled = true;
            this.checkBox13.Enabled = true;
            this.checkBox14.Enabled = true;
            this.checkBox15.Enabled = true;
            this.checkBox16.Enabled = true;
            this.checkBox17.Enabled = true;
            this.checkBox18.Enabled = true;
            this.checkBox19.Enabled = true;
            this.checkBox20.Enabled = true;
            this.checkBox21.Enabled = true;
            this.textBox1.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

        }
    }
}
