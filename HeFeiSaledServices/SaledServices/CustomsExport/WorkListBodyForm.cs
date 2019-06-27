using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class WorkListBodyForm : Form
    {
        public WorkListBodyForm()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            WorkListBodyClass openingstock = new WorkListBodyClass();
            List<WorkOrderList> storeInitList = new List<WorkOrderList>();

            string seq_no = DateTime.Now.ToString("yyyyMMdd") + "4003" + "1";//日期+类型,后面需要加入序号信息
            string boxtype = "4003";//代码
            string flowstateg = "";
            string trade_code = "";
            string ems_no = "";

            string status = "A";
            string today = DateTime.Now.ToString("yyyy/MM/dd");
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select indentifier, book_number from company_fixed_table_new";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    trade_code = querySdr[0].ToString();
                    ems_no = querySdr[1].ToString();
                }
                querySdr.Close();

                //报关出库的板子与出库表关联， 其他出库表待做
                cmd.CommandText = "select returnStore.track_serial_no,input_date,mpn,stock_out_num from returnStore inner join fru_smt_out_stock on fru_smt_out_stock.track_serial_no = returnStore.track_serial_no where return_date='" + today + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    WorkOrderList init1 = new WorkOrderList();
                    init1.wo_no = querySdr[0].ToString();
                    init1.take_date = querySdr[1].ToString();
                    init1.goods_nature = "I";
                    init1.cop_g_no = querySdr[2].ToString();
                    init1.qty = querySdr[3].ToString();
                    init1.unit = "007";//TODO 添加字段
                    init1.emo_no = ems_no;

                    storeInitList.Add(init1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            openingstock.seq_no = seq_no;
            openingstock.boxtype = boxtype;
            openingstock.flowstateg = flowstateg;
            openingstock.trade_code = trade_code;
            openingstock.ems_no = ems_no;
            openingstock.status = status;

            openingstock.workOrderList = storeInitList;

            Untils.createWorkListBodyXML(openingstock, "D:\\MOV\\WO_ITEM" + seq_no + ".xml");

            MessageBox.Show("finish");
        }
    }
}
