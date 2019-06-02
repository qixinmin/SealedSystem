/*1 如果是维修，维修位置，数量，与料号，2  如果是bga则位置，数量为1， 与料号， 3 如果是mylar，则位置，数量，与料号*/
CREATE TABLE stationInfoRecord(
Id INT PRIMARY KEY IDENTITY, 
trackno NVARCHAR(128) NOT NULL, /*订单编号*/
station NVARCHAR(128) NOT NULL, /*站别*/
recordstatus NVARCHAR(128) NOT NULL,
recorddate NVARCHAR(128) NOT NULL,

repairplace1 NVARCHAR(128) ,
repairnum1 NVARCHAR(128) ,
repairMaterial1 NVARCHAR(128) ,

repairplace2 NVARCHAR(128) ,
repairnum2 NVARCHAR(128) ,
repairMaterial2 NVARCHAR(128) ,

repairplace3 NVARCHAR(128) ,
repairnum3 NVARCHAR(128) ,
repairMaterial3 NVARCHAR(128) ,

repairplace4 NVARCHAR(128) ,
repairnum4 NVARCHAR(128) ,
repairMaterial4 NVARCHAR(128) ,

repairplace5 NVARCHAR(128) ,
repairnum5 NVARCHAR(128) ,
repairMaterial5 NVARCHAR(128) ,
inputer NVARCHAR(128)
)

/*增加数据验证的东西，防止收货的时候输错*/
CREATE TABLE flexid_8s_mpn_table(
Id INT PRIMARY KEY IDENTITY, 
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
flexid NVARCHAR(128) NOT NULL,/*flexid*/
mpn NVARCHAR(128) NOT NULL,/*板子料号*/
_8sCode NVARCHAR(128), /*8s*/
)


/*不良品MB/SMT/BGA出库记录*/
/*把fru的材料也放進去了，但是默認現在不報關出去，因爲要交稅，在上報的時候過濾一下*/
CREATE TABLE mb_smt_bga_ng_out_house_table(
Id INT PRIMARY KEY IDENTITY, 
mpn NVARCHAR(128) NOT NULL,/*料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
declare_unit NVARCHAR(128), /*申报单位,需要转换*/
declare_number NVARCHAR(128), /*报关单号*/
custom_request_number NVARCHAR(128), /*申请单号*/
)

/*不良品SMT/BGA入库记录*/
CREATE TABLE smt_bga_ng_in_house_table(
Id INT PRIMARY KEY IDENTITY, 
mpn NVARCHAR(128) NOT NULL,/*料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
)

/* 只放入buffer MB不良品库*/
/*buffer 主板直接入不良品库， 为海关信息生成做准备*/
CREATE TABLE store_house_ng_buffer_mb(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位*/
mpn NVARCHAR(128), /*存储料号(MB的71料号,vendormaterialNo)*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
)

/*SMT/BGA不良品库房信息 把CID的MB不良品库也放入一起*/
/*把fru的材料也放進去了，但是默認現在不報關出去，因爲要交稅*/
CREATE TABLE store_house_ng(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位*/
mpn NVARCHAR(128), /*存储料号(如果是MB则对应客户料号 0开始的料号)*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
)

/*MB不良品出库表记录*/
/*CREATE TABLE fault_mb_out_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
)*/

/*MB不良品入库表记录 Buffer专用*/
CREATE TABLE fault_mb_enter_record_table_buffer(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
inputer NVARCHAR(128) NOT NULL, /*维修人*/
input_date date, /*输入日期*/
)

/*MB不良品入库表记录 CID专用*/
CREATE TABLE fault_mb_enter_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
)

/*存储文件数据库*/
CREATE TABLE TestCpu(
    id varchar(10),
    cpupn image,
    chkcpu image 
)

/*良品数量库房*/
CREATE TABLE repaired_left_house_table(
Id INT PRIMARY KEY IDENTITY,
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
leftNumber NVARCHAR(128), /*出库数量*/
)

/*良品出库上传海关信息*/
CREATE TABLE repaired_out_house_excel_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
declare_number NVARCHAR(128), /*报关单号*/
input_date date, /*输入日期*/
)

/*良品出库*/
CREATE TABLE repaired_out_house_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
out_number NVARCHAR(128), /*出库数量*/
input_date date, /*输入日期*/
)

/*良品入库*/
CREATE TABLE repaired_in_house_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
)

