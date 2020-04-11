using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Repair;
using SaledServices.User;

namespace SaledServices.Store
{
    public partial class RequestFRUSMTStoreForm : Form
    {
        public RequestFRUSMTStoreForm()
        {
            InitializeComponent();

            this.dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            requesterTextBox.Text = LoginForm.currentUser;

            if (Untils.isTimeError(this.dateTextBox.Text.Trim()))
            {
                this.requestbutton.Enabled = false;
            }
        }

        string status = "request";
        private void requestbutton_Click(object sender, EventArgs e)
        {
            if (this.mb_brieftextBox.Text.Trim() == "" 
                || this.not_good_placeTextBox.Text.Trim() == ""
                || this.numberTextBox.Text.Trim() == ""
                || this.materialMpnTextBox.Text.Trim() =="")
            {
                MessageBox.Show("输入框的内容不能为空！");
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

                    //首先查询是否包含未用完的料号，条件状态close 并且usedNumber 小于realnumber
                    cmd.CommandText = "select Id from request_fru_smt_to_store_table where requester='" + UserSelfForm.username+ "' and _status ='close' and usedNumber < realNumber and material_mpn='" + this.materialMpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if(querySdr.HasRows)
                    {
                        MessageBox.Show("你已经有了此料号的材料，不能再申请");
                        querySdr.Close();
                        conn.Close();
                        return;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select Id from request_fru_smt_to_store_table where requester='" + UserSelfForm.username + "' and _status ='request' and material_mpn='" + this.materialMpnTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("你已经申请了此料号的材料，不能再申请");
                        querySdr.Close();
                        conn.Close();
                        return;
                    }
                    querySdr.Close();


                    cmd.CommandText = "INSERT INTO request_fru_smt_to_store_table VALUES('"
                        + this.mb_brieftextBox.Text.Trim() + "','"
                        + this.not_good_placeTextBox.Text.Trim() + "','"
                        + this.materialMpnTextBox.Text.Trim() + "','"
                        + this.materialDescribetextBox.Text.Trim().Replace('\'','_') + "','"
                        + this.numberTextBox.Text.Trim() + "','"
                        +  "0','"//realNumber, 开始为0
                        + this.requesterTextBox.Text.Trim() + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                        + status + "','"
                        + "0" + "','"
                        + "" + "','"
                        + "" + "','"
                        + "" + "')";
                   
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("发送请求成功，请到库房领料！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        class useClassQuery
        {
            public string mb_brief { get; set; }
            public string mpn { get; set; }
            public string material_num { get; set; }
            public string L1 { get; set; }
            public string materialName { get; set; }
            public string materialDescribe { get; set; }
            public string storeNum { get; set; }
            public string house { get; set; }
            public string place { get; set; }
        }

        class useClass
        {
            public string materialName{get;set;}
            public string materialDescribe{get;set;}
            public string storeNum { get; set; }
            public string house { get; set; }
            public string place { get; set; }
        }

        private void not_good_placeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.mb_brieftextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先MB简称的内容");
                    this.mb_brieftextBox.Focus();
                    return;
                }

                dataGridView.DataSource = null;

                string not_good_place = this.not_good_placeTextBox.Text.Trim();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    List<useClass> list = new List<useClass>();
                    List<useClass> reallist = new List<useClass>();
                    if (this.mb_brieftextBox.Text != "")
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8,material_describe from " + Constlist.table_name_LCFC_MBBOM + " where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "' and vendor ='"+this.comboBoxVendor.Text.Trim()+"'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        
                        while (querySdr.Read())
                        {
                            useClass useclass = new useClass();
                            string material_mpn = querySdr[0].ToString();
                            string temp = querySdr[1].ToString().Trim();
                            string matertialDes = querySdr[9].ToString();
                           
                            if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[2].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[3].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[4].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[5].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[6].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[7].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[8].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            }
                            
                        }
                        querySdr.Close();
                    }

