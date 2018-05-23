using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices.CustomsContentClass
{
    public class WorkOrderHead
    {
        public string wo_no;
        public string wo_date;
        public string goods_nature;
        public string cop_g_no;
        public string qty;
        public string unit;
        public string emo_no;
    }

    public class WorkListHeadClass
    {
        public string seq_no;
        public string boxtype;
        public string flowstateg;
        public string trade_code;
        public string ems_no;
        public string status;

        public List<WorkOrderHead> workOrderHeadList;
    }
}
