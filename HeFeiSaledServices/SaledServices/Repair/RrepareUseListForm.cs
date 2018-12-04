using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Repair
{
    public partial class RrepareUseListForm : Form
    {
        private Form mParentForm;
        public int fromIndex = -1;
        public RrepareUseListForm(Form parentForm)
        {
            InitializeComponent();
            refreshbutton_Click(null, null);
            this.mParentForm = parentForm;

            if (parentForm != null)
            {
                this.choosebutton.Enabled = true;
            }
            else
            {
                this.choosebutton.Enabled = false;
            }
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // string sqlStr = "select top 100 * from fru_smt_out_stock where requester='"+tester+"'";
                string sqlStr = "select Id,mb_brief,material_mpn,material_describe,not_good_place,realNumber,usedNumber from request_fru_smt_to_store_table where _status !='request' and _status !='return' and realNumber !='0' and realNumber != usedNumber and requester='" + LoginForm.currentUser + "'";

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "request_fru_smt_to_store_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = { "ID", "机型", "材料mpn", "物料描述", "不良位置", "已有数量", "使用过的数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                    dataGridView1.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //数量要减一， 同时如果变成0，数量不显示出来
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.material_mpntextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.materialdescribetextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.notgood_placetextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.realNumbertextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();

            try
            {
                int usedNum = Int32.Parse(dataGridView1.SelectedCells[6].Value.ToString().Trim());
                this.usedNumbertextBox.Text = usedNum+"";
              //  this.choosebutton.Enabled = true;
            }
            catch (Exception ex)
            {
               
              //  this.choosebutton.Enabled = false;
                this.usedNumbertextBox.Text = "0";
               // MessageBox.Show("出现严重问题，请联系管理员，不要在进行操作了!!!!!");
               // MessageBox.Show("请确认使用过的数量 是不是 正确的，不对请联系管理员! 如果正确请继续！");
            }                  
        }

        private string totalUseNumber ="";
        //private string totalNumber;
        private void thisNumbertextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                calculateNum();
            }
        }

        private void calculateNum()
        {
            //检查输入的数量不能大于能使用的数量
            if (this.realNumbertextBox.Text == "")
            {
                MessageBox.Show("存储的数量请点击出来！");
                return;
            }

            if (this.thisNumbertextBox.Text == "")
            {
                MessageBox.Show("要使用的数量请填入！");
                return;
            }
            else
            {
                try
                {
                    Int32.Parse(this.thisNumbertextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("本次使用的數量 輸入框 輸入了非法字符，請檢查");
                    return;
                }
            }

            try
            {
                int totalNumber = Int32.Parse(this.realNumbertextBox.Text);
                int usedNumber = Int32.Parse(this.usedNumbertextBox.Text);
                int thisTryToUse = Int32.Parse(this.thisNumbertextBox.Text);

                if (thisTryToUse + usedNumber > totalNumber)
                {
                    MessageBox.Show("输入的数量不能大于能使用的数量!");
                    this.thisNumbertextBox.Clear();
                    this.thisNumbertextBox.Focus();
                }
                else
                {
                    totalUseNumber = (thisTryToUse + usedNumber).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void choosebutton_Click(object sender, EventArgs e)
        {
            if (this.material_mpntextBox.Text == "" || this.realNumbertextBox.Text == "")
            {
                MessageBox.Show("请选择一行做为使用！");
                return;
            }

            //if (this.usedNumbertextBox.Text.Trim() == "-1")
            //{
            //    MessageBox.Show("请联系管理员，有问题！");
            //    return;
            //}

            if (this.thisNumbertextBox.Text == "")
            {
                MessageBox.Show("请输入使用数量！");
                return;
            }
            else
            {
                try
                {
                    Int32.Parse(this.thisNumbertextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("本次使用的數量 輸入框 輸入了非法字符，請檢查");
                    return;
                }
            }

            if (mParentForm is RepairOperationForm)
            {
                ((RepairOperationForm)mParentForm).setPrepareUseDetail(idTextBox.Text.Trim(), mb_brieftextBox.Text.Trim(), material_mpntextBox.Text.Trim(), notgood_placetextBox.Text.Trim(), this.thisNumbertextBox.Text.Trim(), totalUseNumber, fromIndex);
            }
            else if (mParentForm is Test_Outlook.OutLookForm)
            {
                ((Test_Outlook.OutLookForm)mParentForm).setPrepareUseDetail(idTextBox.Text.Trim(), mb_brieftextBox.Text.Trim(), material_mpntextBox.Text.Trim(), notgood_placetextBox.Text.Trim(), this.thisNumbertextBox.Text.Trim(), totalUseNumber, fromIndex);
            }
            else if (mParentForm is Test_Outlook.PackageForm)
            {
                ((Test_Outlook.PackageForm)mParentForm).setPrepareUseDetail(idTextBox.Text.Trim(), mb_brieftextBox.Text.Trim(), material_mpntextBox.Text.Trim(), notgood_placetextBox.Text.Trim(), this.thisNumbertextBox.Text.Trim(), totalUseNumber, fromIndex);
            }
            
            this.Close();
        }

        private void returnMaterialbutton_Click(object sender, EventArgs e)
        {
            if (this.material_mpntextBox.Text.Trim() == "")
            {
                MessageBox.Show("料号为空");
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

                    //查询之前的数量，已数据库的数据为准
                    cmd.CommandText = " select realNumber,usedNumber from request_fru_smt_to_store_table where Id='" + idTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string realNum = "", usedNum = "";
                    if (querySdr.HasRows)
                    {
                        while (querySdr.Read())
                        {
                            realNum = querySdr[0].ToString();
                            usedNum = querySdr[1].ToString();
                        }
                    }
                    querySdr.Close();
                    int realNumInt = 0, usedNumInt = 0;
                    try
                    {
                        realNumInt = Int32.Parse(realNum);
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        usedNumInt = Int32.Parse(usedNum);
                    }
                    catch (Exception ex)
                    {
                        usedNumInt = 0;
                    }
                    cmd.CommandText = "INSERT INTO fru_smt_return_store_record VALUES('"
                        + this.material_mpntextBox.Text.Trim() + "','"
                        +(realNumInt-usedNumInt) + "','"
                        + this.notgood_placetextBox.Text.Trim() + "','"
                        + LoginForm.currentUser + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                        + "" + "','"
                        + "" + "','"
                        + "request" + "','"
                        + idTextBox.Text.Trim() + "')";
                   
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("发送请求成功，请归还物料到库房, 库房才能消除本条申请！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void requestQuerybutton_Click(object sender, EventArgs e)
        {
            this.refreshbutton.Enabled = false;
            this.choosebutton.Enabled = false;
            this.returnMaterialbutton.Enabled = false;

            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // string sqlStr = "select top 100 * from fru_smt_out_stock where requester='"+tester+"'";
                string sqlStr = "select Id,mb_brief,material_mpn,material_describe,not_good_place,realNumber,usedNumber from request_fru_smt_to_store_table where _status ='request' and requester='" + LoginForm.currentUser + "'";

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "request_fru_smt_to_store_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = { "ID", "机型", "材料mpn", "物料描述", "不良位置", "已有数量", "使用过的数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                    dataGridView1.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //数量要减一， 同时如果变成0，数量不显示出来
        }

        private void havedbutton_Click(object sender, EventArgs e)
        {
            this.refreshbutton.Enabled = true;
            if (this.mParentForm != null)
            {
                this.choosebutton.Enabled = true;
            }
            else
            {
                this.choosebutton.Enabled = false;
            }
            
            this.returnMaterialbutton.Enabled = true;
            refreshbutton_Click(null, null);
        }

        private void thisNumbertextBox_Leave(object sender, EventArgs e)
        {
            calculateNum();
        }

        private void statusbutton_Click(object sender, EventArgs e)
        {
            this.refreshbutton.Enabled = false;
            this.choosebutton.Enabled = false;
            this.returnMaterialbutton.Enabled = false;

            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // string sqlStr = "select top 100 * from fru_smt_out_stock where requester='"+tester+"'";
                string sqlStr = "select Id,mb_brief,material_mpn,material_describe,not_good_place,realNumber,_status from request_fru_smt_to_store_table where _status !='request' and requester='" + LoginForm.currentUser + "'";

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "request_fru_smt_to_store_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                string[] hTxt = { "ID", "机型", "材料mpn", "物料描述", "不良位置", "已有数量","状态" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                    dataGridView1.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RrepareUseListForm_Load(object sender, EventArgs e)
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
