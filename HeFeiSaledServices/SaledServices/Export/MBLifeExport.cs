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
    public partial class MBLifeExport : Form
    {
        private String tableName = "stationInfoRecord";
        public MBLifeExport()
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

            List<stationInfoRecord> stationList = new List<stationInfoRecord>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id, trackno, station,recordstatus,recorddate,repairplace1,repairnum1,repairMaterial1,inputer from  " + tableName + " where recorddate BETWEEN CONVERT(datetime,'" + startTime + "',20) AND CONVERT(datetime,'" + endTime + "',20)";

                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    stationInfoRecord temp = new stationInfoRecord();
                    temp.id = querySdr[0].ToString();
                    temp.trackno = querySdr[1].ToString();
                    temp.station = querySdr[2].ToString();
                    temp.recordstatus = querySdr[3].ToString();
                    temp.recorddate = querySdr[4].ToString();
                    temp.repairplace1 = querySdr[5].ToString();
                    temp.repairnum1 = querySdr[6].ToString();
                    temp.repairMaterial1 = querySdr[7].ToString();
                    temp.inputer = querySdr[8].ToString();
                    stationList.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(stationList, startTime, endTime);
        }

        public void generateExcelToCheck(List<stationInfoRecord> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("站别");
            titleList.Add("状态");
            titleList.Add("日期");
            titleList.Add("维修位置");
            titleList.Add("数量");
            titleList.Add("维修材料");
            titleList.Add("輸入人");

            foreach (stationInfoRecord stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.trackno);
                ct1.Add(stockcheck.station);
                ct1.Add(stockcheck.recordstatus);
                ct1.Add(stockcheck.recorddate);
                ct1.Add(stockcheck.repairplace1);
                ct1.Add(stockcheck.repairnum1);
                ct1.Add(stockcheck.repairMaterial1);
                ct1.Add(stockcheck.inputer);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\MB生命周期信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }
}
