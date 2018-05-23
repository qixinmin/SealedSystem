using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class StoreHouseInnerNGForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "store_house_ng";

        public StoreHouseInnerNGForm()
        {
            InitializeComponent();
            loadAdditionInfomation();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void loadAdditionInfomation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct house from " + tableName;
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.houseComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.houseComboBox.Text.Trim() == "" || this.placeTextBox.Text.Trim() == "")
            {
                MessageBox.Show("库房或储位内容为空!");
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
                    //首先判断是否存在
                    string sqlStr = "select * from " + tableName + " where house= '"+this.houseComboBox.Text.Trim().ToUpper()+"' and place = '" + this.placeTextBox.Text.Trim().ToUpper() + "'";
                    cmd.CommandText = sqlStr;
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        conn.Close();
                        MessageBox.Show("相同的库位已经有了，请检查！");
                        return;
                    }
                    querySdr.Close();

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" + this.houseComboBox.Text.Trim() + "','" + this.placeTextBox.Text.Trim() + "','','')";
                    
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr = "select top 100 * from " + tableName;

                if (this.houseComboBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where house= '" + houseComboBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and house= '" + houseComboBox.Text.Trim() + "' ";
                    }
                }

                if (this.placeTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where place like '%" + placeTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and place like '%" + placeTextBox.Text.Trim() + "%' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "库房","储位","物料","数量" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
            dr["house"] = this.houseComboBox.Text.Trim();
            dr["place"] = this.placeTextBox.Text.Trim();
            dr["mpn"] = this.mpntextBox.Text.Trim();
            dr["number"] = this.numbertextBox.Text.Trim();            

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("删除完毕!");
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.houseComboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.placeTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.mpntextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.numbertextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
        }

        private void releasePlacebutton_Click(object sender, EventArgs e)
        {
            //当确定不需要此储位的时候，可以释放位置,把相应的物料与数量清空即可
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "update " + tableName + " set mpn = '',number = '' where Id='" + this.idTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("解除完毕，后面可以使用此处储位了!");
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void placeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }
    }
}
