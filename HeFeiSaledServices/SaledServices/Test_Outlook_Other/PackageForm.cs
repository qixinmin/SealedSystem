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
    public partial class PackageForm : Form
    {
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

                    if (station == "外观" || station == "Obe")
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
                            this.testerTextBox.Text = LoginForm.currentUser;
                            this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            this.confirmbutton.Enabled = true;
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
                        MessageBox.Show("板子已经经过站别" + station);
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

                    //外观做完自动出良品库，同时更新良品库的数量
                    cmd.CommandText = "select custommaterialNo from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
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

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Package', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
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
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入Package数据OK");
                this.confirmbutton.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }      
    }
}
