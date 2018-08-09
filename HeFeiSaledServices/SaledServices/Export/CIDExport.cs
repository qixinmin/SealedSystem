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
    public partial class CIDExport : Form
    {
        public CIDExport()
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

            List<CidStruct> receiveOrderList = new List<CidStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,vendor,product,custom_order,custommaterialNo,custom_serial_no,mb_brief, mpn,order_receive_date,custom_fault,custom_res_type,customResponsibility,short_cut,inputdate from cidRecord where inputdate between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    CidStruct temp = new CidStruct();
                    temp.track_serial_no =  querySdr[0].ToString();
                    temp.vendor =  querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.custom_order= querySdr[3].ToString();
                    temp.custommaterialNo = querySdr[4].ToString();
                    temp.custom_serial_no = querySdr[5].ToString();
                    temp.mb_brief = querySdr[6].ToString();

                    temp.mpn =  querySdr[7].ToString();
                    temp.order_receive_date =  querySdr[8].ToString();
                    temp.custom_fault = querySdr[9].ToString();
                    temp.custom_res_type = querySdr[10].ToString();
                    temp.customResponsibility= querySdr[11].ToString();
                    temp.short_cut = querySdr[12].ToString();
                    temp.inputdate = querySdr[13].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<CidStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("订单编号");
            titleList.Add("客户料号");
            titleList.Add("客户序号");
            titleList.Add("MB简称");
            titleList.Add("MPN");
            titleList.Add("收货日期");
            titleList.Add("客责类别");
            titleList.Add("客责描述");
            titleList.Add("短路电压");
            titleList.Add("录入日期");
                      
            foreach (CidStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.custom_order);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.mb_brief);

                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.order_receive_date);
                ct1.Add(stockcheck.custom_res_type);
                ct1.Add(stockcheck.customResponsibility);
                ct1.Add(stockcheck.short_cut);
                ct1.Add(stockcheck.inputdate);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\Cid信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class CidStruct
    {
        public string track_serial_no;
        public string vendor;
        public string product;
        public string custom_order;
        public string custommaterialNo;
        public string custom_serial_no;
        public string mb_brief;
        public string mpn;
        public string order_receive_date;
        public string custom_fault;
        public string custom_res_type;
        public string customResponsibility;
        public string short_cut;
        public string inputdate;
    }
}