/*待维修库数量*/
CREATE TABLE wait_repair_left_house_table(
Id INT PRIMARY KEY IDENTITY, 
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
leftNumber NVARCHAR(128), /*出库数量*/
)
/*待维修出库*/
CREATE TABLE wait_repair_out_house_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
out_number NVARCHAR(128), /*出库数量*/
input_date date, /*输入日期*/
)

/*待维修入库*/
CREATE TABLE wait_repair_in_house_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
)

/*库房信息*/
CREATE TABLE store_house(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位,储位的名称开始字母可以区分可以存储的类型，fru/smt, bga, mb*/
mpn NVARCHAR(128), /*存储料号,购买的主板信息使用71料号*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
)

/*请求归还料表 库房*/
CREATE TABLE fru_smt_return_store_record(
Id INT PRIMARY KEY IDENTITY, 
material_mpn NVARCHAR(128), /*材料mpn*/
return_number NVARCHAR(128), /*请求数量*/
stock_place NVARCHAR(128),/*库位*/

requester NVARCHAR(128), /*请求人*/
request_date date, /*请求日期*/

processer NVARCHAR(128), /*处理人*/
processe_date date, /*处理日期*/
_status NVARCHAR(128), /*request/done 2中状态*/
fromId NVARCHAR(128),/*从request_fru_smt_to_store_table中过来的id，还需要此id来更新状态*/
)

/*需要创建一个表格，记录维修的每条使用记录 TODO...*/
CREATE TABLE fru_smt_used_record(
Id INT PRIMARY KEY IDENTITY, 
inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
track_serial_no NVARCHAR(128),/*跟踪条码*/

material_mpn NVARCHAR(128), /*材料mpn*/
thisNumber NVARCHAR(128), /*此次使用的数量*/
stock_place NVARCHAR(128),/*库位*/
)

CREATE TABLE request_fru_smt_to_store_table(
Id INT PRIMARY KEY IDENTITY, 
mb_brief NVARCHAR(1280), /*机型*/
not_good_place NVARCHAR(128), /*不良位置*/
material_mpn NVARCHAR(128), /*材料mpn*/
material_describe NVARCHAR(128), /*材料描述*/
number NVARCHAR(128), /*请求数量*/
realNumber NVARCHAR(128), /*获得的真正数量*/
requester NVARCHAR(128), /*请求人*/
_date date, /*请求日期*/
_status NVARCHAR(128), /*状态, request/close/part/wait,return*/
usedNumber NVARCHAR(128), /*使用的数量,是个累加数量*/
stock_place NVARCHAR(128),/*库位->剩余数量*/
processer NVARCHAR(128), /*处理人*/
processe_date date, /*处理日期*/
)

CREATE TABLE mb_out_stock(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
describe NVARCHAR(128) NOT NULL,/*描述*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serial_no NVARCHAR(128) NOT NULL,/*厂商序号*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号->输入*/
dpk_type NVARCHAR(128),/*DPK类型 物料对照表带出*/
dpkpn NVARCHAR(128),/*DPKPN 物料对照表带出*/
stock_place NVARCHAR(128),/*库位*/
isdeclare NVARCHAR(128), /*是否报关*/
note NVARCHAR(128),/*备注*/
taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

CREATE TABLE mb_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/

number  NVARCHAR(128) NOT NULL,/*订单数量*/
input_number  NVARCHAR(128),/*入库数量*/
stock_place NVARCHAR(128),/*库位*/

pricePer NVARCHAR(128) NOT NULL,/*单价*/
isdeclare NVARCHAR(128), /*是否报关*/

mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serial_no NVARCHAR(128) NOT NULL,/*厂商序号*/
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/

note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
)

CREATE TABLE bga_out_stock(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
bga_brief NVARCHAR(128) NOT NULL,/*BGA简称*/
bga_describe NVARCHAR(128) NOT NULL,/*BGA描述*/

stock_place NVARCHAR(128),/*库位*/
out_number  NVARCHAR(128),/*出库数量*/
isdeclare NVARCHAR(128), /*是否报关*/
note NVARCHAR(128),/*备注*/

taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

CREATE TABLE bga_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/
isdeclare NVARCHAR(128), /*是否报关*/

bga_describe NVARCHAR(128) NOT NULL,/*BGA简述*/
order_number NVARCHAR(128),/*订单数量*/
input_number  NVARCHAR(128),/*入库数量*/
bgasn NVARCHAR(128),/*BGASN*/
stock_place NVARCHAR(128),/*库位*/
note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

CREATE TABLE fru_smt_out_stock(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_brief NVARCHAR(1280) NOT NULL,/*MB简称*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
isdeclare NVARCHAR(128), /*是否报关*/
stock_out_num  NVARCHAR(128),/*出库数量*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/
stock_place NVARCHAR(128),/*库位*/
taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
use_describe NVARCHAR(128),/*用途*/
note NVARCHAR(128),/*备注*/
input_date date,/*日期*/
)

CREATE TABLE fru_smt_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
number  NVARCHAR(128) NOT NULL,/*订单数量*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/
isdeclare NVARCHAR(128), /*是否报关*/

mb_brief NVARCHAR(1280) NOT NULL,/*MB简称*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
stock_in_num  NVARCHAR(128),/*入库数量*/
totalMoney  NVARCHAR(128),/*金额合计*/
stock_place NVARCHAR(128),/*库位*/
note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

/*材料入库单*/
CREATE TABLE stock_in_sheet(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
number  NVARCHAR(128) NOT NULL,/*订单数量*/
pricePer  NVARCHAR(128) NOT NULL,/*单价*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
totalMoney  NVARCHAR(128),/*金额合计*/
stock_in_num  NVARCHAR(128),/*入库数量*/

_status NVARCHAR(128),/*状态 open/close*/

inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
isdeclare NVARCHAR(128), /*是否报关*/

declare_unit NVARCHAR(128), /*申报单位*/
declare_number NVARCHAR(128), /*报关单号*/
custom_request_number NVARCHAR(128), /*申请单号*/
)

/*bga维修序列记录*/
CREATE TABLE todo_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
bgatype NVARCHAR(128), /*bga类型*/
/*BGA不良(维修 初始状态） -> code 1

BGA待换（bga维修 初始状态）-> code 2
更换OK待测量 （bga维修转维修站)-> code 3
更换NG（bga维修转维修站）-> code 4

BGA更换OK待测（维修）-> code 5
BGA更换报废（维修，板子报废 需主管确认->录入不良品库）-> code 6*/

bga_status NVARCHAR(128) NOT NULL,
)

/*bga待维修记录*/
CREATE TABLE bga_wait_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
_status NVARCHAR(128) NOT NULL, /*BGA当前的状态*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
mbfa1 NVARCHAR(128) NOT NULL, /*FA分析*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
bgatype NVARCHAR(128), /*bga类型*/
BGAPN NVARCHAR(128), /*BGAPN*/
BGA_place NVARCHAR(128), /*BGA位置*/
bga_brief NVARCHAR(128) , /*BGA简述*/
repairer NVARCHAR(128) NOT NULL, /*bga维修人*/
repair_date date, /*bga维修日期*/
countNum NVARCHAR(128) NOT NULL/*总共录入了几次，从1开始，防止同一片板子在维修与BGA来回折腾*/
)

