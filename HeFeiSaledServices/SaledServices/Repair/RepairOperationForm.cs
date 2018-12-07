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
    public partial class RepairOperationForm : Form
    {
        private PrepareUseDetail mPrepareUseDetail1 = null;
        private PrepareUseDetail mPrepareUseDetail2 = null;
        private PrepareUseDetail mPrepareUseDetail3 = null;
        private PrepareUseDetail mPrepareUseDetail4 = null;
        private PrepareUseDetail mPrepareUseDetail5 = null;
        public RepairOperationForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            repairertextBox.Text = LoginForm.currentUser;

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            track_serial_noTextBox.Focus();
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

                cmd.CommandText = "select distinct _type from repairFaultType";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.fault_typecomboBox.Items.Add(temp);
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
                this.track_serial_noTextBox.Text = this.track_serial_noTextBox.Text.ToUpper();//防止输入小写字符
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;


                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string stationInfo = "";
                    while (querySdr.Read())
                    {
                        stationInfo = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if(stationInfo == "维修" || stationInfo == "收货")                   
                    {
                        this.add.Enabled = true;                       
                    }
                    else
                    {
                        MessageBox.Show("此序列号的站别已经在:" + stationInfo + "，不能走下面的流程！");
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }

                    //查询是不是从待维修库出来的，如果不是，则不能进行下面的内容
                    cmd.CommandText = "select Id from wait_repair_out_house_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        MessageBox.Show("此序列号的不是从待维修库里面出来的:【" + this.track_serial_noTextBox.Text.Trim() + "】，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }
                    else
                    {
                        this.add.Enabled = true;
                    }
                    querySdr.Close();                   
                    //end

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

                    cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                     querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    string sourceBrief = "", customOrder = "", order_receive_date = "", custom_serial_no = "", vendor_serial_no = "", mb_make_date = "", custom_fault = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                        sourceBrief = querySdr[1].ToString();
                        customOrder = querySdr[2].ToString();
                        order_receive_date = DateTime.Parse(querySdr[3].ToString()).ToString("yyyy/MM/dd");
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
                        this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                        if (Untils.isTimeError(this.repair_datetextBox.Text.Trim()))
                        {
                            this.add.Enabled = false;
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

                this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                if (!error)
                {
                    repair_resultcomboBox.Focus();
                    repair_resultcomboBox.SelectAll();
                }
            }
        }
        private bool checkRepeat(string str1, string str2, string str3, string str4, string str5)
        {
            if (str1 != "" && (str1 == str2 || str1 == str3 || str1 == str4 || str1 == str5))
            {
                return true;
            }

            if (str2 != "" && (str2 == str3 || str2 == str4 || str2 == str5))
            {
                return true;
            }

            if (str3 != "" && (str3 == str4 || str3 == str5))
            {
                return true;
            }

            if (str4 != "" && (str4 == str5))
            {
                return true;
            }

            return false;
        }
        private void not_good_placetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                TextBox not_good_place1 = (TextBox)sender;

                ComboBox relatedCombo = null;
                if (not_good_place1.Name.EndsWith("1"))
                {
                    relatedCombo = this.material_mpnComboBox1;
                }
                else if (not_good_place1.Name.EndsWith("2"))
                {
                    relatedCombo = this.material_mpnComboBox2;
                }
                else if (not_good_place1.Name.EndsWith("3"))
                {
                    relatedCombo = this.material_mpnComboBox3;
                }
                else if (not_good_place1.Name.EndsWith("4"))
                {
                    relatedCombo = this.material_mpnComboBox4;
                }
                else if (not_good_place1.Name.EndsWith("5"))
                {
                    relatedCombo = this.material_mpnComboBox5;
                }

                bool error = false;
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入追踪条码的内容");
                    this.track_serial_noTextBox.Focus();
                    return;
                }

                if (not_good_place1.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入内容");
                    not_good_place1.Focus();
                    return;
                }

                if (checkRepeat(not_good_placetextBox1.Text, not_good_placetextBox2.Text, not_good_placetextBox3.Text, not_good_placetextBox4.Text, not_good_placetextBox5.Text))
                {
                    MessageBox.Show("输入的不良位置有重复内容！");
                    return;
                }

                string tableName = Constlist.table_name_LCFC_MBBOM;

                string not_good_place = not_good_place1.Text.Trim();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    //先用mpn在bom表中找一遍，如果找不到，然后用mb简称再查一遍，如果都没有，要不输错了，要不bom表不全
                    cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8 from " + tableName + " where MPN ='" + this.mpntextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    relatedCombo.Items.Clear();
                    while (querySdr.Read())
                    {
                        string material_mpn = querySdr[0].ToString(); ;
                        string temp = querySdr[1].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[2].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[3].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[4].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[5].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[6].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[7].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        } temp = querySdr[8].ToString().Trim();
                        if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                        {
                            relatedCombo.Items.Add(material_mpn);
                            continue;
                        }
                    }
                    querySdr.Close();

                    if (relatedCombo.Items.Count == 0)
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8 from " + tableName + " where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        relatedCombo.Items.Clear();
                        while (querySdr.Read())
                        {
                            string material_mpn = querySdr[0].ToString(); ;
                            string temp = querySdr[1].ToString().Trim();
                            if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[2].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[3].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[4].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[5].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[6].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[7].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[8].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                relatedCombo.Items.Add(material_mpn);
                                continue;
                            }
                        }
                        querySdr.Close();
                    }

                    if (relatedCombo.Items.Count == 0)
                    {
                        error = true;
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                    }                    

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (error)
                {
                    relatedCombo.Focus();
                    relatedCombo.SelectAll();
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
            this.checkBox19.Checked = false;
            this.checkBox20.Checked = false;
            this.checkBox21.Checked = false;
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
            } if (checkBox19.Checked)
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
            tableLayoutPanel5.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel5, true, null);
        }       

        private void add_Click(object sender, EventArgs e)
        {

            if (this.mb_brieftextBox.Text == "" || this.vendorTextBox.Text == "")
            {
                MessageBox.Show("输入完跟踪条码需要回车！");
                return;
            }

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
            string fault_describe_txt = this.fault_describetextBox.Text.Trim();
            string mbfa1rich_txt = this.mbfa1richTextBox.Text.Trim();
            string short_cut_txt = getShortCutText();
            string software_update_txt = this.software_updatecomboBox.Text.Trim();
            //string not_good_place_txt = this.not_good_placetextBox.Text.Trim();
            //string material_mpn_txt = this.material_mpnComboBox.Text.Trim();
            //string material_71pn_txt = this.material_71pntextBox.Text.Trim();
            string material_type_txt = this.material_typetextBox.Text.Trim();
            string fault_type_txt = this.fault_typecomboBox.Text.Trim();
            string action_txt = this.actioncomboBox.Text.Trim();
          
            string ECO_txt = this.ECOtextBox.Text.Trim();
            string repair_result_txt = this.repair_resultcomboBox.Text.Trim();
            string repairer_txt = this.repairertextBox.Text.Trim();
            string repair_date_txt = this.repair_datetextBox.Text.Trim();

            bool isNTF = false;

            if (repair_resultcomboBox.Text.Contains("NTF"))
            {
                isNTF = true;

                mbfa1rich_txt = "NTF";
                software_update_txt = "NTF";
                material_type_txt = "NTF";
                fault_type_txt = "NTF";
                action_txt = "NTF";
            }
            else //非NTF状态
            {
                isNTF = false;
                if (fault_describetextBox.Text.Trim() == ""
                    || fault_typecomboBox.Text.Trim() == ""
                    || actioncomboBox.Text.Trim() == "")
                {
                    MessageBox.Show("必须输入的框中有空值!");
                    return;
                }
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

                    //检查所有要是使用的数据，如果超过所拥有的数量，则不能生产任何记录

                    bool isUseMaterial = false;
                   
                    if (mPrepareUseDetail1!=null && mPrepareUseDetail1.Id != null)
                    {
                        //防止总数不对，实时查询totalUseNumber 并减去本次使用的数量
                        cmd.CommandText = "select usedNumber,realNumber from request_fru_smt_to_store_table where Id='" + mPrepareUseDetail1.Id + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string usedNumberStr = "";
                        while (querySdr.Read())
                        {
                            usedNumberStr = querySdr[0].ToString();                            
                        }
                        querySdr.Close();
                        int usedNumberInt=0;
                        try
                        {
                            usedNumberInt = Int32.Parse(mPrepareUseDetail1.thisUseNumber);
                            usedNumberInt += Int32.Parse(usedNumberStr);                        
                        }
                        catch (Exception ex)
                        {  
                        }

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + usedNumberInt + "' "
                                  + "where Id = '" + mPrepareUseDetail1.Id + "'";
                        cmd.ExecuteNonQuery();

                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + mPrepareUseDetail1.material_mpn + "','"
                           + mPrepareUseDetail1.thisUseNumber + "','"
                           + mPrepareUseDetail1.stock_place + "')";
                        cmd.ExecuteNonQuery();
                        

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                           "','维修','OK','" + DateTime.Now.ToString() + "','"
                           + mPrepareUseDetail1.stock_place + "','"
                           + mPrepareUseDetail1.thisUseNumber + "','"
                           + mPrepareUseDetail1.material_mpn + "','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                        isUseMaterial = true;

                        //使用完毕需要清空
                        mPrepareUseDetail1.Id = null;
                    }

                    if (mPrepareUseDetail2 != null && mPrepareUseDetail2.Id != null)
                    { 
                        //防止总数不对，实时查询totalUseNumber 并减去本次使用的数量
                        cmd.CommandText = "select usedNumber from request_fru_smt_to_store_table where Id='" + mPrepareUseDetail2.Id + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string usedNumberStr = "";
                        while (querySdr.Read())
                        {
                            usedNumberStr = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        int usedNumberInt = 0;
                        try
                        {
                            usedNumberInt = Int32.Parse(mPrepareUseDetail2.thisUseNumber);
                            usedNumberInt += Int32.Parse(usedNumberStr);
                        }
                        catch (Exception ex)
                        {
                        }

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + usedNumberInt + "' "
                                  + "where Id = '" + mPrepareUseDetail2.Id + "'";
                        cmd.ExecuteNonQuery();

                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + mPrepareUseDetail2.material_mpn + "','"
                           + mPrepareUseDetail2.thisUseNumber + "','"
                           + mPrepareUseDetail2.stock_place + "')";
                        cmd.ExecuteNonQuery();
                        

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                         "','维修','OK','" + DateTime.Now.ToString() + "','"
                         + mPrepareUseDetail2.stock_place + "','"
                         + mPrepareUseDetail2.thisUseNumber + "','"
                         + mPrepareUseDetail2.material_mpn + "','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                        isUseMaterial = true;

                        //使用完毕需要清空
                        mPrepareUseDetail2.Id = null;
                    }

                    if (mPrepareUseDetail3 != null && mPrepareUseDetail3.Id != null)
                    {

                        //防止总数不对，实时查询totalUseNumber 并减去本次使用的数量
                        cmd.CommandText = "select usedNumber from request_fru_smt_to_store_table where Id='" + mPrepareUseDetail3.Id + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string usedNumberStr = "";
                        while (querySdr.Read())
                        {
                            usedNumberStr = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        int usedNumberInt = 0;
                        try
                        {
                            usedNumberInt = Int32.Parse(mPrepareUseDetail3.thisUseNumber);
                            usedNumberInt += Int32.Parse(usedNumberStr);
                        }
                        catch (Exception ex)
                        {
                        }

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + usedNumberInt + "' "
                                  + "where Id = '" + mPrepareUseDetail3.Id + "'";
                        cmd.ExecuteNonQuery();


                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + mPrepareUseDetail3.material_mpn + "','"
                           + mPrepareUseDetail3.thisUseNumber + "','"
                           + mPrepareUseDetail3.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                         "','维修','OK','" + DateTime.Now.ToString() + "','"
                         + mPrepareUseDetail3.stock_place + "','"
                         + mPrepareUseDetail3.thisUseNumber + "','"
                         + mPrepareUseDetail3.material_mpn + "','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                        isUseMaterial = true;

                        //使用完毕需要清空
                        mPrepareUseDetail3.Id = null;
                    }

                    if (mPrepareUseDetail4 != null && mPrepareUseDetail4.Id != null)
                    {
                        //防止总数不对，实时查询totalUseNumber 并减去本次使用的数量
                        cmd.CommandText = "select usedNumber from request_fru_smt_to_store_table where Id='" + mPrepareUseDetail4.Id + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string usedNumberStr = "";
                        while (querySdr.Read())
                        {
                            usedNumberStr = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        int usedNumberInt = 0;
                        try
                        {
                            usedNumberInt = Int32.Parse(mPrepareUseDetail4.thisUseNumber);
                            usedNumberInt += Int32.Parse(usedNumberStr);
                        }
                        catch (Exception ex)
                        {
                        }

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + usedNumberInt + "' "
                                  + "where Id = '" + mPrepareUseDetail4.Id + "'";
                        cmd.ExecuteNonQuery();

                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + mPrepareUseDetail4.material_mpn + "','"
                           + mPrepareUseDetail4.thisUseNumber + "','"
                           + mPrepareUseDetail4.stock_place + "')";
                        cmd.ExecuteNonQuery();
                        

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                         "','维修','OK','" + DateTime.Now.ToString() + "','"
                         + mPrepareUseDetail4.stock_place + "','"
                         + mPrepareUseDetail4.thisUseNumber + "','"
                         + mPrepareUseDetail4.material_mpn + "','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                        isUseMaterial = true;

                        //使用完毕需要清空
                        mPrepareUseDetail4.Id = null;
                    }

                    if (mPrepareUseDetail5 != null && mPrepareUseDetail5.Id != null)
                    {
                        //防止总数不对，实时查询totalUseNumber 并减去本次使用的数量
                        cmd.CommandText = "select usedNumber from request_fru_smt_to_store_table where Id='" + mPrepareUseDetail5.Id + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string usedNumberStr = "";
                        while (querySdr.Read())
                        {
                            usedNumberStr = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        int usedNumberInt = 0;
                        try
                        {
                            usedNumberInt = Int32.Parse(mPrepareUseDetail5.thisUseNumber);
                            usedNumberInt += Int32.Parse(usedNumberStr);
                        }
                        catch (Exception ex)
                        {
                        }

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + usedNumberInt + "' "
                                  + "where Id = '" + mPrepareUseDetail5.Id + "'";
                        cmd.ExecuteNonQuery();

                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + mPrepareUseDetail5.material_mpn + "','"
                           + mPrepareUseDetail5.thisUseNumber + "','"
                           + mPrepareUseDetail5.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                         "','维修','OK','" + DateTime.Now.ToString() + "','"
                         + mPrepareUseDetail5.stock_place + "','"
                         + mPrepareUseDetail5.thisUseNumber + "','"
                         + mPrepareUseDetail5.material_mpn + "','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                        isUseMaterial = true;

                        //使用完毕需要清空
                        mPrepareUseDetail5.Id = null;
                    }

                    if (isUseMaterial == false)
                    {
                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                                               "','维修','NTF','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + repairer_txt + "')";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "INSERT INTO repair_record_table VALUES('"
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
                       + fault_describe_txt + "','"
                       + mbfa1rich_txt + "','"
                       + short_cut_txt + "','"
                       + software_update_txt + "','"
                       + "" + "','"
                       + "" + "','"
                       + "" + "','"
                       + material_type_txt + "','"
                       + fault_type_txt + "','"
                       + action_txt + "','"

                       + ECO_txt + "','"
                       + repair_result_txt + "','"
                       + repairer_txt + "','"
                       + repair_date_txt + "')";

                    cmd.ExecuteNonQuery();

                    //更新维修站别
                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                               + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
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
                MessageBox.Show("添加维修数据成功");

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
                this.fault_describetextBox.Text = "";
                this.mbfa1richTextBox.Text = "";
                uncheckShortCut();
                this.software_updatecomboBox.Text = "";

                not_good_placetextBox1.Text = "";
                material_mpnComboBox1.Items.Clear();
                material_71pntextBox1.Text = "";
                useNum1.Text = "";
                not_good_placetextBox2.Text = "";
                material_mpnComboBox2.Items.Clear();
                material_71pntextBox2.Text = "";
                useNum2.Text = "";
                not_good_placetextBox3.Text = "";
                material_mpnComboBox3.Items.Clear();
                material_71pntextBox3.Text = "";
                useNum3.Text = "";
                not_good_placetextBox4.Text = "";
                material_mpnComboBox4.Items.Clear();
                material_71pntextBox4.Text = "";
                useNum4.Text = "";
                not_good_placetextBox5.Text = "";
                material_mpnComboBox5.Items.Clear();
                material_71pntextBox5.Text = "";
                useNum5.Text = "";
                this.material_typetextBox.Text = "";
                this.fault_typecomboBox.Text = "";
                this.actioncomboBox.Text = "";
               
                this.ECOtextBox.Text = "";
                this.repair_resultcomboBox.Text = "";
                this.repair_datetextBox.Text = "";

                if (isNTF)//非NTF复位
                {
                    this.software_updatecomboBox.Enabled = true;
                    tableLayoutPanel5.Enabled = true;
                    this.fault_typecomboBox.Enabled = true;
                    this.actioncomboBox.Enabled = true;
                    this.mbfa1richTextBox.Enabled = true;
                    this.fault_describetextBox.Enabled = true;

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
                
                cmd.CommandType = CommandType.Text;

                string sqlStr = "select  top 20 * from repair_record_table";

                if (track_serial_noTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where track_serial_no like '%" + track_serial_noTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and track_serial_no like '%" + track_serial_noTextBox.Text.Trim() + "%'";
                    }
                }

                sqlStr += " order by Id desc";

                cmd.CommandText = sqlStr;

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

            string[] hTxt = {"ID", "跟踪条码", "厂商","客户别","来源","订单编号",
                             "收货日期","MB描述","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","故障原因","mbfa1","短路电压","软体更新",
                             "不良位置","材料MPN","材料71PN","材料类别","故障类别", "动作",
                             "ECO","修复结果","维修人", "修复日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void repair_resultcomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.repair_resultcomboBox.Text.Contains("NTF"))
            {
                this.software_updatecomboBox.Enabled = false;
                tableLayoutPanel5.Enabled = false;
                this.fault_typecomboBox.Enabled = false;
                this.actioncomboBox.Enabled = false;
                this.mbfa1richTextBox.Enabled = false;
                this.fault_describetextBox.Enabled = false;             

                this.checkBox1.Enabled = false;
                this.checkBox2.Enabled = false;
                this.checkBox3.Enabled = false;
                this.checkBox4.Enabled = false;
                this.checkBox5.Enabled = false;
                this.checkBox6.Enabled = false;
                this.checkBox7.Enabled = false;
                this.checkBox8.Enabled = false;
                this.checkBox9.Enabled = false;
                this.checkBox10.Enabled = false;
                this.checkBox11.Enabled = false;
                this.checkBox12.Enabled = false;
                this.checkBox13.Enabled = false;
                this.checkBox14.Enabled = false;
                this.checkBox15.Enabled = false;
                this.checkBox16.Enabled = false;
                this.checkBox17.Enabled = false;
                this.checkBox18.Enabled = false;
                this.checkBox19.Enabled = false;
                this.checkBox20.Enabled = false;
                this.checkBox21.Enabled = false;
                this.textBox1.Enabled = false;                
            }
            else
            {
                this.software_updatecomboBox.Enabled = true;
                tableLayoutPanel5.Enabled = true;
                this.fault_typecomboBox.Enabled = true;
                this.actioncomboBox.Enabled = true;
                this.mbfa1richTextBox.Enabled = true;
                this.fault_describetextBox.Enabled = true;              

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

                this.software_updatecomboBox.Focus();
                this.software_updatecomboBox.SelectAll();
            }          
        }

        private void choose_material_button_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            Button currentButton = (Button)sender;
            if (currentButton.Name.EndsWith("1"))
            {
                prepareUseList.fromIndex = 1;
            }
            else if (currentButton.Name.EndsWith("2"))
            {
                prepareUseList.fromIndex = 2;
            }
            else if (currentButton.Name.EndsWith("3"))
            {
                prepareUseList.fromIndex = 3;
            }
            else if (currentButton.Name.EndsWith("4"))
            {
                prepareUseList.fromIndex = 4;
            }
            else if (currentButton.Name.EndsWith("5"))
            {
                prepareUseList.fromIndex = 5;
            }
            prepareUseList.Show();
        }

        public void setPrepareUseDetail(string id, string mb_brief, string material_mpn, string stock_place, string thisUseNumber, string totalUseNumber, int index)
        {
            switch (index)
            {
                case 1:
                    if (material_mpnComboBox1.Text == "" || material_mpnComboBox1.Text != material_mpn)
                    {
                        MessageBox.Show("当前的MPN内容为空或不与选择的MPN相同！");
                        if (mPrepareUseDetail1 != null)
                        {
                            mPrepareUseDetail1 = null;
                        }
                        return;
                    }
                    
                    mPrepareUseDetail1 = new PrepareUseDetail();
                    mPrepareUseDetail1.Id = id;
                    mPrepareUseDetail1.mb_brief = mb_brief;
                    mPrepareUseDetail1.material_mpn = material_mpn;
                    mPrepareUseDetail1.stock_place = stock_place;
                    mPrepareUseDetail1.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail1.totalUseNumber = totalUseNumber;
                    this.useNum1.Text = thisUseNumber;
                    
                    break;
                case 2:
                    if (material_mpnComboBox2.Text == "" || material_mpnComboBox2.Text != material_mpn)
                    {
                        MessageBox.Show("当前的MPN内容为空或不与选择的MPN相同！");
                        if (mPrepareUseDetail2 != null)
                        {
                            mPrepareUseDetail2 = null;
                        }
                        return;
                    }
                    mPrepareUseDetail2 = new PrepareUseDetail();
                    mPrepareUseDetail2.Id = id;
                    mPrepareUseDetail2.mb_brief = mb_brief;
                    mPrepareUseDetail2.material_mpn = material_mpn;
                    mPrepareUseDetail2.stock_place = stock_place;
                    mPrepareUseDetail2.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail2.totalUseNumber = totalUseNumber;
                    this.useNum2.Text = thisUseNumber;
                    
                    break;
                case 3:
                    if (material_mpnComboBox3.Text == "" || material_mpnComboBox3.Text != material_mpn)
                    {
                        MessageBox.Show("当前的MPN内容为空或不与选择的MPN相同！");
                        if (mPrepareUseDetail3 != null)
                        {
                            mPrepareUseDetail3 = null;
                        }
                        return;
                    }
                    mPrepareUseDetail3 = new PrepareUseDetail();
                    mPrepareUseDetail3.Id = id;
                    mPrepareUseDetail3.mb_brief = mb_brief;
                    mPrepareUseDetail3.material_mpn = material_mpn;
                    mPrepareUseDetail3.stock_place = stock_place;
                    mPrepareUseDetail3.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail3.totalUseNumber = totalUseNumber;
                    this.useNum3.Text = thisUseNumber;

                    break;
                case 4:
                    if (material_mpnComboBox4.Text == "" || material_mpnComboBox4.Text != material_mpn)
                    {
                        MessageBox.Show("当前的MPN内容为空或不与选择的MPN相同！");
                        if (mPrepareUseDetail4 != null)
                        {
                            mPrepareUseDetail4 = null;
                        }
                        return;
                    }
                    mPrepareUseDetail4 = new PrepareUseDetail();
                    mPrepareUseDetail4.Id = id;
                    mPrepareUseDetail4.mb_brief = mb_brief;
                    mPrepareUseDetail4.material_mpn = material_mpn;
                    mPrepareUseDetail4.stock_place = stock_place;
                    mPrepareUseDetail4.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail4.totalUseNumber = totalUseNumber;
                    this.useNum4.Text = thisUseNumber;
                    
                    break;
                case 5:
                    if (material_mpnComboBox5.Text == "" || material_mpnComboBox5.Text != material_mpn)
                    {
                        MessageBox.Show("当前的MPN内容为空或不与选择的MPN相同！");
                        if (mPrepareUseDetail5 != null)
                        {
                            mPrepareUseDetail5 = null;
                        }
                        return;
                    }
                    mPrepareUseDetail5 = new PrepareUseDetail();
                    mPrepareUseDetail5.Id = id;
                    mPrepareUseDetail5.mb_brief = mb_brief;
                    mPrepareUseDetail5.material_mpn = material_mpn;
                    mPrepareUseDetail5.stock_place = stock_place;
                    mPrepareUseDetail5.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail5.totalUseNumber = totalUseNumber;
                    this.useNum5.Text = thisUseNumber;
                    
                    break;
            }            
        }

        private void material_mpnComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox relatedCombo = (ComboBox)sender;

            if (relatedCombo.Text == "")
            {
                MessageBox.Show("请选择一个值！");
                return;
            }
            TextBox current71pn = null;
            if (relatedCombo.Name.EndsWith("1"))
            {
                current71pn = this.material_71pntextBox1;
            }
            else if (relatedCombo.Name.EndsWith("2"))
            {
                current71pn = this.material_71pntextBox2;
            }
            else if (relatedCombo.Name.EndsWith("3"))
            {
                current71pn = this.material_71pntextBox3;
            }
            else if (relatedCombo.Name.EndsWith("4"))
            {
                current71pn = this.material_71pntextBox4;
            }
            else if (relatedCombo.Name.EndsWith("5"))
            {
                current71pn = this.material_71pntextBox5;
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
                    cmd.CommandText = "select material_vendor_pn from LCFC71BOM_table where material_mpn='" + relatedCombo.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string material_71pn_txt = "";
                    while (querySdr.Read())
                    {
                        material_71pn_txt = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (material_71pn_txt != "")
                    {
                        current71pn.Text = material_71pn_txt;
                    }
                    else
                    {
                        MessageBox.Show("LCFC71BOM表中" + relatedCombo.Text.Trim() + "信息不全！");
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clear1_Click(object sender, EventArgs e)
        {
            this.not_good_placetextBox1.Text = "";
            this.material_mpnComboBox1.Text = "";
            this.material_71pntextBox1.Text = "";
            this.useNum1.Text = "";
            if (mPrepareUseDetail1 != null)
            {
                mPrepareUseDetail1.Id = null;
            }
        }

        private void clear2_Click(object sender, EventArgs e)
        {
            this.not_good_placetextBox2.Text = "";
            this.material_mpnComboBox2.Text = "";
            this.material_71pntextBox2.Text = "";
            this.useNum2.Text = "";
            if (mPrepareUseDetail2 != null)
            {
                mPrepareUseDetail2.Id = null;
            }
        }

        private void clear3_Click(object sender, EventArgs e)
        {
            this.not_good_placetextBox3.Text = "";
            this.material_mpnComboBox3.Text = "";
            this.material_71pntextBox3.Text = "";
            this.useNum3.Text = "";
            if (mPrepareUseDetail3 != null)
            {
                mPrepareUseDetail3.Id = null;
            }
        }

        private void clear4_Click(object sender, EventArgs e)
        {
            this.not_good_placetextBox4.Text = "";
            this.material_mpnComboBox4.Text = "";
            this.material_71pntextBox4.Text = "";
            this.useNum4.Text = "";
            if (mPrepareUseDetail4 != null)
            {
                mPrepareUseDetail4.Id = null;
            }
        }

        private void clear5_Click(object sender, EventArgs e)
        {
            this.not_good_placetextBox5.Text = "";
            this.material_mpnComboBox5.Text = "";
            this.material_71pntextBox5.Text = "";
            this.useNum5.Text = "";
            if (mPrepareUseDetail5 != null)
            {
                mPrepareUseDetail5.Id = null;
            }
        }
    }
}