                    if (list.Count == 0)
                    {
                        error = true;
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        foreach (useClass temp in list)
                        {
                            cmd.CommandText = "select number,house,place from store_house where mpn ='" + temp.materialName + "' and number >0";
                            SqlDataReader querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                temp.storeNum = querySdr[0].ToString();
                                temp.house = querySdr[1].ToString();
                                temp.place = querySdr[2].ToString();
                                reallist.Add(temp);
                            }
                            querySdr.Close();
                        }

                        dataGridView.DataSource = reallist;
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void checkRequestListbutton_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(null);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.Show();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                return;
            }
            this.materialMpnTextBox.Text = dataGridView.SelectedCells[0].Value.ToString();
            this.materialDescribetextBox.Text = dataGridView.SelectedCells[1].Value.ToString();
        }

        private void mb_brieftextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                not_good_placeTextBox.Focus();
                not_good_placeTextBox.SelectAll();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void materialNoQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.materialNoQuery.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入料号的内容");
                    this.materialNoQuery.Focus();
                    return;
                }

                dataGridView1.DataSource = null;
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    List<useClassQuery> list = new List<useClassQuery>();
                   
                    cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8,material_describe,vendor,mb_brief,MPN,material_num from " + Constlist.table_name_LCFC_MBBOM + " where material_mpn like '%" + this.materialNoQuery.Text.Trim() + "%'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        useClassQuery useclass = new useClassQuery();
                        string material_mpn = querySdr[0].ToString();
                        string temp = querySdr[1].ToString().Trim();
                        string matertialDes = querySdr[9].ToString();

                        useclass.materialName = material_mpn;
                        useclass.materialDescribe = matertialDes;
                        useclass.mb_brief = querySdr[11].ToString();
                        useclass.mpn = querySdr[12].ToString();
                        useclass.material_num = querySdr[13].ToString();
                        useclass.L1 = querySdr[1].ToString();
                        list.Add(useclass);
                    }
                    querySdr.Close();

                    if (list.Count == 0)
                    {
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        foreach (useClassQuery temp in list)
                        {
                            cmd.CommandText = "select number,house,place from store_house where mpn ='" + temp.materialName + "'";
                            querySdr = cmd.ExecuteReader();
                            string storeNum = "0";
                            while (querySdr.Read())
                            {
                                temp.storeNum = querySdr[0].ToString();
                                temp.house = querySdr[1].ToString();
                                temp.place = querySdr[2].ToString();
                            }

                            querySdr.Close();
                        }

                        dataGridView1.DataSource = list;
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void materialDesQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.materialDesQuery.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入料号描述的内容");
                    this.materialDesQuery.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                dataGridView1.DataSource = null;
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    List<useClassQuery> list = new List<useClassQuery>();

                    cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8,material_describe,vendor,mb_brief,MPN,material_num from " + Constlist.table_name_LCFC_MBBOM + " where material_describe like '%" + this.materialDesQuery.Text.Trim() + "%'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        useClassQuery useclass = new useClassQuery();
                        string material_mpn = querySdr[0].ToString();
                        string temp = querySdr[1].ToString().Trim();
                        string matertialDes = querySdr[9].ToString();

                        useclass.materialName = material_mpn;
                        useclass.materialDescribe = matertialDes;
                        useclass.mb_brief = querySdr[11].ToString();
                        useclass.mpn = querySdr[12].ToString();
                        useclass.material_num = querySdr[13].ToString();
                        useclass.L1 = querySdr[1].ToString();
                        list.Add(useclass);
                    }
                    querySdr.Close();

                    if (list.Count == 0)
                    {
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        foreach (useClassQuery temp in list)
                        {
                            cmd.CommandText = "select number,house,place from store_house where mpn ='" + temp.materialName + "'";
                            querySdr = cmd.ExecuteReader();
                            string storeNum = "0";
                            while (querySdr.Read())
                            {
                                temp.storeNum = querySdr[0].ToString();
                                temp.house = querySdr[1].ToString();
                                temp.place = querySdr[2].ToString();
                            }
                            querySdr.Close();
                        }

                        dataGridView1.DataSource = list;
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void RequestFRUSMTStoreForm_Load(object sender, EventArgs e)
        {

        }
    }
}

