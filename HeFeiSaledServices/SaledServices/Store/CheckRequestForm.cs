using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Store
{
    public partial class CheckRequestForm : Form
    {
        public CheckRequestForm()
        {
            InitializeComponent();
            requestertextBox.Text = LoginForm.currentUser;
            this.dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            loadInfo();
        }

        private void loadInfo()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select Id,mb_brief,not_good_place,material_mpn,number,requester,_date,_status,usedNumber from  request_fru_smt_to_store_table where _status ='request'";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "request_fru_smt_to_store_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
            string[] hTxt = { "ID", "机型", "位置","材料MPN","请求数量","申请人","申请日期","状态","使用的数量"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.not_good_placetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.materialMpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.requestNumbertextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.requestertextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.dateTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.statustextBox.Text  = dataGridView1.SelectedCells[7].Value.ToString();

            processRequest();
        }

        private void processRequest()
        {
            //根据mpn查询库位的剩余数量，如果不满足条件，则直接disable处理情况，只负责更新状态

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.materialMpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "";
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    if (house == "" || place == "")
                    {
                        MessageBox.Show("此料不在库存里面！");
                        this.processRequestbutton.Enabled = false;
                        this.currentNumbertextBox.Text = "";
                        this.stockplacetextBox.Text = "";
                        conn.Close();
                        return;
                    }
                    else
                    {
                        this.processRequestbutton.Enabled = true;
                    }

                    int requestNumber = Int32.Parse(this.requestNumbertextBox.Text.Trim());
                    int totalCurentNumber = Int32.Parse(number);
                    this.currentNumbertextBox.Text = number;
                    this.stockplacetextBox.Text = house+","+place;

                    if (requestNumber <= totalCurentNumber)
                    {
                        processRequestbutton.Enabled = true;
                        //waitbutton.Enabled = false;
                    }
                    else
                    {
                        processRequestbutton.Enabled = false;
                        //waitbutton.Enabled = true;
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

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            loadInfo();
        }

        private void processRequestbutton_Click(object sender, EventArgs e)
        {
            if (this.materialMpnTextBox.Text == "")
            {
                MessageBox.Show("请选择一行作为处理！");
                return;
            }
            //在处理之前的时候可以使用mpn的字段查询库存中是否有相应的内容，如果没有则提示说此库存没有相关库存，要备料
            FRU_SMT_OutSheetForm frusmtout = new FRU_SMT_OutSheetForm();
            frusmtout.MdiParent = Program.parentForm;
            frusmtout.setparamters(this.mb_brieftextBox.Text.Trim(), this.materialMpnTextBox.Text.Trim(),  this.requestNumbertextBox.Text.Trim(), this.idTextBox.Text.Trim(),this.requestertextBox.Text.Trim());
            frusmtout.Show();
            frusmtout.doRequestUsingMpn();

            //清空数据，防止误操作
            this.materialMpnTextBox.Text = "";

            this.mb_brieftextBox.Text = "";
            this.not_good_placetextBox.Text = "";
            this.requestNumbertextBox.Text = "";
            this.statustextBox.Text = "";
            //在处理完请求后需要把本条记录状态修改为close或其他状态
        }

        private void waitbutton_Click(object sender, EventArgs e)
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
                    //跟新请求表格的状态
                    cmd.CommandText = "update request_fru_smt_to_store_table set _status = 'wait' where Id = '" + this.idTextBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("更新状态成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void requestNumbertextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                processRequest();
            }
        }
    }
}
