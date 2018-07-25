using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Export
{
    public partial class FlexIdExport : Form
    {
        public FlexIdExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            List<FlexIdStruct> receiveOrderList = new List<FlexIdStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no from DeliveredTable where order_receive_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    FlexIdStruct temp = new FlexIdStruct();
                    temp.track_serial_no =  querySdr[0].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                foreach (FlexIdStruct stockcheck in receiveOrderList)
                { 
                    cmd.CommandText = "select custom_order,custommaterialNo,flex_id,_status from flexidRecord where track_serial_no='" + stockcheck.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stockcheck.custom_order = querySdr[0].ToString();
                        stockcheck.custommaterialNo = querySdr[1].ToString();
                        stockcheck.flex_id = querySdr[2].ToString();
                        stockcheck._status = querySdr[3].ToString();
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList,startTime, endTime);
        }

        public void generateExcelToCheck(List<FlexIdStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("订单号");
            titleList.Add("客户料号");
            titleList.Add("FlexId");
            titleList.Add("跟踪条码");
            titleList.Add("状态");

            foreach (FlexIdStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.custom_order);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.flex_id);
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck._status);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\FlexId相关信息" +startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class FlexIdStruct
    {
        public string custom_order;
        public string custommaterialNo;
        public string flex_id;
        public string track_serial_no;
        public string _status;
    }
}
