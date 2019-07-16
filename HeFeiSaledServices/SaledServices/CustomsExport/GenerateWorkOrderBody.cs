using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaledServices.CustomsContentClass;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace SaledServices.CustomsExport
{
    public class GenerateWorkOrderBody
    {
        public WorkListBodyClass workListBody = new WorkListBodyClass();
        List<WorkOrderList> workOrderList = new List<WorkOrderList>();

        string seq_no = "";//DateTime.Now.ToString("yyyyMMdd") + "4003" + "1";//日期+类型,后面需要加入序号信息
        string boxtype = "4003";//代码
        string flowstateg = "";
        string trade_code = "";
        string ems_no = "";

        string status = "A";
        string startTime;
        string endTime;
        //string today = DateTime.Now.ToString("yyyy/MM/dd");

        StockInOutForm stockInOutForm;

        public GenerateWorkOrderBody(string tradeCode, string emsNo, DateTime start, DateTime end, StockInOutForm stockInOutForm)
        {
            this.trade_code = tradeCode;
            this.ems_no = emsNo;
            this.startTime = start.ToString("yyyy/MM/dd");
            this.endTime = end.ToString("yyyy/MM/dd");
            seq_no = end.ToString("yyyyMMdd") + "4003" + "1";
            this.stockInOutForm = stockInOutForm;
        }

        public void addWorkOrderList(List<TrackNoCustomRelation> TrackNoCustomRelationList, ref Dictionary<string, string> _71bomDic)
        {
            try
            {
                List<MaterialCustomRelation> MaterialCustomRelationList = new List<MaterialCustomRelation>();

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //for smt材料
                cmd.CommandText = "select track_serial_no, material_mpn,thisNumber,input_date from fru_smt_used_record where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                    MaterialCustomRelationTemp.id = querySdr[0].ToString();

                    if(_71bomDic.ContainsKey(querySdr[1].ToString()) == false)
                    {
                        MessageBox.Show(querySdr[1].ToString()+"在bom表中不存在！");
                    }

                    MaterialCustomRelationTemp.mpn = _71bomDic[querySdr[1].ToString()];//因为报关原因，需要改成71料号（联想料号）done
                    MaterialCustomRelationTemp.num = "-"+querySdr[2].ToString();//海关要求表体为负数
                    MaterialCustomRelationTemp.date = querySdr[3].ToString();

                    MaterialCustomRelationList.Add(MaterialCustomRelationTemp);                   
                }
                querySdr.Close();

                if (MaterialCustomRelationList.Count > 0)
                {
                    foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                    {
                        cmd.CommandText = "select declare_unit from stock_in_sheet where mpn ='" + materialTemp.mpn + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            materialTemp.declare_unit = querySdr[0].ToString();
                            break;//只取一次信息即可
                        }
                        querySdr.Close();
                    }
                }

                foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                {
                    WorkOrderList init1 = new WorkOrderList();
                    init1.wo_no = materialTemp.id;
                    init1.take_date = Untils.getCustomDate(materialTemp.date);
                    init1.goods_nature = "I";
                    init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                    init1.qty = materialTemp.num;
                    init1.unit =Untils.getCustomCode(materialTemp.declare_unit);
                    init1.emo_no = ems_no;

                    workOrderList.Add(init1);
                }

                //for bga材料
                MaterialCustomRelationList.Clear();
                cmd.CommandText = "select track_serial_no,BGAPN,repair_date from bga_repair_record_table where bga_repair_result!='BGA待换' and bga_repair_date between '" + startTime + "' and '" + endTime + "'";
                querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                    MaterialCustomRelationTemp.id = querySdr[0].ToString();

                    if (_71bomDic.ContainsKey(querySdr[1].ToString()) == false)
                    {
                        MessageBox.Show(querySdr[1].ToString() + "在bom表中不存在！");
                    }

                    MaterialCustomRelationTemp.mpn = _71bomDic[querySdr[1].ToString()];//因为报关原因，需要改成71料号（联想料号）done
                    MaterialCustomRelationTemp.date = querySdr[2].ToString();
                    MaterialCustomRelationTemp.num = "-1";

                    MaterialCustomRelationList.Add(MaterialCustomRelationTemp);
                }
                querySdr.Close();

                if (MaterialCustomRelationList.Count > 0)
                {
                    foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                    {
                        cmd.CommandText = "select declare_unit from stock_in_sheet where mpn ='" + materialTemp.mpn + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            materialTemp.declare_unit = querySdr[0].ToString();
                            break;//只取一次信息即可
                        }
                        querySdr.Close();
                    }
                }

                foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                {
                    WorkOrderList init1 = new WorkOrderList();
                    init1.wo_no = materialTemp.id;
                    init1.take_date = Untils.getCustomDate(materialTemp.date);
                    init1.goods_nature = "I";
                    init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                    init1.qty = materialTemp.num;
                    init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                    init1.emo_no = ems_no;

                    workOrderList.Add(init1);
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void doGenerate(bool isAuto)
        {
            workListBody.seq_no = seq_no;
            workListBody.boxtype = boxtype;
            workListBody.flowstateg = flowstateg;
            workListBody.trade_code = trade_code;
            workListBody.ems_no = ems_no;
            workListBody.status = status;

            workListBody.workOrderList = workOrderList;

            bool isHasError = false;
            foreach (WorkOrderList temp in workOrderList)
            {
                try
                {
                    Int32.Parse(temp.qty);
                }
                catch (Exception ex)
                {
                    isHasError = true;
                    StockInOutForm.showMessage(startTime + "生成工单表体XML文件有误，请检查！", isAuto, true);
                    break;
                }
            }
            if (isHasError == false)
            {
                if (workOrderList.Count > 0)
                {
                    string fileName = seq_no;

                   // if (stockInOutForm.newBankNo.Checked)
                    {
                        fileName = seq_no + "_新账册号";
                    }

                    Untils.createWorkListBodyXML(workListBody, "D:\\MOV\\WO_ITEM" + fileName + ".xml");
                    StockInOutForm.showMessage(startTime + "工单表体信息产生成功！", isAuto);
                }
                else
                {
                    StockInOutForm.showMessage(startTime + "工单表体信息不存在！", isAuto);
                }
            }
            else
            {
                StockInOutForm.showMessage(startTime + "工单表体信息不存在！", isAuto);
            }
        }
    }
}
