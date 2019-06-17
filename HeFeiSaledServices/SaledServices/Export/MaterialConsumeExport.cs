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
    public partial class MaterialConsumeExport : Form
    {
        public MaterialConsumeExport()
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

            List<MaterialConsumeStruct> receiveOrderList = new List<MaterialConsumeStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                //SELECT material_mpn, SUM(cast(thisNumber as int))
                //FROM [SaledService].[dbo].[fru_smt_used_record]  where input_date >= '2018-07-18' group by material_mpn
                cmd.CommandText = "select material_mpn, SUM(cast(thisNumber as int)) from fru_smt_used_record where input_date between '" + startTime + "' and '" + endTime + "' group by material_mpn";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MaterialConsumeStruct temp = new MaterialConsumeStruct();
                    temp.mpn =  querySdr[0].ToString();
                    temp.out_number =  querySdr[1].ToString();
                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                //SELECT BGAPN, count(*)
                //FROM [SaledService].[dbo].[bga_repair_record_table] where bga_repair_date >= '2018-07-17' and newSn !='' group by BGAPN

                cmd.CommandText = "select BGAPN, count(*) from bga_repair_record_table where bga_repair_date between '" + startTime + "' and '" + endTime + "' and newSn !='' group by BGAPN";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MaterialConsumeStruct temp = new MaterialConsumeStruct();
                    temp.mpn = querySdr[0].ToString();
                    temp.out_number = querySdr[1].ToString();
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                //加入VGA
                cmd.CommandText = "select BGAPN, count(*) from bga_repair_record_table where bga_repair_date between '" + startTime + "' and '" + endTime + "' and bgatype='VGA' and bga_repair_result='更换OK待测量'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MaterialConsumeStruct temp = new MaterialConsumeStruct();
                    temp.mpn = querySdr[0].ToString();
                    temp.out_number = querySdr[1].ToString();
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();


                foreach (MaterialConsumeStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = "select house, place,number  from store_house where mpn='"+stockcheck.mpn+"'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stockcheck.stock_place = querySdr[0].ToString() + "," + querySdr[1].ToString();
                        stockcheck.left_number = querySdr[2].ToString();
                    }
                    querySdr.Close();
                }

                //材料消耗信息需要增加一列数据---某段时间内出库数据
                foreach (MaterialConsumeStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = "select mpn, count(*) from fru_smt_out_stock where input_date between '" + startTime + "' and '" + endTime + "' and mpn='"+stockcheck.mpn+"' group by mpn";
                    querySdr = cmd.ExecuteReader();
                    string number = "";
                    while (querySdr.Read())
                    {
                        number = querySdr[1].ToString();
                    }
                    querySdr.Close();
                    if (number == "")
                    {
                        cmd.CommandText = "select mpn, count(*) from bga_out_stock where input_date between '" + startTime + "' and '" + endTime + "' and mpn='" + stockcheck.mpn + "' group by mpn";
                        querySdr = cmd.ExecuteReader();
                        number = "";
                        while (querySdr.Read())
                        {
                            number = querySdr[1].ToString();
                        }
                        querySdr.Close();
                    }

                    stockcheck.stock_out_number = number;
                }

                //foreach (MaterialConsumeStruct stockcheck in receiveOrderList)
                //{
                //    cmd.CommandText = "select house, place,number  from store_house where mpn='" + stockcheck.mpn + "'";
                //    querySdr = cmd.ExecuteReader();
                //    while (querySdr.Read())
                //    {
                //        stockcheck.stock_place = querySdr[0].ToString() + "," + querySdr[1].ToString();
                //        stockcheck.left_number = querySdr[2].ToString();
                //    }
                //    querySdr.Close();
                //}

                foreach (MaterialConsumeStruct stockcheck in receiveOrderList)
                {
                    cmd.CommandText = "select describe from stock_in_sheet where mpn='" + stockcheck.mpn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stockcheck.brief = querySdr[0].ToString();
                        break;//只需要一条记录即可
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<MaterialConsumeStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("MPN");
            titleList.Add("描述");
            titleList.Add("存储位置");
            titleList.Add("此段时间出库数量");
            titleList.Add("消耗数量");
            titleList.Add("当前库存数量");

            foreach (MaterialConsumeStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.brief);
                ct1.Add(stockcheck.stock_place);
                ct1.Add(stockcheck.stock_out_number);
                ct1.Add(stockcheck.out_number);
                ct1.Add(stockcheck.left_number);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Untils.createExcel("D:\\材料消耗信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class MaterialConsumeStruct
    {      
        public string mpn;
        public string brief;
        public string stock_place;
        public string stock_out_number;
        public string out_number;        
        public string left_number;
    }
}
