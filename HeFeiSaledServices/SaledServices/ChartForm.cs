using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SaledServices
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒  
            timer.Start();
            //timer.Enabled判断timer是否在运行
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数  
            //int iHour = 23;
            int iMinute = 59;
            int iSecond = 00;

            if (intMinute == iMinute && intSecond == iSecond)
            {
               // Console.WriteLine("每个小时的60分钟开始执行一次！");
                if (intHour % 4 == 0)//每个四个小时
                {
                     ChartForm_Load(null, null);
                }
            }           
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // 设置曲线的样式
            Series series1 = chart1.Series[0];
            // 画样条曲线（Spline）
            series1.ChartType = SeriesChartType.Spline;
            // 线宽2个像素
            series1.BorderWidth = 2;
            // 线的颜色：红色
            series1.Color = System.Drawing.Color.Red;
            // 图示上的文字
            series1.LegendText = "CID";

            LCDDisplay.query();

            int x = 0;
            foreach(string str in LCDDisplay.listDic.Keys)
            {
                series1.Points.AddXY(x, LCDDisplay.listDic[str].totalCidRate);

                CustomLabel label = new CustomLabel();
                label.Text = str;
                label.FromPosition = x - 1;
                label.ToPosition = x + 1;
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);

                x++;
            }
            // 准备数据 
            float[] values = { 95, 30, 20, 23, 60, 87, 42, 7 };

            // 在chart中显示数据
            //
            //foreach (float v in values)
            //{
            //    series1.Points.AddXY(x, v);                

            //    CustomLabel label = new CustomLabel();
            //    label.Text = "自定义"+x;
            //    label.FromPosition = x-1 ;
            //    label.ToPosition = x+1; 
            //    chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);

            //    x++;
            //}

            // 设置显示范围
            //ChartArea chartArea = chart1.ChartAreas[0];
            //chartArea.AxisX.Minimum = 0;
            //chartArea.AxisX.Maximum = 10;
            //chartArea.AxisY.Minimum = 0d;
            //chartArea.AxisY.Maximum = 100d;


            // 设置曲线的样式
            Series series2 = chart1.Series[1];
            // 画样条曲线（Spline）
            series2.ChartType = SeriesChartType.Spline;
            // 线宽2个像素
            series2.BorderWidth = 2;
            // 线的颜色：红色
            series2.Color = System.Drawing.Color.SkyBlue;
            // 图示上的文字
            series2.LegendText = "DOA";

            // 准备数据 
           // float[] values = { 95, 30, 20, 23, 60, 87, 42, 77, 92, 51, 29 };

            // 在chart中显示数据
            x = 0;
            //foreach (float v in values)
            //{
            //    series2.Points.AddXY(x, v);
            //    x++;
            //}

            foreach (string str in LCDDisplay.listDic.Keys)
            {
                series2.Points.AddXY(x, LCDDisplay.listDic[str].totalDoa);

                CustomLabel label = new CustomLabel();
                label.Text = str;
                label.FromPosition = x - 1;
                label.ToPosition = x + 1;
                chart1.ChartAreas[1].AxisX.CustomLabels.Add(label);

                x++;
            }

            // 设置曲线的样式
            Series series3 = chart1.Series[2];
            // 画样条曲线（Spline）
            series3.ChartType = SeriesChartType.Spline;
            // 线宽2个像素
            series3.BorderWidth = 2;
            // 线的颜色：红色
            series3.Color = System.Drawing.Color.Yellow;
            // 图示上的文字
            series3.LegendText = "TAT";

            // 准备数据 
            // float[] values = { 95, 30, 20, 23, 60, 87, 42, 77, 92, 51, 29 };

            // 在chart中显示数据
            x = 0;
            //foreach (float v in values)
            //{
            //    series3.Points.AddXY(x, v);
            //    x++;
            //}
            foreach (string str in LCDDisplay.listDic.Keys)
            {
                series3.Points.AddXY(x, LCDDisplay.listDic[str].totalTat);

                CustomLabel label = new CustomLabel();
                label.Text = str;
                label.FromPosition = x - 1;
                label.ToPosition = x + 1;
                chart1.ChartAreas[2].AxisX.CustomLabels.Add(label);

                x++;
            }

            // 设置曲线的样式
            Series series4 = chart1.Series[3];
            // 画样条曲线（Spline）
            series4.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series4.BorderWidth = 2;
            // 线的颜色：红色
            series4.Color = System.Drawing.Color.Blue;
            // 图示上的文字
            series4.LegendText = "TOTAL";
            x = 0;

            float totalreceive = 0;
            double repairCount = 0;
            double bgaCount = 0;
            double testCount = 0;
            double outlookCount = 0;
            double packageCount = 0;
            double shipCount = 0;
            foreach (string str in LCDDisplay.listDic.Keys)
            {            
                foreach (RmaContent rma in LCDDisplay.listDic[str].rmaList)
                {
                    totalreceive = rma.trackList.Count + totalreceive;
                    repairCount = rma.wip_repair + repairCount;
                    bgaCount = rma.wip_bga + bgaCount;
                    testCount = rma.wip_test + testCount;
                    outlookCount = rma.wip_outlook + outlookCount;
                    packageCount = rma.wip_package + packageCount;
                    shipCount = rma.wip_ship + shipCount;
                }               
            }

            // 准备数据 
            double[] countValue = { totalreceive, repairCount, bgaCount, testCount, outlookCount, packageCount, shipCount };
            string[] colName = { "RECEIVE","REPAIR","BGA","TEST","OUTLOOK","PACKAGE","SHIP"};
            // 在chart中显示数据           

            foreach (float v in countValue)
            {
                series4.Points.AddXY(x, v);
                series4.Points[x].Label = v+"";
                series4.Points[x].LabelForeColor = Color.Blue;

                CustomLabel label = new CustomLabel();
                label.Text = colName[x];
                label.FromPosition = x - 1;
                label.ToPosition = x + 1;
                chart1.ChartAreas[3].AxisX.CustomLabels.Add(label);

                x++;
            }

        }        
    }
}
