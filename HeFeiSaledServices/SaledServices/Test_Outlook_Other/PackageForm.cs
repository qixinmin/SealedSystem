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
    public partial class PackageForm : Form
    {
        private PrepareUseDetail mPrepareUseDetail1 = null;
        private PrepareUseDetail mPrepareUseDetail2 = null;
        private PrepareUseDetail mPrepareUseDetail3 = null;

        private String tableName = "Packagetable";
        public PackageForm()
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

                    if (station == "TakePhoto" || station == "Obe")//station == "外观" || station == "Obe"
                    {
                        cmd.CommandText = "select custommaterialNo from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        string customMaterialNo = "";

                        while (querySdr.Read())
                        {
                            customMaterialNo = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (customMaterialNo != "")
                        {

                            //如果在表中有抽检则需查询obe站别是否ok，否则不能走包装站别，有一个问题，如果第一次fail，第二次可以不走obe，如何判断？
                            cmd.CommandText = "select ischeck from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            querySdr = cmd.ExecuteReader();
                            string ischeck = "";

                            while (querySdr.Read())
                            {
                                ischeck = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            if (ischeck == "True")
                            {
                                cmd.CommandText = "select checkresult from ObeStationtable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                                querySdr = cmd.ExecuteReader();
                                string checkresult = "";

                                while (querySdr.Read())
                                {
                                    checkresult = querySdr[0].ToString();
                                }
                                querySdr.Close();
                                if (checkresult == "" || checkresult != "P")
                                {
                                    MessageBox.Show("追踪条码的内容在OBE站别中，没有检查结果！");
                                    this.confirmbutton.Enabled = false;
                                }
                                else
                                {
                                    this.confirmbutton.Enabled = true;
                                }
                            }
                            else
                            {
                                this.testerTextBox.Text = LoginForm.currentUser;
                                this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                this.confirmbutton.Enabled = true;
                            }
                        }
                        else
                        {
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("板子已经经过站别=" + station);
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
                        if (exist1 != "")
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

                    if (mPrepareUseDetail3 != null && mPrepareUseDetail3.material_mpn != "")
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

                    //外观做完自动出良品库，同时更新良品库的数量
                    cmd.CommandText = "select custommaterialNo,custom_order,custom_serial_no from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "",custom_order="",custom_serial_no="";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                        custom_order = querySdr[1].ToString();
                        custom_serial_no = querySdr[2].ToString();
                    }
                    querySdr.Close();

                    if (customMaterialNo == "")
                    {
                        MessageBox.Show("客户料号不能为空，请检查序列号是否正确!");
                        conn.Close();
                        return;
                    }

                    //防止重复入库
                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string Id = "";
                    while (querySdr.Read())
                    {
                        Id = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (Id != "")
                    {
                        MessageBox.Show("此序列号已经存在！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }

                    bool isUsedMaterial = false;

                    if (mPrepareUseDetail1 != null && mPrepareUseDetail1.Id != null && mPrepareUseDetail1.material_mpn != "")
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

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                             "','Package','OK','" + DateTime.Now.ToString() + "','"
                             + mPrepareUseDetail1.stock_place + "','"
                             + mPrepareUseDetail1.thisUseNumber + "','"
                             + mPrepareUseDetail1.material_mpn + "','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        isUsedMaterial = true;

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

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                             "','Package','OK','" + DateTime.Now.ToString() + "','"
                             + mPrepareUseDetail2.stock_place + "','"
                             + mPrepareUseDetail2.thisUseNumber + "','"
                             + mPrepareUseDetail2.material_mpn + "','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        isUsedMaterial = true;

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

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                             "','Package','OK','" + DateTime.Now.ToString() + "','"
                             + mPrepareUseDetail3.stock_place + "','"
                             + mPrepareUseDetail3.thisUseNumber + "','"
                             + mPrepareUseDetail3.material_mpn + "','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        isUsedMaterial = true;

                        //更新预领料表的数量
                        cmd.CommandText = "update request_fru_smt_to_store_table set usedNumber = '" + mPrepareUseDetail3.totalUseNumber + "' "
                                  + "where Id = '" + mPrepareUseDetail3.Id + "'";
                        cmd.ExecuteNonQuery();

                        //使用完毕需要清空
                        mPrepareUseDetail3.Id = null;
                    }

                    if (isUsedMaterial == false)
                    {
                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                             "','Package','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();              

                    cmd.CommandText = "update stationInformation set station = 'Package', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO repaired_in_house_table VALUES('" +
                       this.tracker_bar_textBox.Text.Trim() + "','" +
                       customMaterialNo + "','" +
                       "1" + "','" +
                       DateTime.Now.ToString("yyyy/MM/dd") + "')";
                    cmd.ExecuteNonQuery();
                    //更新数量
                    cmd.CommandText = "select Id,leftNumber from repaired_left_house_table where custom_materialNo='" + customMaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    string exist = "";
                    string left_number = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                        left_number = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (exist == "")
                    {
                        cmd.CommandText = "INSERT INTO repaired_left_house_table VALUES('"
                        + customMaterialNo + "','"
                        + "1" + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        try
                        {
                            int totalLeft = Int32.Parse(left_number);
                            int thistotal = totalLeft + 1;
                            cmd.CommandText = "update repaired_left_house_table set leftNumber = '" + thistotal + "'"
                                   + "where custom_materialNo = '" + customMaterialNo + "'";
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    querySdr.Close();

                    //查询是否有2此NTF，如果有进入锁定表格
                    cmd.CommandText = "select _action,orderno,repair_result,repair_date,fault_describe,software_update from repair_record_table where custom_serial_no ='" + custom_serial_no.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    int ntfcount = 0;
                    List<string> orderlist = new List<string>();
                    while (querySdr.Read())
                    {
                        if( querySdr[0].ToString().Trim().ToUpper() == "NTF")
                        {
                            ntfcount++;

                            if (!orderlist.Contains(querySdr[1].ToString().Trim()))
                            {
                                orderlist.Add(querySdr[1].ToString().Trim());
                            }
                        }
                    }
                    querySdr.Close();

                    if (ntfcount >= 2 && orderlist.Count >=2)
                    {
                        cmd.CommandText = "INSERT INTO need_to_lock VALUES('" +
                                    "ntf_twice" + "','" +
                                this.tracker_bar_textBox.Text.Trim() + "','" +
                                custom_order.Trim() + "','" +
                                custom_serial_no.Trim() + "','" +
                                "true" + "','" +
                                DateTime.Now.ToString("yyyy/MM/dd") +
                                "','')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("提示：因为超过2个订单的记录有2次NTF，已经锁定，继续后续操作");
                    }

                    //查找如果是2次回来，并且第二次是ntf，则锁住
                    cmd.CommandText = "select _action,orderno,repair_result,repair_date,fault_describe,software_update from repair_record_table where custom_serial_no ='" + custom_serial_no.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    ntfcount = 0;
                    orderlist = new List<string>();
                    bool secondNTF = false;
                    while (querySdr.Read())
                    {
                      //  if (querySdr[0].ToString().Trim().ToUpper() == "NTF")
                        {
                            ntfcount++;

                            if (!orderlist.Contains(querySdr[1].ToString().Trim()))
                            {
                                orderlist.Add(querySdr[1].ToString().Trim());

                                if (ntfcount > 1 && querySdr[0].ToString().Trim().ToUpper() == "NTF")
                                {
                                    secondNTF = true;
                                }
                            }
                        }
                    }
                    querySdr.Close();

                    if (secondNTF && orderlist.Count >= 2)
                    {
                        cmd.CommandText = "INSERT INTO need_to_lock VALUES('" +
                                    "second_ntf_or_more" + "','" +
                                this.tracker_bar_textBox.Text.Trim() + "','" +
                                custom_order.Trim() + "','" +
                                custom_serial_no.Trim() + "','" +
                                "true" + "','" +
                                DateTime.Now.ToString("yyyy/MM/dd") +
                                "','')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("提示：因为超过2个订单,并且第二个记录有NTF，已经锁定，继续后续操作");
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入Package数据OK");
                this.confirmbutton.Enabled = false;

                this.tracker_bar_textBox.Text = "";
                this.innerboxtextBox.Text = "";
                this.innerboxnumtextBox.Text = "";
                this.paomiantextBox.Text = "";
                this.paomiannumtextBox.Text = "";
                this.outboxtextBox.Text = "";
                this.outboxNumtextBox.Text = "";

    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool checkRepeat(string str1, string str2, string str3)
        {
            if (str1 != "" && (str1 == str2 || str1 == str3 ))
            {
                return true;
            }

            if (str2 != "" && (str2 == str3 ))
            {
                return true;
            }
            
            return false;
        }

        public void setPrepareUseDetail(string id, string mb_brief, string material_mpn, string stock_place, string thisUseNumber, string totalUseNumber, int index)
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
                    this.innerboxtextBox.Text = material_mpn;
                    this.innerboxnumtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.innerboxtextBox.Text, this.paomiantextBox.Text, this.outboxtextBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail1 = null;
                        this.innerboxtextBox.Text = "";
                        this.innerboxnumtextBox.Text = "";
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
                    this.paomiantextBox.Text = material_mpn;
                    this.paomiannumtextBox.Text = thisUseNumber;
                   if (checkRepeat(this.innerboxtextBox.Text, this.paomiantextBox.Text, this.outboxtextBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail2 = null;
                        this.paomiantextBox.Text = "";
                        this.paomiannumtextBox.Text = "";
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
                    this.outboxtextBox.Text = material_mpn;
                    this.outboxNumtextBox.Text = thisUseNumber;
                    if (checkRepeat(this.innerboxtextBox.Text, this.paomiantextBox.Text, this.outboxtextBox.Text))
                    {
                        MessageBox.Show("输入的料号有重复，请检查！！");
                        mPrepareUseDetail3 = null;
                        this.outboxtextBox.Text = "";
                        this.outboxNumtextBox.Text = "";
                        return;
                    }
                    break;             
            }
        }        

        private void chooseInnerbox_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 1;
            prepareUseList.Show();
        }

        private void choosepaomian_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 2;
            prepareUseList.Show();
        }

        private void chooseoutbox_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(this);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.fromIndex = 3;
            prepareUseList.Show();
        }      
    }
}
