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
    public partial class ProcessReturnStoreForm : Form
    {
        public ProcessReturnStoreForm()
        {
            InitializeComponent();
            requestertextBox.Text = LoginForm.currentUser;
            this.requestdateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            loadInfo();
        }

        private void loadInfo()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select material_mpn,return_number,stock_place,requester,request_date,Id, fromId from fru_smt_return_store_record where _status ='request'";
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
         
            string[] hTxt = {"材料MPN","归还数量","库位","申请人","申请日期","ID","请求来源ID"};
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

            this.materialMpnTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.returnNumbertextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.stock_placetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.requestertextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.requestdateTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.idtextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.fromIdtextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            loadInfo();
        }

        private void returnStorebutton_Click(object sender, EventArgs e)
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

                    //1 修改归还仓库状态
                    cmd.CommandText = "update fru_smt_return_store_record set _status = 'done',processer = '" + requestertextBox.Text.Trim() +
                                "', processe_date = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                + "where Id = '" + this.idtextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //2.归还的数量要加到库存储位的数量上
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

                    cmd.CommandText = "update store_house set number = '" + (Int32.Parse(number) + Int32.Parse(this.returnNumbertextBox.Text)) + "'  where house='"+ house+"' and place='"+place+"'";
                    cmd.ExecuteNonQuery();

                    //3.更新表request_fru_smt_to_store_table中的status的字段的数量为return
                    cmd.CommandText = "update request_fru_smt_to_store_table set _status = 'return' where Id='"+this.fromIdtextBox.Text.Trim()+"'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("归还库存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