/*outlookcheck*/
CREATE TABLE outlookcheck(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*testall, TBG,DT, AIO*/
CREATE TABLE testalltable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*Package*/
CREATE TABLE Packagetable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)


/*Obe*/
CREATE TABLE Obetable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*TakePhototable*/
CREATE TABLE TakePhototable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*Running*/
CREATE TABLE Runningtable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*test2 LBG 使用*/
CREATE TABLE test2table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*test1 LBG*/
CREATE TABLE test1table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

CREATE TABLE bga_repair_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
fault_describe NVARCHAR(128) NOT NULL, /*故障原因*/
mbfa1 NVARCHAR(128) NOT NULL, /*mbfa1*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
bgatype NVARCHAR(128), /*bga类型*/
BGAPN NVARCHAR(128), /*BGAPN*/
BGA_place NVARCHAR(128), /*BGA位置*/
bga_brief NVARCHAR(128) , /*BGA简述*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/

bga_repairer NVARCHAR(128) NOT NULL, /*bga维修人*/
bga_repair_date date, /*bga修复日期*/
bga_repair_result NVARCHAR(128) NOT NULL,/*bga修复状态*/
countNum NVARCHAR(128) NOT NULL,/*总共录入了几次，从1开始，防止同一片板子在维修与BGA来回折腾*/
oldSn NVARCHAR(128), /*换下sn*/
newSn NVARCHAR(128), /*换上sn*/
)

