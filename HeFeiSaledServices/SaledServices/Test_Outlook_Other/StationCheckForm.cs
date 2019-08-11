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
    public partial class StationCheckForm : Form
    {
        public StationCheckForm()
        {
            InitializeComponent();

            this.tracker_bar_textBox.Focus();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.stationComboBox.Visible = false;
                this.modify.Visible = false;
            }
            else
            {
                this.stationComboBox.Visible = true;
                this.modify.Visible = true;
            }
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

                    if (station != "")
                    {
                        this.stationlabel.Text = station;
                    }
                    else
                    {
                        this.stationlabel.Text = "没有站别信息！";
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT station FROM stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if(querySdr.HasRows == false)
                    {
                        MessageBox.Show("没有相关站别信息，请检查序列号是否正确");
                        querySdr.Close();
                        conn.Close();
                        return;
                    }
                    querySdr.Close();

                    string currentStation = stationComboBox.Text.Trim();
                    if(currentStation == "Package")
                    {
                        string track_serial_no_temp = "";                 
                        cmd.CommandText = "select track_serial_no from Packagetable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            track_serial_no_temp = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        if (track_serial_no_temp == "")
                        {
                            MessageBox.Show("此单在Package站別沒有操作信息，是不是沒有操作过Package界面？");
                            conn.Close();
                            return;
                        }                    
                        querySdr.Close();

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                     "','Package_change','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + User.UserSelfForm.username + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = 'Package', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                  + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Packagetable VALUES('"
                            + this.tracker_bar_textBox.Text.Trim() + "','"
                            + LoginForm.currentUser +"','"
                            + DateTime.Now.ToString()
                            + "')";
                            cmd.ExecuteNonQuery();    
                    }
                    else if(currentStation=="TakePhoto")
                    {
                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                    "','TakePhoto_change','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + User.UserSelfForm.username + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = 'TakePhoto', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                  + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                    }
                    else //if (currentStation == "维修")
                    {
                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                  "','"+currentStation+"_change','OK','" + DateTime.Now.ToString() + "','','','','','','','','','','','','','','','','" + User.UserSelfForm.username + "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = '" + currentStation + "', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                  + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("修改站别数据OK");            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
