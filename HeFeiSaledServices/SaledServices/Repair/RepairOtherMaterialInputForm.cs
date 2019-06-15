using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Repair
{
    public partial class RepairOtherMaterialInputForm : Form
    {
        public RepairOtherMaterialInputForm()
        {
            InitializeComponent();

            repairertextBox.Text = LoginForm.currentUser;

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            track_serial_noTextBox.Focus();
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    this.track_serial_noTextBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
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

                    cmd.CommandText = "select _8sCode from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and isLock='true'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此序列号需要分析但是已经锁定，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
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

                       
                        this.ordernotextBox.Text = customOrder;
                        this.mb_describetextBox.Text = mb_describe;
                        this.mb_brieftextBox.Text = mb_brief;
                        this.mpntextBox.Text = mpn;
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
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
        }

        private void not_good_placetextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void add_Click(object sender, EventArgs e)
        {
            if (this.mb_brieftextBox.Text == "")
            {
                MessageBox.Show("输入完跟踪条码需要回车！");
                return;
            }

            if (this.useNum1.Text.Trim() == "" &&
                this.useNum2.Text.Trim() == "" &&
                this.useNum3.Text.Trim() == "" &&
                this.useNum4.Text.Trim() == "" &&
                this.useNum5.Text.Trim() == "")
            {
                MessageBox.Show("需输入数字！");
                return;
            }

            if (this.useNum1.Text.Trim() != "")
            {
                try
                {
                    Int16.Parse(this.useNum1.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数字1的内容不是数字！");
                    return;
                }
            }

            if (this.useNum2.Text.Trim() != "")
            {
                try
                {
                    Int16.Parse(this.useNum2.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数字2的内容不是数字！");
                    return;
                }
            }

            if (this.useNum3.Text.Trim() != "")
            {
                try
                {
                    Int16.Parse(this.useNum3.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数字3的内容不是数字！");
                    return;
                }
            }

            if (this.useNum4.Text.Trim() != "")
            {
                try
                {
                    Int16.Parse(this.useNum4.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数字4的内容不是数字！");
                    return;
                }
            }

            if (this.useNum5.Text.Trim() != "")
            {
                try
                {
                    Int16.Parse(this.useNum5.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数字5的内容不是数字！");
                    return;
                }
            }

            bool error = false;
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string mb_describe_txt = this.mb_describetextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string repairer_txt = this.repairertextBox.Text.Trim();
            string repair_date_txt = this.repair_datetextBox.Text.Trim();

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

                    if (this.not_good_placetextBox1.Text.Trim() != "" && this.material_mpnComboBox1.Text.Trim() !="" && this.useNum1.Text.Trim() !="")
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record_other VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + this.material_mpnComboBox1.Text.Trim() + "','"
                           + this.useNum1.Text.Trim() + "','"
                           + not_good_placetextBox1.Text.Trim() + "','')";
                        cmd.ExecuteNonQuery();
                    }

                    if (this.not_good_placetextBox2.Text.Trim() != "" && this.material_mpnComboBox2.Text.Trim() != "" && this.useNum2.Text.Trim() != "")
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record_other VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + this.material_mpnComboBox2.Text.Trim() + "','"
                           + this.useNum2.Text.Trim() + "','"
                           + not_good_placetextBox2.Text.Trim() + "','')";
                        cmd.ExecuteNonQuery();
                    }

                    if (this.not_good_placetextBox3.Text.Trim() != "" && this.material_mpnComboBox3.Text.Trim() != "" && this.useNum3.Text.Trim() != "")
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record_other VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + this.material_mpnComboBox3.Text.Trim() + "','"
                           + this.useNum3.Text.Trim() + "','"
                           + not_good_placetextBox3.Text.Trim() + "','')";
                        cmd.ExecuteNonQuery();
                    }

                    if (this.not_good_placetextBox4.Text.Trim() != "" && this.material_mpnComboBox4.Text.Trim() != "" && this.useNum4.Text.Trim() != "")
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record_other VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                          + this.material_mpnComboBox4.Text.Trim() + "','"
                           + this.useNum4.Text.Trim() + "','"
                           + not_good_placetextBox4.Text.Trim() + "','')";
                        cmd.ExecuteNonQuery();
                    }

                    if (this.not_good_placetextBox5.Text.Trim() != "" && this.material_mpnComboBox5.Text.Trim() != "" && this.useNum5.Text.Trim() != "")
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record_other VALUES('"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + track_serial_no_txt + "','"
                           + this.material_mpnComboBox5.Text.Trim() + "','"
                           + this.useNum5.Text.Trim() + "','"
                           + not_good_placetextBox5.Text.Trim() + "','')";
                        cmd.ExecuteNonQuery();
                    }

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
                this.ordernotextBox.Text = "";
                this.mb_describetextBox.Text = "";
                this.mb_brieftextBox.Text = "";
                this.mpntextBox.Text = "";

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
                this.repair_datetextBox.Text = "";

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

                string sqlStr = "select  top 20 * from fru_smt_used_record_other";

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
                sda.Fill(ds, "fru_smt_used_record_other");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            string[] hTxt = {"ID", "维修人", "修复日期","跟踪条码", "材料mpn","数量","位置","备注"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void RepairOtherMaterialInputForm_Load(object sender, EventArgs e)
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
            tableLayoutPanel5.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel5, true, null);
        }
    }
}
