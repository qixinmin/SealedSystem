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

namespace SaledServices.Test_Outlook
{
    public partial class OutLookForm : Form
    {
        private PrepareUseDetail mPrepareUseDetail1 = null;
        private PrepareUseDetail mPrepareUseDetail2 = null;
        private PrepareUseDetail mPrepareUseDetail3 = null;
        private PrepareUseDetail mPrepareUseDetail4 = null;
        private PrepareUseDetail mPrepareUseDetail5 = null;

        private String tableName = "outlookcheck";
        public OutLookForm()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.tracker_bar_textBox.Focus();
        }

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {
                    if (Untils.isTimeError(testdatetextBox.Text.Trim()))
                    {
                        this.confirmbutton.Enabled = false;
                    }

                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "外观" && station != "Running")//等于外观怕mylar一个页面的数据插入不够，此时不能拦截
                    {
                        MessageBox.Show("板子已经经过站别" + station);
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        this.confirmbutton.Enabled = true;
                        this.button1.Enabled = true;
                    }

                    //cmd.CommandText = "select track_serial_no from outlookcheck where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    //SqlDataReader querySdr = cmd.ExecuteReader();
                    //string track_serial_no = "";
                    //while (querySdr.Read())
                    //{
                    //    track_serial_no = querySdr[0].ToString();
                    //}
                    //querySdr.Close();

                    //if (track_serial_no != "")
                    //{
                    //    MessageBox.Show("追踪条码的内容已经在外观站别了，请检查！");
                    //    mConn.Close();
                    //    return;
                    //}

                    cmd.CommandText = "select track_serial_no from Runningtable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string track_serial_no = "";                   
                    while (querySdr.Read())
                    {
                        track_serial_no = querySdr[0].ToString();                       
                    }
                    querySdr.Close();

                    if (track_serial_no == "")
                    {
                        MessageBox.Show("追踪条码的不在Running表中，请检查！");
                        mConn.Close();
                        return;
                    }
                    
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void confirmbutton_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
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

                    //检查物料是不是今天是不是已经输入进去了，如果有，同一天同一个板子不允许有同样的物料进去
                    if (mPrepareUseDetail1 != null && mPrepareUseDetail1.material_mpn != "")
                    {
                        cmd.CommandText = "select Id from fru_smt_used_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim()
                            + "' and material_mpn='" + mPrepareUseDetail1.material_mpn + "' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string exist1 = "";
                        while (querySdr1.Read())
                        {
                            exist1 = querySdr1[0].ToString();
                        }
                        querySdr1.Close();
                        if(exist1 != "")
                        {
                            MessageBox.Show("1此物料" + mPrepareUseDetail1.material_mpn + "对应本板子" + this.tracker_bar_textBox.Text.Trim() + "已经在今天使用过了！");
                            conn.Close();
                            return;
                        }
                    }

                    if (mPrepareUseDetail2 != null && mPrepareUseDetail2.material_mpn != "")
                    {
                        cmd.CommandText = "select Id from fru_smt_used_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim()
                            + "' and material_mpn='" + mPrepareUseDetail2.material_mpn + "' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string exist1 = "";
                        while (querySdr1.Read())
                        {
                            exist1 = querySdr1[0].ToString();
                        }
                        querySdr1.Close();
                        if (exist1 != "")
                        {
                            MessageBox.Show("2此物料" + mPrepareUseDetail2.material_mpn + "对应本板子" + this.tracker_bar_textBox.Text.Trim() + "已经在今天使用过了！");
                            conn.Close();
                            return;
                        }
                    }

                    if (mPrepareUseDetail3 != null && mPrepareUseDetail3.material_mpn !="")
                    {
                        cmd.CommandText = "select Id from fru_smt_used_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim()
                            + "' and material_mpn='" + mPrepareUseDetail3.material_mpn + "' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string exist1 = "";
                        while (querySdr1.Read())
                        {
                            exist1 = querySdr1[0].ToString();
                        }
                        querySdr1.Close();
                        if (exist1 != "")
                        {
                            MessageBox.Show("3此物料" + mPrepareUseDetail3.material_mpn + "对应本板子" + this.tracker_bar_textBox.Text.Trim() + "已经在今天使用过了！");
                            conn.Close();
                            return;
                        }
                    }

                    if (mPrepareUseDetail4 != null && mPrepareUseDetail4.material_mpn != "")
                    {
                        cmd.CommandText = "select Id from fru_smt_used_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim()
                            + "' and material_mpn='" + mPrepareUseDetail4.material_mpn + "' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string exist1 = "";
                        while (querySdr1.Read())
                        {
                            exist1 = querySdr1[0].ToString();
                        }
                        querySdr1.Close();
                        if (exist1 != "")
                        {
                            MessageBox.Show("4此物料" + mPrepareUseDetail4.material_mpn + "对应本板子" + this.tracker_bar_textBox.Text.Trim() + "已经在今天使用过了！");
                            conn.Close();
                            return;
                        }
                    }

