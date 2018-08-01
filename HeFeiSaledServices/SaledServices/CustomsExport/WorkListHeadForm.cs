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

    public partial class WorkListHeadForm : Form
    {
        public WorkListHeadForm()
        {
            InitializeComponent();
        }              

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            WorkListHeadClass workListHead = new WorkListHeadClass();
            List<WorkOrderHead> workOrderHeadList = new List<WorkOrderHead>();

            string seq_no = DateTime.Now.ToString("yyyyMMdd") + "4002" + "1";//日期+类型,后面需要加入序号信息
            string boxtype = "4002";//代码
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

                cmd.CommandText = "select indentifier, book_number from company_fixed_table";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    trade_code = querySdr[0].ToString();
                    ems_no = querySdr[1].ToString();
                }
                querySdr.Close();

                //报关出库的板子
                cmd.CommandText = "select track_serial_no,return_date ,custom_serial_no,declare_unit from returnStore inner join receiveOrder on receiveOrder.orderno = returnStore.orderno where return_date='" + today + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    WorkOrderHead init1 = new WorkOrderHead();
                    init1.wo_no = querySdr[0].ToString();
                    init1.wo_date = today;
                    init1.goods_nature = "E";
                    init1.cop_g_no = querySdr[2].ToString();
                    init1.qty = "1";
                    init1.unit = querySdr[4].ToString();
                    init1.emo_no = ems_no;

                    workOrderHeadList.Add(init1);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            workListHead.seq_no = seq_no;
            workListHead.boxtype = boxtype;
            workListHead.flowstateg = flowstateg;
            workListHead.trade_code = trade_code;
            workListHead.ems_no = ems_no;
            workListHead.status = status;

            workListHead.workOrderHeadList = workOrderHeadList;

            Untils.createWorkListHeadXML(workListHead, "D:\\AutoGenerate\\WO_HEAD" + seq_no + ".xml");

            MessageBox.Show("finish");
        }
    }
}
