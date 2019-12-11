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

            bool _3dmarkerExist = false, burnExist = false, lscExist60 = false, lscExist20 = false;
            string[] folders3dmark = Directory.GetFiles(_3dmark);
            string _3dmarkfileName = null;
            FileInfo _3dmarkFile = null;
            foreach (string file in folders3dmark)
            {
                _3dmarkfileName = Path.GetFileName(file);
               // Console.WriteLine(filename);
                if (_3dmarkfileName.Contains(custom_serial_no_temp) && _3dmarkfileName.Contains("_PASS"))
                {
                    _3dmarkerExist = true;
                    //move to backup
                    _3dmarkFile = new FileInfo(file);//移动
                   // _3dmarkFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + _3dmarkfileName);
                    break;
                }
            }

            string[] foldersburn = Directory.GetFiles(Burninlog);
            string burnfileName = null;
            FileInfo burnFile = null;
            foreach (string file in foldersburn)
            {
                burnfileName = Path.GetFileName(file);
                if (burnfileName.Contains(custom_serial_no_temp) && burnfileName.Contains("_PASS"))
                {
                    burnExist = true;
                    //move to backup
                    burnFile = new FileInfo(file);//移动
                    burnFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + burnfileName);
                    break;
                }
            }

            string[] foldersLSC20 = Directory.GetFiles(LSC20LOG);
            string lsc20name = null;
            FileInfo lsc20File = null;
            foreach (string file in foldersLSC20)
            {
                lsc20name = Path.GetFileName(file);
                if (lsc20name.Contains(custom_serial_no_temp) && lsc20name.Contains("_PASS"))
                {
                    lscExist20 = true;
                    //move to backup
                    lsc20File = new FileInfo(file);//移动
                   // lsc20File.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + lsc20name);
                    break;
                }
            }

            string[] foldersLSC60 = Directory.GetFiles(LSC60LOG);
            string lsc60name = null;
            FileInfo lsc60File = null;
            foreach (string file in foldersLSC60)
            {
                lsc60name = Path.GetFileName(file);
                if (lsc60name.Contains(custom_serial_no_temp) && lsc60name.Contains("_PASS"))
                {
                    lscExist60 = true;
                    //move to backup
                    lsc60File = new FileInfo(file);//移动
                    //lsc60File.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + lsc60name);
                    break;
                }
            }

            if (_3dmarkerExist == false && burnExist == false)
            {
                MessageBox.Show("_3dmarker 与Burning都不存在，请检查！");
                return false;
            }
            
            if (lscExist20 == false && lscExist60 == false)
            {
                MessageBox.Show("LSC内容为空，请检查！");
                return false;
            }

            if(_3dmarkerExist)
            {
                _3dmarkFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + _3dmarkfileName);
            }
            if (burnExist)
            {
                burnFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + burnfileName);
            }
            if (lscExist20)
            {
                lsc20File.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + lsc20name);
            }
            if (lscExist60)
            {
                lsc60File.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + lsc60name);
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
            string _3dmarkfileName = null;
            FileInfo _3dmarkFile = null;
            foreach (string file in folders3dmark)
            {
                _3dmarkfileName = Path.GetFileName(file);
               // Console.WriteLine(filename);
                if (_3dmarkfileName.Contains(custom_serial_no_temp) && _3dmarkfileName.Contains("_PASS"))
                {
                    _3dmarkerExist = true;
                    //move to backup
                    _3dmarkFile = new FileInfo(file);//移动
                   // _3dmarkFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + _3dmarkfileName);
                    break;
                }
            }

            if (_3dmarkerExist == false)
            {
                MessageBox.Show("_3dmarker 不存在，请检查！");
                return false;
            }

            if (_3dmarkerExist)
            {
                _3dmarkFile.MoveTo(_3dmarkbackup + DateTime.Now.ToString("yyyyMMddHHmmss") + _3dmarkfileName);
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


                    //检测时间间隔，如果少于规定时间则不能通过
                    bool isPassTimeSpan = false;
                    int timeSpan = 1;
                    //需要加入RR，NTF(NOT_NTF)， BGA维修记录， 不稳定的为四个小时，否则一个小时
                    cmd.CommandText = "select Id from mb_repair_status_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' and feature ='RR' or feature='BGA'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string exist = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (exist != "")
                    {
                        //提示四个小时-》改为2个小时
                        //MessageBox.Show("此主板需要运行    2个小时");
                        timeSpan = 2;
                    }
                    else//如果上面找到记录，则下面就不用再找一遍了，否在判断NTF
                    {
                        //NTF比较特殊， 可能有多条NTF记录，所以只查询NOT_NTF,只有有就认为不是NTF
                        cmd.CommandText = "select Id from mb_repair_status_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' and feature ='NOT_NTF'";
                        querySdr = cmd.ExecuteReader();
                        exist = "";
                        while (querySdr.Read())
                        {
                            exist = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        if (exist != "")
                        {
                            //提示1个小时
                            //MessageBox.Show("此主板需要运行   1个小时");
                            timeSpan = 1;
                        }
                        else
                        {
                            //MessageBox.Show("此主板需要运行   2个小时");
                            timeSpan = 2;
                        }
                    }

                 


                    if (this.tracker_bar_textBox.Text.Trim() != "")
                    {
                        cmd.CommandText = "select storehouse,custom_serial_no,product from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        SqlDataReader querySdr1 = cmd.ExecuteReader();
                        string storehouse = "", custom_serial_no = "", product = "";

                        while (querySdr1.Read())
                        {
                            storehouse = querySdr1[0].ToString();
                            custom_serial_no = querySdr1[1].ToString();
                            product = querySdr1[2].ToString().Trim();
                        }
                        querySdr1.Close();

                        if (storehouse.ToUpper() != "CN")//只检查国外的产品
                        {
                            //计算当前时间与上一个test的时间差
                            isPassTimeSpan = Untils.isTimeSpanRight(timeSpan, this.tracker_bar_textBox.Text.Trim());
                            if (!isPassTimeSpan)
                            {
                                MessageBox.Show("此主板需要运行   " + timeSpan + "个小时,但是时间间隔不够");
                                return;
                            }

                        }

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

                    //end时间间隔检测
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
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
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
