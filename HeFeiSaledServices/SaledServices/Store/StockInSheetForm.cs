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
    public partial class StockInSheetForm : Form
    {
        private String tableName = Constlist.table_name_stock_in_sheet;
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public StockInSheetForm()
        {
            InitializeComponent();
            inputerTextBox.Text = LoginForm.currentUser;
            this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

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
                        this.buy_order_serial_noTextBox.Text.Trim() + "','" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.buy_typeTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.material_typeTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.numberTextBox.Text.Trim() + "','" +
                        this.pricePerTextBox.Text.Trim() + "','" +
                        this.material_nameTextBox.Text.Trim() + "','" +
                        this.totalMoneyTextBox.Text.Trim() + "','" +
                        this.stock_in_numTextBox.Text.Trim() + "','" +                       
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +
                        this.declare_unittextBox.Text.Trim() + "','" +
                        this.declare_numbertextBox.Text.Trim() + "','" +
                        this.custom_request_numbertextBox.Text.Trim() + "')";

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

                string sqlStr =  "select * from " + tableName;

                if (vendorTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where vendor like '%" + vendorTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and vendor like '%" + vendorTextBox.Text.Trim() + "%'";
                    }
                }

                if (productTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product like '%" + productTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and product like '%" + productTextBox.Text.Trim() + "%' ";
                    }
                }

                if (mpnTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + mpnTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnTextBox.Text.Trim() + "%' ";
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

            string[] hTxt = { "ID", "采购订单编号", "厂商", "采购类别", "客户别", "材料大类", "MPN", "厂商料号", "描述", "订单数量", "单价", "材料名称", "金额合计", "入库数量", "状态", "输入人", "日期", "是否报关", "申报单位", "报关单号", "申请单号" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }

            this.buy_order_serial_noTextBox.Text = "";
            this.vendorTextBox.Text = "";
            this.buy_typeTextBox.Text = "";
            this.productTextBox.Text = "";
            this.material_typeTextBox.Text = "";
           // this.mpnTextBox.Text = "";
            this.vendormaterialNoTextBox.Text = "";
            this.describeTextBox.Text = "";
            this.numberTextBox.Text = "";
            this.pricePerTextBox.Text = "";
            this.material_nameTextBox.Text = "";
            this.totalMoneyTextBox.Text = "";
            this.stock_in_numTextBox.Text = "";
            this.isDeclareTextBox.Text = "";                    
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());

            dr["buy_order_serial_no"] = this.buy_order_serial_noTextBox.Text.Trim();
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["buy_type"] = this.buy_typeTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["material_type"] = this.material_typeTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["vendormaterialNo"] = this.vendormaterialNoTextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
            dr["number"] = this.numberTextBox.Text.Trim();
            dr["pricePer"] = this.pricePerTextBox.Text.Trim();
            dr["material_name"] = this.material_nameTextBox.Text.Trim();
            dr["totalMoney"] = this.totalMoneyTextBox.Text.Trim();
            dr["stock_in_num"] = this.stock_in_numTextBox.Text.Trim();
            dr["inputer"] = this.inputerTextBox.Text.Trim();
            dr["input_date"] = this.input_dateTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();

            dr["declare_unit"] = this.declare_unittextBox.Text.Trim();
            dr["declare_number"] = this.declare_numbertextBox.Text.Trim();
            dr["custom_request_number"] = this.custom_request_numbertextBox.Text.Trim();
        

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
            this.buy_order_serial_noTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.buy_typeTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.material_typeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.describeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.numberTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.pricePerTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.material_nameTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.totalMoneyTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.stock_in_numTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            //status 14

            this.inputerTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.input_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[16].Value.ToString()).ToString("yyyy/MM/dd");
            this.isDeclareTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            this.declare_unittextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            this.declare_numbertextBox.Text = dataGridView1.SelectedCells[19].Value.ToString();
            this.custom_request_numbertextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
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

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }
    }
}
