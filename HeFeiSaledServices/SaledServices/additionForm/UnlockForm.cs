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
    public partial class UnlockForm : Form
    {
        private String tableName = "need_to_lock";
        public UnlockForm()
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

                    if (station != "收货")
                    {
                        MessageBox.Show("板子已经经过站别【 " + station+" 】");
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        this.confirmbutton.Enabled = false;
                        return;
                    }

                    this.confirmbutton.Enabled = true;

                    cmd.CommandText = "select isLock from need_to_lock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string islock = "";
                    while (querySdr.Read())
                    {
                        islock = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (islock == "")//先检查是否 三次返回锁定，然后检查是否需要分析锁定
                    {
                        MessageBox.Show("板子不存在，不需要解锁");
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        this.confirmbutton.Enabled = false;
                        return;
                    }
                    else if (islock == "false")
                    {
                        MessageBox.Show("板子已经解锁，不需要解锁啦");
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        this.confirmbutton.Enabled = false;
                        return;
                    }
                    else if (islock == "true")
                    {
                        this.confirmbutton.Enabled = true;
                    }
                   
                    this.testerTextBox.Text = LoginForm.currentUser;
                    this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
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

                    cmd.CommandText = "update need_to_lock set isLock = 'false', unlcok_date = GETDATE() "
                             + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                   "','解锁','OK',GETDATE(),'','','','','','','','','','','','','','','','" + this.testerTextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("解锁数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Close();
        }       
    }
}
