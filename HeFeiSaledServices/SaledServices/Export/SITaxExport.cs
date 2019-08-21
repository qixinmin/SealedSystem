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
    public partial class SITaxExport : Form
    {
        public SITaxExport()
        {
            InitializeComponent();
        }

        Dictionary<string, string> materialbomDic = new Dictionary<string, string>();

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

            List<SIRepairRecordStruct> receiveOrderList = new List<SIRepairRecordStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                SqlDataReader querySdr = null;               
                
                cmd.CommandText = "SELECT track_serial_no,COUNT(*)  from repair_record_table where repair_date between '" + startTime + "' and '" + endTime + "'  group by track_serial_no";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    SIRepairRecordStruct temp = new SIRepairRecordStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    // temp.repair_Num = querySdr[1].ToString();
                    temp.mb_num = "1";

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                cmd.CommandText = "select distinct custommaterialNo,vendormaterialNo from MBMaterialCompare";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (materialbomDic.ContainsKey(querySdr[0].ToString().Trim()) == false)
                    {
                        materialbomDic.Add(querySdr[0].ToString().Trim(), querySdr[1].ToString().Trim());
                    }
                }
                querySdr.Close();

                foreach (SIRepairRecordStruct repairRecord in receiveOrderList)
                {
                    cmd.CommandText = "select vendor,product,receivedate,mb_describe,mb_brief,custom_serial_no,vendor_serail_no,mpn,repairer from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        //repairRecord.vendor = querySdr[0].ToString();
                        //repairRecord.product = querySdr[1].ToString();
                        //repairRecord.receivedate = querySdr[2].ToString();
                        //repairRecord.mb_describe = querySdr[3].ToString();
                        repairRecord.mb_brief = querySdr[4].ToString();
                        repairRecord.custom_serial_no = querySdr[5].ToString();
                        //repairRecord.vendor_serail_no = querySdr[6].ToString();
                        //repairRecord.mpn = querySdr[7].ToString();
                        //repairRecord.repairer = querySdr[8].ToString();
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select custom_fault,custommaterialNo from DeliveredTable where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        //repairRecord.custom_fault= querySdr[0].ToString();
                       
                        repairRecord.custommaterialNo = querySdr[1].ToString();

                        if (materialbomDic.ContainsKey(repairRecord.custommaterialNo))
                        {
                            repairRecord.custommaterialNo = materialbomDic[repairRecord.custommaterialNo];
                        }
                    }
                    querySdr.Close();

                    //cmd.CommandText = "select _action,repair_result,repair_date,fault_describe,software_update from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    //querySdr = cmd.ExecuteReader();
                    //repairRecord.subRecords = new List<SISubRepairRecord>();
                    //while (querySdr.Read())
                    //{
                    //    SISubRepairRecord sub = new SISubRepairRecord();

                    //    sub._action = querySdr[0].ToString();
                    //    sub.repair_result = querySdr[1].ToString();
                    //    sub.repair_date = querySdr[2].ToString();
                    //    sub.fault_describe = querySdr[3].ToString();
                    //    sub.software_update = querySdr[4].ToString();

                    //    repairRecord.subRecords.Add(sub);
                    //}
                    //querySdr.Close();

                    //只考虑了带sn号
                    cmd.CommandText = "select BGAPN,bgatype,bga_brief,BGA_place,newSn,product from bga_repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' and newSn !=''";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.bgaRecords = new List<SIBgaRecord>();
                    while (querySdr.Read())
                    {
                        SIBgaRecord sub = new SIBgaRecord();

                        sub.bgampn = querySdr[0].ToString();
                        sub.usedNum = "1";
                        //sub.bgatype = querySdr[1].ToString();
                        //sub.bgabrief = querySdr[2].ToString();
                        //sub.bga_place = querySdr[3].ToString();
                        //sub.newsn = querySdr[4].ToString();
                        //sub.product_type = querySdr[5].ToString();

                        repairRecord.bgaRecords.Add(sub);
                    }
                    querySdr.Close();

                    //现在考虑不带sn的，如VGA
                    cmd.CommandText = "select BGAPN,bgatype,bga_brief,BGA_place,newSn,product from bga_repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' and bgatype='VGA' and bga_repair_result='更换OK待测量'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        SIBgaRecord sub = new SIBgaRecord();

                        sub.bgampn = querySdr[0].ToString();

                        sub.usedNum = "1";
                        //sub.bgatype = querySdr[1].ToString();
                        //sub.bgabrief = querySdr[2].ToString();
                        //sub.bga_place = querySdr[3].ToString();
                        //sub.newsn = querySdr[4].ToString();
                        //sub.product_type = querySdr[5].ToString();                        

                        repairRecord.bgaRecords.Add(sub);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select material_mpn,thisNumber,stock_place from fru_smt_used_record where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.smtRecords = new List<SISmtRecort>();
                    while (querySdr.Read())
                    {
                        SISmtRecort sub = new SISmtRecort();

                        sub.smtMpn = querySdr[0].ToString();
                        sub.usedNum = querySdr[1].ToString();
                        //sub.smtNum = querySdr[1].ToString();
                        //sub.smtplace = querySdr[2].ToString();

                        repairRecord.smtRecords.Add(sub);
                    }
                    querySdr.Close();

                    //if (checkBox_other_materials.Checked)
                    //{
                    //    //拆件的数据
                    //    cmd.CommandText = "select material_mpn,thisNumber,stock_place from fru_smt_used_record_other where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    //    querySdr = cmd.ExecuteReader();
                    //    repairRecord.smtRecords = new List<SISmtRecort>();
                    //    while (querySdr.Read())
                    //    {
                    //        SISmtRecort sub = new SISmtRecort();

                    //        sub.smtMpn = querySdr[0].ToString();
                    //        //sub.smtNum = querySdr[1].ToString();
                    //        //sub.smtplace = querySdr[2].ToString();

                    //        repairRecord.smtRecords.Add(sub);
                    //    }
                    //    querySdr.Close();
                    //}
                }
                //
                foreach (SIRepairRecordStruct repairRecord in receiveOrderList)
                {
                    foreach (SIBgaRecord bgarecord in repairRecord.bgaRecords)
                    {
                        cmd.CommandText = "select pricePer from stock_in_sheet where mpn ='" + bgarecord.bgampn + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            bgarecord.thisprice = querySdr[0].ToString();
                            if (bgarecord.thisprice != null)
                            {
                                bgarecord.totalMoney = Double.Parse(bgarecord.thisprice) * Double.Parse(bgarecord.usedNum) + "";
                            }
                            else
                            {
                                bgarecord.totalMoney = "0";
                            }
                            break;
                        }
                        querySdr.Close();

                        //if (bgarecord.product_type.ToUpper() == "SERVER")//将来需要改进
                        //{
                        //    cmd.CommandText = "select material_vendor,product_date,pici from server_material_more_information where mpn ='" + bgarecord.bgampn + "'";
                        //    querySdr = cmd.ExecuteReader();
                        //    while (querySdr.Read())
                        //    {
                        //        bgarecord.material_vendor = querySdr[0].ToString();
                        //        bgarecord.product_date = querySdr[1].ToString();
                        //        bgarecord.pici = querySdr[2].ToString();
                        //        break;
                        //    }
                        //    querySdr.Close();
                        //}
                    }
                }


                foreach (SIRepairRecordStruct repairRecord in receiveOrderList)
                {
                    foreach (SISmtRecort smtrecord in repairRecord.smtRecords)
                    {
                        cmd.CommandText = "select pricePer from stock_in_sheet where mpn ='" + smtrecord.smtMpn + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            smtrecord.thisprice = querySdr[0].ToString();
                            if (smtrecord.thisprice != null)
                            {
                                smtrecord.totalMoney = Double.Parse(smtrecord.thisprice) * Double.Parse(smtrecord.usedNum) + "";
                            }
                            else
                            {
                                smtrecord.totalMoney = "0";
                            }
                            break;
                        }
                        querySdr.Close();
                    }
                }

                foreach (SIRepairRecordStruct repairRecord in receiveOrderList)
                {
                    if (repairRecord.smtRecords.Count == 0 && repairRecord.bgaRecords.Count == 0)
                    {
                        repairRecord.totalMoney = "25";
                        SIBgaRecord sub = new SIBgaRecord();

                        sub.bgampn = "无故障";
                        repairRecord.bgaRecords.Add(sub);
                        continue;
                    }
                    else
                    {
                        double totalMoney = 25.0;

                        foreach (SIBgaRecord bgarecord in repairRecord.bgaRecords)
                        {
                            try
                            {
                                totalMoney += Double.Parse(bgarecord.totalMoney);
                            }
                            catch (Exception ex)
                            {
                                totalMoney += 0;
                            }
                        }


                        foreach (SISmtRecort smtrecord in repairRecord.smtRecords)
                        {
                            try
                            {
                                totalMoney += Double.Parse(smtrecord.totalMoney);
                            }
                            catch (Exception ex)
                            {
                                totalMoney += 0;
                            }
                        }

                        repairRecord.totalMoney = totalMoney+"";
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<SIRepairRecordStruct> repairRecordList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
          
            titleList.Add("机型");
            titleList.Add("料号");
            titleList.Add("数量");
            titleList.Add("8S码");
            titleList.Add("Tracking#");
            titleList.Add("金额");
            
            int bgaLength = 3;
            int smtLength = 10;

            for (int i = 1; i <= bgaLength; i++)
            {
                titleList.Add("更换材料" + i);
                titleList.Add("品名" + i);
                titleList.Add("数量" + i);
                titleList.Add("价格" + i);
                titleList.Add("金额" + i);
            }

            for (int i = 1; i <= smtLength; i++)
            {
                titleList.Add("更换材料" + i);
                titleList.Add("品名" + i);
                titleList.Add("数量" + i);
                titleList.Add("价格" + i);
                titleList.Add("金额" + i);
            }

            foreach (SIRepairRecordStruct repaircheck in repairRecordList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(repaircheck.mb_brief);
                ct1.Add(repaircheck.custommaterialNo);
                ct1.Add(repaircheck.mb_num);
                ct1.Add(repaircheck.custom_serial_no);
                ct1.Add(repaircheck.track_serial_no);
                ct1.Add(repaircheck.totalMoney);

                for (int i = 0; i < bgaLength; i++)
                {
                    if (i < repaircheck.bgaRecords.Count)
                    {
                        ct1.Add(repaircheck.bgaRecords[i].bgampn);
                        ct1.Add(repaircheck.bgaRecords[i].pinming);
                        ct1.Add(repaircheck.bgaRecords[i].usedNum);
                        ct1.Add(repaircheck.bgaRecords[i].thisprice);
                        ct1.Add(repaircheck.bgaRecords[i].totalMoney);
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

                for (int i = 0; i < smtLength; i++)
                {
                    if (i < repaircheck.smtRecords.Count)
                    {
                        ct1.Add(repaircheck.smtRecords[i].smtMpn);
                        ct1.Add(repaircheck.smtRecords[i].pinming);
                        ct1.Add(repaircheck.smtRecords[i].usedNum);
                        ct1.Add(repaircheck.smtRecords[i].thisprice);
                        ct1.Add(repaircheck.smtRecords[i].totalMoney);
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

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }
            
            Untils.createExcel("D:\\SI交税" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }

        private void RepairRecordExport_Load(object sender, EventArgs e)
        {

        }
    }

    public class SIrepairRecord
    {
        public string track_serial_no;
        public string orderno;
        public string custom_serial_no;
        public int count;
    }

    /**
     * SELECT track_serial_no,COUNT(*) as 次数
     
  FROM [SaledService].[dbo].[repair_record_table] group by track_serial_no
     * */
    public class SIRepairRecordStruct
    {
        public string mb_brief;
        public string custommaterialNo;
        public string mb_num;
        public string custom_serial_no;
        public string track_serial_no;
        public string totalMoney;

        //public string vendor;
        //public string product;
        //public string receivedate;
        //public string mb_describe;       
       
        //public string vendor_serail_no;
        //public string mpn;
        //public string repairer;
        //public string custom_fault;
       
        //public string repair_Num;//维修次数

        //public List<SISubRepairRecord> subRecords;

        public List<SIBgaRecord> bgaRecords;
        public List<SISmtRecort> smtRecords;
    }

    //public class SISubRepairRecord
    //{
    //    public string fault_describe;
    //    public string software_update;
    //    public string _action;//维修动作
    //    public string repair_result;//维修结果
    //    public string repair_date; /*修复日期*/
    //}

    public class SIBgaRecord
   {         
       public string bgampn;
       public string pinming;//品名
       public string usedNum;
       public string thisprice;
       public string totalMoney;

       //public string bgatype;
       //public string bgabrief;
       //public string bgambfa1;
       //public string bgashort_cut;
       //public string bga_place;
       //public string newsn;

       //public string product_type;//材料类别，判断是否为server
       //public string material_vendor;// NVARCHAR(128),/*原材料厂商*/
       //public string product_date;// date,/*生产日期*/
       //public string pici;// NVARCHAR(128),/*生产批次*/
   }

    public class SISmtRecort
   {
       public string smtMpn;
       public string pinming;//品名
       public string usedNum;
       public string thisprice;
       public string totalMoney;

       //public string smtplace;
       //public string smtNum;

       //public string product_type;//材料类别，判断是否为server
       //public string material_vendor;// NVARCHAR(128),/*原材料厂商*/
       //public string product_date;// date,/*生产日期*/
       //public string pici;// NVARCHAR(128),/*生产批次*/
   }
}