                    if (mPrepareUseDetail5 != null && mPrepareUseDetail5.material_mpn !="")
                    {
                        cmd.CommandText = "select Id from fru_smt_used_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim()
                            + "' and material_mpn='" + mPrepareUseDetail5.material_mpn + "' and input_date between '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string exist1 = "";
                        while (querySdr1.Read())
                        {
                            exist1 = querySdr1[0].ToString();
                        }
                        querySdr1.Close();
                        if (exist1 != "")
                        {
                            MessageBox.Show("5此物料" + mPrepareUseDetail5.material_mpn + "对应本板子" + this.tracker_bar_textBox.Text.Trim() + "已经在今天使用过了！");
                            conn.Close();
                            return;
                        }
                    }

                    //外观信息有可能录入多次，所以不用检查记录是否存在
                    //cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    //SqlDataReader querySdr = cmd.ExecuteReader();
                    //string Id = "";
                    //while (querySdr.Read())
                    //{
                    //    Id = querySdr[0].ToString();
                    //}
                    //querySdr.Close();
                    //if (Id != "")
                    //{
                    //    //MessageBox.Show("此序列号已经存在！");
                    //    this.tracker_bar_textBox.Text = "";
                    //    conn.Close();
                    //    return;
                    //}
                    //else
                    {
                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + this.testerTextBox.Text.Trim() + "','"
                       + this.testdatetextBox.Text.Trim()
                       + "')";

                        cmd.ExecuteNonQuery();

                    }

