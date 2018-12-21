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
    public partial class FaultMaterialOutForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "mb_smt_bga_ng_out_house_table";

        private string ng_tablename = "store_house_ng";

        private ChooseStock chooseStock = new ChooseStock();

        public FaultMaterialOutForm()
        {
            InitializeComponent();

            ngHouseComboBox.SelectedIndex = 0;

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void ngHouseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.idTextBox.Text = "";
            this.mpnTextBox.Text = ""; 
            this.numberTextBox.Text = "";
            this.unitComboBox.Text = "";
            this.housetextBox.Text = "";
            this.placetextBox.Text = "";
            this.declare_numberTextBox.Text= "";
            if (ngHouseComboBox.Text == "主要不良品库")
            {
                ng_tablename = "store_house_ng";
            }
            else//Buffer不良品库
            {
                ng_tablename = "store_house_ng_buffer_mb";
            }

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();           
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.mpnTextBox.Text.Trim() == "" 
                || this.numberTextBox.Text.Trim() == ""
                || this.unitComboBox.Text.Trim() == ""
                || this.housetextBox.Text.Trim() == ""
                || this.placetextBox.Text.Trim() == ""
                || this.declare_numberTextBox.Text.Trim() == ""
               
                )
            {
                MessageBox.Show("需要输入的内容为空!");
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

                    //不良品库, 需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from " + ng_tablename + " where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "0";
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    int outNumber = Int32.Parse(this.numberTextBox.Text);
                    int stockNumber = Int32.Parse(number);
                    if(outNumber > stockNumber)
                    {
                        MessageBox.Show("出库的数量大于库存的数量！");
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "update " + ng_tablename + " set number = '" + (stockNumber - outNumber) + "', mpn='" + this.mpnTextBox.Text.Trim() + "'  where house='" + this.housetextBox.Text + "' and place='" + this.placetextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //插入一条不良品出库记录
                    cmd.CommandText = "INSERT INTO mb_smt_bga_ng_out_house_table VALUES('" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.numberTextBox.Text.Trim() + "','" +
                        DateTime.Now.ToString("yyyy/MM/dd") + "','" +
                        this.unitComboBox.Text.Trim() + "','" +
                        this.declare_numberTextBox.Text.Trim() + "','" +
                        "" + //申请单号可以为空在E0002的时候                 
                        "')";

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("新增成功！");
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.mpnTextBox.Text ="";
            this.numberTextBox.Text = "";
            this.placetextBox.Text = "";
            this.housetextBox.Text = "";
            this.idTextBox.Text = "";
            this.currentNumbertextBox.Text = "";
            this.unitComboBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string sql = "select Id,mpn,in_number,input_date,declare_unit,declare_number from  " + tableName;

                if (this.mpnTextBox.Text.Trim() != "")
                {
                     sql += " where mpn like '%"+this.mpnTextBox.Text.Trim()+"%'";
                }
                cmd.CommandText = sql;
                

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

            string[] hTxt = { "ID", "MPN","数量","时间","单位","报关单号" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
            MessageBox.Show("查询完成！");
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
          
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["in_number"] = this.numberTextBox.Text.Trim();
            dr["declare_unit"] = this.unitComboBox.Text.Trim();
            dr["declare_number"] = this.declare_numberTextBox.Text.Trim();

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
         
            this.mpnTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.numberTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            //3 is time
            this.unitComboBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.declare_numberTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString(); 
        }

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.mpnTextBox.Text == "")
                {
                    MessageBox.Show("MPN的内容不能为空！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select house,place,Id,number from " + ng_tablename + " where mpn like '%" + this.mpnTextBox.Text.Trim() + "%'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "";

                    int rowNum = 0;
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();

                        rowNum++;
                    }
                    querySdr.Close();

                    if (rowNum == 0)
                    {
                        MessageBox.Show("输入的内容查出 0 条记录，请输入准确料号！");
                        mConn.Close();
                        return;
                    }
                    else if (rowNum > 1)
                    {
                        MessageBox.Show("输入的内容查出多条记录，请输入准确料号！");
                        mConn.Close();
                        return;
                    }

                    if (house != "" && place != "")
                    {                        
                        chooseStock.Id = Id;
                        chooseStock.house = house;
                        chooseStock.place = place;
                        chooseStock.number = number;

                        this.currentNumbertextBox.Text = number;

                        this.housetextBox.Text = house;
                        this.placetextBox.Text = place;
                    }
                    
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
             }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<mb_smt_bga_ng_out_house> receiveOrderList = new List<mb_smt_bga_ng_out_house>();
            List<mb_smt_bga_ng_out_houseSum> bagWaitSumList = new List<mb_smt_bga_ng_out_houseSum>();
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT  [mpn],[in_number],[input_date],[declare_unit],[declare_number] FROM [SaledService].[dbo].[mb_smt_bga_ng_out_house_table]";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    mb_smt_bga_ng_out_house temp = new mb_smt_bga_ng_out_house();
                    temp.mpn = querySdr[0].ToString();
                    temp.in_number = querySdr[1].ToString();
                    temp.input_date = querySdr[2].ToString();
                    temp.declare_unit = querySdr[3].ToString();
                    temp.declare_number = querySdr[4].ToString();

                    receiveOrderList.Add(temp);

                    //下面是统计信息
                    if (bagWaitSumList.Count == 0)
                    {
                        if (temp.mpn != null && temp.mpn.Trim() != "")
                        {
                            mb_smt_bga_ng_out_houseSum newTemp = new mb_smt_bga_ng_out_houseSum();
                            newTemp.mpn = temp.mpn;
                            newTemp.outnumber = temp.in_number;
                            bagWaitSumList.Add(newTemp);
                        }
                    }
                    else
                    {
                        bool exist = false;
                        foreach (mb_smt_bga_ng_out_houseSum oldrecord in bagWaitSumList)
                        {
                            if (oldrecord.equals(temp.mpn))
                            {
                                oldrecord.outnumber = Int16.Parse(oldrecord.outnumber) + Int16.Parse(temp.in_number) + "";
                                exist = true;
                                break;
                            }
                        }

                        if (exist == false)
                        {
                            if (temp.mpn != null && temp.mpn.Trim() != "")
                            {
                                mb_smt_bga_ng_out_houseSum newTemp = new mb_smt_bga_ng_out_houseSum();
                                newTemp.mpn = temp.mpn;
                                newTemp.outnumber = temp.in_number;
                                bagWaitSumList.Add(newTemp);
                            }
                        }
                    }
                }
                querySdr.Close();

                foreach (mb_smt_bga_ng_out_houseSum temp in bagWaitSumList)
                {
                    cmd.CommandText = "select number from " + ng_tablename + " where mpn like '%" + temp.mpn + "%'";
                    querySdr = cmd.ExecuteReader();
                 
                    while (querySdr.Read())
                    {
                        temp.leftnumber = querySdr[0].ToString();
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, bagWaitSumList);
        }


        public void generateExcelToCheck(List<mb_smt_bga_ng_out_house> StockCheckList, List<mb_smt_bga_ng_out_houseSum> bagWaitSumList)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent firstsheet = new allContent();
            firstsheet.sheetName = "不良品出库信息";
            firstsheet.titleList = new List<string>();
            firstsheet.contentList = new List<object>();

            firstsheet.titleList.Add("MPN");
            firstsheet.titleList.Add("出库数量");
            firstsheet.titleList.Add("输入日期");
            firstsheet.titleList.Add("单位");
            firstsheet.titleList.Add("报关单号");

            foreach (mb_smt_bga_ng_out_house stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.in_number);
                ct1.Add(Untils.modifyDataFormat(stockcheck.input_date));
                ct1.Add(stockcheck.declare_unit);
                ct1.Add(stockcheck.declare_number);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(firstsheet);

            allContent secondsheet = new allContent();
            secondsheet.sheetName = "统计信息";
            secondsheet.titleList = new List<string>();
            secondsheet.contentList = new List<object>();

            secondsheet.titleList.Add("mpn");
            secondsheet.titleList.Add("出库总数");
            secondsheet.titleList.Add("剩余数量");

            foreach (mb_smt_bga_ng_out_houseSum stockcheck in bagWaitSumList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.outnumber);
                ct1.Add(stockcheck.leftnumber);

                ctest1.contentArray = ct1;
                secondsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(secondsheet);

            Untils.createMulitSheetsUsingNPOI("D:\\不良品报关出库信息" + DateTime.Now.ToString("yyyy-MM-dd").Replace('/', '-') + ".xls", allcontentList);
        }
    }

    public class mb_smt_bga_ng_out_house
    {
        public string mpn;
        public string in_number;
        public string input_date;
        public string declare_unit;
        public string declare_number;
    }

    public class mb_smt_bga_ng_out_houseSum
    {
        public string mpn;
        public string outnumber;
        public string leftnumber;

        public bool equals(string mpn)
        {
            return (this.mpn == mpn);
        }
    }
}
