using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices.CustomsContentClass
{
    public class StoreInit
    {
        public string ems_no;
        public string cop_g_no;
        public string qty;
        public string unit;
        public string goods_nature;
        public string bom_version;
        public string check_date;
        public string date_type;
        public string whs_code;
        public string location_code;
        public string note;
    }


    public class OpeningStockClass
    {
        public string seq_no;
        public string boxtype;
        public string flowstateg;
        public string trade_code;
        public string ems_no;
        public string status;

        public List<StoreInit> storeInitList;

    }
}
