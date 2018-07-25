using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SaledServices
{
    public partial class DeliveredTableForm : Form
    {
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "DeliveredTable";

        public DeliveredTableForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            inputUserTextBox.Text = LoginForm.currentUser;

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
                cmd.CommandText = "select distinct source from sourceTable";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.source_briefComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select fault_index, fault_describe from customFault";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string index = querySdr[0].ToString();
                    string temp = querySdr[1].ToString();
                    if (temp != "")
                    {
                        this.custom_faultComboBox.Items.Add(temp);
                        myDictionary.Add(index, temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct guarantee_describe from guarantee";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.guaranteeComboBox.Items.Add(temp);
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
                        this.customResponsibilityComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                //加载没有收完货的订单
                cmd.CommandText = "select distinct orderno from receiveOrder where _status = 'open'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_orderComboBox.Items.Add(temp);
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

        private void simulateEnter()
        {
            if (custom_orderComboBox.Text == "" || custommaterialNoTextBox.Text == "")
            {
                MessageBox.Show("无效订单编号");
                return;
            }
            string status = "";
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendor, product, storehouse, _status from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                    + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    this.vendorTextBox.Text = querySdr[0].ToString();
                    this.productTextBox.Text = querySdr[1].ToString();
                    this.storehouseTextBox.Text = querySdr[2].ToString();
                    status = querySdr[3].ToString();
                }
                querySdr.Close();

                if (status == "open")
                {
                    cmd.CommandText = "select custom_machine_type,mb_brief,dpk_type,mpn,mb_descripe,warranty_period from MBMaterialCompare where custommaterialNo ='"
                        + this.custommaterialNoTextBox.Text + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.custom_machine_typeTextBox.Text = querySdr[0].ToString();
                        this.mb_briefTextBox.Text = querySdr[1].ToString();
                        this.dpk_statusTextBox.Text = querySdr[2].ToString();
                        this.mpnTextBox.Text = querySdr[3].ToString();
                        this.mb_describeTextBox.Text = querySdr[4].ToString();
                        this.warranty_periodTextBox.Text = querySdr[5].ToString();
                    }
                    querySdr.Close();
                }
                else if (status == "close")
                {
                    this.custommaterialNoTextBox.Focus();
                    this.custommaterialNoTextBox.SelectAll();
                    MessageBox.Show("客户料号：" + this.custom_orderComboBox.Text + " 已经收货完毕，请检测是否有错误!");
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (status == "close")
            {
                this.custommaterialNoTextBox.Text = "";
                this.custommaterialNoTextBox.Focus();
                this.custommaterialNoTextBox.SelectAll();
            }
            else
            {
                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }

        private void custom_orderComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                simulateEnter();
            }
        }

        private void custom_orderComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.custom_orderComboBox.Text;
            if (str == "")
            {
                return;
            }

            string substr = "";
            string inTime = "";
            //try
            //{
            //    substr = str.Substring(str.Length - 8, 6);
            //    inTime = "20" + substr.Substring(0, 2) + "/" + substr.Substring(2, 2) + "/" + substr.Substring(4, 2);
            //    this.order_out_dateTextBox.Text = inTime;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("订单号码时间格式错误, 设置默认值！");
            //    this.order_out_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //}
            this.order_out_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (Untils.isTimeError(this.order_out_dateTextBox.Text.Trim()))
            {
                this.add.Enabled = false;
            }

            try
            {
                DateTime dt1 = DateTime.Parse(inTime);
                DateTime dt2 = DateTime.Now;

                TimeSpan ts = dt2.Subtract(dt1);
                if (ts.TotalDays < 0)
                {
                    MessageBox.Show("请检测当前机器的时间是否正确！");
                    return;
                }
                this.order_receive_dateTextBox.Text = dt2.ToString("yyyy/MM/dd");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("订单号码时间格式错误, 设置默认值！");
                this.order_receive_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
      
            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            try
            {
                this.dataGridViewWaitToReturn.DataSource = null;
                dataGridViewWaitToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                //加入条件判断，只显示未收完的货物
                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,ordernum, receivedNum from receiveOrder where orderno='" + this.custom_orderComboBox.Text + "' and _status='open'" ;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
                dataGridViewWaitToReturn.DataSource = ds.Tables[0];
                dataGridViewWaitToReturn.RowHeadersVisible = false;


                string[] hTxt = { "订单编号", "客户料号", "MB简称", "订单数量", "收货数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewWaitToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewWaitToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.DefaultCellStyle.BackColor = Color.Red;
                dc.Name = "差数";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "差数";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewWaitToReturn.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridViewWaitToReturn.Rows)
                {
                    try
                    {
                        int oNum = Int32.Parse(dr.Cells["订单数量"].Value.ToString());
                        int rNum = Int32.Parse(dr.Cells["收货数量"].Value.ToString());

                        if(oNum-rNum == 0)
                        {
                            dr.Cells["差数"].Style.BackColor = Color.Green;
                        }
                        dr.Cells["差数"].Value = (oNum - rNum) + " ";
                    }
                    catch (Exception ex)
                    { }
                }

                mConn.Close();

                if (ds.Tables[0].Rows.Count > 0) 
                {
                    dataGridViewWaitToReturn.Rows[0].Selected = false;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                string customSerialNo = this.custom_serial_noTextBox.Text;
                customSerialNo = Regex.Replace(customSerialNo, "[^a-zA-Z0-9]", "");
                this.custom_serial_noTextBox.Text = customSerialNo;
                if (customSerialNo.StartsWith("8S"))
                {
                    if (customSerialNo.Length != 23)
                    {
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        MessageBox.Show("客户序号的长度不是23位，请检查！");
                        return;
                    }
                }
                else if (customSerialNo.StartsWith("11S"))
                {
                    if (customSerialNo.Length != 22)
                    {
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        MessageBox.Show("客户序号的长度不是22位，请检查！");
                        return;
                    }
                }
                
                if (this.productTextBox.Text != "TBG" && this.productTextBox.Text !="DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                {
                    //需要去掉前面的非0字段
                    string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');

                    if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()) == false)
                    {
                        MessageBox.Show("在" + this.productTextBox.Text + "下客户序号没有包含客户料号");
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        return;
                    }
                }
               
                string subData = "";
                if (customSerialNo.StartsWith("8S"))
                {
                    subData = customSerialNo.Substring(customSerialNo.Length - 7, 3);
                }
                else if (customSerialNo.StartsWith("11S"))
                {
                    subData = customSerialNo.Substring(customSerialNo.Length - 6, 3);
                }
                else
                {
                    MessageBox.Show("客户序号没有包含,没有做计算时间处理");
                    this.custom_serial_noTextBox.Focus();
                    this.custom_serial_noTextBox.SelectAll();
                    return;
                }

                //检查客户序号或厂商序号是否已经存在本订单编号里面了，收货表中
                string vendor = "";
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where custom_serial_no = '" + this.custom_serial_noTextBox.Text
                        + "' and custom_order = '" + this.custom_orderComboBox.Text + "'"; 

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (vendor != "")
                    {
                        MessageBox.Show("客户序号：" + this.custom_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        return;
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                string year = Untils.getTimeByChar(true, Convert.ToChar(subData.Substring(0, 1)));
                string mouth = Untils.getTimeByChar(false, Convert.ToChar(subData.Substring(1, 1)));
                string day = Untils.getTimeByChar(false, Convert.ToChar(subData.Substring(2, 1)));
                this.mb_make_dateTextBox.Text = year + "/" + mouth + "/" + day;

                try
                {
                    DateTime dt1 = Convert.ToDateTime(this.mb_make_dateTextBox.Text);
                    DateTime dt2 = Convert.ToDateTime(this.order_receive_dateTextBox.Text);

                    string period = this.warranty_periodTextBox.Text;
                    if (period != "")
                    {
                        int warranty = Int32.Parse(period.Substring(0, period.Length - 1));

                        dt1 = dt1.AddMonths(warranty);//生产日期加上保修期
                        TimeSpan ts = dt2.Subtract(dt1);

                        int overdays = ts.Days;

                        if (overdays >= 0)
                        {
                            this.guaranteeComboBox.Text = "保外";
                            this.guaranteeComboBox.Enabled = false;
                            this.customResponsibilityComboBox.Text = "过保";
                           // this.customResponsibilityComboBox.Enabled = false;
                            MessageBox.Show((overdays) + " 天超过， 已经过保!");
                        }
                        else
                        {
                            this.guaranteeComboBox.Text = "";
                            this.guaranteeComboBox.Enabled = true;
                            this.customResponsibilityComboBox.Text = "";
                            //this.customResponsibilityComboBox.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("客户料号里面的日期规则不对!");
                }

                this.vendor_serail_noTextBox.Focus();
                this.vendor_serail_noTextBox.SelectAll();
            }
        }
       
        private void add_Click(object sender, EventArgs e)
        {
            if (checkInputIsNull())
            {
                MessageBox.Show("需要输入的内容为空，请检查！");
                return;
            }
            try
            {
                //事前检查
                if (this.custommaterialNoTextBox.Text.Trim().Substring(3) != this.inputCustommaterialNoTextBox.Text.Trim())
                {
                    MessageBox.Show("输入的客户料号与选择的客户料号没有关联，请检查");
                    return;
                }

                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string existTrack = "";
                    while (querySdr.Read())
                    {
                        existTrack = querySdr[0].ToString();
                    }
                    if (existTrack != "")
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        querySdr.Close();
                        conn.Close();
                        clearInputContent();
                        MessageBox.Show("跟踪条码：" + this.track_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        return;
                    }
                    else
                    {
                        querySdr.Close();
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" + 
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.source_briefComboBox.Text.Trim() + "','" +
                        this.storehouseTextBox.Text.Trim() + "','" +
                        this.custom_orderComboBox.Text.Trim() + "','" +
                        this.order_out_dateTextBox.Text.Trim() + "','" +
                        this.order_receive_dateTextBox.Text.Trim() + "','" +
                        this.custom_machine_typeTextBox.Text.Trim() + "','" +
                        this.mb_briefTextBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.dpk_statusTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serail_noTextBox.Text.Trim() + "','" +
                        this.uuidTextBox.Text.Trim() + "','" +
                        this.macTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.mb_describeTextBox.Text.Trim() + "','" +
                        this.mb_make_dateTextBox.Text.Trim() + "','" +
                        this.warranty_periodTextBox.Text.Trim() + "','" +
                        this.custom_faultComboBox.Text.Trim().Replace('\'', '_') + "','" +
                        this.guaranteeComboBox.Text.Trim() + "','" +
                        this.customResponsibilityComboBox.Text.Trim() + "','" +
                        this.lenovo_custom_service_noTextBox.Text.Trim() + "','" +
                        this.lenovo_maintenance_noTextBox.Text.Trim() + "','" +
                        this.lenovo_repair_noTextBox.Text.Trim() + "','" +
                        this.whole_machine_noTextBox.Text.Trim() + "','" +
                        this.inputUserTextBox.Text.Trim()+
                        "')";
                   
                    cmd.ExecuteNonQuery();

                    //插入flexid的记录
                    cmd.CommandText = "INSERT INTO flexidRecord VALUES('" +
                        this.custom_orderComboBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.flexidTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        "" +//status
                        "')";
                    cmd.ExecuteNonQuery();

                    //除正常插入数据外，还需要把收还货表格的数量修改 TODO...
                    //1. 修改收还货表格的收货数量， 判断，小于 等于，大于的情况
                    //2 如果小于 只是修改数据
                    //3 如果等于 则需要把状态也修改位close， 如果大于则直接报错
                    //update receiveOrder set returnNum = '1' where id = '1'

                    cmd.CommandText = "select _status, ordernum, receivedNum, receivedate from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                         + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    int orderNum;
                    int receivedNum=0;
                    string status = "open";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if (querySdr[0].ToString() == "close")
                        {
                            MessageBox.Show("本板子已经收货完毕，请检测是否有错误!");
                        }
                        else
                        {
                            orderNum = Int32.Parse(querySdr[1].ToString());
                            receivedNum = Int32.Parse(querySdr[2].ToString()); 
                            if (orderNum == receivedNum + 1)
                            {
                                status = "close";
                            }
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "update receiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum + 1) +
                                "', receivedate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                + "where orderno = '" + this.custom_orderComboBox.Text
                                + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    //为了海关的信息，需要记录板子的出入 待维修库 信息，先记录入库信息，然后记录出库信息，剩餘的數量一直是0
                    cmd.CommandText = "INSERT INTO wait_repair_in_house_table VALUES('" +
                       this.track_serial_noTextBox.Text.Trim() + "','" +
                       this.custommaterialNoTextBox.Text.Trim() + "','" +
                       "1" + "','" +
                       DateTime.Now.ToString("yyyy/MM/dd") + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO wait_repair_out_house_table VALUES('" +
                       this.track_serial_noTextBox.Text.Trim() + "','" +
                       this.custommaterialNoTextBox.Text.Trim() + "','" +
                       "1" + "','" +
                       DateTime.Now.ToString("yyyy/MM/dd") + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select Id,leftNumber from wait_repair_left_house_table where custom_materialNo='" + this.custommaterialNoTextBox.Text + "'";
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
                        cmd.CommandText = "INSERT INTO wait_repair_left_house_table VALUES('"
                        + this.custommaterialNoTextBox.Text + "','"
                        + "0" + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update wait_repair_left_house_table set leftNumber = '0'"
                                + "where custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                        cmd.ExecuteNonQuery();
                    }
                    querySdr.Close();

                    //记录站别信息
                    cmd.CommandText = "INSERT INTO stationInformation VALUES('"
                        + this.track_serial_noTextBox.Text.Trim() + "','收货','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("收货成功！");

                clearInputContent();
                doQueryAfterSelection();
                queryLatest(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearInputContent()
        {
            this.custommaterialNoTextBox.Text = "";

           // this.custom_orderComboBox.Text = "";
            this.source_briefComboBox.Text = "";
            this.source_briefComboBox.SelectedIndex = -1;
            this.track_serial_noTextBox.Text = "";
            this.flexidTextBox.Text = "";
            this.custom_serial_noTextBox.Text = "";
            this.vendor_serail_noTextBox.Text = "";
          //  this.uuidTextBox.Text = "";
            this.macTextBox.Text = "";
            this.custom_faultComboBox.Text = "";
            this.custom_faultComboBox.SelectedIndex = -1;
            this.guaranteeComboBox.Text = "";
            this.guaranteeComboBox.SelectedIndex = -1;
            this.customResponsibilityComboBox.Text = "";
            this.customResponsibilityComboBox.SelectedIndex = -1;
            this.lenovo_custom_service_noTextBox.Text = "";
            this.lenovo_maintenance_noTextBox.Text = "";
            this.lenovo_repair_noTextBox.Text = "";
            this.whole_machine_noTextBox.Text = "";
        }

        private bool checkInputIsNull()
        {
            if (this.custom_orderComboBox.Text == ""
                || this.source_briefComboBox.Text == ""

                || this.custommaterialNoTextBox.Text == ""
                || this.track_serial_noTextBox.Text == ""
                || this.flexidTextBox.Text == ""
                || this.custom_serial_noTextBox.Text == ""
                || this.vendor_serail_noTextBox.Text == ""
                //|| this.uuidTextBox.Text == ""
                || this.macTextBox.Text == ""
                || this.custom_faultComboBox.Text == ""
                || this.guaranteeComboBox.Text == ""
                //|| this.customResponsibilityComboBox.Text == ""
                //|| this.lenovo_custom_service_noTextBox.Text == ""
                //|| this.lenovo_maintenance_noTextBox.Text == ""
                //|| this.lenovo_repair_noTextBox.Text == ""
                //|| this.whole_machine_noTextBox.Text == ""
                )
            {
                return true;
            }

            //if (customResponsibilityComboBox.Enabled == true && customResponsibilityComboBox.Text == "")
            //{
            //    return true;
            //}

            return false;           
        }

        private void queryLatest(bool latest)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                if (latest)
                {
                    cmd.CommandText = "select top 3 * from " + tableName + " order by id desc"; 
                }
                else
                {
                    cmd.CommandText = "select * from  " + tableName;
                }
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


            string[] hTxt = { "ID", "厂商", "客户别","来源"
            ,"库别","订单编号","客户出库日期","收货日期","客户机型","mb简称","客户料号","DPK状态","跟踪条码",
            "客户序号","厂商序号","UUID","MAC","厂商料号","mb描述","MB生产日期",",保修期",",客户故障","保内/保外"
            ,"客责描述","联想客服序号","联想维修站编号","联想维修单编号","整机序号","收货人"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            queryLatest(false);
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());

            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["source_brief"] = this.source_briefComboBox.Text.Trim();
            dr["storehouse"] = this.storehouseTextBox.Text.Trim();
            dr["custom_order"] = this.custom_orderComboBox.Text.Trim();
            dr["order_out_date"] = this.order_out_dateTextBox.Text.Trim();
            dr["order_receive_date"] = this.order_receive_dateTextBox.Text.Trim();            
            dr["custom_machine_type"] = this.custom_machine_typeTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_briefTextBox.Text.Trim();
            dr["custommaterialNo"] = this.custommaterialNoTextBox.Text.Trim();
            dr["dpk_status"] = this.dpk_statusTextBox.Text.Trim();
            dr["track_serial_no"] = this.track_serial_noTextBox.Text.Trim();

            dr["custom_serial_no"] = this.custom_serial_noTextBox.Text.Trim();
            dr["vendor_serail_no"] = this.vendor_serail_noTextBox.Text.Trim();
            dr["uuid"] = this.uuidTextBox.Text.Trim();
            dr["mac"] = this.macTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["mb_describe"] = this.mb_describeTextBox.Text.Trim();
            dr["mb_make_date"] = this.mb_make_dateTextBox.Text.Trim();
            dr["warranty_period"] = this.warranty_periodTextBox.Text.Trim();
            dr["custom_fault"] = this.custom_faultComboBox.Text.Trim();
            dr["guarantee"] = this.guaranteeComboBox.Text.Trim();
            dr["customResponsibility"] = this.customResponsibilityComboBox.Text.Trim();
            dr["lenovo_custom_service_no"] = this.lenovo_custom_service_noTextBox.Text.Trim();
            dr["lenovo_maintenance_no"] = this.lenovo_maintenance_noTextBox.Text.Trim();
            dr["lenovo_repair_no"] = this.lenovo_repair_noTextBox.Text.Trim();
            dr["whole_machine_no"] = this.whole_machine_noTextBox.Text.Trim();
            dr["inputuser"] = this.inputUserTextBox.Text.Trim();

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
                    cmd.CommandType = CommandType.Text;                    

                    cmd.CommandText = "select receivedNum from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                         + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                   
                    int receivedNum = 0;
                    string status = "open";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        receivedNum = Int32.Parse(querySdr[0].ToString());                           
                    }
                    querySdr.Close();

                    cmd.CommandText = "update receiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum - 1) + "' "
                                + "where orderno = '" + this.custom_orderComboBox.Text
                                + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("删除完毕!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeliveredTableForm_Load(object sender, EventArgs e)
        {
             //当TableLayoutPanel控件中的需要更新的Label过多的时候，刷新Label的时候会出现闪烁问题，主要解决办法就是增加双缓冲，代码如下

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.numTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.source_briefComboBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.storehouseTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.custom_orderComboBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.order_out_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[6].Value.ToString()).ToString("yyyy/MM/dd");
            this.order_receive_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[7].Value.ToString()).ToString("yyyy/MM/dd");
            this.custom_machine_typeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.custommaterialNoTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.dpk_statusTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.track_serial_noTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.custom_serial_noTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.vendor_serail_noTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.uuidTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.macTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            this.mb_describeTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            this.mb_make_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[19].Value.ToString()).ToString("yyyy/MM/dd");
            this.warranty_periodTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
            this.custom_faultComboBox.Text = dataGridView1.SelectedCells[21].Value.ToString();
            this.guaranteeComboBox.Text = dataGridView1.SelectedCells[22].Value.ToString();
            this.customResponsibilityComboBox.Text = dataGridView1.SelectedCells[23].Value.ToString();
            this.lenovo_custom_service_noTextBox.Text = dataGridView1.SelectedCells[24].Value.ToString();
            this.lenovo_maintenance_noTextBox.Text = dataGridView1.SelectedCells[25].Value.ToString();
            this.lenovo_repair_noTextBox.Text = dataGridView1.SelectedCells[26].Value.ToString();
            this.whole_machine_noTextBox.Text = dataGridView1.SelectedCells[27].Value.ToString();

            this.inputUserTextBox.Text = dataGridView1.SelectedCells[28].Value.ToString(); 
        }
        
        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.track_serial_noTextBox.Text == "" || this.track_serial_noTextBox.Text.Length != 15)
                {
                    MessageBox.Show("检查内容是否为空或长度不对！");
                    this.track_serial_noTextBox.Text = "";
                    this.track_serial_noTextBox.Focus();
                    return;
                }
                //检查跟踪条码是否在系统中存在过，否则报错
                string vendor = "";
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    mConn.Close();

                    if (vendor != "")
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("跟踪条码：" + this.track_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (vendor == "")
                {
                    this.flexidTextBox.Focus();
                    this.flexidTextBox.SelectAll();
                }
                else 
                {
                    this.track_serial_noTextBox.Focus();
                    this.track_serial_noTextBox.SelectAll();
                }
            }
        }

        private void vendor_serail_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.vendor_serail_noTextBox.Text.Length != 13)
                {
                    this.vendor_serail_noTextBox.SelectAll();
                    MessageBox.Show("厂商序号的内容长度不是32位，请检查！");
                    return; 
                }

                //检查客户序号或厂商序号是否已经存在本订单编号里面了，收货表中
                string vendor = "";
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where vendor_serail_no = '" + this.vendor_serail_noTextBox.Text
                        + "' and custom_order = '" + this.custom_orderComboBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (vendor != "")
                    {
                        MessageBox.Show("客户序号：" + this.custom_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        this.vendor_serail_noTextBox.Focus();
                        this.vendor_serail_noTextBox.SelectAll();
                        return;
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.macTextBox.Focus();
                this.macTextBox.SelectAll();
            }
        }

        private void uuidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.uuidTextBox.Text.Length > 32)
                {
                    string[] temp = this.uuidTextBox.Text.Split(' ');
                    foreach (string str in temp)
                    {
                        if (str.Length == 32)
                        {
                            this.uuidTextBox.Text = str;
                            break;
                        }
                    }
                }

                //当uuid为0的时候也可以通过
                int length = this.uuidTextBox.Text.Length;
                
                if (length != 0 && this.uuidTextBox.Text.Length != 32)
                {
                    MessageBox.Show("UUID中的长度不是32位，请检查！");
                    uuidTextBox.Focus();
                    uuidTextBox.SelectAll();
                    return;
                }
                else if (length == 0)
                {
                }

                //this.macTextBox.Focus();
                //this.macTextBox.SelectAll();
            }
        }

        private void macTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                string mactext = this.macTextBox.Text.Trim();
                mactext = mactext.Substring(3);
                   // Regex.Replace(mactext, "[^a-zA-Z0-9]", "");
                this.macTextBox.Text = mactext;

                if (this.macTextBox.Text.Length != 12)
                {
                    this.macTextBox.SelectAll();
                    MessageBox.Show("MAC的长度不是12位，请检查！");
                    return;
                }

                this.custom_faultComboBox.Focus();
                this.custom_faultComboBox.SelectAll();
            }
        }

        private void lenovo_custom_service_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.lenovo_maintenance_noTextBox.Focus();
                this.lenovo_maintenance_noTextBox.SelectAll();
            }
        }

        private void lenovo_maintenance_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.lenovo_repair_noTextBox.Focus();
                this.lenovo_repair_noTextBox.SelectAll();
            }
        }

        private void lenovo_repair_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.whole_machine_noTextBox.Focus();
                this.whole_machine_noTextBox.SelectAll();
            }
        }

        private void whole_machine_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //this.macTextBox.Focus();
                //this.macTextBox.SelectAll();
            }
        }

        private void dataGridViewWaitToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewWaitToReturn.CurrentRow == null)
            {
                return;
            }
            this.custommaterialNoTextBox.Text = dataGridViewWaitToReturn.SelectedCells[1].Value.ToString();
            simulateEnter();
        }

        private void custom_faultComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.custom_faultComboBox.Text != "" && Regex.IsMatch(this.custom_faultComboBox.Text, @"^[+-]?\d*[.]?\d*$"))
                {
                    try
                    {
                        this.custom_faultComboBox.Text = myDictionary[this.custom_faultComboBox.Text.Trim()];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("故障代码" + this.custom_faultComboBox.Text.Trim() + "不存在");
                    }
                }
            }
        }

        private void guaranteeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (this.guaranteeComboBox.Text.Trim() == "保内")
            //{
            //    customResponsibilityComboBox.Text = "";
            //    customResponsibilityComboBox.Enabled = false;
            //}
            //else 
            //{
            //    customResponsibilityComboBox.Enabled = true;
            //}
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flexidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.flexidTextBox.Text.Trim().Length != 10)
                {
                    MessageBox.Show("FleId不是10位!");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from flexidRecord where flex_id = '" + this.flexidTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    bool exist = false;
                    while (querySdr.Read())
                    {
                        exist = true;
                        break;
                    }
                    querySdr.Close();
                    mConn.Close();

                    if (exist)
                    {
                        MessageBox.Show("此FlexId已经存在之前的收货记录了");
                        this.add.Enabled = false;
                        this.flexidTextBox.Focus();
                        this.flexidTextBox.SelectAll();
                    }
                    else
                    {
                        this.add.Enabled = true;
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void inputCustommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.custommaterialNoTextBox.Text.Trim().Length != 0)
                {
                    MessageBox.Show("请先选择客户料号!");
                    return;
                }

                if(this.custommaterialNoTextBox.Text.Trim().EndsWith(this.inputCustommaterialNoTextBox.Text.Trim()) == false)
                {
                    MessageBox.Show("输入的客户料号与选择的客户料号没有关联，请检查");
                    return;
                }

                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }
    }
}
