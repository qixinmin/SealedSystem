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
    public partial class ReturnStoreForm : Form
    {
        string tableName = "returnStore";

        private string vendorStr = "";
        private string productStr = "";

        //Dictionary<string, string> myDictionary = new Dictionary<string, string>();

        public ReturnStoreForm()
        {
            InitializeComponent();
            loadToReturnInformation();
            this.inputUserTextBox.Text = LoginForm.currentUser;

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }
        
        public void loadToReturnInformation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct vendor from receiveOrder";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.vendorComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct product from receiveOrder";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.productComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct _type from customResponsibilityType";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_res_typeComboBox.Items.Add(temp);
                        //myDictionary.Add(temp, temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct _status from returnStoreStatus";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.statusComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct responsibility_describe from customResponsibility";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.response_describeComboBox.Items.Add(temp);
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

        private void vendorComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.vendorStr = this.vendorComboBox.Text;
            doQueryAfterSelection();
        }

        private void productComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.productStr = this.productComboBox.Text;
            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            if (this.vendorStr == "" || this.productStr == "")
            {
                return;
            }
            
            try
            {
                dataGridViewToReturn.DataSource = null;
                dataGridViewToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //start

                //在更新收货表的同时，需要同时更新导入的表格收货数量，不然数据会乱掉
                cmd.CommandText = "select _status, ordernum, receivedNum, returnNum,cid_number,Id from receiveOrder where vendor='" + vendorStr
                    + "' and product ='" + productStr + "' and _status = 'close'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                List<string> finishID = new List<string>();
                int receivedNum = 0, cidNum = 0, returnNum=0;
                while (querySdr.Read())
                {
                    returnNum = Int32.Parse(querySdr[3].ToString());
                    cidNum = Int32.Parse(querySdr[4].ToString());
                    receivedNum = Int32.Parse(querySdr[2].ToString());

                    if (receivedNum == cidNum || (receivedNum == cidNum+returnNum))
                    {
                        finishID.Add(querySdr[5].ToString());
                    }
                }
                querySdr.Close();

                foreach (string id in finishID)
                {
                    cmd.CommandText = "update receiveOrder set _status = 'return'"
                                + "where Id = '" + id + "'";
                    cmd.ExecuteNonQuery();
                }
                //end


                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,receivedNum,returnNum,cid_number,ordertime from receiveOrder where vendor='" + vendorStr 
                    + "' and product ='" + productStr + "' and _status = 'close'";                

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
                dataGridViewToReturn.DataSource = ds.Tables[0];
                dataGridViewToReturn.RowHeadersVisible = false;


                string[] hTxt = { "订单编号", "客户料号", "MB简称", "收货数量", "还货数量", "CID","制单时间" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.Name = "TAT";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "TAT";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewToReturn.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridViewToReturn.Rows)
                {
                    try
                    {
                        DateTime dt1 = Convert.ToDateTime(dr.Cells["制单时间"].Value.ToString());
                        DateTime dt2 = DateTime.Now;

                        TimeSpan ts = dt2.Subtract(dt1);
                        int overdays = ts.Days;

                        dr.Cells["TAT"].Value = overdays + " ";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                mConn.Close();

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

        private void simulateEnter(string custommaterialNo, string orderNo, string tat)
        {
            try
            {
                this.return_file_noTextBox.Text = generateFileNo();
                this.ordernoTextBox.Text =orderNo;
                this.custommaterialNoTextBox.Text = custommaterialNo;
                this.return_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                if (Untils.isTimeError(this.return_dateTextBox.Text.Trim()))
                {
                    this.returnStore.Enabled = false;
                }

                this.tatTextBox.Text = tat;


                //根据输入的客户料号，查询MB物料对照表找到dpk状态与mpn
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select storehouse from receiveOrder where vendor='" + vendorStr
                    + "' and product ='" + productStr + "' and _status = 'close' and orderno ='" + this.ordernoTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.storehouseTextBox.Text = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    //列出所有可用的flexid给flexidcomboBox
                    cmd.CommandText = "select flex_id from flexidRecord where custom_order='" + ordernoTextBox.Text
                    + "' and custommaterialNo ='" + custommaterialNoTextBox.Text + "' and _status = ''";
                    querySdr = cmd.ExecuteReader();
                    flexidcomboBox.Items.Clear();
                    while (querySdr.Read())
                    {
                        flexidcomboBox.Items.Add(querySdr[0].ToString());
                    }
                    querySdr.Close();


                    cmd.CommandText = "select dpk_type, mpn, replace_custom_materialNo from MBMaterialCompare where custommaterialNo = '"
                        + this.custommaterialNoTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.dpkpnTextBox.Text = querySdr[0].ToString();
                        this.bommpnTextBox.Text = querySdr[1].ToString();
                        this.replace_custom_materialNotextBox.Text = querySdr[2].ToString();
                    }
                    querySdr.Close();

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void dataGridViewToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewToReturn.CurrentRow == null)
            {
                return;
            }
            clearInputData();
            try
            {
                this.return_file_noTextBox.Text = generateFileNo();
                this.ordernoTextBox.Text = dataGridViewToReturn.SelectedCells[0].Value.ToString();
                this.custommaterialNoTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
                this.return_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                if (Untils.isTimeError(this.return_dateTextBox.Text.Trim()))
                {
                    this.returnStore.Enabled = false;
                }

                this.tatTextBox.Text = dataGridViewToReturn.SelectedCells[5].Value.ToString();


                //根据输入的客户料号，查询MB物料对照表找到dpk状态与mpn
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select storehouse from receiveOrder where vendor='" + vendorStr
                    + "' and product ='" + productStr + "' and _status = 'close' and orderno ='" + this.ordernoTextBox.Text.Trim() + "'"; 

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.storehouseTextBox.Text = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    //列出所有可用的flexid给flexidcomboBox
                    cmd.CommandText = "select flex_id from flexidRecord where custom_order='" + ordernoTextBox.Text
                    + "' and custommaterialNo ='" + custommaterialNoTextBox.Text + "' and _status = ''";
                    querySdr = cmd.ExecuteReader();
                    flexidcomboBox.Items.Clear();
                    while (querySdr.Read())
                    {
                        flexidcomboBox.Items.Add(querySdr[0].ToString());
                    }
                    querySdr.Close();
                    

                    cmd.CommandText = "select dpk_type, mpn, replace_custom_materialNo from MBMaterialCompare where custommaterialNo = '"
                        + this.custommaterialNoTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.dpkpnTextBox.Text = querySdr[0].ToString();
                        this.bommpnTextBox.Text = querySdr[1].ToString();
                        this.replace_custom_materialNotextBox.Text = querySdr[2].ToString();
                    }
                    querySdr.Close();

                    mConn.Close();                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private string generateFileNo()
        {
            string retStr = "";
            string preStr = this.vendorComboBox.Text.Trim() + this.productComboBox.Text.Trim() + DateTime.Now.ToString("yyMMdd");

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct return_file_no from returnStore where vendor ='" + this.vendorComboBox.Text.Trim() + "' and product ='" + this.productComboBox.Text.Trim() + "'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string subRetStr= "";
                while (querySdr.Read())
                {                    
                    string queryStr = querySdr[0].ToString();
                    if (queryStr != "")
                    {
                        subRetStr = queryStr.Substring(preStr.Length, 2);
                    }
                }
                querySdr.Close();

                if (subRetStr == "")
                {
                    retStr = preStr + "01";
                }
                else
                {
                    int last = Int32.Parse(subRetStr);

                    if (checkBoxMakeNew.Checked)
                    {
                        last += 1;
                    }

                    if (last < 10)
                    {
                        retStr = preStr + "0" + last;
                    }
                    else
                    {
                        retStr = preStr + last;
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return retStr;
        }

        private void returnStore_Click(object sender, EventArgs e)
        {
            if (checkIsNull())
            {
                MessageBox.Show("输入的内容为空, 请检查！");
                return;                
            }
            if (this.flexidcomboBox.Text.Trim() == "")
            {
                MessageBox.Show("FlexId的内容为空, 请检查！");
                return;
            }

            if (this.input8sTextBox.Text.Trim() == "")
            {
                MessageBox.Show("输入的8s内容为空, 请检查！");
                this.input8sTextBox.Focus();
                return;
            }

            if (this.matertiallibMpnTextBox.Text.Trim() != this.bommpnTextBox.Text.Trim())
            {
                MessageBox.Show("收获表中的mpn与物料对照表的mpn不一致，请检查！");
                this.track_serial_noTextBox.Text = "";
                this.track_serial_noTextBox.Focus();
                this.custom_serial_noTextBox.Text = "";
                return;
            }

            if (this.input8sTextBox.Text.Trim() != this.custom_serial_noTextBox.Text.Trim())
            {
                MessageBox.Show("收货的8s与输入的8s不一致，请检查！");
                return;
            }

            if(!this.custommaterialNoTextBox.Text.Trim().Contains(this.inputCustomMaterialtextBox.Text.Trim()) 
                ||!this.custommaterialNoTextBox.Text.Trim().EndsWith(this.inputCustomMaterialtextBox.Text.Trim()))
            {
                MessageBox.Show("选择的客户料号与输入的客户料号不一致，请检查！");
                return;
            }

            //if (statusComboBox.Text.Trim() == "不良品")
            //{
            //    if (this.lenovo_maintenance_noTextBox.Text == "" || this.lenovo_repair_noTextBox.Text == "")
            //    {
            //        MessageBox.Show("联想的相关信息不能为空！");
            //        return;
            //    }
            //}

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader querySdr = null;
                    //if (statusComboBox.Text.Trim() == "不良品")    

                    cmd.CommandText = "select custom_order from flexidRecord where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    string customOrder = "";
                    while (querySdr.Read())
                    {
                        customOrder = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (customOrder != ordernoTextBox.Text.Trim())
                    {
                        MessageBox.Show("此序列号的订单编号是" + customOrder + "，跟选择的订单编号应该不符合！");
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +                        
                        this.vendorComboBox.Text.Trim() + "','" +
                        this.productComboBox.Text.Trim() + "','" +
                        this.return_file_noTextBox.Text.Trim() + "','" +
                        this.storehouseTextBox.Text.Trim() + "','" +
                        this.return_dateTextBox.Text.Trim() + "','" +
                        this.ordernoTextBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.dpkpnTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serail_noTextBox.Text.Trim() + "','" +
                        this.bommpnTextBox.Text.Trim() + "','" +
                        this.statusComboBox.Text.Trim() + "','" +
                        this.custom_res_typeComboBox.Text.Trim() + "','" +
                        this.response_describeComboBox.Text.Trim() + "','"+
                        this.tatTextBox.Text.Trim() + "','" +
                        this.inputUserTextBox.Text.Trim() + "','" +
                        this.lenovo_maintenance_noTextBox.Text.Trim() + "','" +
                        this.lenovo_repair_noTextBox.Text.Trim() + 
                        "')";
                    
                    cmd.ExecuteNonQuery();

                    //更新flexid状态
                    cmd.CommandText = "update flexidRecord set _status = 'return' where track_serial_no = '"
                        + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //在更新收货表的同时，需要同时更新导入的表格收货数量，不然数据会乱掉
                    cmd.CommandText = "select _status, ordernum, receivedNum, returnNum,cid_number from receiveOrder where orderno = '" + this.ordernoTextBox.Text
                           + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                    int receivedNum = 0, returnNum =0,cidNum=0;
                    string status = "close";
                    querySdr = cmd.ExecuteReader();
                    bool isDone = false;
                    while (querySdr.Read())
                    {
                        cidNum = Int32.Parse(querySdr[4].ToString());
                        receivedNum = Int32.Parse(querySdr[2].ToString());
                        int leftNum = receivedNum - cidNum;
                        if (querySdr[3].ToString() == "")
                        {
                            returnNum = 0;
                        }
                        else
                        {
                            returnNum = Int32.Parse(querySdr[3].ToString());
                        }

                        if (returnNum >= leftNum)
                        {
                            MessageBox.Show("本料号已经还完！");
                            isDone = true;
                        }
                        else if (leftNum == returnNum + 1)
                        {
                            status = "return";
                        }                        
                    }
                    querySdr.Close();

                    if (isDone == false)
                    {
                        cmd.CommandText = "update receiveOrder set _status = '" + status + "',returnNum = '" + (returnNum + 1) +"' "
                                    + "where orderno = '" + this.ordernoTextBox.Text
                                    + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                        cmd.ExecuteNonQuery();
                        if (status == "return")
                        {
                            MessageBox.Show("本料号已经还完！");
                        }
                    }

                    //外观做完自动出良品库，同时更新良品库的数量                    
                    string customMaterialNo = this.custommaterialNoTextBox.Text;
                    cmd.CommandText = "INSERT INTO repaired_out_house_table VALUES('" +
                       this.track_serial_noTextBox.Text.Trim() + "','" +
                       customMaterialNo + "','" +
                       "1" + "','" +
                       "1900-01-01" +"')";//DateTime.Now.ToString("yyyy/MM/dd")，在上传报关信息的时候在更新数据，那个时候才可以上传海关信息
                    cmd.ExecuteNonQuery();
                    //更新数量
                    cmd.CommandText = "select Id,leftNumber from repaired_left_house_table where custom_materialNo='" + customMaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    string exist = "";
                    string left_number = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                        left_number = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (exist == "")
                    {
                        cmd.CommandText = "INSERT INTO repaired_left_house_table VALUES('"
                        + customMaterialNo + "','"
                        + "1" + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        try
                        {
                            int totalLeft = Int32.Parse(left_number);
                            int thistotal = totalLeft - 1;
                            if (thistotal >= 0)
                            {
                                cmd.CommandText = "update repaired_left_house_table set leftNumber = '" + thistotal + "'"
                                       + "where custom_materialNo = '" + customMaterialNo + "'";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("数据不对,请停止操作，请立即联系技术人员!");
                                //conn.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
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

            //dataGridViewToReturn里面的数据要更新
            doQueryAfterSelection();
            clearInputData();
            queryLastest(true);
        }

        private bool checkIsNull()
        {
            if (custommaterialNoTextBox.Text.Trim() == ""
               // || this.replace_custom_materialNotextBox.Text.Trim() == ""
                || this.track_serial_noTextBox.Text.Trim() == ""
                || this.custom_serial_noTextBox.Text.Trim() == ""
                || this.statusComboBox.Text.Trim() == ""
                || this.dpkpnTextBox.Text.Trim() == ""
                || this.matertiallibMpnTextBox.Text.Trim() == ""
                || this.vendor_serail_noTextBox.Text.Trim() == ""
                || this.bommpnTextBox.Text.Trim() == ""
                || this.storehouseTextBox.Text.Trim() == ""
                || this.return_dateTextBox.Text.Trim() == ""
                || this.ordernoTextBox.Text.Trim() == ""
                || this.tatTextBox.Text.Trim() == "")
            {
                return true;
            }

            if (custom_res_typeComboBox.Enabled &&  response_describeComboBox.Enabled)
            {
                if (this.custom_res_typeComboBox.Text.Trim() == ""
                    || this.response_describeComboBox.Text.Trim() == "")
                {
                    return true;
                }
            }
               
            return false;           
        }

        private void clearInputData()
        {
            custommaterialNoTextBox.Text = "";
            replace_custom_materialNotextBox.Text = "";
            track_serial_noTextBox.Text = "";

            custom_serial_noTextBox.Text = "";
            custom_res_typeComboBox.SelectedIndex = -1;
            response_describeComboBox.SelectedIndex = -1;
            dpkpnTextBox.Text = "";

            vendor_serail_noTextBox.Text = "";
            bommpnTextBox.Text = "";
            storehouseTextBox.Text = "";
            return_dateTextBox.Text = "";

            ordernoTextBox.Text = "";
            tatTextBox.Text = "";

            this.flexidcomboBox.Text = "";
            this.input8sTextBox.Text = "";
        }


        private void queryLastest(bool latest)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                if (latest)
                {
                    cmd.CommandText = "select top 3 * from " + tableName + " order by id desc";
                }
                else
                {
                    cmd.CommandText = "select top 20 * from  " + tableName + " order by Id desc";
                }
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridViewReturnedDetail.DataSource = ds.Tables[0];
                dataGridViewReturnedDetail.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "厂商","客户别","还货文件编号","客户库别","还货时间","订单编号","客户料号","DPK状态",
                                "跟踪条码","客户序号","厂商序号","厂商料号","状态","客责类别","客责描述","TAT","还货人"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewReturnedDetail.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            queryLastest(false);
        }

        private void modify_Click(object sender, EventArgs e)
        {
           
        }

        private void ReturnStoreForm_Load(object sender, EventArgs e)
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

        string currentMaterialNo, orderNo, tat;
        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {                
                //跟踪条码可以为空，如果为空则代表本板子是通过库房拿过来替换的，原来的板子没有维修好
                if (track_serial_noTextBox.Text == "")
                {
                    return;
                }

                try
                {
                    int row = dataGridViewToReturn.Rows.Count;
                    for (int i = 0; i < row; i++)
                    {
                        dataGridViewToReturn.Rows[i].Selected = false;
                    }

                    //int count = 0;
                    //currentMaterialNo = this.custommaterialNoTextBox.Text.Trim();
                    //for (int i = 0; i < row; i++)
                    //{
                    //    string queryedStr = dataGridViewToReturn.Rows[i].Cells[1].Value.ToString();
                    //    if (queryedStr.EndsWith(currentMaterialNo)
                    //        && this.ordernoTextBox.Text.Trim() == dataGridViewToReturn.Rows[i].Cells[0].Value.ToString().Trim())
                    //    {
                    //        count++;
                    //        this.custommaterialNoTextBox.Text = queryedStr;
                    //        orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.Rows[i].Cells[0].Value.ToString();
                    //        tat = this.tatTextBox.Text = dataGridViewToReturn.Rows[i].Cells[5].Value.ToString();

                    //        dataGridViewToReturn.Rows[i].Selected = true;
                    //        dataGridViewToReturn.CurrentCell = dataGridViewToReturn.Rows[i].Cells[0];
                    //        break;
                    //    }
                    //}

                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string returnExist = "";
                    while (querySdr.Read())
                    {
                        returnExist = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (returnExist != "")
                    {
                        MessageBox.Show("此序列号已经还货了，请检查！");
                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select _8sCode from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and isLock='true'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此序列号已经锁定，不能走下面的流程！");
                        querySdr.Close();
                        mConn.Close();
                        this.returnStore.Enabled = false;
                        return;
                    }
                    this.returnStore.Enabled = true;
                    querySdr.Close();

                    
                    
                    cmd.CommandText = "select Id from cidRecord where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    string isExist = "";
                    while (querySdr.Read())
                    {
                        isExist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (isExist != "")
                    {
                        MessageBox.Show("此序列号在CID库中，不能还货！");
                        mConn.Close();
                        this.track_serial_noTextBox.Text = "";
                        this.flexidcomboBox.Text = "";
                        this.custom_serial_noTextBox.Text = "";
                        this.matertiallibMpnTextBox.Text = "";
                        this.vendor_serail_noTextBox.Text = "";
                        return;
                    }                    

                    //先检查CID，如果在cid存在，则跳过站别检查
                    cmd.CommandText = "select Id from cidRecord where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                     querySdr = cmd.ExecuteReader();
                    string cidExist = "";
                    while (querySdr.Read())
                    {
                        cidExist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    string track_serial_no = "";
                    if (cidExist == "")
                    {
                        //增加站别检查，如果没有经过最后一站，则认为此板子有问题，不能归还 
                        //cmd.CommandText = "select track_serial_no from outlookcheck where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                        //暂时从Test2走
                        //string currentUsedTable="test2table";
                        //if (this.productComboBox.Text == "DT" || productComboBox.Text == "AIO" || productComboBox.Text == "TBG")
                        //{
                        //    currentUsedTable ="testalltable";
                        //}
                        cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        string station = "";
                        while (querySdr.Read())
                        {
                            station = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (station != "Package")
                        {
                            MessageBox.Show("板子没有经过Package站别");
                            this.returnStore.Enabled = false;
                            mConn.Close();                           
                            return;
                        }
                        this.returnStore.Enabled = true;

                        string currentUsedTable = "Packagetable";
                        cmd.CommandText = "select track_serial_no from "+currentUsedTable+" where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();                       
                        while (querySdr.Read())
                        {
                            track_serial_no = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        if (track_serial_no == "")
                        {
                            MessageBox.Show("此单在Package站別沒有操作信息，是不是沒有操作过Package界面？");
                            mConn.Close();
                            return;
                        }
                    }
              

                    //开始查询订单号与料号根据跟踪条码
                    cmd.CommandText = "select custom_serial_no, vendor_serail_no,mpn ,lenovo_maintenance_no,lenovo_repair_no,custom_order,custommaterialNo from DeliveredTable where track_serial_no = '"
                        + this.track_serial_noTextBox.Text + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                        this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                        this.matertiallibMpnTextBox.Text = querySdr[2].ToString();              
                        this.lenovo_maintenance_noTextBox.Text = querySdr[3].ToString();
                        this.lenovo_repair_noTextBox.Text = querySdr[4].ToString();
                        this.orderNo = querySdr[5].ToString();
                        this.currentMaterialNo = querySdr[6].ToString();
                    }
                    querySdr.Close();

                    //开始查询内容
                    for (int i = 0; i < row; i++)
                    {
                        string queryedStr = dataGridViewToReturn.Rows[i].Cells[1].Value.ToString();
                        if (queryedStr.EndsWith(currentMaterialNo)
                            && this.orderNo.Trim() == dataGridViewToReturn.Rows[i].Cells[0].Value.ToString().Trim())
                        {
                            this.custommaterialNoTextBox.Text = queryedStr;
                            orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.Rows[i].Cells[0].Value.ToString();
                            tat = this.tatTextBox.Text = dataGridViewToReturn.Rows[i].Cells[5].Value.ToString();

                            dataGridViewToReturn.Rows[i].Selected = true;
                            dataGridViewToReturn.CurrentCell = dataGridViewToReturn.Rows[i].Cells[0];
                            break;
                        }
                    }
                    simulateEnter(currentMaterialNo, orderNo, tat);

                    cmd.CommandText = "select flex_id from flexidRecord where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        flexidcomboBox.Text = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select custom_order from flexidRecord where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    string customOrder = "";
                    while (querySdr.Read())
                    {
                        customOrder = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (customOrder != ordernoTextBox.Text.Trim())
                    {
                        MessageBox.Show("此序列号的订单编号是" + customOrder + "，跟选择的订单编号应该不符合！");
                        mConn.Close();
                        return;
                    }

                    if (this.custom_serial_noTextBox.Text == "")//说明板子从buffer库出来的
                    {
                        cmd.CommandText = "select custom_serial_no, vendor_serial_no,mpn from mb_out_stock where track_serial_no = '"
                      + this.track_serial_noTextBox.Text + "'";

                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                            this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                            this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                            this.lenovo_maintenance_noTextBox.Text = "";
                            this.lenovo_repair_noTextBox.Text = "";
                        }
                        querySdr.Close();
                    }
                    mConn.Close();

                    if (this.custom_serial_noTextBox.Text == "")
                    {
                        MessageBox.Show("客户序号不能为空，严重，检查！");
                    }

                    if (this.productComboBox.Text != "TBG" && this.productComboBox.Text != "DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                    {
                        //需要去掉前面的非0字段
                        string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');
                        string replacedCustomSerial = this.replace_custom_materialNotextBox.Text.TrimStart('0');

                        if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()))
                        {
                        }
                        else
                        {
                            if(this.custom_serial_noTextBox.Text.ToLower().Contains(replacedCustomSerial.ToLower()))
                            {
                            }
                            else
                            {
                                MessageBox.Show("在" + this.productComboBox.Text + "下客户序号没有包含客户料号, 请检查追踪条码是否正确");
                                this.track_serial_noTextBox.Focus();
                                this.track_serial_noTextBox.SelectAll();

                                this.custom_serial_noTextBox.Text = "";
                                this.returnStore.Enabled = false;
                                return;
                            }
                        }                          
                    }                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.input8sTextBox.Focus();
                this.input8sTextBox.SelectAll();
            }
        }

        private void custommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.response_describeComboBox.Focus();

                if (track_serial_noTextBox.Text.Trim() == "")
                {
                    if (this.productComboBox.Text != "TBG" && this.productComboBox.Text != "DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                    {
                        //需要去掉前面的非0字段
                        string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');
                        string replacedCustomSerial = this.replace_custom_materialNotextBox.Text.TrimStart('0');

                        if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()) == false
                            || this.custom_serial_noTextBox.Text.ToLower().Contains(replacedCustomSerial.ToLower()))
                        {
                            this.track_serial_noTextBox.Focus();
                            this.track_serial_noTextBox.SelectAll();

                            this.custom_serial_noTextBox.Text = "";

                            MessageBox.Show("在" + this.productComboBox.Text + "下客户序号没有包含客户料号, 请检查追踪条码是否正确");
                            return;
                        }
                    }

                    MessageBox.Show("需要查询库存表，然后调出厂商序号! todo");
                }
            }
        }

        private void statusComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.statusComboBox.Text == "良品")
            {
                custom_res_typeComboBox.Enabled = false;
                response_describeComboBox.Enabled = false;
            }
            else if (this.statusComboBox.Text == "不良品")
            {
                custom_res_typeComboBox.Enabled = true;
                response_describeComboBox.Enabled = true;
            }
        }

        private void custom_res_typeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == System.Convert.ToChar(13))
            //{
            //    try
            //    {
            //        //if (myDictionary[this.custom_res_typeComboBox.Text.Trim()] == "11")
            //        {
            //            this.custom_res_typeComboBox.Text = myDictionary[this.custom_res_typeComboBox.Text.Trim()];
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("故障代码" + this.custom_res_typeComboBox.Text.Trim() + "不存在");
            //    }
            //}
        }

        private void dataGridViewToReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void print_Click(object sender, EventArgs e)
        {
            if (this.custommaterialNoTextBox.Text == "")
            {
                MessageBox.Show("客户料号为空!");
                return;
            }
            //根据客户料号，查询对应的厂商料号
            string vendormaterialNo = "";
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendormaterialNo from MBMaterialCompare where custommaterialNo = '" + this.custommaterialNoTextBox.Text + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    vendormaterialNo = querySdr[0].ToString();
                    break;
                }
                querySdr.Close();
                if (vendormaterialNo == "")
                {
                    MessageBox.Show("在物料对照表中与" + this.custommaterialNoTextBox.Text + "厂商料号不存在，请检查！");
                    mConn.Close();
                    return;
                }
                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string customMaterialSub = custommaterialNoTextBox.Text.Substring(2);
            if (customMaterialSub.Length != 8)
            {
                MessageBox.Show("客户料号的长度不是8位");
                return;
            }

            if (this.storehouseTextBox.Text.ToUpper().StartsWith("CN"))
            {
                PrintUtils.printCustomMaterialNoChina(ordernoTextBox.Text, custommaterialNoTextBox.Text.Substring(3), custom_serial_noTextBox.Text);
            }
            else
            {
                PrintUtils.printCustomMaterialNo(ordernoTextBox.Text, vendormaterialNo, custommaterialNoTextBox.Text.Substring(3), flexidcomboBox.Text, custom_serial_noTextBox.Text);
            }
        }

        private void input8sTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.custom_serial_noTextBox.Focus();
                this.custom_serial_noTextBox.SelectAll();
            }
        }

        private void inputCustomMaterialtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.inputCustomMaterialtextBox.Text.Trim() == "")
                {
                    MessageBox.Show("输入的客户料号的不对");
                    return;
                }
                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }
    }
}
