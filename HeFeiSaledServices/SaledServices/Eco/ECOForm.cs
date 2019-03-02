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
    public partial class EcoForm : Form
    {
        private String tableName = "eco_table";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public EcoForm()
        {
            InitializeComponent();
            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }

            this.inputerTextBox.Text = LoginForm.currentUser;
            this.inputDateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void add_Click(object sender, EventArgs e)
        {

            if (this.track_serial_no.Text.Trim() == ""
                  || this.mpnTextBox.Text.Trim() == ""
                ||this.custom_material_noTextBox.Text.Trim() == "")
            {
                MessageBox.Show("内容为空");
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
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.track_serial_no.Text.Trim() + "','" +                      
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.custom_material_noTextBox.Text.Trim() + "','" +
                        this.currentVersionTextBox.Text.Trim() + "','" +
                        this.newVersionTextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.inputDateTextBox.Text.Trim() + "')";

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //更新ECO站别
                    cmd.CommandText = "update stationInformation set station = 'ECO', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                               + "where track_serial_no = '" + this.track_serial_no.Text + "'";
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

                string sqlStr =  "select top 10 * from " + tableName;

                //if (material_mpnTextBox.Text.Trim() != "")
                //{
                //    if (!sqlStr.Contains("where"))
                //    {
                //        sqlStr += " where material_mpn like '%" + material_mpnTextBox.Text.Trim() + "%' ";
                //    }
                //    else
                //    {
                //        sqlStr += " and material_mpn like '%" + material_mpnTextBox.Text.Trim() + "%' ";
                //    }
                //}

                if (custom_material_noTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where material_vendor_pn= '" + custom_material_noTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and material_vendor_pn= '" + custom_material_noTextBox.Text.Trim() + "' ";
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

            string[] hTxt = {"ID","日期","MB简称","材料厂商PN","材料MPN","描述","价格"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            //DataTable dt = ds.Tables[tableName];
            //sda.FillSchema(dt, SchemaType.Mapped);
            //DataRow dr = dt.Rows.Find(this.newVersionTextBox.Text.Trim());          
            //dr["material_describe"] = this.currentVersionTextBox.Text.Trim();
            //dr["MPN"] = this.custom_material_noTextBox.Text.Trim();
            //dr["material_mpn"] = this.material_mpnTextBox.Text.Trim();
            //dr["material_box_place"] = this.descriptionTextBox.Text.Trim();
            //dr["mb_brief"] = this.mpnTextBox.Text.Trim();
            //dr["_date"] = this.track_serial_no.Text.Trim();

            //SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            //sda.Update(dt);
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
                MessageBox.Show("删除成功");
                this.newVersionTextBox.Text = "";
                this.currentVersionTextBox.Text = "";
                this.custom_material_noTextBox.Text = "";
                this.mpnTextBox.Text = "";
                this.track_serial_no.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.CurrentRow == null)
            //{
            //    return;
            //}

            //this.newVersionTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            //this.track_serial_no.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //this.mpnTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            //this.custom_material_noTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            //this.material_mpnTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            //this.descriptionTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            //this.currentVersionTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();            
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

        private void material_mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }

        private void track_serial_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.track_serial_no.Text.Trim() == "")
                {
                    this.track_serial_no.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    error = true;
                    return;
                }
                this.track_serial_no.Text = this.track_serial_no.Text.ToUpper();//防止输入小写字符
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select _8sCode from need_to_lock where track_serial_no='" + this.track_serial_no.Text.Trim() + "' and isLock='true'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此序列号需要分析但是已经锁定，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_no.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string stationInfo = "";
                    while (querySdr.Read())
                    {
                        stationInfo = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (stationInfo == "收货")
                    {
                        this.add.Enabled = true;
                    }
                    else
                    {
                        cmd.CommandText = "select Id from cidRecord where track_serial_no='" + this.track_serial_no.Text.Trim() + "' and  custom_res_type like '%不可修%'";
                        querySdr = cmd.ExecuteReader();
                        string cidExist = "";
                        while (querySdr.Read())
                        {
                            cidExist = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (cidExist != "")
                        {
                            MessageBox.Show("此序列号已经在CID不可修中，不能走下面的流程！");
                            mConn.Close();
                            this.add.Enabled = false;
                            return;
                        }

                        MessageBox.Show("此序列号的站别已经在:" + stationInfo + "，不能走下面的流程！");
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }

                    //查询是不是从待维修库出来的，如果不是，则不能进行下面的内容
                    cmd.CommandText = "select Id from wait_repair_out_house_table where track_serial_no='" + this.track_serial_no.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        MessageBox.Show("此序列号的不是从待维修库里面出来的:【" + this.track_serial_no.Text.Trim() + "】，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }
                    else
                    {
                        this.add.Enabled = true;
                    }
                    querySdr.Close();
                    //end

                    cmd.CommandText = "select custommaterialNo, mpn from DeliveredTable where track_serial_no='" + this.track_serial_no.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();                
                    while (querySdr.Read())
                    {
                        this.custom_material_noTextBox.Text = querySdr[0].ToString();
                        this.mpnTextBox.Text = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }  
            }
        }
    }
}
