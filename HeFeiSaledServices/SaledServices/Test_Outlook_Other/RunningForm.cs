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
    public partial class RunningForm : Form
    {
        private String tableName = "Runningtable";
        public RunningForm()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.tracker_bar_textBox.Focus();
            this.confirmbutton.Enabled = false;
        }

        private bool isCheckFromServer(String custom_serial_no)
        { //根据服务器要判断内容是否存在即可，找到要移动走，现在并发欠考虑
            string custom_serial_no_temp = custom_serial_no;// this.tracker_bar_textBox.Text.Trim();
            //根据网络状态查询状态
            string serverIPaddress = "\\\\192.168.1.1\\test\\";// +Environment.MachineName + "\\E$";
            string _3dmark = serverIPaddress + "3DMARKLOG\\";
            string Burninlog = serverIPaddress + "Burninlog\\";
            string LSC20LOG = serverIPaddress + "LSC20LOG\\";
            string LSC60LOG = serverIPaddress + "LSC60LOG\\";

            string _3dmarkbackup = serverIPaddress + "backup\\3DMARKLOG\\";
            string Burninlogbackup = serverIPaddress + "backup\\Burninlog\\";
            string LSC20LOGbackup = serverIPaddress + "backup\\LSC20LOG\\";
            string LSC60LOGbackup = serverIPaddress + "backup\\LSC60LOG\\";

            bool _3dmarkerExist = false, burnExist = false, lscExist = false;
            string[] folders3dmark = Directory.GetFiles(_3dmark);
            foreach (string file in folders3dmark)
            {
                string filename = Path.GetFileName(file);
                Console.WriteLine(filename);
                if (filename.Contains(custom_serial_no_temp) && filename.Contains("_PASS"))
                {
                    _3dmarkerExist = true;
                    //move to backup
                    FileInfo myfile = new FileInfo(file);//移动
                    myfile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                    break;
                }
            }

            string[] foldersburn = Directory.GetFiles(Burninlog);
            foreach (string file in foldersburn)
            {
                string filename = Path.GetFileName(file);
                if (filename.Contains(custom_serial_no_temp) && filename.Contains("_PASS"))
                {
                    burnExist = true;
                    //move to backup
                    FileInfo myfile = new FileInfo(file);//移动
                    myfile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                    break;
                }
            }

            string[] foldersLSC20 = Directory.GetFiles(LSC20LOG);
            foreach (string file in foldersLSC20)
            {
                string filename = Path.GetFileName(file);
                if (filename.Contains(custom_serial_no_temp) && filename.Contains("_PASS"))
                {
                    lscExist = true;
                    //move to backup
                    FileInfo myfile = new FileInfo(file);//移动
                    myfile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                    break;
                }
            }

            string[] foldersLSC60 = Directory.GetFiles(LSC60LOG);
            foreach (string file in foldersLSC60)
            {
                string filename = Path.GetFileName(file);
                if (filename.Contains(custom_serial_no_temp) && filename.Contains("_PASS"))
                {
                    lscExist = true;
                    //move to backup
                    FileInfo myfile = new FileInfo(file);//移动
                    myfile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                    break;
                }
            }

            if (_3dmarkerExist == false && burnExist == false)
            {
                MessageBox.Show("_3dmarker 与Burning都不存在，请检查！");
                return false;
            }

            if (lscExist == false)
            {
                MessageBox.Show("LSC内容为空，请检查！");
                return false;
            }
            return true;
        }

        private bool isCheckFromServerCN(String custom_serial_no)
        { //根据服务器要判断内容是否存在即可，找到要移动走，现在并发欠考虑
            string custom_serial_no_temp = custom_serial_no;// this.tracker_bar_textBox.Text.Trim();
            //根据网络状态查询状态
            string serverIPaddress = "\\\\192.168.1.1\\test\\";// +Environment.MachineName + "\\E$";
            string _3dmark = serverIPaddress + "3DMARKLOG\\";
            //string Burninlog = serverIPaddress + "Burninlog\\";
            //string LSC20LOG = serverIPaddress + "LSC20LOG\\";
            //string LSC60LOG = serverIPaddress + "LSC60LOG\\";

            string _3dmarkbackup = serverIPaddress + "backup\\3DMARKLOG\\";
            //string Burninlogbackup = serverIPaddress + "backup\\Burninlog\\";
            //string LSC20LOGbackup = serverIPaddress + "backup\\LSC20LOG\\";
            //string LSC60LOGbackup = serverIPaddress + "backup\\LSC60LOG\\";

            bool _3dmarkerExist = false;//, burnExist = false, lscExist = false;
            string[] folders3dmark = Directory.GetFiles(_3dmark);
            foreach (string file in folders3dmark)
            {
                string filename = Path.GetFileName(file);
                Console.WriteLine(filename);
                if (filename.Contains(custom_serial_no_temp) && filename.Contains("_PASS"))
                {
                    _3dmarkerExist = true;
                    //move to backup
                    FileInfo myfile = new FileInfo(file);//移动
                    myfile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                    break;
                }
            }

            if (_3dmarkerExist == false)
            {
                MessageBox.Show("_3dmarker 不存在，请检查！");
                return false;
            }
           
            return true;
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

                    if (station != "Test2" && station != "Test1&2")
                    {
                        MessageBox.Show("板子已经经过站别==>" + station);
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        return;
                    }

                    //if (track_serial_no == "")
                    //{
                    //    cmd.CommandText = "select track_serial_no from test2table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    //    querySdr = cmd.ExecuteReader();
                    //    string track_serial_no_test2 = "";
                    //    while (querySdr.Read())
                    //    {
                    //        track_serial_no_test2 = querySdr[0].ToString();
                    //    }
                    //    querySdr.Close();
                    //    if (track_serial_no_test2 == "")
                    //    {
                    //        MessageBox.Show("板子没有经过测试站别");
                    //        mConn.Close();
                    //        this.tracker_bar_textBox.Focus();
                    //        this.tracker_bar_textBox.SelectAll();
                    //        return;
                    //    }
                    //}


                    //移动到确定的时候操作，防止误操作
                    //if (this.tracker_bar_textBox.Text.Trim() != "")
                    //{
                    //    cmd.CommandText = "select storehouse,custom_serial_no from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    //    querySdr = cmd.ExecuteReader();
                    //    string storehouse = "", custom_serial_no="";

                    //    while (querySdr.Read())
                    //    {
                    //        storehouse = querySdr[0].ToString();
                    //        custom_serial_no = querySdr[1].ToString();
                    //    }
                    //    querySdr.Close();

                    //    if (storehouse.ToUpper().Trim() != "CN")
                    //    {
                    //        if (isCheckFromServer(custom_serial_no) == false)
                    //        {
                    //           // MessageBox.Show("服务端读取文件失败");
                    //            mConn.Close();
                    //            return;
                    //        }
                    //    }
                    //}
                    //else 
                    //{
                    //    MessageBox.Show("此追踪条码没有Test2站别的记录！");
                    //}
                    this.testerTextBox.Text = LoginForm.currentUser;
                    this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    mConn.Close();
                    this.confirmbutton.Enabled = true;
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
            if (this.testerTextBox.Text.Trim() == "")
            {
                MessageBox.Show("用户为空！请回车");
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

                    if (this.tracker_bar_textBox.Text.Trim() != "")
                    {
                        cmd.CommandText = "select storehouse,custom_serial_no,product from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string storehouse = "", custom_serial_no = "", product="";

                        while (querySdr.Read())
                        {
                            storehouse = querySdr[0].ToString();
                            custom_serial_no = querySdr[1].ToString();
                            product = querySdr[1].ToString().Trim();
                        }
                        querySdr.Close();

                        if (product != "DT")
                        {
                            if (storehouse.ToUpper().Trim() != "CN")
                            {
                                if (isCheckFromServer(custom_serial_no) == false)
                                {
                                    MessageBox.Show("not CN服务端读取文件失败");
                                    conn.Close();
                                    return;
                                }
                            }
                            else
                            {
                                if (isCheckFromServerCN(custom_serial_no) == false)
                                {
                                    MessageBox.Show("CN服务端读取文件失败");
                                    conn.Close();
                                    return;
                                }

                            }
                        }
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                   "','Running','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Running', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入Running2数据OK");
                this.testerTextBox.Text = "";
                this.confirmbutton.Enabled = true;
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
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                  "','Running','FAIL','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入Running的Fail数据, 現在需要把板子給維修人員");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
