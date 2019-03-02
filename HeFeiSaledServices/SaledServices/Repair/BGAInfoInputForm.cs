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
    public partial class BGAInfoInputForm : Form
    {
        public BGAInfoInputForm()
        {
            InitializeComponent();
            this.repairertextBox.Text = LoginForm.currentUser;
            repair_datetextBox.Text  = DateTime.Now.ToString("yyyy/MM/dd");

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
                        mConn.Close();
                        return;
                    }

                    //查询bga的维修记录，如果有bga的维修记录，则取最后一条，判断状态是否是不是“BGA待换”，否则提示报错，然后把相关bga的内容填入相应内容中去
                    cmd.CommandText = "select top 1 bga_repair_result,bgatype,BGAPN,BGA_place,bga_brief,mbfa1 from bga_repair_record_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'  order by Id desc";

                    querySdr = cmd.ExecuteReader();
                    string bga_repair_result="", bgatype="", BGAPN="", BGA_place="", bga_brief="", mbfa1 = "";
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
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        this.bga_brieftextBox.Text = bga_brief;
                        this.BGAPNtextBox.Text = BGAPN;
                        this.BGA_placetextBox.Text = BGA_place;
                        this.mbfa1richTextBox.Text = mbfa1;
                        if (bgatype == "VGA")
                        {
                            this.VGA.Checked = true;
                        }
                        else if (bgatype == "CPU")
                        {
                            this.CPU.Checked = true;
                        }
                        else if (bgatype == "PCH")
                        {
                            this.PCH.Checked = true;
                        }
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
                    
                }
            }
        }

        private void CPU_CheckedChanged(object sender, EventArgs e)
        {
            if (this.VGA.Checked == false && this.CPU.Checked == false && this.PCH.Checked == false)
            {
                return;
            }

            this.BGA_placetextBox.ReadOnly = false;
            this.BGA_placetextBox.Text = "";
            string bga_mpn = "";
            string bga_brief = "";
            if (this.mpntextBox.Text.Trim() == "")
            {
                MessageBox.Show("请输入追踪条码！");
                this.VGA.Checked = false;
                this.CPU.Checked = false;
                this.PCH.Checked = false;
                return;
            }

            if (this.VGA.Checked)
            {
                bga_mpn = "vendor_vga_mpn";
                bga_brief = "vga_brief_describe";
            }
            else if (this.CPU.Checked)
            {
                bga_mpn = "vendor_cpu_mpn";
                bga_brief = "cpu_brief";
            }
            else if (this.PCH.Checked)
            {
                bga_mpn = "vendor_pch_mpn";
                bga_brief = "pcb_brief_describe";
            }
            else
            {
                return;
            }

            string tableName = "";
            //if (this.vendorTextBox.Text.Trim() == "LCFC")
            //{
                tableName = Constlist.table_name_LCFC_MBBOM;
            //}
            //else if (this.vendorTextBox.Text.Trim() == "COMPAL")
            //{
            //    tableName = Constlist.table_name_COMPAL_MBBOM;
            //}

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select " + bga_mpn + "," + bga_brief + " from MBMaterialCompare where mpn='" + this.mpntextBox.Text.Trim() + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                string bga_mpn_txt = "", bga_brief_txt = "";
                while (querySdr.Read())
                {
                    bga_mpn_txt = querySdr[0].ToString();
                    bga_brief_txt = querySdr[1].ToString();
                }
                querySdr.Close();

                //根据bgapn与mb_brief 结合查询L1的位置
                cmd.CommandText = "select L1 from " + tableName + " where material_mpn ='" + this.BGAPNtextBox.Text.Trim() + "' and mb_brief ='" + this.mb_brieftextBox.Text.Trim()+"'";
                querySdr = cmd.ExecuteReader();
                string l1 = "";
                while (querySdr.Read())
                {
                    l1 = querySdr[0].ToString();
                    if (l1 != "")
                    {
                        this.BGA_placetextBox.Text = l1;
                        break;
                    }
                }
                querySdr.Close();


                this.BGAPNtextBox.Text = bga_mpn_txt;
                this.bga_brieftextBox.Text = bga_brief_txt;

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void add_Click(object sender, EventArgs e)
        {
            if (this.track_serial_noTextBox.Text == ""
                || this.statusComboBox.Text == ""
                || this.mpntextBox.Text == ""
                || this.BGAPNtextBox.Text == ""
                //|| this.BGA_placetextBox.Text == ""
                || this.bga_brieftextBox.Text == ""
                || this.repairertextBox.Text == ""
                || this.repair_datetextBox.Text == "")
            {
                MessageBox.Show("输入的内容有空，请检查！");
                return;
            }
            if (this.VGA.Checked == false && this.CPU.Checked == false && this.PCH.Checked == false)
            {
                MessageBox.Show("VGA,CPU, PCH 必须选择一个！");
                return;
            }

            if (this.BGA_placetextBox.Text == "")
            {
                MessageBox.Show("BGA位置必须输入信息!");
                return;
            }

            if (this.BGAPNtextBox.Text.Trim().Length != 11)
            {
                MessageBox.Show("BGA PN 内容不对，请检查！");
                return;
            }

            string chooseBGA = "";
            if (this.VGA.Checked)
            {
                chooseBGA = "VGA";
            }
            else if (this.CPU.Checked)
            {
                chooseBGA = "CPU";
            }
            else if (this.PCH.Checked)
            {
                chooseBGA = "PCH";
            }

            DialogResult dr = MessageBox.Show("你确定选择的是[" + chooseBGA+"]", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            bool error = false;
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();

            string status = this.statusComboBox.Text.Trim();

            string bgaType = "";
            if (this.CPU.Checked)
            {
                bgaType = "CPU";
            }
            else if (this.VGA.Checked)
            {
                bgaType = "VGA";
            }
            else if (this.PCH.Checked)
            {
                bgaType = "PCH";
            }

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
           
            string mbfa1rich_txt = this.mbfa1richTextBox.Text.Trim();
            string short_cut_txt = getShortCutText();           
           
            string ECO_txt = this.ECOtextBox.Text.Trim();          
            string BGAPN_txt = this.BGAPNtextBox.Text.Trim();
            string BGA_place_txt = this.BGA_placetextBox.Text.Trim();
            string bga_brief_txt = this.bga_brieftextBox.Text.Trim();
           
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

                    //事先查询板子的状态，1.序列号不存在，则可以查入 2.若之前的记录是在（BGA更换不良/BGA更换OK待测）也可以插入 其他情况不可以
                    cmd.CommandText = "select top 1 _status from bga_wait_record_table where track_serial_no='" + track_serial_no_txt + "' order by Id desc";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string previousStatus = "";
                    while (querySdr.Read())
                    {
                        previousStatus = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (previousStatus == "")
                    {
                        if (this.statusComboBox.Text != "BGA不良")
                        {
                            error = true;
                            MessageBox.Show("状态输入框错误，之前是:" + previousStatus);
                            conn.Close();
                            return;
                        }
                    }
                    else if (previousStatus == "BGA不良")
                    {
                        if (this.statusComboBox.Text == "BGA不良")
                        {
                            error = true;
                            MessageBox.Show("状态输入框错误，之前是:" + previousStatus);
                            conn.Close();
                            return;
                        }
                        else//其他三种情况（BGA更换不良、BGA更换OK待测、BGA更换报废)，查询BGA的维修记录
                        {
                            cmd.CommandText = "select top 1 bga_repair_result from bga_repair_record_table where track_serial_no='" + track_serial_no_txt + "' order by Id desc";
                            querySdr = cmd.ExecuteReader();
                            string bgarepairStatus = "";
                            while (querySdr.Read())
                            {
                                bgarepairStatus = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            if (bgarepairStatus == "更换OK待测量" || bgarepairStatus == "更换NG")
                            {
                                error = false;
                            }
                            else
                            {
                                error = true;
                                MessageBox.Show("BGA站别维修状态不对！现在是:" + bgarepairStatus);
                                conn.Close();
                                return;
                            }
                        }
                    }
                    else //previousStatus其他三种情况（BGA更换不良、BGA更换OK待测、BGA更换报废)，查询BGA的维修记录
                    {
                        if (this.statusComboBox.Text != "BGA不良")
                        {
                            error = true;
                            MessageBox.Show("状态输入框错误，之前是:" + previousStatus);
                            conn.Close();
                            return;
                        }                       
                    }

                    if (error == false)
                    {
                        cmd.CommandText = "INSERT INTO bga_wait_record_table VALUES('"
                           + track_serial_no_txt + "','"
                           + status + "','"
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
                           + mbfa1rich_txt + "','"
                           + short_cut_txt + "','"
                           + bgaType + "','"
                           + BGAPN_txt + "','"
                           + BGA_place_txt + "','"
                           + bga_brief_txt + "','"
                           + repairer_txt + "','"
                           + repair_date_txt + "','"
                           + "0" + "')";

                        cmd.ExecuteNonQuery();

                        string stationInfo = "BGA";
                        if (status == "BGA更换OK待测" || status == "BGA更换报废")
                        {
                            stationInfo = "维修";
                        }

                        cmd.CommandText = "update stationInformation set station = '" + stationInfo + "', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                      + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";

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
                this.statusComboBox.SelectedIndex = -1;
                
                this.mpntextBox.Text = "";                
                
                this.BGAPNtextBox.Text = "";
                this.BGA_placetextBox.Text = "";
                this.bga_brieftextBox.Text = "";

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

                this.mbfa1richTextBox.Text = "";

                this.ECOtextBox.Text = "";
                this.BGAPNtextBox.Text = "";
                this.BGA_placetextBox.Text = "";
                this.bga_brieftextBox.Text = "";

                //this.repairertextBox.Text = "";
                this.repair_datetextBox.Text = "";
               
                //this.repairertextBox.Text = "";
                this.repair_datetextBox.Text = "";

                this.VGA.Checked = false;
                this.CPU.Checked = false;
                this.PCH.Checked = false;

                uncheckShortCut();

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

                string sqlStr = "select  top 20 * from bga_wait_record_table";

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

            string[] hTxt = {"ID", "跟踪条码", "BGA状态","厂商","客户别","来源","订单编号",
                             "收货日期","MB描述","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","ECO","FA分析","短路电压", "BGA类型", "BGAPN","BGA位置","BGA简述","录入人", "录入日期","当前次数"};
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
            tableLayoutPanel4.GetType().
               GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
               SetValue(tableLayoutPanel4, true, null);
        }

        private void checkPlace()
        {
            if (this.vendorTextBox.Text == "" || this.mb_brieftextBox.Text == "")
            {
                MessageBox.Show("请先输入条形码！");
                return;
            }

            if (this.BGA_placetextBox.Text == "")
            {
                MessageBox.Show("请先输入BGA位置！");
                return;
            }

            string tableName = "";
            tableName = Constlist.table_name_LCFC_MBBOM;

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select L1 from " + tableName + " where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "' and vendor='" + this.vendorTextBox.Text.Trim() + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                string not_good_place = this.BGA_placetextBox.Text.Trim();
                bool exist = false;
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                    {
                        exist = true;
                        break;
                    }
                }
                querySdr.Close();

                if (exist == false)
                {
                    this.BGA_placetextBox.Focus();
                    this.BGA_placetextBox.SelectAll();
                    MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BGA_placetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                checkPlace();
            }
        }

        private void statusComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.statusComboBox.Text == "BGA不良")
            {
                this.customFaulttextBox.ReadOnly = false;
            }
            else
            {
                this.customFaulttextBox.ReadOnly = true;
            }
        }

        private void BGA_placetextBox_Leave(object sender, EventArgs e)
        {
            checkPlace();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            //在维修人员误操作后的删除动作
            if (tobedeletedId.Trim() == "")
            {
                MessageBox.Show("请选择要删除的行ID");
                return;
            }

            DialogResult dr = MessageBox.Show("你确定要删除ID 为" + tobedeletedId + "的信息吗， 一旦删除不能恢复", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
             if (dr == System.Windows.Forms.DialogResult.Cancel)
             {
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

                    cmd.CommandText = "Delete from bga_wait_record_table where Id = '" + tobedeletedId + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("删除完毕!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string tobedeletedId = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            tobedeletedId = dataGridView1.SelectedCells[0].Value.ToString();
        }
    }
}
