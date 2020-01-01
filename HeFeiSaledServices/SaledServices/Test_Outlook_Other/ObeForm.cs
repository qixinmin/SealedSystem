using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SaledServices.Test_Outlook
{
    public partial class ObeForm : Form
    {
        private String tableName = "Obetable";
        public ObeForm()
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
                    if (station != "TakePhoto")
                    {
                       

                        //if (customMaterialNo != "")
                        {
                            MessageBox.Show("板子还没有经过外观站别, 现在在="+station);
                            
                            this.confirmbutton.Enabled = false;
                            this.button1.Enabled = false;
                        }

                        //else
                        //{
                        //    this.tracker_bar_textBox.Focus();
                        //    this.tracker_bar_textBox.SelectAll();
                        //    MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        //}
                    }
                    else 
                    { 
                        //判断是否必须走obe站别
                        cmd.CommandText = "select Id from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        string obecheckexist = "";
                        while (querySdr.Read())
                        {
                            obecheckexist = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (obecheckexist == "")
                        {
                            MessageBox.Show("此板子不是必须过obe站别");

                            this.confirmbutton.Enabled = false;
                            this.button1.Enabled = false;
                        }
                        else
                        {
                            this.confirmbutton.Enabled = true;
                            this.button1.Enabled = true;
                        }
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private bool isCheckFromServer(String custom_serial_no, String storehouse)
        { //根据服务器要判断内容是否存在即可，找到要移动走，现在并发欠考虑
            string custom_serial_no_temp = custom_serial_no;// this.tracker_bar_textBox.Text.Trim();
            //根据网络状态查询状态
            string serverIPaddress = "\\\\192.168.1.1\\test\\Testlog\\";// +Environment.MachineName + "\\E$";
            string fctlog = serverIPaddress + "OBALOG\\";

            string fctlogbackup = serverIPaddress + "backup\\OBALOG\\";
            if (storehouse.ToUpper() == "CN")//中国区
            {
                bool fctlogExistcn = false;
                string[] foldersfctlog = Directory.GetFiles(fctlog);
                foreach (string file in foldersfctlog)
                {
                    string filename = Path.GetFileName(file);
                    //Console.WriteLine(filename);
                    if (filename.Contains(custom_serial_no_temp))
                    {
                        fctlogExistcn = true;
                        //move to backup
                        FileInfo myfile = new FileInfo(file);//移动
                        myfile.MoveTo(fctlogbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                        break;
                    }
                }

                if (fctlogExistcn == false)
                {
                    MessageBox.Show("OBA LOG 内容为空，请检查！");
                    return false;
                }
            }
            else//国外的板子判断，路径不一样
            {
                bool fctlogexist = false, lsclogexist = false;
                FileInfo fctfile = null, lscfile = null;
                string fctfilename = "", lscfilename = "";
                string fctpath = fctlog + "FCT\\", lscpath = fctlog + "LSC\\";

                string[] foldersfctlog = Directory.GetFiles(fctpath);
                foreach (string file in foldersfctlog)
                {
                    fctfilename = Path.GetFileName(file);
                    //Console.WriteLine(filename);
                    if (fctfilename.Contains(custom_serial_no_temp))
                    {
                        fctlogexist = true;
                        //move to backup
                         fctfile = new FileInfo(file);//移动
                       // myfile.MoveTo(fctlogbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                        break;
                    }
                }

                string[] folderslsclog = Directory.GetFiles(lscpath);
                foreach (string file in folderslsclog)
                {
                    lscfilename = Path.GetFileName(file);
                    //Console.WriteLine(filename);
                    if (lscfilename.Contains(custom_serial_no_temp))
                    {
                        lsclogexist = true;
                        //move to backup
                        lscfile = new FileInfo(file);//移动
                        // myfile.MoveTo(fctlogbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                        break;
                    }
                }

                if (fctlogexist == false || lsclogexist == false)
                {
                    MessageBox.Show("OBA LOG 内容为空，请检查！");
                    return false;
                }

                if (lsclogexist)
                {
                    lscfile.MoveTo(fctlogbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + lscfilename);
                }

                if (fctlogexist)
                {
                    fctfile.MoveTo(fctlogbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + fctfilename);
                }

            }
            return true;
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
                    //    MessageBox.Show("此序列号已经存在！");
                    //    this.tracker_bar_textBox.Text = "";
                    //    conn.Close();
                    //    return;
                    //}

                    //查询要检查的类型
                    //查询板子类型
                    cmd.CommandText = "select mb_brief,custom_serial_no,storehouse from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string mb_brief = "", custom_serial_no = "", storehouse="";
                    while (querySdr.Read())
                    {
                        mb_brief = querySdr[0].ToString();
                        custom_serial_no = querySdr[1].ToString();
                        storehouse = querySdr[2].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select mbbrief from obecheckmbbrief";
                    querySdr = cmd.ExecuteReader();
                    bool ischecktest = false;
                    while (querySdr.Read())
                    {
                        if (querySdr[0].ToString().ToUpper().Trim().Equals(mb_brief.ToUpper()))
                        {
                            ischecktest = true;
                        }
                    }
                    querySdr.Close();

                    if (ischecktest)
                    {
                        if (isCheckFromServer(custom_serial_no, storehouse) == false)
                        {
                            MessageBox.Show("obe服务端读取文件失败。。。");
                            conn.Close();
                            return;
                        }
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                 "','OBE','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Obe', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select orderno,custom_materialNo from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                     querySdr = cmd.ExecuteReader();
                    string orderno = "", custom_materialNo="";
                    while (querySdr.Read())
                    {
                        orderno = querySdr[0].ToString();
                        custom_materialNo = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    //插入obe站别信息
                    cmd.CommandText = "INSERT INTO ObeStationtable VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + orderno + "','"
                       + custom_materialNo + "','"
                       + "P','"// checkresult P- Pass
                       + "','"//failreason empty
                       + this.testerTextBox.Text.Trim() + "','"
                       + this.testdatetextBox.Text.Trim()
                       + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入OBE数据OK");
                this.confirmbutton.Enabled = false;
                this.button1.Enabled = false;
                this.tracker_bar_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
                return;
            }

            if (this.failtextBox.Text.Trim() == "")
            {
                MessageBox.Show("失败的原因为空，请检查！");
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

                    //防止重复入库
                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
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

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                 "','OBE','FAIL','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "select orderno,custom_materialNo from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string orderno = "", custom_materialNo = "";
                    while (querySdr.Read())
                    {
                        orderno = querySdr[0].ToString();
                        custom_materialNo = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    //插入obe站别信息
                    cmd.CommandText = "INSERT INTO ObeStationtable VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + orderno + "','"
                       + custom_materialNo + "','"
                       + "F','"// checkresult F fail
                       + this.failtextBox.Text.Trim() +"','"//failreason non empty
                       + this.testerTextBox.Text.Trim() + "','"
                       + this.testdatetextBox.Text.Trim()
                       + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入OBE Fail数据, 現在需要把板子給維修人員");
                this.failtextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
