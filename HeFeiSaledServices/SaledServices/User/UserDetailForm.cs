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
    public partial class UserDetailForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "users";


        public UserDetailForm()
        {
            InitializeComponent();
        }

        private void UserDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm form = this.MdiParent as MainForm;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.userNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("用户名内容为空!");
                return;
            }

            if (this.passwordTextBox.Text.Trim() == "")
            {
                MessageBox.Show("密码内容为空!");
                return;
            }

            if (this.super_checkBox.Checked == false &&
                this.bgaCheckBox.Checked == false &&
                this.repairCheckBox.Checked == false &&
                this.test_allCheckBox.Checked == false &&
                this.test1CheckBox.Checked == false &&
                this.test2CheckBox.Checked == false &&
                this.receive_returnCheckBox.Checked == false &&
                this.outlookCheckBox.Checked == false &&
                this.storeCheckBox.Checked == false &&
                this.runningcheckBox.Checked == false && 
                this.obecheckBox.Checked == false)
            {
                MessageBox.Show("请选择至少一个权限!");
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
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" 
                        + this.userNameTextBox.Text.Trim() + "','"
                        + this.workIdTextBox.Text.Trim() + "','"
                        + this.passwordTextBox.Text.Trim() + "','"
                        + this.super_checkBox.Checked + "','"
                        + this.bgaCheckBox.Checked + "','"
                        + this.repairCheckBox.Checked + "','"
                        + this.test_allCheckBox.Checked + "','"
                        + this.test1CheckBox.Checked + "','"
                        + this.test2CheckBox.Checked + "','"
                        + this.receive_returnCheckBox.Checked + "','"
                        + this.storeCheckBox.Checked + "','"
                        + this.outlookCheckBox.Checked + "','"
                        + this.runningcheckBox.Checked + "','"
                        + this.obecheckBox.Checked + 
                        "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("添加成功！");
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
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName;
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

            string[] hTxt = { "ID", "用户名","工号","密码", "超级管理员", "BGA", "维修", "测试ALL","测试1","测试2","收还货","库存","外观","Running","OBE" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }


        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
            dr["username"] = this.userNameTextBox.Text.Trim();
            dr["workId"] = this.workIdTextBox.Text.Trim();
            dr["_password"] = this.passwordTextBox.Text.Trim();
            dr["super_manager"] = this.super_checkBox.Checked;
            dr["bga"] = this.bgaCheckBox.Checked;
            dr["repair"] = this.repairCheckBox.Checked;
            dr["test_all"] = this.test_allCheckBox.Checked;
            dr["test1"] = this.test1CheckBox.Checked;
            dr["test2"] = this.test2CheckBox.Checked;
            dr["receive_return"] = this.receive_returnCheckBox.Checked;
            dr["store"] = this.storeCheckBox.Checked;
            dr["outlook"] = this.outlookCheckBox.Checked;
            dr["running"] = this.runningcheckBox.Checked;
            dr["obe"] = this.obecheckBox.Checked;

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
            this.userNameTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.workIdTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.passwordTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();

            if (dataGridView1.SelectedCells[4].Value.ToString() == "True")
            {
                this.super_checkBox.Checked = true;
            }
            else
            {
                this.super_checkBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[5].Value.ToString() == "True")
            {
                this.bgaCheckBox.Checked = true;
            }
            else
            {
                this.bgaCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[6].Value.ToString() == "True")
            {
                this.repairCheckBox.Checked = true;
            }
            else
            {
                this.repairCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[7].Value.ToString() == "True")
            {
                this.test_allCheckBox.Checked = true;
            }
            else
            {
                this.test_allCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[8].Value.ToString() == "True")
            {
                this.test1CheckBox.Checked = true;
            }
            else
            {
                this.test1CheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[9].Value.ToString() == "True")
            {
                this.test2CheckBox.Checked = true;
            }
            else
            {
                this.test2CheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[10].Value.ToString() == "True")
            {
                this.receive_returnCheckBox.Checked = true;
            }
            else
            {
                this.receive_returnCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[11].Value.ToString() == "True")
            {
                this.storeCheckBox.Checked = true;
            }
            else
            {
                this.storeCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[12].Value.ToString() == "True")
            {
                this.outlookCheckBox.Checked = true;
            }
            else
            {
                this.outlookCheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[13].Value.ToString() == "True")
            {
                this.runningcheckBox.Checked = true;
            }
            else
            {
                this.runningcheckBox.Checked = false;
            }

            if (dataGridView1.SelectedCells[14].Value.ToString() == "True")
            {
                this.obecheckBox.Checked = true;
            }
            else
            {
                this.obecheckBox.Checked = false;
            }    
        }
    }
}