/*DPK 导入表格*/
CREATE TABLE DPK_table(
Id INT PRIMARY KEY IDENTITY, 
dpk_order_no NVARCHAR(128) NOT NULL, /*联想DPK订单编号*/
dpk_type NVARCHAR(128) NOT NULL, /*DPK类别*/
KEYPN NVARCHAR(128) NOT NULL, /*KEYPN*/
KEYID  NVARCHAR(128) NOT NULL, /*KEYID*/
KEYSERIAL NVARCHAR(128) NOT NULL, /*KEYSERIAL*/
upload_date date, /*上传日期*/
_status NVARCHAR(128) NOT NULL, /*状态*/
burn_date date,/*烧录日期*/
custom_serial_no NVARCHAR(128) /*客户序号*/
)

/*材料名称*/
CREATE TABLE materialNameTable(
Id INT PRIMARY KEY IDENTITY, 
materialName NVARCHAR(128) NOT NULL
)

/*维修故障类别*/
CREATE TABLE repairFaultType(
Id INT PRIMARY KEY IDENTITY, 
_type NVARCHAR(128) NOT NULL
)

CREATE TABLE repair_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
fault_describe NVARCHAR(128) NOT NULL, /*故障原因*/
mbfa1 NVARCHAR(128) NOT NULL, /*FA分析*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
software_update NVARCHAR(128) NOT NULL, /*软体更新*/
not_good_place NVARCHAR(128), /*不良位置*/
material_mpn NVARCHAR(128), /*材料MPN*/
material_71pn NVARCHAR(128), /*材料71PN*/
material_type NVARCHAR(128), /*材料类别*/
fault_type NVARCHAR(128), /*故障类别*/
_action NVARCHAR(128) NOT NULL, /*动作*/
ECO NVARCHAR(128), /*ECO*/
repair_result NVARCHAR(128) NOT NULL, /*修复结果*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/
)

CREATE TABLE LCFC71BOM_table(
Id INT PRIMARY KEY IDENTITY, 
_date date, /*日期*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
material_vendor_pn NVARCHAR(128), /*材料厂商PN*/
material_mpn NVARCHAR(128), /*材料MPN*/
_description NVARCHAR(128), /*Description*/
price NVARCHAR(128)/*price*/
)

/*
CREATE TABLE COMPAL_MBBOM_table(
Id INT PRIMARY KEY IDENTITY, 
_date date, /*日期*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
MPN NVARCHAR(128), /*MPN*/
material_mpn NVARCHAR(128), /*材料MPN*/
material_box_place NVARCHAR(128), /*料盒位置*/
material_describe NVARCHAR(128), /*物料描述*/
material_num NVARCHAR(128), /*用料数量*/
L1 NVARCHAR(128) , /*L1*/
L2 NVARCHAR(128) , /*L2*/
L3 NVARCHAR(128) , /*L3*/
L4 NVARCHAR(128) , /*L4*/
L5 NVARCHAR(128) , /*L5*/
L6 NVARCHAR(128) , /*L6*/
L7 NVARCHAR(128) , /*L7*/
L8 NVARCHAR(128)  /*L8*/ 
)
*/

CREATE TABLE LCFC_MBBOM_table(
Id INT PRIMARY KEY IDENTITY, 
_date date, /*日期*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
MPN NVARCHAR(128), /*MPN*/
material_mpn NVARCHAR(128), /*材料MPN*/
material_box_place NVARCHAR(128), /*料盒位置*/
material_describe NVARCHAR(128), /*物料描述*/
material_num NVARCHAR(128), /*用料数量*/
L1 NVARCHAR(128) , /*L1*/
L2 NVARCHAR(128) , /*L2*/
L3 NVARCHAR(128) , /*L3*/
L4 NVARCHAR(128) , /*L4*/
L5 NVARCHAR(128) , /*L5*/
L6 NVARCHAR(128) , /*L6*/
L7 NVARCHAR(128) , /*L7*/
L8 NVARCHAR(128)  /*L8*/ 
)

/*客责类别*/
CREATE TABLE customResponsibilityType(
Id INT PRIMARY KEY IDENTITY, 
_type NVARCHAR(128) NOT NULL
)

/*还货状态*/
CREATE TABLE returnStoreStatus(
Id INT PRIMARY KEY IDENTITY, 
_status NVARCHAR(128) NOT NULL
)

