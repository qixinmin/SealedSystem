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
    public partial class ReceiveOrderForm : Form
    {
        private String tableName = "receiveOrder";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public ReceiveOrderForm()
        {
            InitializeComponent();

            loadStoreHouse();
        }

        private void loadStoreHouse()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct storehouse_describe from storehouse";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    this.storeHouseComboBox.Items.Add(querySdr[0].ToString());                    
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
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" + 
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.ordernoTextBox.Text.Trim() + "','" +
                        this.custom_materialNoTextBox.Text.Trim() + "','" +
                        this.custom_material_describeTextBox.Text.Trim() + "','" +
                        this.ordernumTextBox.Text.Trim() + "','" +
                        this.mb_briefTextBox.Text.Trim() + "','" +
                        this.vendor_materialNoTextBox.Text.Trim() + "','" +
                        this.usernameTextBox.Text.Trim() + "','" +
                        this.ordertimeTextBox.Text.Trim() + "','" +
                        this.receivedNumTextBox.Text.Trim() + "','" +
                        this.receivedateTextBox.Text.Trim() + "','" +
                        this.statusTextBox.Text.Trim() + "','" +
                        this.storeHouseComboBox.Text.Trim() + "','" +
                        this.returnNumTextBox.Text.Trim() + "','" +
                        this.declare_unittextBox.Text.Trim() + "','" +
                        this.declare_numbertextBox.Text.Trim() + "','" +
                        this.custom_request_numbertextBox.Text.Trim() + "','" +
                        this.cidNumberTextBox.Text.Trim() + "')";

                    cmd.CommandType = CommandType.Text;
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
            MessageBox.Show("添加成功！");
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlStr = "select * from " + tableName;
                if (vendorTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where vendor= '" + vendorTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and vendor= '" + vendorTextBox.Text.Trim() + "' ";
                    }
                }

                if (productTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product= '" + productTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and product= '" + productTextBox.Text.Trim() + "' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText =sqlStr;
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

            string[] hTxt = {"ID", "厂商", "客户别","订单编号",
                                "客户料号","客户物料描述","订单数量","MB简称",
                                "厂商料号","制单人","制单时间","收货数量","收货日期","订单状态","仓库别","还货数量", "申报单位", "报关单号", "申请单号","Cid数量"};
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
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();

            dr["orderno"] = this.ordernoTextBox.Text.Trim();
            dr["custom_materialNo"] = this.custom_materialNoTextBox.Text.Trim();
            dr["custom_material_describe"] = this.custom_material_describeTextBox.Text.Trim();
            dr["ordernum"] = this.ordernumTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_briefTextBox.Text.Trim();
            dr["vendor_materialNo"] = this.vendor_materialNoTextBox.Text.Trim();
            dr["username"] = this.usernameTextBox.Text.Trim();
            dr["ordertime"] = this.ordertimeTextBox.Text.Trim();
            dr["receivedNum"] = this.receivedNumTextBox.Text.Trim();
            dr["receivedate"] = this.receivedateTextBox.Text.Trim();
            dr["_status"] = this.statusTextBox.Text.Trim();
            dr["storehouse"] = this.storeHouseComboBox.Text.Trim();
            dr["returnNum"] = this.returnNumTextBox.Text.Trim();

            dr["declare_unit"] = this.declare_unittextBox.Text.Trim();
            dr["declare_number"] = this.declare_numbertextBox.Text.Trim();
            dr["custom_request_number"] = this.custom_request_numbertextBox.Text.Trim();
            dr["cid_number"] = this.cidNumberTextBox.Text.Trim();

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
            MessageBox.Show("修改成功！");
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

                query_Click(null, null);
                MessageBox.Show("删除成功！");
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
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.ordernoTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.custom_materialNoTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.custom_material_describeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.ordernumTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.vendor_materialNoTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.usernameTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.ordertimeTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[10].Value.ToString()).ToString("yyyy/MM/dd");
            this.receivedNumTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.receivedateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[12].Value.ToString()).ToString("yyyy/MM/dd");
            this.statusTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.storeHouseComboBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.returnNumTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();

            this.declare_unittextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.declare_numbertextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();
            this.custom_request_numbertextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();

            this.cidNumberTextBox.Text = dataGridView1.SelectedCells[19].Value.ToString();
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
