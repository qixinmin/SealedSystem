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
    public partial class DPKForm : Form
    {
        private String tableName = "DPK_table";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public DPKForm()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {            
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.dpk_order_noTextBox.Text.Trim() + "','" +
                        this.dpk_typeTextBox.Text.Trim() + "','" +
                        this.KEYPNTextBox.Text.Trim() + "','" +
                        this.KEYIDTextBox.Text.Trim() + "','" +
                        this.KEYSERIALTextBox.Text.Trim() + "','" +
                        this.upload_dateTextBox.Text.Trim() + "','" +
                        this.statusTextBox.Text.Trim() + "','" +
                        this.burn_dateTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "')";

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
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

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 100 * from " + tableName;

                if (KEYPNTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where KEYPN= '" + KEYPNTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and KEYPN= '" + KEYPNTextBox.Text.Trim() + "' ";
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

            string[] hTxt = { "ID", "DPK订单编号","DPK类别","KEYPN","KEYID","KEYSERIAL","上传日期","状态","烧录日期","客户序号" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
            dr["dpk_order_no"] = this.dpk_order_noTextBox.Text.Trim();
            dr["dpk_type"] = this.dpk_typeTextBox.Text.Trim();

            dr["_status"] = this.statusTextBox.Text.Trim();
            dr["KEYID"] = this.KEYIDTextBox.Text.Trim();
            dr["KEYSERIAL"] = this.KEYSERIALTextBox.Text.Trim();
            dr["upload_date"] = this.upload_dateTextBox.Text.Trim();
            dr["KEYPN"] = this.KEYPNTextBox.Text.Trim();
            dr["burn_date"] = this.burn_dateTextBox.Text.Trim();
            dr["custom_serial_no"] = this.custom_serial_noTextBox.Text.Trim();            

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
            this.dpk_order_noTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.dpk_typeTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.KEYPNTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.KEYIDTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.KEYSERIALTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.upload_dateTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.statusTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.burn_dateTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.custom_serial_noTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();            
        }

        private void ReceiveOrderForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
              GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
              SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel2, true, null);
            tableLayoutPanel3.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel3, true, null);            
            
        }
    }
}
