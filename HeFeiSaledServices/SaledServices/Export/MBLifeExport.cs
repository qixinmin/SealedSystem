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

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd")+" 00:00:00";
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd")+" 23:59:59";

            List<stationInfoRecord> stationList = new List<stationInfoRecord>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                //select top 1000000 Id, trackno, station,recordstatus,convert(varchar(100), recorddate ,20) as k1,repairplace1,repairnum1,repairMaterial1,inputer 

 //from  stationInfoRecord where convert(varchar(100), recorddate ,20) between convert(varchar(100),'2019/8/20',20) and convert(varchar(100),'2019/8/26',20)
 
                //上面的sql有bug， 开始时间写成2019/08/20就查不出来了，很奇怪


                cmd.CommandText = "select Id, trackno, station,recordstatus,recorddate,repairplace1,repairnum1,repairMaterial1,inputer from  " + tableName + " where  recorddate BETWEEN '" + startTime + "' AND '" + endTime + "'";

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

            generateExcelToCheck(stationList, this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"), this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<stationInfoRecord> stationList = new List<stationInfoRecord>();

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                //select top 1000000 Id, trackno, station,recordstatus,convert(varchar(100), recorddate ,20) as k1,repairplace1,repairnum1,repairMaterial1,inputer 

                //from  stationInfoRecord where convert(varchar(100), recorddate ,20) between convert(varchar(100),'2019/8/20',20) and convert(varchar(100),'2019/8/26',20)

                //上面的sql有bug， 开始时间写成2019/08/20就查不出来了，很奇怪


                cmd.CommandText = "select Id, trackno, station,recordstatus,recorddate,repairplace1,repairnum1,repairMaterial1,repairplace2,repairnum2,repairMaterial2,repairplace3,repairnum3,repairMaterial3,repairplace4,repairnum4,repairMaterial4,repairplace5,repairnum5,repairMaterial5,inputer from  stationInfoRecord where Id>376684";

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

                    temp.repairplace2 = querySdr[8].ToString();
                    temp.repairnum2 = querySdr[9].ToString();
                    temp.repairMaterial2 = querySdr[10].ToString();

                    temp.repairplace3 = querySdr[11].ToString();
                    temp.repairnum3 = querySdr[12].ToString();
                    temp.repairMaterial3 = querySdr[13].ToString();

                    temp.repairplace4 = querySdr[14].ToString();
                    temp.repairnum4 = querySdr[15].ToString();
                    temp.repairMaterial4 = querySdr[16].ToString();

                    temp.repairplace5 = querySdr[17].ToString();
                    temp.repairnum5 = querySdr[18].ToString();
                    temp.repairMaterial5 = querySdr[19].ToString();

                    temp.inputer = querySdr[20].ToString();
                    stationList.Add(temp);
                }
                querySdr.Close();

                foreach (stationInfoRecord info in stationList)
                {

                    cmd.CommandText = "insert into stationInfoRecord2  VALUES('" + info.trackno 
            + "','" + info.station + "','" + info.recordstatus + "','" + info.recorddate 
            + "','" + info.repairplace1 + "','" + info.repairnum1 + "','" + info.repairMaterial1
            + "','" + info.repairplace2 + "','" + info.repairnum2 + "','" + info.repairMaterial2
            + "','" + info.repairplace3 + "','" + info.repairnum3 + "','" + info.repairMaterial3
            + "','" + info.repairplace4 + "','" + info.repairnum4 + "','" + info.repairMaterial4
            + "','" + info.repairplace5 + "','" + info.repairnum5 + "','" + info.repairMaterial5
            + "','" + info.inputer + "')";
                    cmd.ExecuteNonQuery();
                }


                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
