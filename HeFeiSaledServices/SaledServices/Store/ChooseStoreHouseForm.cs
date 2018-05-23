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
    public partial class ChooseStoreHouseForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "store_house";

        private Form mFromFrom;

        public ChooseStoreHouseForm(Form fromFrom)
        {
            InitializeComponent();
            mFromFrom = fromFrom;

            loadAdditionInfomation();
        }

        public ChooseStoreHouseForm(Form fromFrom, string newtablename)
        {
            InitializeComponent();
            mFromFrom = fromFrom;
            tableName = newtablename;
            loadAdditionInfomation();
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

                cmd.CommandText = "select distinct house from " + tableName + " where mpn=''";
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

        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr = "select top 100 * from " + tableName +" where mpn=''";

                

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
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());
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
            this.numTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.houseComboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.placeTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.mpntextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.numbertextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.choose.Enabled = true;
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

                    cmd.CommandText = "update store_house set mpn = '',number = ''";
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

        private void choose_Click(object sender, EventArgs e)
        {
            if (this.houseComboBox.Text.Trim() == "" || this.placeTextBox.Text.Trim() == "")
            {
                MessageBox.Show("请选择一个库位!");
                return;
            }
            if (mFromFrom is FRU_SMT_InSheetForm)
            {
                ((FRU_SMT_InSheetForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
            }
            else if (mFromFrom is BGA_InSheetForm)
            {
                ((BGA_InSheetForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
            }
            else if (mFromFrom is MB_InSheetForm)
            {
                ((MB_InSheetForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
            }
            else if (mFromFrom is FaultMBStoreForm)
            {
                ((FaultMBStoreForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
            }
            else if (mFromFrom is BGA_OutSheetForm)
            {
                if (tableName == "store_house_ng")
                {
                    ((BGA_OutSheetForm)mFromFrom).setChooseNGStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
                }
                else
                {
                    ((BGA_OutSheetForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
                }
            }
            else if (mFromFrom is FaultSMTStoreForm)
            {
                ((FaultSMTStoreForm)mFromFrom).setChooseStock(this.numTextBox.Text.Trim(), this.houseComboBox.Text.Trim(), this.placeTextBox.Text.Trim());
            }
            
            this.Close();
        }

        private void houseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.houseComboBox.Text;
            if (str == "")
            {
                return;
            }

            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            try
            {
                this.dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from " + tableName + " where mpn='' and house ='" + this.houseComboBox.Text +"'";
                cmd.CommandType = CommandType.Text;

                mConn = new SqlConnection(Constlist.ConStr);

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = { "ID", "库房", "储位", "物料", "数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                }
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
