using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Test_Outlook
{
    public partial class TakePhotoForm : Form
    {
        private String tableName = "TakePhototable";
        public TakePhotoForm()
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

                    if (station != "外观")
                    {
                        MessageBox.Show("板子还没有经过外观站别, 现在在: ["+station+"]");
                            
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;                        
                    }
                    else 
                    {
                        //需要添加判断本地文件的逻辑，看图片是否存在，如果不存在，则不能通过 TODO ？？ 提示重新拍照
                        if (false)
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

                    //开始计算决定是否走obe站别，规则如下：根据跟踪条码查到对应的订单号，从订单号可以在receiveorder中查找这个订单有多少量，然后根据比率（有默认值）来抽检
                    //如果在表中有抽检则需查询obe站别是否ok，否则不能走包装站别，有一个问题，如果第一次fail，第二次可以不走obe，如何判断？
                    cmd.CommandText = "select custom_order,custom_materialNo from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string customorder = "", custom_materialNo="";
                    while (querySdr.Read())
                    {
                        customorder = querySdr[0].ToString();
                        custom_materialNo = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    if (customorder == "")
                    {
                        MessageBox.Show("此序列号查不到对应的订单编号！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }
                  
                    cmd.CommandText = "select ordernum from receiveOrder where orderno='" + customorder + "'";
                    querySdr = cmd.ExecuteReader();
                    string ordernum = "";
                    while (querySdr.Read())
                    {
                        ordernum = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (ordernum == "")
                    {
                        MessageBox.Show("此订单编号不存在！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }
                    try
                    {
                        int num = Int16.Parse(ordernum);
                        if (num <= 0)
                        {
                            MessageBox.Show("此订单编号对应的数量不对！");
                            this.tracker_bar_textBox.Text = "";
                            conn.Close();
                            return;
                        }

                        //现在基数有了，查询数据库中有多少个，然后决定当前跟踪条码是第几个
                        double rate = 0.15;

                        int totalchecknum = (int)Math.Ceiling(num * rate);

                        //查询现在有多少个了，只需要查最后一个，也许没有
                        cmd.CommandText = "select COUNT(*)  from decideOBEchecktable where orderno='" + customorder + "'";
                        querySdr = cmd.ExecuteReader();
                        string existnum = "";
                        while (querySdr.Read())
                        {
                            existnum = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        int currentIndex = Int16.Parse(existnum) + 1;
                        bool ischeck = isObeCheck(num, totalchecknum, currentIndex);

                        //后续要插入到数据库中
                        cmd.CommandText = "INSERT INTO decideOBEchecktable VALUES('"
                          + this.tracker_bar_textBox.Text.Trim() + "','"
                          + customorder + "','"
                          + custom_materialNo + "','"
                          + num + "','"
                          + rate + "','"
                          + currentIndex + "','"
                          + (ischeck ? "True" : "False") + "','"
                          + this.testerTextBox.Text.Trim() + "','"
                          + this.testdatetextBox.Text.Trim()
                          + "')";
                        cmd.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("此订单编号对应的数量不对--！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                 "','TakePhoto','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'TakePhoto', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入拍照数据OK");
                this.confirmbutton.Enabled = false;
                this.button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool isObeCheck(int totalnum, int totalchecknum, int currentIndex)
        {
            if (totalnum == currentIndex)//比如总数为1
            {
                return true;
            }

            if (totalchecknum == currentIndex)//比如最后一个
            {
                return true;
            }

            int step = (int)Math.Floor(totalnum / totalchecknum*1.0);//步长取数值
            int forward = 1;
            while (forward <= totalchecknum)
            {
                forward += step;
                if (forward == currentIndex)
                {
                    return true;
                }
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (this.tracker_bar_textBox.Text.Trim() == "")
            //{
            //    MessageBox.Show("追踪条码的内容为空，请检查！");
            //    return;
            //}

            //try
            //{
            //    SqlConnection conn = new SqlConnection(Constlist.ConStr);
            //    conn.Open();

            //    if (conn.State == ConnectionState.Open)
            //    {
            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = conn;
            //        cmd.CommandType = CommandType.Text;

            //        //防止重复入库
            //        cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
            //        SqlDataReader querySdr = cmd.ExecuteReader();
            //        string Id = "";
            //        while (querySdr.Read())
            //        {
            //            Id = querySdr[0].ToString();
            //        }
            //        querySdr.Close();
            //        if (Id != "")
            //        {
            //            MessageBox.Show("此序列号已经存在！");
            //            this.tracker_bar_textBox.Text = "";
            //            conn.Close();
            //            return;
            //        }

            //        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
            //     "','TakePhoto','FAIL','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
            //        cmd.ExecuteNonQuery();

            //        cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
            //                  + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
            //        cmd.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        MessageBox.Show("SaledService is not opened");
            //    }

            //    conn.Close();
            //    MessageBox.Show("插入拍照 Fail数据, 現在需要把板子給維修人員");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }
}
