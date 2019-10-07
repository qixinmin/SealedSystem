using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Export;

namespace SaledServices
{
    public partial class BGA_OutSheetForm : Form
    {
        private String tableName = "bga_out_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private string requestId = "";
        private string requestNumber = "";

        private string notgood_house;
        private string notgood_place;

       // private ChooseStock chooseStock = new ChooseStock();

        public BGA_OutSheetForm()
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
            if (this.notgoodplacetextBox.Text == "")
            {
                MessageBox.Show("不良品库位为空!");
                return;
            }

            if (this.takertextBox.Text == "")
            {
                MessageBox.Show("领用人为空!");
                return;
            }

            if (this.stock_out_numTextBox.Text == "")
            {
                MessageBox.Show("领用数量为空!");
                return;
            }

            try
            {
                int currentStockNumber = Int32.Parse(this.currentStockNumbertextBox.Text.Trim());
                int outNumber = Int32.Parse(this.stock_out_numTextBox.Text.Trim());
                if (outNumber > currentStockNumber)
                {
                    MessageBox.Show("输入的数量不能大于库存数量!!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                    //良品库
                    //需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.mpnTextBox.Text.Trim() + "'";
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

                    try
                    {
                        int left = (Int32.Parse(number) - Int32.Parse(this.stock_out_numTextBox.Text));
                        if (left < 0)
                        {
                            MessageBox.Show("输入的数量不能大于库存数量!");
                            conn.Close();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("输入的数量不能大于库存数量~~~~");
                        conn.Close();
                        return;
                    }


                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.vendorcomboBox.Text.Trim() + "','" +
                        this.productcomboBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.bga_brieftextBox.Text.Trim() + "','" +
                        this.bgadescribeTextBox.Text.Trim() + "','" +
                        this.stock_placetextBox.Text.Trim() + "','" +
                        this.stock_out_numTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +
                        this.notetextBox.Text.Trim() + "','" +
                        this.takertextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "')";
                    
                    cmd.ExecuteNonQuery();                                                         

                    cmd.CommandText = "update store_house set number = '" + (Int32.Parse(number) - Int32.Parse(this.stock_out_numTextBox.Text)) + "'  where house='"+ house+"' and place='"+place+"'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select mpn from store_house where mpn != '' group by mpn having COUNT(*) > 1 ";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("请关闭窗口之前上报管理员并拍照");
                    }
                    querySdr.Close();

                    //不良品库, 需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house_ng where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    house = ""; place = ""; Id = ""; number = "";
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    //若库房不存在，则自动生成库
                    if (house == "")
                    {
                        cmd.CommandText = "INSERT INTO store_house_ng VALUES('" + this.notgood_house.Trim() + "','" + this.notgood_place.Trim() + "','" + this.mpnTextBox.Text.Trim() + "','" + this.stock_out_numTextBox.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();

                        this.notgoodplacetextBox.Text = "";
                    }
                    else
                    {
                        cmd.CommandText = "update store_house_ng set number = '" + (Int32.Parse(number) + Int32.Parse(this.stock_out_numTextBox.Text)) + "'  where house='" + notgood_house + "' and place='" + notgood_place + "'";
                        cmd.ExecuteNonQuery();
                    }                  

                    //同时生成一条BGA不良品入库记录
                    cmd.CommandText = "INSERT INTO smt_bga_ng_in_house_table VALUES('" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.stock_out_numTextBox.Text.Trim() + "','"+
                        this.input_dateTextBox.Text.Trim() + "')";

                    cmd.ExecuteNonQuery();  
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                clearTexts();
                query_Click(null, null);
                MessageBox.Show("BGA出库成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearTexts()
        {
            this.vendorcomboBox.Text = "";
            this.productcomboBox.Text = "";
            this.mpnTextBox.Text = "";
            this.bga_brieftextBox.Text = "";
            this.bgadescribeTextBox.Text = "";
            this.stock_placetextBox.Text = "";
            this.stock_out_numTextBox.Text = "";
            this.isDeclareTextBox.Text = "";
            this.notetextBox.Text = "";
            this.takertextBox.Text = "";          
            this.input_dateTextBox.Text = "";
            this.notgoodplacetextBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 100 * from " + tableName;

                if (this.vendorcomboBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where vendor= '" + vendorcomboBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and vendor= '" + vendorcomboBox.Text.Trim() + "' ";
                    }
                }

                if (this.productcomboBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product= '" + productcomboBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and product= '" + productcomboBox.Text.Trim() + "' ";
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

            string[] hTxt = { "ID", "厂商", "客户别", "MPN", "BGA简称", "BGA描述", "库位", "出库数量", "是否报关", "备注", "领用人", "出库人", "出库日期"};
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
           
            dr["vendor"] = this.vendorcomboBox.Text.Trim();
            dr["product"] = this.productcomboBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["bga_brief"]= this.bga_brieftextBox.Text.Trim();
            dr["bga_describe"] = this.bgadescribeTextBox.Text.Trim();
            dr["stock_place"] = this.stock_placetextBox.Text.Trim();
            dr["stock_out_num"]= this.stock_out_numTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();           
            dr["note"] = this.notetextBox.Text.Trim();
           
            dr["taker"] = this.takertextBox.Text.Trim();
            dr["inputer"] = this.inputerTextBox.Text.Trim();
            dr["input_date"] = this.input_dateTextBox.Text.Trim();

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
            this.vendorcomboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productcomboBox.Text = dataGridView1.SelectedCells[2].Value.ToString();           
            this.mpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.bga_brieftextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();         
            this.bgadescribeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.stock_placetextBox.Text= dataGridView1.SelectedCells[6].Value.ToString();
            this.stock_out_numTextBox.Text= dataGridView1.SelectedCells[7].Value.ToString();
            this.isDeclareTextBox.Text= dataGridView1.SelectedCells[8].Value.ToString();
            this.notetextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.takertextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[11].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
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

            tableLayoutPanel4.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel4, true, null);
        }

        public void doRequestUsingMpn()
        {
            try
            {
                this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //查询库位和数量
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.mpnTextBox.Text.Trim() + "'";
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
                        conn.Close();
                        return;
                    }
                    else
                    {
                        notgood_house = "N-" + house;
                        notgood_place = "N-" + place;
                        this.notgoodplacetextBox.Text = notgood_house + "," + notgood_place;
                    }

                    this.currentStockNumbertextBox.Text = number;
                    this.stock_placetextBox.Text = house + "," + place;

                    cmd.CommandText = "select vendor,product,bga_describe,describe,isdeclare from bga_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.vendorcomboBox.Text = querySdr[0].ToString();
                        this.productcomboBox.Text = querySdr[1].ToString();
                        this.bga_brieftextBox.Text = querySdr[2].ToString();
                        this.bgadescribeTextBox.Text = querySdr[3].ToString();
                        this.isDeclareTextBox.Text = querySdr[4].ToString();
                        break;
                    }
                    querySdr.Close();
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
        public void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                doRequestUsingMpn();
            }
        }

        public void setparamters(string mb_brief, string material_mpn, string requestNumber, string index)
        {
            this.bga_brieftextBox.Text = mb_brief;
            this.mpnTextBox.Text = material_mpn;
            this.requestNumber = requestNumber;
            requestId = index;
        }

        private void queryStock_Click(object sender, EventArgs e)
        {
            if (this.bga_brieftextBox.Text == "")
            {
                MessageBox.Show("BGA简称不能为空！");
                return;
            }
            try
            {

                this.mpnTextBox.Text = "";
                this.bgadescribeTextBox.Text = "";
                this.currentStockNumbertextBox.Text = "";
                this.stock_placetextBox.Text = "";
                this.stock_out_numTextBox.Text = "";
                this.isDeclareTextBox.Text = "";
                this.notetextBox.Text = "";
                this.notgoodplacetextBox.Text = "";

                queryvendorproduct();

                this.dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;

                string sql = "select distinct mpn,stock_place,vendor,bga_describe,isdeclare from bga_in_stock where bga_describe='" + this.bga_brieftextBox.Text + "'";
                
                if (this.vendorcomboBox.Text != "")
                {
                    sql += " and vendor='" + this.vendorcomboBox.Text + "'";
                }
                
                if (this.productcomboBox.Text != "")
                {
                    sql += " and product='" + this.productcomboBox.Text + "'";
                }

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "stock_in_sheet");
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.RowHeadersVisible = false;
                mConn.Close();

                string[] hTxt = { "MPN", "库位", "厂商","描述","是否申报" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView2.Columns[i].HeaderText = hTxt[i];
                    dataGridView2.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void stock_out_numTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.currentStockNumbertextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入mpn号并回车！");
                    this.mpnTextBox.Focus();
                    return;
                }

                if (this.stock_out_numTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入数量并回车！");
                    this.stock_out_numTextBox.Focus();
                    return;
                }

                try
                {
                    int currentStockNumber = Int32.Parse(this.currentStockNumbertextBox.Text.Trim());
                    int outNumber = Int32.Parse(this.stock_out_numTextBox.Text.Trim());
                    if (outNumber > currentStockNumber)
                    {
                        MessageBox.Show("输入的数量不能大于库存数量");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void queryvendorproduct()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct vendor from bga_in_stock where bga_describe like '%" + this.bga_brieftextBox.Text + "%'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                this.vendorcomboBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.vendorcomboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct product from bga_in_stock where  bga_describe like '%" + this.bga_brieftextBox.Text + "%'";
                querySdr = cmd.ExecuteReader();
                this.productcomboBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.productcomboBox.Items.Add(temp);
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

        private void bga_brieftextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                queryvendorproduct();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            this.mpnTextBox.Text = dataGridView2.SelectedCells[0].Value.ToString();
            doRequestUsingMpn();
        }

        public void setChooseNGStock(string id, string house, string place)
        {
            this.notgoodplacetextBox.Text = house + "," + place;
        }

        public void setChooseStock(string id, string house, string place)
        {
            this.stock_placetextBox.Text = house + "," + place;
        }

        private void notgoodplacetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //打开选择界面，并把结果返回到本界面来
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this,"store_house_ng");
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }

        private void exportExcel_Click(object sender, EventArgs e)
        {   
            BgaOutExport bgaout = new BgaOutExport();
           // bgaout.MdiParent = this;
            bgaout.BringToFront();
            bgaout.Show();           
        }
    }
}