/*还货表*/
CREATE TABLE returnStore(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*--厂商*/
product NVARCHAR(128) NOT NULL, /*--客户别*/
return_file_no NVARCHAR(128) NOT NULL,/*--还货文件编号*/
storehouse NVARCHAR(128) NOT NULL,/*--客户库别*/
return_date date,/*--还货时间*/
orderno NVARCHAR(128),/*--订单编号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*--客户料号*/
dpkpn NVARCHAR(128),/*--DPK状态*/
track_serial_no NVARCHAR(128),/*--跟踪条码*/
custom_serial_no NVARCHAR(128),/*--客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL,/*--厂商序号*/
vendormaterialNo NVARCHAR(128),/*--厂商料号*/
_status NVARCHAR(128),/*--状态*/
custom_res_type NVARCHAR(128),/*--客责类别*/
response_describe NVARCHAR(128),/*--客责描述*/
tat NVARCHAR(128),/*--TAT*/
inputuser NVARCHAR(128),/*还货人*/
lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
)

/*收货*/
CREATE TABLE DeliveredTable(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source_brief NVARCHAR(128) NOT NULL,/*来源*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
order_out_date date,/*客户出库日期*/
order_receive_date date,/*收货日期*/
custom_machine_type NVARCHAR(128),/*客户机型*/
mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
dpk_status NVARCHAR(128) NOT NULL,/*DPK状态*/
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL,/*厂商序号*/
uuid NVARCHAR(128) NOT NULL,/*UUID*/
mac NVARCHAR(128) NOT NULL,/*MAC*/
mpn NVARCHAR(128) NOT NULL,/*厂商料号*/
mb_describe NVARCHAR(128) NOT NULL,/*mb描述*/
mb_make_date date,/*MB生产日期*/
warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
guarantee NVARCHAR(128) NOT NULL,/*保内/保外*/
customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
lenovo_custom_service_no NVARCHAR(128),/*联想客服序号*/
lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
whole_machine_no NVARCHAR(128),/*整机序号*/
inputuser NVARCHAR(128)/*收货人*/
)


/*仓库别 来货*/
CREATE TABLE storehouse(
Id INT PRIMARY KEY IDENTITY, 
storehouse_describe NVARCHAR(128) NOT NULL
)

/*客责描述*/
CREATE TABLE customResponsibility(
Id INT PRIMARY KEY IDENTITY, 
responsibility_describe NVARCHAR(128) NOT NULL
)

/*保内/保外*/
CREATE TABLE guarantee(
Id INT PRIMARY KEY IDENTITY, 
guarantee_describe NVARCHAR(128) NOT NULL
)

/*客户故障*/
CREATE TABLE customFault(
Id INT PRIMARY KEY IDENTITY, 
fault_index NVARCHAR(128) NOT NULL,
fault_describe NVARCHAR(128) NOT NULL
)

/*来源	正常RMA\RMA_DOA\NB_DOA\RMA_IQC\成都DT\北京DT\惠阳DT*/
CREATE TABLE sourceTable(
Id INT PRIMARY KEY IDENTITY, 
source NVARCHAR(128) NOT NULL
)

/*users表*/
CREATE TABLE users(
Id INT PRIMARY KEY IDENTITY, 
username NVARCHAR(128) NOT NULL, 
workId NVARCHAR(128) NOT NULL, 
_password NVARCHAR(128) NOT NULL, 
super_manager NVARCHAR(128) NOT NULL,
bga  NVARCHAR(128) NOT NULL,
repair  NVARCHAR(128) NOT NULL,
test_all  NVARCHAR(128) NOT NULL,
test1  NVARCHAR(128) NOT NULL,
test2  NVARCHAR(128) NOT NULL,
receive_return  NVARCHAR(128) NOT NULL,
store  NVARCHAR(128) NOT NULL,
outlook  NVARCHAR(128) NOT NULL,
running  NVARCHAR(128) NOT NULL,
obe  NVARCHAR(128) NOT NULL,
)

/*客户别*/
CREATE TABLE vendorProduct(Id INT PRIMARY KEY IDENTITY, vendor NVARCHAR(128) NOT NULL, product NVARCHAR(128) NOT NULL)

/*
INSERT INTO users VALUES('Value1','Value1','Value1')

/*修改列名：*/
sp_rename 'DeliveredTable.vendormaterialNo',mpn,'column'

/*表中添加列*/
alter table MBMaterialCompare add inputuser NVARCHAR(128)

