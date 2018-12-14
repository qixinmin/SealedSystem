using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.additionForm
{
    public partial class OutWaitRepairByHand : Form
    {
        List<waitToOutRepairHouse> allWaitlist = new List<waitToOutRepairHouse>();
        List<waitToOutRepairHouse> targetWaitlist = new List<waitToOutRepairHouse>();

        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;

        public OutWaitRepairByHand()
        {
            InitializeComponent();

            this.button1.Enabled = false;
            this.button2.Enabled = false;
        }

        private void OutWaitRepairByHand_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                targetWaitlist.Clear();
                allWaitlist.Clear();
              
                mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string sql = "select track_serial_no,custom_materialNo,input_date from   wait_repair_in_house_table where  wait_repair_in_house_table.track_serial_no not in (select track_serial_no from  wait_repair_out_house_table)";
              
                cmd.CommandText = sql;
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    waitToOutRepairHouse temp = new waitToOutRepairHouse();
                    temp.trackno = querySdr[0].ToString();
                    temp.materialno = querySdr[1].ToString();
                    temp.inputdate = Untils.modifyDataFormat(querySdr[2].ToString());
                    allWaitlist.Add(temp);
                }
                querySdr.Close();

                foreach (waitToOutRepairHouse temp in allWaitlist)
                {
                    cmd.CommandText = "select custom_order from DeliveredTable where track_serial_no='" + temp.trackno + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.orderno = querySdr[0].ToString();
                    }
                    querySdr.Close();
                }

                mConn.Close();

                //增加过滤条件
                string orderno = "", trackno = "", materialno = "";
                if (ordernotextBox.Text.Trim() != "")
                {
                    orderno = this.ordernotextBox.Text.Trim();
                }
                if (this.tracknotextBox.Text.Trim() != "")
                {
                    trackno = this.tracknotextBox.Text.Trim();
                }

                if (this.materialnotextBox.Text.Trim() != "")
                {
                    materialno = this.materialnotextBox.Text.Trim();
                }

                if (orderno == "" && trackno == "" && materialno == "")
                {
                    foreach (waitToOutRepairHouse temp in allWaitlist)
                    {
                        targetWaitlist.Add(temp);
                    }
                }
                else
                {
                    if (orderno != "")
                    {
                        foreach (waitToOutRepairHouse temp in allWaitlist)
                        {
                            if (temp.orderno == orderno && targetWaitlist.Contains(temp) == false)
                            {
                                targetWaitlist.Add(temp);
                            }
                        }
                    }

                    if (trackno != "")
                    {
                        foreach (waitToOutRepairHouse temp in allWaitlist )
                        {
                            if (temp.trackno == trackno && targetWaitlist.Contains(temp) == false)
                            {
                                targetWaitlist.Add(temp);
                            }
                        }
                    }

                    if (materialno != "")
                    {
                        foreach (waitToOutRepairHouse temp in allWaitlist)
                        {
                            if (temp.materialno == materialno && targetWaitlist.Contains(temp) == false)
                            {
                                targetWaitlist.Add(temp);
                            }
                        }
                    }
                }

                dataGridView1.DataSource = targetWaitlist;
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "订单编号", "跟踪条码", "料号", "日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }

            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //事前检查
                if (this.targetWaitlist.Count <=0)
                {
                    MessageBox.Show("没有数据，不能出库，请先查询");
                    return;
                }

                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader querySdr = null;
                    foreach (waitToOutRepairHouse temp in targetWaitlist)
                    {
                        cmd.CommandText = "select Id,leftNumber from wait_repair_left_house_table where custom_materialNo='" + temp.materialno + "'";
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

                        try
                        {
                            if (Int16.Parse(left_number) < 1)
                            {
                                MessageBox.Show("待维修库的料号"+temp.materialno+"小于1，请查询");
                                conn.Close();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        if (exist == "")
                        {
                            MessageBox.Show("没有在待维修库里查到料号["+temp.materialno+"]");
                            conn.Close();
                            return;
                        }
                        else
                        {
                            cmd.CommandText = "update wait_repair_left_house_table set leftNumber = '" + (Int16.Parse(left_number) - 1) + "'"
                                    + "where custom_materialNo = '" + temp.materialno + "'";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "INSERT INTO wait_repair_out_house_table VALUES('" +
                             temp.trackno + "','" +
                             temp.materialno + "','" +
                             "1" + "','" +
                             DateTime.Now.ToString("yyyy/MM/dd") + "')";
                        cmd.ExecuteNonQuery();
                    }
                }            
            
                conn.Close();
                this.ordernotextBox.Text = "";
                this.materialnotextBox.Text = "";
                this.tracknotextBox.Text = "";
                button3_Click(null, null);
                MessageBox.Show("出待维修库成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要全部出库吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            try
            {
                //事前检查
                if (this.allWaitlist.Count <= 0)
                {
                    MessageBox.Show("没有数据，不能出库，请先查询");
                    return;
                }

                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader querySdr = null;
                    foreach (waitToOutRepairHouse temp in allWaitlist)
                    {
                        cmd.CommandText = "select Id,leftNumber from wait_repair_left_house_table where custom_materialNo='" + temp.materialno + "'";
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

                        try
                        {
                            if (Int16.Parse(left_number) < 1)
                            {
                                MessageBox.Show("待维修库的料号" + temp.materialno + "小于1，请查询");
                                conn.Close();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        if (exist == "")
                        {
                            MessageBox.Show("没有在待维修库里查到料号[" + temp.materialno + "]");
                            conn.Close();
                            return;
                        }
                        else
                        {
                            cmd.CommandText = "update wait_repair_left_house_table set leftNumber = '" + (Int16.Parse(left_number) - 1) + "'"
                                    + "where custom_materialNo = '" + temp.materialno + "'";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "INSERT INTO wait_repair_out_house_table VALUES('" +
                             temp.trackno + "','" +
                             temp.materialno + "','" +
                             "1" + "','" +
                             DateTime.Now.ToString("yyyy/MM/dd") + "')";
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
                this.ordernotextBox.Text = "";
                this.materialnotextBox.Text = "";
                this.tracknotextBox.Text = "";
                button3_Click(null, null);
                MessageBox.Show("出待维修库成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class waitToOutRepairHouse
    {
        public string orderno{ set; get; }
        public string trackno{ set; get; }
        public string materialno{ set; get; }
        public string inputdate{ set; get; }

    }
}
