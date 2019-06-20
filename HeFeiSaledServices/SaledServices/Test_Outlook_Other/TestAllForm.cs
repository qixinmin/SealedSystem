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
using System.Diagnostics;

namespace SaledServices.Test_Outlook
{
    public partial class TestAllForm : Form
    {
        private String tableName = "testalltable";
        public TestAllForm()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.tracker_bar_textBox.Focus();
        }

        string track_serial_no = "", product = "";
        string customMaterialNo = "", vendor_serail_no = "", mac = "", uuid = "", custom_serial_no = "", mb_brief = "";
        string KEYID = "", KEYSERIAL = "";
        string cpu_type = "", cpu_freq = "", dpk_type = "", dpkpn = "", mpn = "";

        bool existBuffer = false, existRepair = false;

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
                    //if (Untils.isTimeError(testdatetextBox.Text.Trim()))
                    //{
                    //    this.bomdownload.Enabled = false;
                    //}

                    Untils.deleteFile("D:\\fru\\", "BOM.bat");
                    Untils.deleteFile("D:\\fru\\", "BOM.NSH");
                    Untils.deleteFile("D:\\fru\\", "DPK.TXT");

                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    if (Untils.isTimeError(DateTime.Now.ToString("yyyy/MM/dd")))
                    {
                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "维修" && station != "BGA")
                    {
                        MessageBox.Show("板子已经经过站别" + station);
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        return;
                    }

                    cmd.CommandText = "select track_serial_no,product from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    track_serial_no = ""; product = "";
                    this.bomdownload.Enabled = false; this.buffertest.Enabled = false; this.isburn.Enabled = false;
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();
                        MessageBox.Show("本条形码不在维修表中，现在检查buffer表！");

