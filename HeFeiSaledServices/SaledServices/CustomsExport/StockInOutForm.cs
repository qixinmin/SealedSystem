using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaledServices.CustomsContentClass;
using System.Data.SqlClient;

namespace SaledServices.CustomsExport
{
    public partial class StockInOutForm : Form
    {
        public StockInOutForm()
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
            
            Dictionary<string, string> _71bomDic = new Dictionary<string, string>();
            Dictionary<string, string> materialbomDic = new Dictionary<string, string>();
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //查询71bom
                cmd.CommandText = "select distinct material_mpn,material_vendor_pn from LCFC71BOM_table";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (_71bomDic.ContainsKey(querySdr[0].ToString()) == false)
                    {
                        _71bomDic.Add(querySdr[0].ToString(), querySdr[1].ToString());
                    }
                }
                querySdr.Close();
                //查询物料对照表

                cmd.CommandText = "select distinct custommaterialNo,vendormaterialNo from MBMaterialCompare";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (materialbomDic.ContainsKey(querySdr[0].ToString()) == false)
                    {
                        materialbomDic.Add(querySdr[0].ToString(), querySdr[1].ToString());
                    }
                }
                querySdr.Close();
                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            for (DateTime dt = time1; dt <= time2; dt = dt.AddDays(1))
            {
                //MessageBox.Show(dt.ToString("yyyy/MM/dd"));
                //生成每天的数据
                string startTime = dt.ToString("yyyy/MM/dd");
                string endTime = dt.ToString("yyyy/MM/dd");

                StockInOutClass stockinout = new StockInOutClass();
                List<StoreTrans> storeTransList = new List<StoreTrans>();

                GenerateWorkOrderHead generateWorkOrderHead = null;

                GenerateWorkOrderBody generateWorkOrderBody = null;

                string seq_no = dt.ToString("yyyyMMdd") + "4001" + "1";//日期+类型,后面需要加入序号信息
                string boxtype = "4001";//代码
                string flowstateg = "";
                string trade_code = "";
                string ems_no = "";

                string status = "A";
                //string today = DateTime.Now.ToString("yyyy/MM/dd");
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select indentifier, book_number from company_fixed_table";
                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        trade_code = querySdr[0].ToString();
                        ems_no = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    generateWorkOrderHead = new GenerateWorkOrderHead(trade_code, ems_no, dt);
                    generateWorkOrderBody = new GenerateWorkOrderBody(trade_code, ems_no, dt);

                    //板子入库信息,过滤条件是今天DateTime.Now.ToString("yyyy/MM/dd")

                    //1 入待维修库信息
                    List<TrackNoCustomRelation> TrackNoCustomRelationList = new List<TrackNoCustomRelation>();
                    cmd.CommandText = "select track_serial_no,custom_materialNo,input_date from wait_repair_in_house_table where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        TrackNoCustomRelation TrackNoCustomRelationTemp = new TrackNoCustomRelation();
                        TrackNoCustomRelationTemp.trackno = querySdr[0].ToString();
                        TrackNoCustomRelationTemp.custom_materialNo = querySdr[1].ToString();//正常使用客户料号
                        TrackNoCustomRelationTemp.date = querySdr[2].ToString();

                        TrackNoCustomRelationList.Add(TrackNoCustomRelationTemp);
                    }
                    querySdr.Close();
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select custom_order from DeliveredTable where track_serial_no ='" + trackTemp.trackno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.orderno = querySdr[0].ToString();
                            }
                            querySdr.Close();
                        }

                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from receiveOrder where orderno ='" + trackTemp.orderno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.declare_unit = querySdr[0].ToString();
                                trackTemp.declare_number = querySdr[1].ToString();
                                trackTemp.custom_request_number = querySdr[2].ToString();
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = trackTemp.trackno;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(trackTemp.date);
                            init1.cop_g_no = trackTemp.custom_materialNo;
                            init1.qty = "1";
                            init1.unit = Untils.getCustomCode(trackTemp.declare_unit);
                            init1.type = "I0002";
                            init1.chk_code = "";
                            init1.entry_id = trackTemp.declare_number;
                            init1.gatejob_no = trackTemp.custom_request_number;
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //2 待维修出库信息，复用上面的信息
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        //信息完全,生成信息
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = trackTemp.trackno;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(trackTemp.date);
                            init1.cop_g_no = trackTemp.custom_materialNo;//正常使用客户料号
                            init1.qty = "-1";
                            init1.unit = Untils.getCustomCode(trackTemp.declare_unit);
                            init1.type = "E0003";//其他出库
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";


                            storeTransList.Add(init1);
                        }
                    }

                    //3 良品入库信息
                    TrackNoCustomRelationList.Clear();
                    cmd.CommandText = "select track_serial_no,custom_materialNo,input_date from repaired_in_house_table where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        TrackNoCustomRelation TrackNoCustomRelationTemp = new TrackNoCustomRelation();
                        TrackNoCustomRelationTemp.trackno = querySdr[0].ToString();
                        TrackNoCustomRelationTemp.custom_materialNo = querySdr[1].ToString();//正常使用客户料号
                        TrackNoCustomRelationTemp.date = querySdr[2].ToString();

                        TrackNoCustomRelationList.Add(TrackNoCustomRelationTemp);
                    }
                    querySdr.Close();
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select custom_order from DeliveredTable where track_serial_no ='" + trackTemp.trackno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.orderno = querySdr[0].ToString();
                            }
                            querySdr.Close();
                        }

                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from receiveOrder where orderno ='" + trackTemp.orderno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.declare_unit = querySdr[0].ToString();
                                trackTemp.declare_number = querySdr[1].ToString();
                                trackTemp.custom_request_number = querySdr[2].ToString();
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = trackTemp.trackno;
                            init1.goods_nature = "E";//成品
                            init1.io_date = Untils.getCustomDate(trackTemp.date);
                            init1.cop_g_no = trackTemp.custom_materialNo;//正常使用客户料号
                            init1.qty = "1";
                            init1.unit = Untils.getCustomCode(trackTemp.declare_unit);
                            init1.type = "I0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }

                        generateWorkOrderHead.addWorkListHeads(TrackNoCustomRelationList,true, ref materialbomDic);
                    }

                    //4. 良品出库，复用上面的信息，TODO 方案是采用excel上传的方式，比对条形码，料号，保证是今天上传的数据，然后读取上传的数据生成良品出库报文                                
                    TrackNoCustomRelationList.Clear();
                    cmd.CommandText = "select track_serial_no,custom_materialNo,declare_number,input_date from repaired_out_house_excel_table where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        TrackNoCustomRelation TrackNoCustomRelationTemp = new TrackNoCustomRelation();
                        TrackNoCustomRelationTemp.trackno = querySdr[0].ToString();
                        TrackNoCustomRelationTemp.custom_materialNo = querySdr[1].ToString();//正常使用客户料号
                        TrackNoCustomRelationTemp.declare_number = querySdr[2].ToString();
                        TrackNoCustomRelationTemp.date = querySdr[3].ToString();

                        TrackNoCustomRelationList.Add(TrackNoCustomRelationTemp);
                    }
                    querySdr.Close();
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        //信息完全,生成信息
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = trackTemp.trackno;
                            init1.goods_nature = "E";
                            init1.io_date = Untils.getCustomDate(trackTemp.date);
                            init1.cop_g_no = trackTemp.custom_materialNo;//正常使用客户料号
                            init1.qty = "-1";
                            init1.unit = "007";
                            init1.type = "E0002";//报关出库
                            init1.chk_code = "";
                            init1.entry_id = trackTemp.declare_number;
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //5 MB不良品库入库信息
                    TrackNoCustomRelationList.Clear();
                    cmd.CommandText = "select track_serial_no,custommaterialNo,repair_date from fault_mb_enter_record_table where repair_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        TrackNoCustomRelation TrackNoCustomRelationTemp = new TrackNoCustomRelation();
                        TrackNoCustomRelationTemp.trackno = querySdr[0].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo= querySdr[1].ToString();                       
                        if (materialbomDic.ContainsKey(nowMatrialNo))//主板只查询物料对照表
                        {
                            currentDeclear = materialbomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("MB不良品库入库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        TrackNoCustomRelationTemp.custom_materialNo = currentDeclear;//因为报关原因，需要改成71料号（联想料号）done
                        TrackNoCustomRelationTemp.date = querySdr[2].ToString();

                        TrackNoCustomRelationList.Add(TrackNoCustomRelationTemp);
                    }
                    querySdr.Close();
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select custom_order from DeliveredTable where track_serial_no ='" + trackTemp.trackno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.orderno = querySdr[0].ToString();
                            }
                            querySdr.Close();
                        }

                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from receiveOrder where orderno ='" + trackTemp.orderno + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                trackTemp.declare_unit = querySdr[0].ToString();
                                trackTemp.declare_number = querySdr[1].ToString();
                                trackTemp.custom_request_number = querySdr[2].ToString();
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (TrackNoCustomRelation trackTemp in TrackNoCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = trackTemp.trackno;
                            init1.goods_nature = "E";
                            init1.io_date = Untils.getCustomDate(trackTemp.date);
                            init1.cop_g_no = trackTemp.custom_materialNo;//因为报关原因，需要改成71料号（联想料号）-》上面已经修改
                            init1.qty = "1";
                            init1.unit = Untils.getCustomCode(trackTemp.declare_unit);
                            init1.type = "I0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }

                        generateWorkOrderHead.addWorkListHeads(TrackNoCustomRelationList,false,ref materialbomDic);
                    }

                    //6 mb/smt/bga不良品库出库信息
                    List<MaterialCustomRelation> MaterialCustomRelationList = new List<MaterialCustomRelation>();
                    TrackNoCustomRelationList.Clear();
                    cmd.CommandText = "select Id,mpn,in_number,declare_unit,declare_number,custom_request_number,input_date from mb_smt_bga_ng_out_house_table where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[1].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))
                        {
                            currentDeclear = _71bomDic[nowMatrialNo] +"-1";//不良品的料号要加-1
                        }
                        else if (materialbomDic.ContainsKey(nowMatrialNo))//主板只查询物料对照表
                        {
                            currentDeclear = materialbomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("mb/smt/bga不良品库出库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）
                        MaterialCustomRelationTemp.num = querySdr[2].ToString();
                        MaterialCustomRelationTemp.declare_unit = querySdr[3].ToString();
                        MaterialCustomRelationTemp.declare_number = querySdr[4].ToString();
                        MaterialCustomRelationTemp.custom_request_number = querySdr[5].ToString();
                        MaterialCustomRelationTemp.date = querySdr[6].ToString();

                        MaterialCustomRelationList.Add(MaterialCustomRelationTemp);
                    }
                    querySdr.Close();
                    if (TrackNoCustomRelationList.Count > 0)
                    {
                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;

                            cmd.CommandText = "select Id from MBMaterialCompare where custommaterialNo='" + materialTemp.mpn + "'";
                            querySdr = cmd.ExecuteReader();
                            string Id = "";
                            while (querySdr.Read())
                            {
                                Id = querySdr[0].ToString();
                            }
                            if (Id != "")//查到板子的信息
                            {
                                init1.goods_nature = "E";
                            }
                            else
                            {
                                init1.goods_nature = "I";
                            }

                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）->上面已经修改
                            init1.qty = "-" + materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = "E0002";
                            init1.chk_code = "";
                            init1.entry_id = materialTemp.declare_number;
                            init1.gatejob_no = materialTemp.custom_request_number;
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //7 SMT/FRU 入库 信息,todo 牵扯到区内流程的材料，只有申请单号，没有报关单好，todo, 也有报关单号的
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,buy_order_serial_no,mpn,stock_in_num,input_date from fru_smt_in_stock where input_date between '" + startTime + "' and '" + endTime + "'  and isdeclare='是'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        MaterialCustomRelationTemp.buy_order_serial_no = querySdr[1].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[2].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))//非主板材料只需要查询bom表格
                        {
                            currentDeclear = _71bomDic[nowMatrialNo];
                        }                        
                        else
                        {
                            MessageBox.Show("SMT/FRU 入库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）TODO
                        MaterialCustomRelationTemp.num = querySdr[3].ToString();
                        MaterialCustomRelationTemp.date = querySdr[4].ToString();

                        MaterialCustomRelationList.Add(MaterialCustomRelationTemp);
                    }
                    querySdr.Close();

                    if (MaterialCustomRelationList.Count > 0)
                    {
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from stock_in_sheet where buy_order_serial_no ='" + materialTemp.buy_order_serial_no + "' and isdeclare='是'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                materialTemp.declare_unit = querySdr[0].ToString().Trim();
                                materialTemp.declare_number = querySdr[1].ToString().Trim();
                                materialTemp.custom_request_number = querySdr[2].ToString().Trim();

                                if (materialTemp.declare_number != "")
                                {
                                    materialTemp.type = "I0002";
                                }
                                else if (materialTemp.custom_request_number != "")
                                {
                                    materialTemp.type = "I0001";
                                }
                                else
                                {
                                    materialTemp.type = "I0003";
                                }
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）->上面已经修改
                            init1.qty = materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = materialTemp.type;
                            init1.chk_code = "";
                            init1.entry_id = materialTemp.declare_number;
                            init1.gatejob_no = materialTemp.custom_request_number;
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //8 SMT/FRU 出库
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,mpn,stock_out_num,input_date from fru_smt_out_stock where input_date between '" + startTime + "' and '" + endTime + "' and isdeclare = '是'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[1].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))
                        {
                            currentDeclear = _71bomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show(" SMT/FRU 出库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）
                        MaterialCustomRelationTemp.num = querySdr[2].ToString();
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

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）->上面已经修改
                            init1.qty = "-" + materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = "E0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //9 SMT/BGA 不良品入库记录
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,mpn,in_number,input_date from smt_bga_ng_in_house_table where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[1].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))
                        {
                            currentDeclear = _71bomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("SMT/BGA 不良品入库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear +"-1";//因为报关原因，需要改成71料号（联想料号), 修改料号，以示区分
                        MaterialCustomRelationTemp.num = querySdr[2].ToString();
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

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                            init1.qty = materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = "I0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //10 BGA 入库信息 todo 牵扯到区内流程的材料，只有申请单号，没有报关单好，todo, 也有报关单号的
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,buy_order_serial_no,mpn,input_number,input_date from bga_in_stock where input_date between '" + startTime + "' and '" + endTime + "'  and isdeclare='是'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        MaterialCustomRelationTemp.buy_order_serial_no = querySdr[1].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[2].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))
                        {
                            currentDeclear = _71bomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("BGA 入库信息物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）done
                        MaterialCustomRelationTemp.num = querySdr[3].ToString();
                        MaterialCustomRelationTemp.date = querySdr[4].ToString();

                        MaterialCustomRelationList.Add(MaterialCustomRelationTemp);
                    }
                    querySdr.Close();

                    if (MaterialCustomRelationList.Count > 0)
                    {
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from stock_in_sheet where buy_order_serial_no ='" + materialTemp.buy_order_serial_no + "' and isdeclare='是'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                materialTemp.declare_unit = querySdr[0].ToString().Trim();
                                materialTemp.declare_number = querySdr[1].ToString().Trim();
                                materialTemp.custom_request_number = querySdr[2].ToString().Trim();

                                if (materialTemp.declare_number != "")
                                {
                                    materialTemp.type = "I0002";
                                }
                                else if (materialTemp.custom_request_number != "")
                                {
                                    materialTemp.type = "I0001";
                                }
                                else
                                {
                                    materialTemp.type = "I0003";
                                }
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                            init1.qty = materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = materialTemp.type;
                            init1.chk_code = "";
                            init1.entry_id = materialTemp.declare_number;
                            init1.gatejob_no = materialTemp.custom_request_number;
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }
                    //11 BGA出库信息
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,mpn,out_number,input_date from bga_out_stock where input_date between '" + startTime + "' and '" + endTime + "' and isdeclare = '是'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[1].ToString();
                        if (_71bomDic.ContainsKey(nowMatrialNo))
                        {
                            currentDeclear = _71bomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("BGA出库物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）done
                        MaterialCustomRelationTemp.num = querySdr[2].ToString();
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

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                            init1.qty = "-" + materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = "E0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //12. MB 入库信息 todo 牵扯到区内流程的材料，只有申请单号，没有报关单好，todo, 也有报关单号的
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select Id,buy_order_serial_no,mpn,input_number,input_date from mb_in_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        MaterialCustomRelationTemp.buy_order_serial_no = querySdr[1].ToString();

                        string currentDeclear = "";
                        string nowMatrialNo = querySdr[2].ToString();
                        if (materialbomDic.ContainsKey(nowMatrialNo))//主板只查询物料对照表
                        {
                            currentDeclear = materialbomDic[nowMatrialNo];
                        }
                        else
                        {
                            MessageBox.Show("MB 入库信息物料" + nowMatrialNo + "对应找不到71料号！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        MaterialCustomRelationTemp.mpn = currentDeclear;//因为报关原因，需要改成71料号（联想料号）done
                        MaterialCustomRelationTemp.num = querySdr[3].ToString();
                        MaterialCustomRelationTemp.date = querySdr[4].ToString();

                        MaterialCustomRelationList.Add(MaterialCustomRelationTemp);
                    }
                    querySdr.Close();

                    if (MaterialCustomRelationList.Count > 0)
                    {
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            cmd.CommandText = "select declare_unit,declare_number,custom_request_number from stock_in_sheet where buy_order_serial_no ='" + materialTemp.buy_order_serial_no + "' and isdeclare='是'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                materialTemp.declare_unit = querySdr[0].ToString().Trim();
                                materialTemp.declare_number = querySdr[1].ToString().Trim();
                                materialTemp.custom_request_number = querySdr[2].ToString().Trim();
                                if (materialTemp.declare_number != "")
                                {
                                    materialTemp.type = "I0002";
                                }
                                else if (materialTemp.custom_request_number != "")
                                {
                                    materialTemp.type = "I0001";
                                }
                                else
                                {
                                    materialTemp.type = "I0003";
                                }
                            }
                            querySdr.Close();
                        }

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//因为报关原因，需要改成71料号（联想料号）done
                            init1.qty = materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = materialTemp.type;
                            init1.chk_code = "";
                            init1.entry_id = materialTemp.declare_number;
                            init1.gatejob_no = materialTemp.custom_request_number;
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }

                    //13. MB出库信息
                    MaterialCustomRelationList.Clear();
                    cmd.CommandText = "select track_serial_no,custommaterialNo,input_date from mb_out_stock where input_date between '" + startTime + "' and '" + endTime + "' and isdeclare = '是'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        MaterialCustomRelation MaterialCustomRelationTemp = new MaterialCustomRelation();
                        MaterialCustomRelationTemp.id = querySdr[0].ToString();
                        MaterialCustomRelationTemp.mpn = querySdr[1].ToString();//正常料号
                        MaterialCustomRelationTemp.num = "1";
                        MaterialCustomRelationTemp.date = querySdr[2].ToString();

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

                        //信息完全,生成信息
                        foreach (MaterialCustomRelation materialTemp in MaterialCustomRelationList)
                        {
                            StoreTrans init1 = new StoreTrans();
                            init1.ems_no = ems_no;
                            init1.io_no = materialTemp.id;
                            init1.goods_nature = "I";
                            init1.io_date = Untils.getCustomDate(materialTemp.date);
                            init1.cop_g_no = materialTemp.mpn;//正常料号
                            init1.qty = "-" + materialTemp.num;
                            init1.unit = Untils.getCustomCode(materialTemp.declare_unit);
                            init1.type = "E0003";
                            init1.chk_code = "";
                            init1.entry_id = "";
                            init1.gatejob_no = "";
                            init1.whs_code = "";
                            init1.location_code = "";
                            init1.note = "";

                            storeTransList.Add(init1);
                        }
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                stockinout.seq_no = seq_no;
                stockinout.boxtype = boxtype;
                stockinout.flowstateg = flowstateg;
                stockinout.trade_code = trade_code;
                stockinout.ems_no = ems_no;
                stockinout.status = status;

                stockinout.storeTransList = storeTransList;
                if (storeTransList.Count > 0)//没有数据就不产生文件
                {
                    Untils.createStockInOutXML(stockinout, "D:\\WO_HCHX" + seq_no + ".xml");
                    MessageBox.Show( dt.ToString("yyyyMMdd") + "海关出入库信息产生成功！");
                }
                else
                {
                    MessageBox.Show( dt.ToString("yyyyMMdd") + "没有出入库信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (generateWorkOrderHead != null)
                {
                    generateWorkOrderHead.doGenerate();
                }
                if (generateWorkOrderBody != null)
                {
                    generateWorkOrderBody.addWorkOrderList(null, ref _71bomDic);
                    generateWorkOrderBody.doGenerate();
                }
            }
        }
    }
}
