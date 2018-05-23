using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices.CustomsContentClass
{
    public class MaterialCustomRelation
    {
        public string id;
        public string mpn;
        public string num;
        public string buy_order_serial_no;
        public string declare_unit;
        public string declare_number;
        public string custom_request_number;

        public bool needReport;//如果报关单号与申请单号都没有则不需要上报海关， 如果只有报关单号，则I0002报关入库，如果只有申请单号，则I0001非报关入库
        public string type;//I0002/I0001

        public string date;
    }

    public class TrackNoCustomRelation
    {
        public string trackno;
        public string custom_materialNo;
        public string orderno;
        public string declare_unit;
        public string declare_number;
        public string custom_request_number;
        public string date;
    }

    public class StoreTrans
    {
        public string ems_no;
        public string io_no;
        public string goods_nature;
        
        public string io_date;

        public string cop_g_no;
        public string qty;
        public string unit;
        public string type;
        public string chk_code;
        public string entry_id;
        public string gatejob_no;
        public string whs_code;
        public string location_code;
        public string note;
    }

    public class StockInOutClass
    {
        public string seq_no;
        public string boxtype;
        public string flowstateg;
        public string trade_code;
        public string ems_no;
        public string status;

        public List<StoreTrans> storeTransList;
    }
}
