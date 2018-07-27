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
    public partial class LCDDisplay : Form
    {
        const int DOA_DAYS=90;
        public LCDDisplay()
        {
            InitializeComponent();           
        }
      
        public static int diffDays(string start, string end)
        {
            DateTime dt1 = Convert.ToDateTime(end);
            DateTime dt2 = Convert.ToDateTime(start);
            TimeSpan ts = dt1 - dt2;
            return ts.Days;
        }

        private void LCDDisplay_Load(object sender, EventArgs e)
        {
            DisplayContent displayContent = new DisplayContent();

            //查询最近一个月的内容
            DateTime timeEnd = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")); ;
            DateTime timeStart = Convert.ToDateTime(timeEnd.AddMonths(-1).ToString("yyyy/MM/dd"));

            string startTime = timeStart.ToString("yyyy/MM/dd");
            string endTime = timeEnd.ToString("yyyy/MM/dd");

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT distinct orderno, receivedate FROM receiveOrder where receivedate  between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    RmaContent temp = new RmaContent();
                    temp.rma = querySdr[0].ToString();
                    temp.receiveDate = querySdr[0].ToString();

                    displayContent.rmaList.Add(temp);
                }
                querySdr.Close();

                foreach (RmaContent rma in displayContent.rmaList)
                {
                    cmd.CommandText = "SELECT distinct track_serial_no,order_receive_date,custom_serial_no FROM DeliveredTable where custom_order='" + rma.rma + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        TrackConent temp = new TrackConent();
                        temp.trackno = querySdr[0].ToString();
                        temp.tatStart = querySdr[1].ToString();
                        temp._8s = querySdr[2].ToString();
                        temp.receiveDate = querySdr[1].ToString();
                        rma.trackList.Add(temp);
                    }
                    querySdr.Close();
                }

                foreach (RmaContent rma in displayContent.rmaList)
                {
                    foreach (TrackConent temp in rma.trackList)
                    {
                        cmd.CommandText = "SELECT station FROM stationInformation where track_serial_no='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.station = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        //查询CID
                        cmd.CommandText = "SELECT track_serial_no FROM cidRecord where track_serial_no='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.station = "CID";
                        }
                        querySdr.Close();


                        cmd.CommandText = "SELECT input_date FROM repaired_out_house_excel_table where track_serial_no='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.tatEnd = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        //doa
                        cmd.CommandText = "select order_receive_date from  DeliveredTable where custom_serial_no='" + temp._8s + "' and track_serial_no !='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            string beforedate = querySdr[0].ToString();

                            if (diffDays(beforedate, temp.receiveDate) <= DOA_DAYS)     //sub就是两天相差的天数
                            {
                                temp.doa = "true";
                            }
                        }
                        querySdr.Close();
                    }
                }

                foreach (RmaContent rma in displayContent.rmaList)
                {
                    int totalCount = rma.trackList.Count;

                    double tatcount = 0;
                    double cidcount = 0;
                    double doaCount = 0;

                    double repairCount = 0;
                    double bgaCount = 0;
                    double testCount = 0;
                    double outlookCount = 0;
                    double packageCount = 0;
                    foreach (TrackConent temp in rma.trackList)
                    {
                        switch (temp.station)
                        {
                            case "Package":
                                packageCount = packageCount + 1;
                                break;
                            case "维修":
                                repairCount = repairCount + 1;
                                break;
                            case "CID":
                                cidcount = cidcount + 1;
                                break;
                            case "BGA":
                                bgaCount = bgaCount + 1;
                                break;
                            case "Test1&2":
                                testCount = testCount + 1;
                                break;
                            case "外观":
                                outlookCount = outlookCount + 1;
                                break;
                        }

                        if (temp.doa == "true")
                        {
                            doaCount = doaCount + 1;
                        }

                        temp.tat = diffDays(temp.tatStart, temp.tatEnd);
                        tatcount = tatcount + temp.tat;
                    }

                    rma.tat = tatcount / totalCount * 1.0;
                    rma.doa = doaCount / totalCount * 1.0;
                    rma.cidRate = cidcount / totalCount * 1.0;

                    rma.wip_repair = repairCount;
                    rma.wip_bga = bgaCount;
                    rma.wip_test = testCount;
                    rma.wip_outlook = outlookCount;
                    rma.wip_package = packageCount;
                }

                double totalTatCount = 0;//算总平均值
                double totalDoaCount = 0;
                double totalCidRate = 0;
                int rmaCount = displayContent.rmaList.Count;
                foreach (RmaContent rma in displayContent.rmaList)
                {
                    totalTatCount = totalTatCount + rma.tat;
                    totalDoaCount = totalDoaCount + rma.doa;
                    totalCidRate = totalCidRate + rma.cidRate;
                }

                displayContent.totalTat = Math.Round(totalTatCount / rmaCount,2);
                displayContent.totalDoa = Math.Round(totalDoaCount / rmaCount,2);
                displayContent.totalCidRate = Math.Round(totalCidRate / rmaCount,2);

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.totalTatLabel.Text = displayContent.totalTat.ToString();
            this.totalDoaLabel.Text = displayContent.totalDoa.ToString();
            this.totalcidratelable.Text = displayContent.totalCidRate.ToString();

            foreach (RmaContent rma in displayContent.rmaList)
            {
                rma.tat = Math.Round(rma.tat, 2);
                rma.doa = Math.Round(rma.doa, 2);
                rma.cidRate = Math.Round(rma.cidRate, 2);
            }

             displayContent.rmaList.Reverse();
             dataGridView1.DataSource = displayContent.rmaList;

            string[] hTxt = { "订单号", "TAT", "DOA", "CID RATE", "Repair", "BGA", "TEST", "OUTLOOK", "PACKAGE" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];                
            }

            int height = dataGridView1.Height/(displayContent.rmaList.Count);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Height = height;
            }

          //  dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

        }
    }

    class DisplayContent
    {
        public double totalTat;//算总平均值
        public double totalDoa;
        public double totalCidRate;

        public List<RmaContent> rmaList = new List<RmaContent>();
    }

    class RmaContent
    {
        public string rma{set;get;}
        public string receiveDate;

        public List<TrackConent> trackList = new List<TrackConent>();

        public double tat { set; get; }//算平均值
        public double doa { set; get; }
        public double cidRate { set; get; }

        public double wip_repair { set; get; }
        public double wip_bga { set; get; }
        public double wip_test { set; get; }
        public double wip_outlook { set; get; }
        public double wip_package { set; get; }
    }

    class TrackConent
    {
        public string trackno;
        public string station;//保含cid
        public string tatStart;
        public string tatEnd = DateTime.Now.ToString("yyyy/MM/dd");//end-start = tat
        //public string cid;
        public string doa= "false";//default value

        public string _8s;
        public string receiveDate;

        public int tat;
    }
}
