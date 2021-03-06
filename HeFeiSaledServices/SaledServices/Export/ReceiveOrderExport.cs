﻿using System;
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
    public partial class ReceiveOrderExport : Form
    {
        public ReceiveOrderExport()
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

            List<ReceiveOrderStruct> receiveOrderList = new List<ReceiveOrderStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select custom_order,track_serial_no,custommaterialNo,custom_serial_no,dpk_status,mac,mpn,mb_describe from DeliveredTable where order_receive_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReceiveOrderStruct temp = new ReceiveOrderStruct();
                    temp.orderNo =  querySdr[0].ToString();
                    temp.trackno =  querySdr[1].ToString();
                    temp.customMaterialNo = querySdr[2].ToString();
                    temp.custom_serial_no= querySdr[3].ToString();
                    temp.dpktype = querySdr[4].ToString();
                    temp.mac = querySdr[5].ToString();
                    temp.mpn = querySdr[6].ToString();
                    temp.mbdescribe = querySdr[7].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList,startTime, endTime);
        }

        public void generateExcelToCheck(List<ReceiveOrderStruct> StockCheckList,string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("订单号");
            titleList.Add("跟踪条码");
            titleList.Add("客户料号");
            titleList.Add("8S码");
            titleList.Add("DPK类型");
            titleList.Add("MAC");
            titleList.Add("MPN");
            titleList.Add("MB描述");

            foreach (ReceiveOrderStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.orderNo);
                ct1.Add(stockcheck.trackno);
                ct1.Add(stockcheck.customMaterialNo);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.dpktype);
                ct1.Add(stockcheck.mac);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mbdescribe);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\收货单信息" +startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class ReceiveOrderStruct
    {
        public string orderNo;
        public string trackno;
        public string customMaterialNo;
        public string custom_serial_no;
        public string dpktype;
        public string mac;
        public string mpn;
        public string mbdescribe;
    }
}