/*倒叙查询最新的数据：*/
select top 9 * from receiveOrder order by id desc*/

/*MB物料对照表*/
CREATE TABLE MBMaterialCompare(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
mpn NVARCHAR(128) NOT NULL,/*MPN*/
replace_mpn NVARCHAR(128),/*可替换MPN*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
replace_custom_materialNo NVARCHAR(128),/*可替换客户料号*/
fruNo NVARCHAR(128),/*FRU料号*/
replace_fruNo NVARCHAR(128),/*可替换FRU料号*/
mb_descripe NVARCHAR(128) NOT NULL,/*MB描述*/
vendor_pch_mpn NVARCHAR(128),/*厂商PCH_MPN*/
pcb_brief_describe NVARCHAR(128),/*PCH简述*/
pcb_describe NVARCHAR(128),/*PCH描述*/
vendor_vga_mpn NVARCHAR(128),/*厂商VGA_MPN*/
vga_brief_describe NVARCHAR(128),/*VGA简述*/
vga_describe NVARCHAR(128),/*VGA描述*/
vendor_cpu_mpn NVARCHAR(128),/*厂商CPU_MPN*/
cpu_brief NVARCHAR(128),/*CPU简述*/
cpu_describe NVARCHAR(128),/*CPU描述*/
dpk_type NVARCHAR(128),/*DPK类型*/
dpkpn NVARCHAR(128),/*DPKPN*/
warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
custom_machine_type NVARCHAR(128),/*客户机型*/
whole_machine_num NVARCHAR(128) NOT NULL,/*整机出货量*/
area NVARCHAR(128) NOT NULL,/*地区*/
_status NVARCHAR(128) NOT NULL,/*状态*/
cpu_type NVARCHAR(128),/*CPU型号*/
cpu_freq NVARCHAR(128),/*CPU频率*/
eco NVARCHAR(128),/*ECO*/
eol NVARCHAR(128),/*EOL*/
adddate date,/*添加日期*/
inputuser NVARCHAR(128),/*添加人*/
)

/*Flexid对应表*/
CREATE TABLE flexidRecord(
Id INT PRIMARY KEY IDENTITY,
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
flex_id NVARCHAR(128) NOT NULL,/*Flexid*/
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
_status NVARCHAR(128) NOT NULL,/*状态,默认为空，cid / return */
)


/*cid描述*/
CREATE TABLE cidRecord(
Id INT PRIMARY KEY IDENTITY,
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
mpn NVARCHAR(128) NOT NULL,/*厂商料号*/
order_receive_date date,/*收货日期*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
custom_res_type NVARCHAR(128),/*--客责类别*/
customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
inputuser NVARCHAR(128),/*录入人*/
inputdate date/*录入日期*/
)

/*收货单*/
CREATE TABLE receiveOrder(
Id INT PRIMARY KEY IDENTITY,
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
orderno NVARCHAR(128) NOT NULL,/*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
custom_material_describe NVARCHAR(128) NOT NULL,/*客户物料描述*/
ordernum NVARCHAR(128) NOT NULL,/*订单数量*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
vendor_materialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
username NVARCHAR(128) NOT NULL,/*制单人*/
ordertime date,/*制单时间*/
receivedNum NVARCHAR(128),/*收货数量*/
receivedate date,/*收货日期*/
_status NVARCHAR(128) NOT NULL,/*订单状态*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
returnNum NVARCHAR(128), /*还货数量*/

declare_unit NVARCHAR(128), /*申报单位*/
declare_number NVARCHAR(128), /*报关单号*/
custom_request_number NVARCHAR(128), /*申请单号*/

cid_number NVARCHAR(128), /*废弃数量*/
)


/*站别记录信息*/
CREATE TABLE stationInformation(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
station NVARCHAR(128), /*站别信息：收货，维修，(BGA), 测试1， 测试2(或测试1&2）， 外观等，[合肥有Test2->Running->外观->OBE]*/
updateDate date /*更新时间*/
)

/*合肥海关信息*/
CREATE TABLE company_fixed_table(
Id INT PRIMARY KEY IDENTITY, 
indentifier NVARCHAR(128) NOT NULL, /*企业编号*/
book_number NVARCHAR(128), /*账册编号*/
electronic_number NVARCHAR(128), /*电子账册号*/
)

/*SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'SELECT * FROM INFORMATION_SCHEMA.TABLES  列出所有表格*/