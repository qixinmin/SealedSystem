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
    public partial class ObeRateForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "obe_checkrate_table";

        public ObeRateForm()
        {
            InitializeComponent();

            inputertextBox.Text = LoginForm.currentUser;
            this.inputdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            //this.add.Enabled = false;

            doqueryorderno();
        }

        private void doqueryorderno()
        {
            try
            {
                this.dataGridViewquery.DataSource = null;
                dataGridViewquery.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                List<ordernoinfo> modifiblelist = new List<ordernoinfo>();
                List<ordernoinfo> modifiblelistfinal = new List<ordernoinfo>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                //加入条件判断，只显示未收完的货物
                cmd.CommandText = "select orderno,custom_materialNo from receiveOrder where _status!='return'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ordernoinfo info = new ordernoinfo();
                    info.orderno = querySdr[0].ToString();
                    info.custom_materialNo = querySdr[1].ToString();
                    modifiblelist.Add(info);
                    modifiblelistfinal.Add(info);
                }
                querySdr.Close();

                foreach (ordernoinfo str in modifiblelist)
                {
                    cmd.CommandText = "select top 1 orderno,custom_materialNo from obe_checkrate_table where orderno='" + str.orderno + "'";
                    cmd.CommandType = CommandType.Text;

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        modifiblelistfinal.Remove(str);
                    }
                    querySdr.Close();
                }

                dataGridViewquery.DataSource = modifiblelistfinal;
                dataGridViewquery.RowHeadersVisible = false;

                string[] hTxt = { "订单编号","料号"};
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewquery.Columns[i].HeaderText = hTxt[i];
                    dataGridViewquery.Columns[i].Name = hTxt[i];
                }

                mConn.Close();

                //if (modifiblelistfinal.Count > 0)
                //{
                //    dataGridViewquery.Rows[0].Selected = false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public class ordernoinfo
        {
            public string orderno { set; get; }
            public string custom_materialNo { set; get; }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.ordernoTextBox.Text.Trim() == "" || this.checkrateTextBox.Text.Trim() == "" || this.matertialNotextBox.Text.Trim() == "")
            {
                MessageBox.Show("要输入的内容不能为空!");
                return;
            }

            try
            {
                Double.Parse(checkrateTextBox.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("比例输入的格式有问题!");
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

                    cmd.CommandText = "select Id from  " + tableName + " where orderno='" + this.ordernoTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        conn.Close();
                        MessageBox.Show("表中已经有此订单号的数据，不能插入，可以查询！");
                    }
                    querySdr.Close();


                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" 
                        + this.ordernoTextBox.Text.Trim() + "','"
                        + this.matertialNotextBox.Text.Trim() + "','"
                        + this.checkrateTextBox.Text.Trim() + "','"
                        + this.inputertextBox.Text.Trim() + "','"
                        + this.inputdatetextBox.Text.Trim() 
                        + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                clear();
                MessageBox.Show("新增成功！");


                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clear()
        {
            this.numTextBox.Text = "";
            this.ordernoTextBox.Text = "";
            this.matertialNotextBox.Text = "";
            this.checkrateTextBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;

                string sqlStr = "select * from  " + tableName;

                if (ordernoTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where orderno= '" + ordernoTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and orderno= '" + ordernoTextBox.Text.Trim() + "' ";
                    }
                }

                if (this.matertialNotextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where custom_materialNo= '" + matertialNotextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and custom_materialNo= '" + matertialNotextBox.Text.Trim() + "' ";
                    }
                }
                              
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridViewinsert.DataSource = ds.Tables[0];
                dataGridViewinsert.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "订单编号", "料号", "抽查比例", "输入人", "输入时间" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewinsert.Columns[i].HeaderText = hTxt[i];
            }
            MessageBox.Show("查询完成！");
            clear();
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (this.numTextBox.Text.Trim() == "")
            {
                return;
            }
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());
            dr["orderno"] = this.ordernoTextBox.Text.Trim();
            dr["custom_materialNo"] = this.matertialNotextBox.Text.Trim();
            dr["rate"] = this.checkrateTextBox.Text.Trim();

            dr["inputer"] = this.inputertextBox.Text.Trim();

            dr["input_date"] = this.inputdatetextBox.Text.Trim();            

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
            MessageBox.Show("修改成功！");
            clear();

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
                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridViewinsert.SelectedCells[0].Value.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                clear();
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
            if (dataGridViewinsert.CurrentRow == null)
            {
                return;
            }

            this.numTextBox.Text = dataGridViewinsert.SelectedCells[0].Value.ToString();
            this.ordernoTextBox.Text = dataGridViewinsert.SelectedCells[1].Value.ToString();
            this.matertialNotextBox.Text = dataGridViewinsert.SelectedCells[2].Value.ToString();
            this.checkrateTextBox.Text = dataGridViewinsert.SelectedCells[3].Value.ToString();  
        }

        private void dataGridViewquery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewquery.CurrentRow == null)
            {
                return;
            }

            this.ordernoTextBox.Text = dataGridViewquery.SelectedCells[0].Value.ToString();
            this.matertialNotextBox.Text = dataGridViewquery.SelectedCells[1].Value.ToString();
        }

        private void button_queryorder_Click(object sender, EventArgs e)
        {
            this.numTextBox.Text = "";
            this.checkrateTextBox.Text = "";
            this.ordernoTextBox.Text = "";
            doqueryorderno();
        }
    }
}