                        cmd.CommandText = "select track_serial_no,product from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows == false)
                        {
                            MessageBox.Show("本条形码不在buffer出库表中，请检查！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                        else 
                        {
                            while (querySdr.Read())
                            {
                                track_serial_no = querySdr[0].ToString();
                                product = querySdr[1].ToString();
                            }
                            querySdr.Close();
                            existBuffer = true;
                            this.buffertest.Enabled = true;
                            this.isburn.Enabled = true;
                        }
                    }
                    else
                    {
                        while (querySdr.Read())
                        {
                            track_serial_no = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                        }
                        querySdr.Close();
                        this.bomdownload.Enabled = true;
                        existRepair = true;
                    }

                    if (existRepair)
                    {
                        cmd.CommandText = "select top 1 _status from bga_wait_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' order by Id desc";

                        querySdr = cmd.ExecuteReader();
                        string bgastatus = "";
                        while (querySdr.Read())
                        {
                            bgastatus = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (bgastatus == "BGA不良")
                        {
                            MessageBox.Show("BGA的维修记录不对，请检查！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                    }                    

                    if (product != "" && product != "LBG")//TBG, DT, AIO 
                    {
                        if (track_serial_no != "")
                        {
                            customMaterialNo = ""; vendor_serail_no = ""; mac = ""; uuid = ""; custom_serial_no = ""; mb_brief = "";
                            if (existRepair)
                            {
                                cmd.CommandText = "select custommaterialNo, vendor_serail_no,mac,uuid,custom_serial_no,mb_brief  from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    customMaterialNo = querySdr[0].ToString();
                                    vendor_serail_no = querySdr[1].ToString();

                                    mac = querySdr[2].ToString();
                                    uuid = querySdr[3].ToString();
                                    custom_serial_no = querySdr[4].ToString();
                                    mb_brief = querySdr[5].ToString();
                                }
                                querySdr.Close();
                            }
                            else if (existBuffer)
                            {
                                cmd.CommandText = "select custommaterialNo, vendor_serial_no,custom_serial_no,mb_brief from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    customMaterialNo = querySdr[0].ToString();
                                    vendor_serail_no = querySdr[1].ToString();

                                    custom_serial_no = querySdr[2].ToString();
                                    mb_brief = querySdr[3].ToString();
                                }
                                querySdr.Close();
                            }

                            if (customMaterialNo != "")
                            {
                                cpu_type = ""; cpu_freq = ""; dpk_type = ""; dpkpn = ""; mpn = "";
                                cmd.CommandText = "select cpu_type,cpu_freq,dpk_type,dpkpn, mpn from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                                querySdr = cmd.ExecuteReader();

                                while (querySdr.Read())
                                {
                                    cpu_type = querySdr[0].ToString();
                                    cpu_freq = querySdr[1].ToString();
                                    dpk_type = querySdr[2].ToString();
                                    dpkpn = querySdr[3].ToString();
                                    mpn = querySdr[4].ToString();
                                }
                                querySdr.Close();

                                this.cpuTypetextBox.Text = cpu_type;
                                this.cpuFreqtextBox.Text = cpu_freq;
                                this.testerTextBox.Text = LoginForm.currentUser;
                                this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                                KEYID = ""; KEYSERIAL = "";
                                if (dpk_type != "NOK" && dpkpn != "")//此时需要查找导入的dpk表格，查找对应的KEYI KEYSERIAL
                                {
                                    //首先判断这个板子有没有来过，若来过则重新拿号给他，否则去新的
                                    cmd.CommandText = "select burn_date,KEYID,KEYSERIAL from DPK_table where custom_serial_no='" + custom_serial_no + "'";
                                    querySdr = cmd.ExecuteReader();
                                    string burn_date = "";
                                    while (querySdr.Read())
                                    {
                                        burn_date = querySdr[0].ToString();
                                        KEYID = querySdr[1].ToString();
                                        KEYSERIAL = querySdr[2].ToString();
                                    }
                                    querySdr.Close();

                                    if (burn_date != "" && Untils.in90Days(burn_date)) //不为空且在90天内
                                    {
                                        //KEYID,KEYSERIAL按之前的序列号，其他内容不变
                                    }
                                    else//不存在或超过90天，则分配新的东西
                                    {
                                        cmd.CommandText = "select KEYID,KEYSERIAL,Id from DPK_table where KEYPN='" + dpkpn + "' and _status ='未使用' order by Id asc";

                                        querySdr = cmd.ExecuteReader();
                                        bool exist = false;
                                        string id = "";
                                        while (querySdr.Read())
                                        {
                                            exist = true;
                                            KEYID = querySdr[0].ToString();
                                            KEYSERIAL = querySdr[1].ToString();
                                            id = querySdr[2].ToString();
                                            break;
                                        }
                                        querySdr.Close();

                                        if (exist == false)
                                        {
                                            MessageBox.Show("此DPKPN" + dpkpn + "的序列号已经使用完毕或不存在！");
                                            mConn.Close();
                                            return;
                                        }
                                        else
                                        {
                                            //更新烧录日期与custom_serial_no与使用状态
                                            cmd.CommandText = "update DPK_table set _status = '已使用', burn_date = GETDATE(),custom_serial_no = '" + custom_serial_no + "' where Id = '" + id + "'";
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    KEYID = "NOK";
                                    KEYSERIAL = "NOK";
                                }

                                this.keyidtextBox.Text = KEYID;

                                tempKeySerial = KEYSERIAL;
                                this.KEYSERIALtextBox.Text = KEYSERIAL;
                                if (KEYID != "NOK")
                                {
                                    int lastEm = tempKeySerial.LastIndexOf('-');
                                    this.KEYSERIALtextBox.Text = "XXXXX-XXXXX-XXXXX-XXXXX-" + tempKeySerial.Substring(lastEm + 1, 5);
                                }
                            }
                            else
                            {
                                this.tracker_bar_textBox.Focus();
                                this.tracker_bar_textBox.SelectAll();
                                MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                                mConn.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("此追踪条码没有维修记录！");
                            mConn.Close();
                            return;
                        }
                    }
                    else
                    {
                        if (product == "")
                        {
                            MessageBox.Show("此追踪条码没有维修记录！");
                            mConn.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("此追踪条码对应的客户别不是TBG, DT, AIO ！");
                            mConn.Close();
                            return;
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
        string tempKeySerial = "";
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

                    //查询板子类型
                    cmd.CommandText = "select product from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string productCheck = "";
                    while (querySdr.Read())
                    {
                        productCheck = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    bool docheckExist = false;
                    if (productCheck == "TBG")
                    {
                        //do                        
                    }
                    else
                    {
                        if (this.KEYID == "NOK")
                        {
                            //do
                        }
                        else
                        {
                            //check
                            docheckExist = true;
                            //do
                        }
                    }

                    //TBG
                    if (docheckExist)
                    {
                        string generateFile = "D:\\fru\\DPK.txt";
                        if (File.Exists(generateFile) == false)
                        {
                            MessageBox.Show("D:\\fru\\DPK.txt文件不存在！");
                            conn.Close();
                            return;
                        }

                        StreamReader sr = new StreamReader(generateFile, Encoding.Default);
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //  Console.WriteLine(line.ToString());
                            break;
                        }
                        sr.Close();
                        if (tempKeySerial != "" && line != null && line.Contains(tempKeySerial))
                        {
                        }
                        else
                        {
                            MessageBox.Show("文件不存在或者DPK内容与序列号不匹配， 请重新烧录！");
                            conn.Close();
                            return;
                        }
                    }

                    //cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    //querySdr = cmd.ExecuteReader();
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
                    //    this.cpuFreqtextBox.Text = "";
                    //    this.cpuTypetextBox.Text = "";
                    //    this.keyidtextBox.Text = "";
                    //    this.KEYSERIALtextBox.Text = "";
                    //    conn.Close();
                    //    return;
                    //}
                    
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                    "','测试','OK', GETDATE() ,'','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Test1&2', updateDate = GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();


                    //需要加入RR，NTF(NOT_NTF)， BGA维修记录， 不稳定的为四个小时，否则一个小时
                    cmd.CommandText = "select Id from mb_repair_status_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' where feature ='RR' or feature='BGA'";
                    querySdr = cmd.ExecuteReader();
                    string exist = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (exist != "")
                    {
                        //提示四个小时
                        MessageBox.Show("此主板需要运行    4个小时");
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
                            MessageBox.Show("此主板需要运行   1个小时");
                        }
                        else
                        {
                            MessageBox.Show("此主板需要运行   4个小时");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试All数据OK");
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

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                   "','测试','FAIL', GETDATE() ,'','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("已插入测试2 Fail 数据, 現在需要把板子給維修人員");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KEYID == "" || KEYSERIAL == "")
            {
                MessageBox.Show("序列号还没有下载，请检查操作！");
                return;
            }

            //SET MBID=RIBM160907010247     跟踪条码
            //SET SN=1021948402900          厂商序号
            //SET SKU=45101201065           MPN
            //SET MAC=28D24475FE86          MAC
            //SET UUID=11111111111111111111111111111111  UUID
            //SET MB11S=8SSB20A29572L1HF4440062  客户序号
            //SET OA3KEY=N/A    KEYID
            //SET OA3PID=N/A    KEYSERIAL
            //SET FRUPN=04X5152  客户料号
            //SET MODELID=VIUX2  MB简称

            string tempCustomMaterialNo = "";
            if (customMaterialNo.Length == 10 && customMaterialNo.StartsWith("000"))
            {
                tempCustomMaterialNo = customMaterialNo.Substring(3);
            }
            string totalStr = "SET -v MBID " + track_serial_no + "\r\n"
                            + "SET -v SN " + vendor_serail_no + "\r\n"
                            + "SET -v SKU " + mpn + "\r\n"
                            + "SET -v MAC " + mac + "\r\n"
                            + "SET -v UUID " + uuid + "\r\n"
                            + "SET -v MB11S " + custom_serial_no + "\r\n"
                            + "SET -v OA3KEY " + KEYSERIAL + "\r\n"
                            + "SET -v OA3PID " + KEYID + "\r\n"
                            + "SET -v DPKNO " + KEYSERIAL + "\r\n"
                            + "SET -v DPKID " + KEYID + "\r\n"
                            + "SET -v FRUPN " + tempCustomMaterialNo + "\r\n"
                            + "SET -v MODELID " + mb_brief + "\r\n"
                            + "SET -v DPK " + dpk_type;
            Untils.createFile("D:\\fru\\", "BOM.NSH", totalStr);

            totalStr = "SET MBID=" + track_serial_no + "\r\n"
                           + "SET SN=" + vendor_serail_no + "\r\n"
                           + "SET SKU=" + mpn + "\r\n"
                           + "SET MAC=" + mac + "\r\n"
                           + "SET UUID=" + uuid + "\r\n"
                           + "SET MB11S=" + custom_serial_no + "\r\n"
                           + "SET OA3KEY=" + KEYSERIAL + "\r\n"
                           + "SET OA3PID=" + KEYID + "\r\n"
                           + "SET DPKNO=" + KEYSERIAL + "\r\n"
                           + "SET DPKID=" + KEYID + "\r\n"
                           + "SET FRUPN=" + tempCustomMaterialNo + "\r\n"
                           + "SET MODELID=" + mb_brief + "\r\n"
                           + "SET DPK=" + dpk_type;
            Untils.createFile("D:\\fru\\", "BOM.bat", totalStr);

            //Untils.createFile("C:\\CHKCPU\\", "BOM.bat", totalStr);

            //清空变量
            //KEYID = ""; 
            this.keyidtextBox.Text = "";
            //KEYSERIAL = ""; 
            this.KEYSERIALtextBox.Text = "";
            //this.tracker_bar_textBox.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);//fru
            string dir = Directory.GetCurrentDirectory()+"\\Files\\";
            if (Directory.Exists(dir))
            {
                string fileInput = dir + "FileInput.xml";
                if (File.Exists(fileInput))
                {
                    File.Copy(fileInput,@"C:\OA3\FileInput.xml",true);
                }

                downloadFiles(@"C:\CHKCPU\CPUPN.txt",@"C:\CHKCPU\CHKCPU.BAT");
            }            

            runBatFile(@"C:\CHKCPU\", "CHKCPU.BAT");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);//fru

            //downloadFiles(@"C:\CHKCPU\CPUPN.txt", @"C:\CHKCPU\CHKCPU.BAT");

            //runBatFile(@"C:\CHKCPU\", "CHKCPU.BAT");

            //最后做写入记录的动作
           // confirmbutton_Click(null, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            runBatFile(@"C:\CHKDPK\", "CHKDPK.BAT");
            confirmbutton_Click(null, null);
            this.Close();
        }

        private void runBatFile(string path, string filename)
        {
            try
            {
                string targetDir = string.Format(path);//this is where testChange.bat lies
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = filename;
                // proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }

        private void downloadFiles(string cpupnfile, string chkcpufile)
        {
            try
            {
                if (File.Exists(cpupnfile))
                {
                    File.Delete(cpupnfile);
                }
                if (File.Exists(chkcpufile))
                {
                    File.Delete(chkcpufile);
                }
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT cpupn, chkcpu FROM TestCpu";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        byte[] cpupn = (byte[])reader.GetValue(0);
                        byte[] chkcpu = (byte[])reader.GetValue(1);

                        string saveFileName = cpupnfile;
                        int arraysize = new int();
                        arraysize = cpupn.GetUpperBound(0);
                        FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(cpupn, 0, arraysize);
                        fs.Close();

                        saveFileName = chkcpufile;
                        arraysize = chkcpu.GetUpperBound(0);
                        fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(chkcpu, 0, arraysize);
                        fs.Close();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (KEYID == "NOK")
            {
                //过站
                makePassInfo();
            }
            else
            {
                //判断是否下载
                if(isburn.Checked)
                {
                    button4_Click(null,null);
                }
                else
                {
                    makePassInfo();
                }
            }
        }

        private void makePassInfo()
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
                        this.cpuFreqtextBox.Text = "";
                        this.cpuTypetextBox.Text = "";
                        this.keyidtextBox.Text = "";
                        this.KEYSERIALtextBox.Text = "";
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Test1&2', updateDate = GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试All数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
