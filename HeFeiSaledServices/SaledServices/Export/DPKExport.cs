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
    public partial class DPKExport : Form
    {
        public DPKExport()
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

            List<DpkStruct> receiveOrderList = new List<DpkStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select dpk_type,KEYPN,KEYID,KEYSERIAL,_status,burn_date,custom_serial_no from DPK_table where upload_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    DpkStruct temp = new DpkStruct();
                    temp.dpk_type =  querySdr[0].ToString();
                    temp.KEYPN =  querySdr[1].ToString();
                    temp.KEYID = querySdr[2].ToString();
                    temp.KEYSERIAL= querySdr[3].ToString();
                    temp._status = querySdr[4].ToString();
                    temp.burn_date = querySdr[5].ToString();
                    temp.custom_serial_no = querySdr[6].ToString();

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

        public void generateExcelToCheck(List<DpkStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("DPK类别");
            titleList.Add("KEYPN");
            titleList.Add("KEYID");
            titleList.Add("KEYSERIAL");
            titleList.Add("状态");
            titleList.Add("烧录日期");
            titleList.Add("客户序号");

            //    dpk_type NVARCHAR(128) NOT NULL, /*DPK类别*/
            //KEYPN NVARCHAR(128) NOT NULL, /*KEYPN*/
            //KEYID  NVARCHAR(128) NOT NULL, /*KEYID*/
            //KEYSERIAL NVARCHAR(128) NOT NULL, /*KEYSERIAL*/
            //_status NVARCHAR(128) NOT NULL, /*状态*/
            //burn_date date,/*烧录日期*/
            //custom_serial_no NVARCHAR(128) /*客户序号*/

            foreach (DpkStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.dpk_type);
                ct1.Add(stockcheck.KEYPN);
                ct1.Add(stockcheck.KEYID);
                ct1.Add(stockcheck.KEYSERIAL);
                ct1.Add(stockcheck._status);
                ct1.Add(stockcheck.burn_date);
                ct1.Add(stockcheck.custom_serial_no);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\DPK信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class DpkStruct
    {
       public string dpk_type;
       public string KEYPN;
       public string KEYID;
       public string KEYSERIAL;
       public string _status;
       public string burn_date;
        public string taker;
        public string custom_serial_no;
    }
}
