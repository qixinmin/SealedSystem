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
    public partial class MB_InSheetForm : Form
    {
        private String tableName = "mb_in_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private ChooseStock chooseStock = new ChooseStock();

        public MB_InSheetForm()
        {
            InitializeComponent();

            inputerTextBox.Text = LoginForm.currentUser;
            this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

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

                //1 来源 2.客户故障	3.保内/保外	4 .客责描述
                cmd.CommandText = "select distinct buy_order_serial_no from stock_in_sheet where _status = 'open' and material_type in ('MB')";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.buy_order_serial_noComboBox.Items.Add(temp);
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
            if (this.mpnTextBox.Text.Trim() == "" || this.buy_order_serial_noComboBox.Text.Trim() =="")
            {
                MessageBox.Show("MPN 或订单号为空！");
                return;
            }

            if (this.stock_placetextBox.Text.Trim() == "")
            {
                MessageBox.Show("库位为空，请检查！");
                return;
            }           

            if (this.track_serial_noTextBox.Text.Trim() == "")
            {
                MessageBox.Show("跟踪条码为空，请检查！");
                return;
            }

            if (chooseStock.house == "")
            {
                MessageBox.Show("请选择库位为空，而不要手动输入，请检查！");
                this.stock_placetextBox.Text = "";
                this.stock_placetextBox.Focus();
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

                    SqlDataReader querySdr=null;
                    if (this.custom_serial_noTextBox.Text != "")
                    {
                        cmd.CommandText = "select * from " + tableName + " where custom_serial_no='" + this.custom_serial_noTextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if(querySdr.HasRows)
                        {
                            conn.Close();
                            MessageBox.Show("此板子客户序号已经存在数据库中了，请检查是否重复！");
                            return;
                        }
                        querySdr.Close();
                    }

                    cmd.CommandText = "select number, stock_in_num from stock_in_sheet where buy_order_serial_no='" + this.buy_order_serial_noComboBox.Text.Trim()
                                        + "' and mpn='" + this.mpnTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string status = "open";
                    int total_number, in_number_int = 0, this_enter_number = 0;
                    while (querySdr.Read())
                    {
                        string number = querySdr[0].ToString();
                        string in_number = querySdr[1].ToString();
                        total_number = Int32.Parse(number);
                        in_number_int = Int32.Parse(in_number);
                        this_enter_number = Int32.Parse(this.stock_in_numTextBox.Text.Trim());
                        if (in_number_int + this_enter_number > total_number)
                        {
                            MessageBox.Show("输入数量大于订单数量, 查看是否由多次入库引起的!");
                            querySdr.Close();
                            conn.Close();
                            return;
                        }
                        else if (in_number_int + this_enter_number == total_number)
                        {
                            status = "close";
                        }
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.buy_order_serial_noComboBox.Text.Trim() + "','" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.buy_typeTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.material_typeTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.orderNumberTextBox.Text.Trim() + "','" +
                        this.stock_in_numTextBox.Text.Trim() + "','" +
                        this.stock_placetextBox.Text.Trim() + "','" +
                        this.pricePerTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +
                        this.mb_brieftextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serial_noTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.notetextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +
                        DateTime.Now.ToString("yyyy/MM/dd") + "')";

                    cmd.ExecuteNonQuery();

                    //更新采购表里面的数量与状态
                    cmd.CommandText = "update stock_in_sheet set _status = '" + status + "',stock_in_num = '" + (in_number_int + this_enter_number) + "' where mpn='" + this.vendormaterialNoTextBox.Text.Trim() + "' and buy_order_serial_no='" + this.buy_order_serial_noComboBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();

                    //更新库存占用记录，保证库房的信息被更新
                    cmd.CommandText = "select number house from store_house where mpn='" + this.vendormaterialNoTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string stockNumber = "";
                    if (querySdr.HasRows)
                    {
                        while (querySdr.Read())
                        {
                            chooseStock.number = querySdr[0].ToString();
                        }
                    }
                    else
                    {
                        chooseStock.number = "0";
                    }
                    querySdr.Close();
                    //if (this.stock_placetextBox.Enabled == false)
                    {
                        //加上历史数据
                        int total = Int32.Parse(chooseStock.number) + Int32.Parse(stock_in_numTextBox.Text.Trim());
                        stockNumber = total + "";
                    }
                    //else
                    //{
                    //    stockNumber = this.stock_in_numTextBox.Text;
                    //}
                    cmd.CommandText = "update store_house set mpn = '" + this.vendormaterialNoTextBox.Text.Trim() + "',number = '" + stockNumber + "' where house='"+chooseStock.house+"' and place='"+chooseStock.place+"'";
                    cmd.ExecuteNonQuery();

                    //清除历史缓存，保证下次选择是新的
                    chooseStock.house = "";
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                doQueryAfterSelection();
                clearInputText();
                MessageBox.Show("收货成功！");
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

            string[] hTxt = { "ID", "采购订单编号", "厂商", "采购类别", "客户别", "材料大类", "MPN", "厂商料号", "描述", "订单数量", "入库数量", "库位","单价", "是否报关", "MB简称", "客户序号", "厂商序号","跟踪条码", "备注", "输入人", "日期" };
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

            dr["buy_order_serial_no"] = this.buy_order_serial_noComboBox.Text.Trim();
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["buy_type"] = this.buy_typeTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["material_type"] = this.material_typeTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["vendormaterialNo"] = this.vendormaterialNoTextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
             dr["number"] = this.orderNumberTextBox.Text.Trim();
            dr["input_number"] = this.stock_in_numTextBox.Text.Trim();
             dr["stock_place"]= this.stock_placetextBox.Text.Trim();

            dr["pricePer"] = this.pricePerTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();

            dr["mb_brief"] = this.mb_brieftextBox.Text.Trim();
           
            dr["custom_serial_no"] = this.custom_serial_noTextBox.Text.Trim();
            dr["vendor_serial_no"] = this.vendor_serial_noTextBox.Text.Trim();
            dr["track_serial_no"] = this.track_serial_noTextBox.Text.Trim();
           
            dr["note"] = this.notetextBox.Text.Trim();      
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
            this.buy_order_serial_noComboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.buy_typeTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.material_typeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.describeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.orderNumberTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.stock_in_numTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.stock_placetextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.pricePerTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.isDeclareTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.custom_serial_noTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.vendor_serial_noTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();

            this.track_serial_noTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            this.notetextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            this.inputerTextBox.Text = dataGridView1.SelectedCells[19].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
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

        private void simulateMpnEnter()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select buy_order_serial_no, vendor,buy_type,product,material_type,vendormaterialNo, describe,pricePer,isdeclare,number from stock_in_sheet where mpn='" + this.mpnTextBox.Text.Trim() + "' and material_type in ('MB') and buy_order_serial_no='" + this.buy_order_serial_noComboBox.Text.Trim() + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    this.buy_order_serial_noComboBox.Text = querySdr[0].ToString();
                    this.vendorTextBox.Text = querySdr[1].ToString();
                    this.buy_typeTextBox.Text = querySdr[2].ToString();
                    this.productTextBox.Text = querySdr[3].ToString();
                    this.material_typeTextBox.Text = querySdr[4].ToString();
                    this.vendormaterialNoTextBox.Text = querySdr[5].ToString();
                    this.describeTextBox.Text = querySdr[6].ToString();
                    this.pricePerTextBox.Text = querySdr[7].ToString();
                    this.isDeclareTextBox.Text = querySdr[8].ToString();
                    this.orderNumberTextBox.Text = querySdr[9].ToString();
                }
                querySdr.Close();

                cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.vendormaterialNoTextBox.Text.Trim() + "'";
                querySdr = cmd.ExecuteReader();
                string house = "", place = "",Id="", number="";
                while (querySdr.Read())
                {
                    house = querySdr[0].ToString();
                    place = querySdr[1].ToString();
                    Id = querySdr[2].ToString();
                    number = querySdr[3].ToString();
                }
                querySdr.Close();

                if (house != "" && place != "")
                {
                    
                    this.stock_placetextBox.Text = house + "," + place;
                    this.stock_placetextBox.Enabled = false;
                   
                    chooseStock.Id = Id;
                    chooseStock.house = house;
                    chooseStock.place = place;
                    chooseStock.number = number;
                }
                else
                {
                   this.stock_placetextBox.Enabled = true;
                }

                //根据信息查询MB简述
                cmd.CommandText = "select mb_brief from MBMaterialCompare where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    this.mb_brieftextBox.Text = querySdr[0].ToString();
                }
                querySdr.Close();
                

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            clearInputText();
        }

        private void clearInputText()
        {
            this.custom_serial_noTextBox.Text = "";
            this.track_serial_noTextBox.Text = "";
            this.vendor_serial_noTextBox.Text = "";
            this.notetextBox.Text = "";
        }

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.stock_placetextBox.Text = "";//此次需要事前清除
                simulateMpnEnter();
            }
        }

        private void buy_order_serial_noComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.buy_order_serial_noComboBox.Text;
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
                this.dataGridViewToReturn.DataSource = null;
                dataGridViewToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                //加入条件判断，只显示未收完的货物
                cmd.CommandText = "select material_type,mpn, vendormaterialNo, number, stock_in_num from stock_in_sheet where buy_order_serial_no='" + this.buy_order_serial_noComboBox.Text + "' and _status='open' and material_type in ('MB')";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "stock_in_sheet");
                dataGridViewToReturn.DataSource = ds.Tables[0];
                dataGridViewToReturn.RowHeadersVisible = false;
                mConn.Close();

                string[] hTxt = { "材料大类", "MPN", "厂商料号", "订单数量", "入库数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.DefaultCellStyle.BackColor = Color.Red;
                dc.Name = "差数";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "差数";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewToReturn.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridViewToReturn.Rows)
                {
                    try
                    {
                        int oNum = Int32.Parse(dr.Cells["订单数量"].Value.ToString());
                        int rNum = Int32.Parse(dr.Cells["入库数量"].Value.ToString());

                        if (oNum - rNum == 0)
                        {
                            dr.Cells["差数"].Style.BackColor = Color.Green;
                        }
                        dr.Cells["差数"].Value = (oNum - rNum) + " ";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridViewToReturn.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewToReturn.CurrentRow == null)
            {
                return;
            }

            this.mpnTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
            this.stock_placetextBox.Text = "";
            simulateMpnEnter();
        }

        private void stock_in_numTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;

            this.stock_placetextBox.Text = chooseStock.house + "," + chooseStock.place;
        }
        
        private void stock_placetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //打开选择界面，并把结果返回到本界面来
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this);
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //22/23
                if (custom_serial_noTextBox.Text.Length != 22 && this.custom_serial_noTextBox.Text.Length != 23)
                {
                    MessageBox.Show("客户序号长度不对");
                    this.custom_serial_noTextBox.Focus();
                    this.custom_serial_noTextBox.SelectAll();
                    return;
                }
                this.vendor_serial_noTextBox.SelectAll();
                this.vendor_serial_noTextBox.Focus();
            }
        }

        private void vendor_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //13
                if (vendor_serial_noTextBox.Text.Length != 13)
                {
                    MessageBox.Show("厂商序号长度不对");
                    this.vendor_serial_noTextBox.Focus();
                    this.vendor_serial_noTextBox.SelectAll();
                    return;
                }

                this.track_serial_noTextBox.SelectAll();
                this.track_serial_noTextBox.Focus();
            }
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //16
                if (track_serial_noTextBox.Text.Length != 16)
                {
                    MessageBox.Show("厂商序号长度不对");
                    this.track_serial_noTextBox.Focus();
                    this.track_serial_noTextBox.SelectAll();
                    return;
                }
            }
        }
    }
}
