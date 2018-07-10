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
    public partial class RepairRecordExport : Form
    {
        public RepairRecordExport()
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

            List<RepairRecordStruct> receiveOrderList = new List<RepairRecordStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT track_serial_no,COUNT(*)  from repair_record_table where repair_date between '" + startTime + "' and '" + endTime + "'  group by track_serial_no";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    RepairRecordStruct temp = new RepairRecordStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.repair_Num = querySdr[1].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (RepairRecordStruct repairRecord in receiveOrderList)
                {
                    cmd.CommandText = "select vendor,product,receivedate,mb_describe,mb_brief,custom_serial_no,vendor_serail_no,mpn from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.vendor = querySdr[0].ToString();
                        repairRecord.product = querySdr[1].ToString();
                        repairRecord.receivedate = querySdr[2].ToString();
                        repairRecord.mb_describe = querySdr[3].ToString();
                        repairRecord.mb_brief = querySdr[4].ToString();
                        repairRecord.custom_serial_no = querySdr[5].ToString();
                        repairRecord.vendor_serail_no = querySdr[6].ToString();
                        repairRecord.mpn = querySdr[7].ToString();
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select _action,repair_result,repair_date from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.subRecords = new List<SubRepairRecord>();
                    while (querySdr.Read())
                    {
                        SubRepairRecord sub = new SubRepairRecord();

                        sub._action = querySdr[0].ToString();
                        sub.repair_result = querySdr[1].ToString();
                        sub.repair_date = querySdr[2].ToString();

                        repairRecord.subRecords.Add(sub);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select BGAPN,bgatype,bga_brief,BGA_place from bga_repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' and newSn !=''";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.bgaRecords = new List<BgaRecord>();
                    while (querySdr.Read())
                    {
                        BgaRecord sub = new BgaRecord();

                        sub.bgampn = querySdr[0].ToString();
                        sub.bgatype = querySdr[1].ToString();
                        sub.bgabrief = querySdr[2].ToString();
                        sub.bganum = "1";

                        repairRecord.bgaRecords.Add(sub);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select material_mpn,thisNumber,stock_place from fru_smt_used_record where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.smtRecords = new List<SmtRecort>();
                    while (querySdr.Read())
                    {
                        SmtRecort sub = new SmtRecort();

                        sub.smtMpn = querySdr[0].ToString();
                        sub.smtNum = querySdr[1].ToString();                       

                        repairRecord.smtRecords.Add(sub);
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList);
        }

        public void generateExcelToCheck(List<RepairRecordStruct> repairRecordList)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();


        //public List<SubRepairRecord> subRecords;

        //public List<BgaRecord> bgaRecords;
        //public List<SmtRecort> smtRecords;

          
            titleList.Add("跟踪条码");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("收货日期");
            titleList.Add("MB描述");
            titleList.Add("MB简称");
            titleList.Add("客户序号");
            titleList.Add("厂商序号");
            titleList.Add("MPN");
            titleList.Add("维修次数");

            for(int i=1;i<=5;i++)
            {
                 titleList.Add("维修动作"+i);
                 titleList.Add("维修结果"+i);
                 titleList.Add("维修日期"+i);
            }
     
            for (int i = 1; i <= 5; i++)
            {
                titleList.Add("BGA MPN" + i);
                titleList.Add("BGA类型" + i);
                titleList.Add("BGA简称" + i);
                titleList.Add("BGA描述" + i);
                titleList.Add("BGA数量" + i);
            }
    
            for (int i = 1; i <= 10; i++)
            {
                titleList.Add("SMT MPN" + i);
                titleList.Add("SMT描述" + i);
                titleList.Add("SMT数量" + i);
            }


            foreach (RepairRecordStruct repaircheck in repairRecordList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(repaircheck.track_serial_no);
                ct1.Add(repaircheck.vendor);
                ct1.Add(repaircheck.product);
                ct1.Add(repaircheck.receivedate);
                ct1.Add(repaircheck.mb_describe);
                ct1.Add(repaircheck.mb_brief);
                ct1.Add(repaircheck.custom_serial_no);
                ct1.Add(repaircheck.vendor_serail_no);
                ct1.Add(repaircheck.mpn);
                ct1.Add(repaircheck.repair_Num);

                for (int i = 0; i < 5; i++)
                {
                    if (i < repaircheck.subRecords.Count)
                    {
                        ct1.Add(repaircheck.subRecords[i]._action);
                        ct1.Add(repaircheck.subRecords[i].repair_result);
                        ct1.Add(repaircheck.subRecords[i].repair_date);
                    }
                    else
                    {
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    if (i < repaircheck.bgaRecords.Count)
                    {
                        ct1.Add(repaircheck.bgaRecords[i].bgampn);
                        ct1.Add(repaircheck.bgaRecords[i].bgatype);
                        ct1.Add(repaircheck.bgaRecords[i].bgabrief);
                        ct1.Add(repaircheck.bgaRecords[i].bgadescribe);
                        ct1.Add(repaircheck.bgaRecords[i].bganum);
                    }
                    else
                    {
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (i < repaircheck.smtRecords.Count)
                    {
                        ct1.Add(repaircheck.smtRecords[i].smtMpn);
                        ct1.Add(repaircheck.smtRecords[i].smtDescribe);
                        ct1.Add(repaircheck.smtRecords[i].smtNum);
                    }
                    else
                    {
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                    }
                }

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\RepairReecordExort.xlsx", titleList, contentList);
        }
    }

    /**
     * SELECT track_serial_no,COUNT(*) as 次数
     
  FROM [SaledService].[dbo].[repair_record_table] group by track_serial_no
     * */
    public class RepairRecordStruct
    {
        public string track_serial_no;
        public string vendor;
        public string product;
        public string receivedate;
        public string mb_describe;
        public string mb_brief;
        public string custom_serial_no;
        public string vendor_serail_no;
        public string mpn;
        public string repair_Num;//维修次数

        public List<SubRepairRecord> subRecords;

        public List<BgaRecord> bgaRecords;
        public List<SmtRecort> smtRecords;
    }

    public class SubRepairRecord
    {
        public string _action;//维修动作
        public string repair_result;//维修结果
        public string repair_date; /*修复日期*/
    }

   public class BgaRecord
   {         
       public string bgampn;
       public string bgatype;
       public string bgabrief;
       public string bgadescribe;
       public string bganum;
   }

   public class SmtRecort
   {
       public string smtMpn;
       public string smtDescribe;
       public string smtNum;
   }
}