                    if (mPrepareUseDetail1 != null && mPrepareUseDetail1.Id != null)
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + this.testerTextBox.Text.Trim() + "','"
                           + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                           + this.tracker_bar_textBox.Text.Trim() + "','"
                           + mPrepareUseDetail1.material_mpn + "','"
                           + mPrepareUseDetail1.thisUseNumber + "','"
                           + mPrepareUseDetail1.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail1.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail1.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail1.Id = null;
                    }

                    if (mPrepareUseDetail2 != null && mPrepareUseDetail2.Id != null)
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + this.testerTextBox.Text.Trim() + "','"
                           + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                           + this.tracker_bar_textBox.Text.Trim() + "','"
                           + mPrepareUseDetail2.material_mpn + "','"
                           + mPrepareUseDetail2.thisUseNumber + "','"
                           + mPrepareUseDetail2.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail2.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail2.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail2.Id = null;
                    }

                    if (mPrepareUseDetail3 != null && mPrepareUseDetail3.Id != null)
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + this.testerTextBox.Text.Trim() + "','"
                           + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                           + this.tracker_bar_textBox.Text.Trim() + "','"
                           + mPrepareUseDetail3.material_mpn + "','"
                           + mPrepareUseDetail3.thisUseNumber + "','"
                           + mPrepareUseDetail3.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail3.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail3.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail3.Id = null;
                    }

                    if (mPrepareUseDetail4 != null && mPrepareUseDetail4.Id != null)
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + this.testerTextBox.Text.Trim() + "','"
                           + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                           + this.tracker_bar_textBox.Text.Trim() + "','"
                           + mPrepareUseDetail4.material_mpn + "','"
                           + mPrepareUseDetail4.thisUseNumber + "','"
                           + mPrepareUseDetail4.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail4.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail4.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail4.Id = null;
                    }

                    if (mPrepareUseDetail5 != null && mPrepareUseDetail5.Id != null)
                    {
                        //根据预先领料，然后生成frm/smt消耗记录，在新表fru_smt_used_record中
                        cmd.CommandText = "INSERT INTO fru_smt_used_record VALUES('"
                           + this.testerTextBox.Text.Trim() + "','"
                           + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                           + this.tracker_bar_textBox.Text.Trim() + "','"
                           + mPrepareUseDetail5.material_mpn + "','"
                           + mPrepareUseDetail5.thisUseNumber + "','"
                           + mPrepareUseDetail5.stock_place + "')";
                        cmd.ExecuteNonQuery();

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail5.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail5.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail5.Id = null;
                    }

                    //跟新站别
                    cmd.CommandText = "update stationInformation set station = '外观', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                         + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入外观站数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //this.Close();

            //清除内容
            this.tracker_bar_textBox.Text = "";

            this.mylar1textBox.Text = "";
            this.mylar2textBox.Text = "";
            this.mylar3textBox.Text = "";
            this.mylar4textBox.Text = "";
            this.mylar5textBox.Text = "";

            this.mylar1numtextBox.Text = "";
            this.mylar2numtextBox.Text = "";
            this.mylar3numtextBox.Text = "";
            this.mylar4numtextBox.Text = "";
            this.mylar5numtextBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
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

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入外观 Fail数据, 現在需要把板子給維修人員");
                this.tracker_bar_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool checkRepeat(string str1, string str2, string str3, string str4, string str5)
        {
            if(str1 != "" &&(str1 == str2 || str1 == str3 || str1 == str4 || str1 == str5))
            {
                return true;
            }

            if (str2 != "" &&(str2 == str3 || str2 == str4 || str2 == str5))
            {
                return true;
            }

            if (str3 != "" &&(str3 == str4 || str3 == str5))
            {
                return true;
            }

            if (str4 != "" &&(str4 == str5))
            {
                return true;
            }

            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 1;
            prepareUseList.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 2;
            prepareUseList.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 3;
            prepareUseList.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 4;
            prepareUseList.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 5;
            prepareUseList.Show();
        }

        public void setPrepareUseDetail(string id, string mb_brief, string material_mpn, string stock_place, string thisUseNumber, string totalUseNumber,int index)
        {
            switch (index)
            {
                case 1:
                    mPrepareUseDetail1 = new PrepareUseDetail();
                    mPrepareUseDetail1.Id = id;
                    mPrepareUseDetail1.mb_brief = mb_brief;
                    mPrepareUseDetail1.material_mpn = material_mpn;
                    mPrepareUseDetail1.stock_place = stock_place;
                    mPrepareUseDetail1.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail1.totalUseNumber = totalUseNumber;
                    this.mylar1textBox.Text = material_mpn;
                    this.mylar1numtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.mylar1textBox.Text, this.mylar2textBox.Text, this.mylar3textBox.Text, this.mylar4textBox.Text, this.mylar5textBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail1 = null;
                        this.mylar1textBox.Text = "";
                        this.mylar1numtextBox.Text = "";
                        return;
                    }
                   
                    break;
                case 2:
                    mPrepareUseDetail2 = new PrepareUseDetail();
                    mPrepareUseDetail2.Id = id;
                    mPrepareUseDetail2.mb_brief = mb_brief;
                    mPrepareUseDetail2.material_mpn = material_mpn;
                    mPrepareUseDetail2.stock_place = stock_place;
                    mPrepareUseDetail2.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail2.totalUseNumber = totalUseNumber;
                    this.mylar2textBox.Text = material_mpn;
                    this.mylar2numtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.mylar1textBox.Text, this.mylar2textBox.Text, this.mylar3textBox.Text, this.mylar4textBox.Text, this.mylar5textBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail2 = null;
                        this.mylar2textBox.Text = "";
                        this.mylar2numtextBox.Text = "";
                        return;
                    }
                    
                    break;
                case 3:
                    mPrepareUseDetail3 = new PrepareUseDetail();
                    mPrepareUseDetail3.Id = id;
                    mPrepareUseDetail3.mb_brief = mb_brief;
                    mPrepareUseDetail3.material_mpn = material_mpn;
                    mPrepareUseDetail3.stock_place = stock_place;
                    mPrepareUseDetail3.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail3.totalUseNumber = totalUseNumber;
                    this.mylar3textBox.Text = material_mpn;
                    this.mylar3numtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.mylar1textBox.Text, this.mylar2textBox.Text, this.mylar3textBox.Text, this.mylar4textBox.Text, this.mylar5textBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail3 = null;
                        this.mylar3textBox.Text = "";
                        this.mylar3numtextBox.Text = "";
                        return;
                    }
                    
                    break;
                case 4: 
                    mPrepareUseDetail4 = new PrepareUseDetail();
                    mPrepareUseDetail4.Id = id;
                    mPrepareUseDetail4.mb_brief = mb_brief;
                    mPrepareUseDetail4.material_mpn = material_mpn;
                    mPrepareUseDetail4.stock_place = stock_place;
                    mPrepareUseDetail4.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail4.totalUseNumber = totalUseNumber;
                    this.mylar4textBox.Text = material_mpn;
                    this.mylar4numtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.mylar1textBox.Text, this.mylar2textBox.Text, this.mylar3textBox.Text, this.mylar4textBox.Text, this.mylar5textBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail4 = null;
                        this.mylar4textBox.Text = "";
                        this.mylar4numtextBox.Text = "";
                        return;
                    }
                    
                    break;
                case 5:
                    mPrepareUseDetail5 = new PrepareUseDetail();
                    mPrepareUseDetail5.Id = id;
                    mPrepareUseDetail5.mb_brief = mb_brief;
                    mPrepareUseDetail5.material_mpn = material_mpn;
                    mPrepareUseDetail5.stock_place = stock_place;
                    mPrepareUseDetail5.thisUseNumber = thisUseNumber;
                    mPrepareUseDetail5.totalUseNumber = totalUseNumber;
                    this.mylar5textBox.Text = material_mpn;
                    this.mylar5numtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.mylar1textBox.Text, this.mylar2textBox.Text, this.mylar3textBox.Text, this.mylar4textBox.Text, this.mylar5textBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail5 = null;
                        this.mylar5textBox.Text = "";
                        this.mylar5numtextBox.Text = "";
                        return;
                    }
                    
                    break;
            }            
        }        

        private void OutLookForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel1, true, null);
        
        }
    }
}
