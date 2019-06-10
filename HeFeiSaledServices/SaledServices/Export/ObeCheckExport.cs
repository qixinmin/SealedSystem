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
    public partial class ObeCheckExport : Form
    {
        public ObeCheckExport()
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

            List<ObeCheckStruct> receiveOrderList = new List<ObeCheckStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,orderno,custom_materialNo,checkresult,failreason,tester,input_date from ObeStationtable where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ObeCheckStruct temp = new ObeCheckStruct();
                    temp.track_serial_no =  querySdr[0].ToString();
                    temp.orderno =  querySdr[1].ToString();
                    temp.custom_materialNo = querySdr[2].ToString();
                    temp.checkresult= querySdr[3].ToString();
                    temp.failreason = querySdr[4].ToString();
                    temp.tester = querySdr[5].ToString();
                    temp.input_date = querySdr[6].ToString();

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

        public void generateExcelToCheck(List<ObeCheckStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("订单编号");
            titleList.Add("客户料号");
            titleList.Add("检查结果");
            titleList.Add("失败原因");
            titleList.Add("测试者");
            titleList.Add("测试时间");

            foreach (ObeCheckStruct temp in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(temp.track_serial_no);
                ct1.Add(temp.orderno);
                ct1.Add(temp.custom_materialNo);
                ct1.Add(temp.checkresult);
                ct1.Add(temp.failreason);
                ct1.Add(temp.tester);
                ct1.Add(temp.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\OBE抽检信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class ObeCheckStruct
    {
        public string track_serial_no;
        public string orderno;
        public string custom_materialNo;
        public string checkresult;
        public string failreason;
        public string tester;
        public string input_date;
    }
}
