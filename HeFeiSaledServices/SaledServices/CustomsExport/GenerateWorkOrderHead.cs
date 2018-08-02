using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaledServices.CustomsContentClass;
using System.Windows.Forms;

namespace SaledServices.CustomsExport
{
    public class GenerateWorkOrderHead
    {
        WorkListHeadClass workListHead = new WorkListHeadClass();
        List<WorkOrderHead> workOrderHeadList = new List<WorkOrderHead>();

        string trade_code;
        string ems_no;

        string seq_no ="";// DateTime.Now.ToString("yyyyMMdd") + "4002" + "1";//日期+类型,后面需要加入序号信息
        string boxtype = "4002";//代码
        string flowstateg = "";

        string status = "A";

        DateTime currentDay;
        public GenerateWorkOrderHead(string tradeCode, string emsNo,DateTime currentday)
        {
            this.trade_code = tradeCode;
            this.ems_no = emsNo;
            currentDay = currentday;
            seq_no = currentday.ToString("yyyyMMdd") + "4002" + "1";
        }

        public void addWorkListHeads(List<TrackNoCustomRelation> TrackNoCustomRelationList, bool isGood,ref Dictionary<string, string> materialbomDic)
        {

            foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
            {
                WorkOrderHead init1 = new WorkOrderHead();
                init1.wo_no = trackTemp.trackno;
                init1.wo_date = Untils.getCustomDate(trackTemp.date);
                init1.goods_nature = "E";

                //if (isGood)
                //{
                string temp = trackTemp.custom_materialNo;
                if (temp.Length == 10 && temp.StartsWith("000"))
                {
                    temp = temp.Substring(3);
                }

                init1.cop_g_no = temp;//此处要区分对待， 如果良品入库要用正常料号，不良品入库用71料号,之前已经改过，这里直接使用
                //}
                //else
                //{
                //    init1.cop_g_no = materialbomDic[trackTemp.custom_materialNo];
                //}
                init1.qty = "1";
                init1.unit = Untils.getCustomCode(trackTemp.declare_unit);
                init1.emo_no = ems_no;

                workOrderHeadList.Add(init1);
            }
        }

        public void doGenerate(bool isAuto)
        {
            workListHead.seq_no = seq_no;
            workListHead.boxtype = boxtype;
            workListHead.flowstateg = flowstateg;
            workListHead.trade_code = trade_code;
            workListHead.ems_no = ems_no;
            workListHead.status = status;

            workListHead.workOrderHeadList = workOrderHeadList;

            if (workOrderHeadList.Count > 0)
            {
                Untils.createWorkListHeadXML(workListHead, "D:\\MOV\\WO_HEAD" + seq_no + ".xml");
                StockInOutForm.showMessage(currentDay.ToString("yyyyMMdd") + "工单表头信息产生成功！", isAuto);       
            }
            else
            {
                StockInOutForm.showMessage(currentDay.ToString("yyyyMMdd") + "没有工单表头信息产生！", isAuto);       
            }
        }
    }
}
