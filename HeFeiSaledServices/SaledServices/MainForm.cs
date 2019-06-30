using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.CustomsExport;
using SaledServices.additionForm;
using System.Diagnostics;
using System.IO;
using SaledServices.Test_Outlook;
using SaledServices.Export;
using SaledServices.Repair;

namespace SaledServices
{
    public partial class MainForm : Form
    {
        private LoginForm mLoginForm = null;
        private VendorForm mVendorForm = null;
        private ExcelImportForm mExcelForm = null;

        private MBMaterialCompareForm mbFrom = null;

        private ReceiveOrderForm roForm = null;

        private List<Form> allForm = new List<Form>();
        
        public MainForm()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒  
            timer.Start();
            //timer.Enabled判断timer是否在运行
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);

            Version ApplicationVersion = new Version(Application.ProductVersion);
            this.Text += ApplicationVersion.ToString();           
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数  
            int iHour = 23;
            int iMinute = 30;
            int iSecond = 00;
            //// 设置　 每分钟的开始执行一次  
            //if (intSecond == iSecond)
            //{
            //    Console.WriteLine("每分钟的开始执行一次！");
            //}
            //// 设置　每个小时的３０分钟开始执行  
            //if (intMinute == iMinute && intSecond == iSecond)
            //{
            //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
            //}

            // 设置　每天的１０：３０：００开始执行程序  
            if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            {
               // Console.WriteLine("在每天１０点３０分开始执行！");
                new StockInOutForm().exportXMLInfo(DateTime.Now, DateTime.Now, true);

                //第二个任务，数据库备份
                new DatabaseForm().button1_Click(null, null);
            }
            int iHour2 = 11;
            int iMinute2 = 40;
            int iSecond2 = 00;
            if (intHour == iHour2 && intMinute == iMinute2 && intSecond == iSecond2)
            {
                //第二次数据库备份
                new DatabaseForm().button1_Click(null, null);
            }

            if ((e.SignalTime.DayOfWeek == DayOfWeek.Sunday) && (intHour == 23 && intMinute == 10 && intSecond == 10))
            {
                //每个周末备份一下库存
                OpeningStockForm export = new OpeningStockForm();
                export.exportxmlbutton_Click(null, null);
                export.Close();
            }
            

        }

        public void clearAllMenu()
        {
            this.AllMenuStrip.Items.Clear();
        }

        public void appendMenu(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Bga_Repair:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.bGA维修ToolStripMenuItem,
                    this.报表ToolStripMenuItem,
                    });
                    break;
                case MenuType.Repair:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.维修ToolStripMenuItem,
                    this.eCOToolStripMenuItem,
                    this.报表ToolStripMenuItem,
                    });
                    break;
                case MenuType.Recieve_Return:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.receiveReturnStoreMenuItem,
                    this.eCOToolStripMenuItem,
                   });
                    break;
                case MenuType.TestALL:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试ToolStripMenuItem,
                    this.报表ToolStripMenuItem,
                   });
                    break;
                case MenuType.Test1:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试1ToolStripMenuItem1,
                    this.报表ToolStripMenuItem,
                   });
                    break;
                case MenuType.Test2:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试2ToolStripMenuItem1,
                    this.报表ToolStripMenuItem,
                   });
                    break;
                case MenuType.Running:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.runningToolStripMenuItem,
                   });
                    break;
                case MenuType.Outlook:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.外观ToolStripMenuItem,
                    this.报表ToolStripMenuItem,
                   });
                    break;
                case MenuType.Obe:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.oBEToolStripMenuItem,
                   });
                    break;
                case MenuType.Store:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.库存管理ToolStripMenuItem,
                   });
                    break;
                case MenuType.Self:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.UserManageMenuItem,
                   });
                    break;
                case MenuType.Other:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.FunctionMenuItem,           
                    this.additionMenuItem,
                    this.报表ToolStripMenuItem,
                    this.海关ToolStripMenuItem,
					 this.eCOToolStripMenuItem,
                    this.拍照ToolStripMenuItem,
                    });
                    break;
                case MenuType.Package:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.包装ToolStripMenuItem,
                    this.报表ToolStripMenuItem,
                   });
                    break;
            }

            this.LogoutMenuItem.Enabled = true;
        }
       
        private void LoginMenuItem_Click(object sender, EventArgs e)
        {           
        }

        private void VendorChangeMenuItem_Click(object sender, EventArgs e)
        {            
        }

        private void ExcelImportMenuItem_Click(object sender, EventArgs e)
        {
            if (mExcelForm == null || mExcelForm.IsDisposed)
            {
                mExcelForm = new ExcelImportForm();
                mExcelForm.MdiParent = this;
            }

            mExcelForm.BringToFront();
            mExcelForm.Show();

            allForm.Add(mExcelForm);
        }

        private void mBMaterialCompareMenuItem_Click(object sender, EventArgs e)
        {
            if (mbFrom == null || mbFrom.IsDisposed)
            {
                mbFrom = new MBMaterialCompareForm();
                mbFrom.MdiParent = this;
            }

            mbFrom.WindowState = FormWindowState.Maximized;
            mbFrom.BringToFront();
            mbFrom.Show();

            allForm.Add(mbFrom);
        }

        private void receiveOrderMenuItem_Click(object sender, EventArgs e)
        {            
        }

        private SourceForm sourceForm;
        private void sourceMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceForm == null || sourceForm.IsDisposed)
            {
                sourceForm = new SourceForm();
                sourceForm.MdiParent = this;
            }

            sourceForm.BringToFront();
            sourceForm.Show();

            allForm.Add(sourceForm);
        }

        private CustomFaultForm customFaultForm;
        private void customFaultMenuItem_Click(object sender, EventArgs e)
        {
            if (customFaultForm == null || customFaultForm.IsDisposed)
            {
                customFaultForm = new CustomFaultForm();
                customFaultForm.MdiParent = this;
            }

            customFaultForm.BringToFront();
            customFaultForm.Show();

            allForm.Add(customFaultForm);
        }

        private GuaranteeForm guaranteeForm;
        private void guaranteeMenuItem_Click(object sender, EventArgs e)
        {
            if (guaranteeForm == null || guaranteeForm.IsDisposed)
            {
                guaranteeForm = new GuaranteeForm();
                guaranteeForm.MdiParent = this;
            }

            guaranteeForm.BringToFront();
            guaranteeForm.Show();

            allForm.Add(guaranteeForm);
        }

        private CustomResponsibilityForm customResponsibilityForm;
        private void customResponsibilityMenuItem_Click(object sender, EventArgs e)
        {
            if (customResponsibilityForm == null || customResponsibilityForm.IsDisposed)
            {
                customResponsibilityForm = new CustomResponsibilityForm();
                customResponsibilityForm.MdiParent = this;
            }

            customResponsibilityForm.BringToFront();
            customResponsibilityForm.Show();

            allForm.Add(customResponsibilityForm);
        }

        private StoreHouseForm storeHouseForm;
        private void storeHouseMenuItem_Click(object sender, EventArgs e)
        {
            if (storeHouseForm == null || storeHouseForm.IsDisposed)
            {
                storeHouseForm = new StoreHouseForm();
                storeHouseForm.MdiParent = this;
            }

            storeHouseForm.BringToFront();
            storeHouseForm.Show();

            allForm.Add(storeHouseForm);
        }

        private void 收货单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private DeliveredTableForm dtform;
        private void 收货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtform == null || dtform.IsDisposed)
            {
                dtform = new DeliveredTableForm();
                dtform.MdiParent = this;
            }
            dtform.WindowState = FormWindowState.Maximized;
            dtform.BringToFront();
            dtform.Show();

            allForm.Add(dtform);
        }

        private ReturnStoreForm rsForm;
        private void returnStoreMenuItem_Click(object sender, EventArgs e)
        {
            if (rsForm == null || rsForm.IsDisposed)
            {
                rsForm = new ReturnStoreForm();
                rsForm.MdiParent = this;
            }

            rsForm.WindowState = FormWindowState.Maximized;
            rsForm.BringToFront();
            rsForm.Show();

            allForm.Add(rsForm);
        }
        private ReturnStoreStatusForm rssf;
        private void 还货状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rssf == null || rssf.IsDisposed)
            {
                rssf = new ReturnStoreStatusForm();
                rssf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            rssf.BringToFront();
            rssf.Show();

            allForm.Add(rssf);
        }

        private ReturnStoreCustomRespForm rscrf;
        private void 还货客责类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rscrf == null || rscrf.IsDisposed)
            {
                rscrf = new ReturnStoreCustomRespForm();
                rscrf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            rscrf.BringToFront();
            rscrf.Show();

            allForm.Add(rscrf);
        }
        private User.UserSelfForm usf;
        private void 个人信息查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usf == null || usf.IsDisposed)
            {
                usf = new User.UserSelfForm();
                usf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            usf.BringToFront();
            usf.Show();

            allForm.Add(usf);
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in allForm)
            {
                if (form != null && form.IsDisposed == false)
                {
                    form.Close();
                }
            }
            LoginForm.currentUser = "";

            this.LogoutMenuItem.Enabled = false;
            MainForm_Load(null, null);
        }

       
        private void 报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private RepairOperationForm rof;
        private void 维修界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rof == null || rof.IsDisposed)
            {
                rof = new RepairOperationForm();
                rof.MdiParent = this;
            }

            rof.WindowState = FormWindowState.Maximized;
            rof.BringToFront();
            rof.Show();

            allForm.Add(rof);
        }

        private additionForm.RepairFaultTypeForm rftf;
        private void 维修故障类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rftf == null || rftf.IsDisposed)
            {
                rftf = new additionForm.RepairFaultTypeForm();
                rftf.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            rftf.BringToFront();
            rftf.Show();

            allForm.Add(rftf);
        }

        private Test_Outlook.TestAllForm testAllform;
        private void 测试12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (testAllform == null || testAllform.IsDisposed)
            {
                testAllform = new Test_Outlook.TestAllForm();
                testAllform.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            testAllform.BringToFront();
            testAllform.Show();

            allForm.Add(testAllform);
        }

        private Test_Outlook.OutLookForm outlookform;
        private void 外观检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outlookform == null || outlookform.IsDisposed)
            {
                outlookform = new Test_Outlook.OutLookForm();
                outlookform.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            outlookform.BringToFront();
            outlookform.Show();

            allForm.Add(outlookform);
        }

        private void 厂商信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mVendorForm == null || mVendorForm.IsDisposed)
            {
                mVendorForm = new VendorForm();
                mVendorForm.MdiParent = this;
            }

            mVendorForm.BringToFront();
            mVendorForm.Show();

            allForm.Add(mVendorForm);
        }

        private void 收货单ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (roForm == null || roForm.IsDisposed)
            {
                roForm = new ReceiveOrderForm();
                roForm.MdiParent = this;
            }

            roForm.WindowState = FormWindowState.Maximized;
            roForm.BringToFront();
            roForm.Show();

            allForm.Add(roForm);
        }

        private ExportExcelForm eef;
        private void 报表1ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            if (eef == null || eef.IsDisposed)
            {
                eef = new ExportExcelForm();
                eef.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            eef.BringToFront();
            eef.Show();

            allForm.Add(eef);
        }

        private BGAInfoInputForm bgaIf;
        private void bGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaIf == null || bgaIf.IsDisposed)
            {
                bgaIf = new BGAInfoInputForm();
                bgaIf.MdiParent = this;
            }

            bgaIf.WindowState = FormWindowState.Maximized;
            bgaIf.BringToFront();
            bgaIf.Show();

            allForm.Add(bgaIf);
        }

        private LCFC_MBBOMForm lcfcf;
        private void lCFCMBBOM查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lcfcf == null || lcfcf.IsDisposed)
            {
                lcfcf = new LCFC_MBBOMForm();
                lcfcf.MdiParent = this;
            }

            lcfcf.WindowState = FormWindowState.Maximized;
            lcfcf.BringToFront();
            lcfcf.Show();

            allForm.Add(lcfcf);
        }
        private COMPAL_MBBOMForm compalf;
        private void cOMPALMBBOM查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (compalf == null || compalf.IsDisposed)
            {
                compalf = new COMPAL_MBBOMForm();
                compalf.MdiParent = this;
            }

            compalf.WindowState = FormWindowState.Maximized;
            compalf.BringToFront();
            compalf.Show();

            allForm.Add(compalf);
        }

        private LCFC71BOMForm lcfc71bomf;
        private void lCFC71BOM表查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lcfc71bomf == null || lcfc71bomf.IsDisposed)
            {
                lcfc71bomf = new LCFC71BOMForm();
                lcfc71bomf.MdiParent = this;
            }

            lcfc71bomf.WindowState = FormWindowState.Maximized;
            lcfc71bomf.BringToFront();
            lcfc71bomf.Show();

            allForm.Add(lcfc71bomf);
        }

        private DPKForm dpkf;
        private void dPKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dpkf == null || dpkf.IsDisposed)
            {
                dpkf = new DPKForm();
                dpkf.MdiParent = this;
            }

            dpkf.WindowState = FormWindowState.Maximized;
            dpkf.BringToFront();
            dpkf.Show();

            allForm.Add(dpkf);
        }

        private RepairFaultTypeForm repairFaultTypef;
        private void 故障代码表查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repairFaultTypef == null || repairFaultTypef.IsDisposed)
            {
                repairFaultTypef = new RepairFaultTypeForm();
                repairFaultTypef.MdiParent = this;
            }

            repairFaultTypef.WindowState = FormWindowState.Maximized;
            repairFaultTypef.BringToFront();
            repairFaultTypef.Show();

            allForm.Add(repairFaultTypef);
        }

        
        private void bGA维修ToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            
        }

        private StockInSheetForm sisf;
        private void 材料入库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sisf == null || sisf.IsDisposed)
            {
                sisf = new StockInSheetForm();
                sisf.MdiParent = this;
            }

            sisf.WindowState = FormWindowState.Maximized;
            sisf.BringToFront();
            sisf.Show();

            allForm.Add(sisf);  
        }


        private FRU_SMT_InSheetForm frusmtinform;
        private void fRUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frusmtinform == null || frusmtinform.IsDisposed)
            {
                frusmtinform = new FRU_SMT_InSheetForm();
                frusmtinform.MdiParent = this;
            }

            frusmtinform.WindowState = FormWindowState.Maximized;
            frusmtinform.BringToFront();
            frusmtinform.Show();

            allForm.Add(frusmtinform);  
        }

        private FRU_OutSheetForm fruoutform;
        private void fRUSMT入库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fruoutform == null || fruoutform.IsDisposed)
            {
                fruoutform = new FRU_OutSheetForm();
                fruoutform.MdiParent = this;
            }

            fruoutform.WindowState = FormWindowState.Maximized;
            fruoutform.BringToFront();
            fruoutform.Show();

            allForm.Add(fruoutform);  
        }

        private Store.CheckRequestForm checkrequestform;
        private void 出库请求查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkrequestform == null || checkrequestform.IsDisposed)
            {
                checkrequestform = new Store.CheckRequestForm();
                checkrequestform.MdiParent = this;
            }

            checkrequestform.WindowState = FormWindowState.Maximized;
            checkrequestform.BringToFront();
            checkrequestform.Show();

            allForm.Add(checkrequestform);  
        }


        private OpeningStockForm openingstockform;
        private void 期初库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openingstockform == null || openingstockform.IsDisposed)
            {
                openingstockform = new OpeningStockForm();
                openingstockform.MdiParent = this;
            }

            // openingstockform.WindowState = FormWindowState.Maximized;
            openingstockform.BringToFront();
            openingstockform.Show();

            allForm.Add(openingstockform);  
        }

        private RealStockForm realstockform;
        private void 实盘库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (realstockform == null || realstockform.IsDisposed)
            {
                realstockform = new RealStockForm();
                realstockform.MdiParent = this;
            }

            // realstockform.WindowState = FormWindowState.Maximized;
            realstockform.BringToFront();
            realstockform.Show();

            allForm.Add(realstockform);  
        }

        private StockInOutForm stockInOutform;
        private void 出库入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stockInOutform == null || stockInOutform.IsDisposed)
            {
                stockInOutform = new StockInOutForm();
                stockInOutform.MdiParent = this;
            }

            // stockInOutform.WindowState = FormWindowState.Maximized;
            stockInOutform.BringToFront();
            stockInOutform.Show();

            allForm.Add(stockInOutform);
        }

        private WorkListHeadForm worklistheadform;
        private void 工单表头ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worklistheadform == null || worklistheadform.IsDisposed)
            {
                worklistheadform = new WorkListHeadForm();
                worklistheadform.MdiParent = this;
            }

            // worklistheadform.WindowState = FormWindowState.Maximized;
            worklistheadform.BringToFront();
            worklistheadform.Show();

            allForm.Add(worklistheadform);
        }
        private WorkListBodyForm worklistbodyform;
        private void 工单表体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (worklistbodyform == null || worklistbodyform.IsDisposed)
            {
                worklistbodyform = new WorkListBodyForm();
                worklistbodyform.MdiParent = this;
            }

            // worklistbodyform.WindowState = FormWindowState.Maximized;
            worklistbodyform.BringToFront();
            worklistbodyform.Show();

            allForm.Add(worklistbodyform);
        }

        private CompanyFixedForm cfform;
        private void 企业固定信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cfform == null || cfform.IsDisposed)
            {
                cfform = new CompanyFixedForm();
                cfform.MdiParent = this;
            }

           // cfform.WindowState = FormWindowState.Maximized;
            cfform.BringToFront();
            cfform.Show();

            allForm.Add(cfform);  
        }

        private void 库房领料申请ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Store.RequestFRUSMTStoreForm rtsf = new Store.RequestFRUSMTStoreForm();
           // rtsf.setParameters(this.track_serial_noTextBox.Text, this.material_mpntextBox.Text, this.material_71pntextBox.Text);
            rtsf.MdiParent = this;
            rtsf.Show();
        }

        private void 还货请求查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Store.ProcessReturnStoreForm prsf = new Store.ProcessReturnStoreForm();
            prsf.MdiParent = this;
            prsf.Show();
        }

        private void bGA领料申请ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private BGARepairOperationForm vgaRof;
        private void bGA维修界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vgaRof == null || vgaRof.IsDisposed)
            {
                vgaRof = new BGARepairOperationForm();
                vgaRof.MdiParent = this;
            }

            vgaRof.WindowState = FormWindowState.Maximized;
            vgaRof.BringToFront();
            vgaRof.Show();

            allForm.Add(vgaRof);        
        }

        private StoreHouseInnerForm shif;
        private void 库房储位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shif == null || shif.IsDisposed)
            {
                shif = new StoreHouseInnerForm();
                shif.MdiParent = this;
            }

            //shif.WindowState = FormWindowState.Maximized;
            shif.BringToFront();
            shif.Show();

            allForm.Add(shif);     
        }

        private BGA_InSheetForm bgainform;
        private void bGA入库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgainform == null || bgainform.IsDisposed)
            {
                bgainform = new BGA_InSheetForm();
                bgainform.MdiParent = this;
            }

            bgainform.WindowState = FormWindowState.Maximized;
            bgainform.BringToFront();
            bgainform.Show();

            allForm.Add(bgainform);    
        }

        private BGA_OutSheetForm bgaoutform;
        private void bGA出库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaoutform == null || bgaoutform.IsDisposed)
            {
                bgaoutform = new BGA_OutSheetForm();
                bgaoutform.MdiParent = this;
            }

            bgaoutform.WindowState = FormWindowState.Maximized;
            bgaoutform.BringToFront();
            bgaoutform.Show();

            allForm.Add(bgaoutform);    
        }
        private MB_InSheetForm mbinform;
        private void mBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbinform == null || mbinform.IsDisposed)
            {
                mbinform = new MB_InSheetForm();
                mbinform.MdiParent = this;
            }

            mbinform.WindowState = FormWindowState.Maximized;
            mbinform.BringToFront();
            mbinform.Show();

            allForm.Add(mbinform);  
        }

        private MB_OutSheetForm mboutform;
        private void mBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mboutform == null || mboutform.IsDisposed)
            {
                mboutform = new MB_OutSheetForm();
                mboutform.MdiParent = this;
            }

            mboutform.WindowState = FormWindowState.Maximized;
            mboutform.BringToFront();
            mboutform.Show();

            allForm.Add(mboutform);   
        }

        private UserDetailForm mUserDetailForm;
        private void 员工管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mUserDetailForm == null || mUserDetailForm.IsDisposed)
            {
                mUserDetailForm = new UserDetailForm();
                mUserDetailForm.MdiParent = this;
            }

            mUserDetailForm.BringToFront();
            mUserDetailForm.Show();

            allForm.Add(mUserDetailForm);   
        }

        private Test_Outlook.Test1Form test1form;
        private void 测试1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (test1form == null || test1form.IsDisposed)
            {
                test1form = new Test_Outlook.Test1Form();
                test1form.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            test1form.BringToFront();
            test1form.Show();

            allForm.Add(test1form);
        }

        private Test_Outlook.Test2Form test2form;
        private void 测试2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (test2form == null || test2form.IsDisposed)
            {
                test2form = new Test_Outlook.Test2Form();
                test2form.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            test2form.BringToFront();
            test2form.Show();

            allForm.Add(test2form);
        }
        
        private Test_Outlook.RunningForm runningform;
        private void runningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (runningform == null || runningform.IsDisposed)
            {
                runningform = new Test_Outlook.RunningForm();
                runningform.MdiParent = this;
            }

            //runningform.WindowState = FormWindowState.Maximized;
            runningform.BringToFront();
            runningform.Show();

            allForm.Add(runningform);
        }

        private Test_Outlook.ObeForm obeform;
        private void oBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obeform == null || obeform.IsDisposed)
            {
                obeform = new Test_Outlook.ObeForm();
                obeform.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            obeform.BringToFront();
            obeform.Show();

            allForm.Add(obeform);
        }

        private FaultMBStoreForm faultMbStoreForm;
        private void 不良品出入库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultMbStoreForm == null || faultMbStoreForm.IsDisposed)
            {
                faultMbStoreForm = new FaultMBStoreForm();
                faultMbStoreForm.MdiParent = this;
            }

            faultMbStoreForm.WindowState = FormWindowState.Maximized;
            faultMbStoreForm.BringToFront();
            faultMbStoreForm.Show();

            allForm.Add(faultMbStoreForm);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clearAllMenu();

            if (mLoginForm == null || mLoginForm.IsDisposed)
            {
                mLoginForm = new LoginForm(this);
                mLoginForm.MdiParent = this;
            }

            mLoginForm.BringToFront();
            mLoginForm.Show();

            allForm.Add(mLoginForm);
        }

        private MaterialNameForm materialNameForm;
        private void 材料名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (materialNameForm == null || materialNameForm.IsDisposed)
            {
                materialNameForm = new MaterialNameForm();
                materialNameForm.MdiParent = this;
            }

            materialNameForm.WindowState = FormWindowState.Maximized;
            materialNameForm.BringToFront();
            materialNameForm.Show();

            allForm.Add(materialNameForm);
        }
        
        private TimerCheckForm timerCheckForm;
        private void 定时任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerCheckForm == null || timerCheckForm.IsDisposed)
            {
                timerCheckForm = new TimerCheckForm();
                timerCheckForm.MdiParent = this;
            }

           // timerCheckForm.WindowState = FormWindowState.Maximized;
            timerCheckForm.BringToFront();
            timerCheckForm.Show();

            allForm.Add(timerCheckForm);
        }

        private FilesUpdateForm filesUpdateForm;
        private void 文件数据库操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesUpdateForm == null || filesUpdateForm.IsDisposed)
            {
                filesUpdateForm = new FilesUpdateForm();
                filesUpdateForm.MdiParent = this;
            }

            // filesUpdateForm.WindowState = FormWindowState.Maximized;
            filesUpdateForm.BringToFront();
            filesUpdateForm.Show();

            allForm.Add(filesUpdateForm);
        }

        private InputCIDShanghaiForm cidInputshanghaiForm;
        private void cID操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cidInputshanghaiForm == null || cidInputshanghaiForm.IsDisposed)
            {
                cidInputshanghaiForm = new InputCIDShanghaiForm();
                cidInputshanghaiForm.MdiParent = this;
            }

            cidInputshanghaiForm.WindowState = FormWindowState.Maximized;
            cidInputshanghaiForm.BringToFront();
            cidInputshanghaiForm.Show();

            allForm.Add(cidInputshanghaiForm);
        }
        
        //private CIDInputForm cidInputForm;
        private void cID操作合肥ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cidInputForm == null || cidInputForm.IsDisposed)
            //{
            //    cidInputForm = new CIDInputForm();
            //    cidInputForm.MdiParent = this;
            //}

            //cidInputForm.WindowState = FormWindowState.Maximized;
            //cidInputForm.BringToFront();
            //cidInputForm.Show();

            //allForm.Add(cidInputForm);
            cID操作ToolStripMenuItem_Click(null, null);
        }

        private void 收货合肥ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 还货合肥ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cID操作ToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cID操作ToolStripMenuItem_Click(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PrintUtils.disposePrinter();
        }

        private void 预领料申请ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            库房领料申请ToolStripMenuItem_Click(null, null);
        }

       // private PackageForm packageform;       
        private void 包装ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (packageform == null || packageform.IsDisposed)
            //{
            //    packageform = new PackageForm();
            //    packageform.MdiParent = this;
            //}

            ////packageform.WindowState = FormWindowState.Maximized;
            //packageform.BringToFront();
            //packageform.Show();

            //allForm.Add(packageform);
        }

        private StoreHouseInnerNGForm shifng;
        private void 不良品库房储位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shifng == null || shifng.IsDisposed)
            {
                shifng = new StoreHouseInnerNGForm();
                shifng.MdiParent = this;
            }

            //shifng.WindowState = FormWindowState.Maximized;
            shifng.BringToFront();
            shifng.Show();

            allForm.Add(shifng);     
        }

        private FaultSMTStoreForm faultSmtStoreForm;
        private void fRUSMT不良品出入库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultSmtStoreForm == null || faultSmtStoreForm.IsDisposed)
            {
                faultSmtStoreForm = new FaultSMTStoreForm();
                faultSmtStoreForm.MdiParent = this;
            }

            //faultSmtStoreForm.WindowState = FormWindowState.Maximized;
            faultSmtStoreForm.BringToFront();
            faultSmtStoreForm.Show();

            allForm.Add(faultSmtStoreForm);
        }

        private FaultMaterialOutForm faultOutForm;
        private void 不良品出庫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultOutForm == null || faultOutForm.IsDisposed)
            {
                faultOutForm = new FaultMaterialOutForm();
                faultOutForm.MdiParent = this;
            }

            //faultOutForm.WindowState = FormWindowState.Maximized;
            faultOutForm.BringToFront();
            faultOutForm.Show();

            allForm.Add(faultOutForm);
        }

        private ReturnCustomInfoImportForm rsimf;
        private void 上传出货海关信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rsimf == null || rsimf.IsDisposed)
            {
                rsimf = new ReturnCustomInfoImportForm();
                rsimf.MdiParent = this;
            }

            //rsimf.WindowState = FormWindowState.Maximized;
            rsimf.BringToFront();
            rsimf.Show();

            allForm.Add(rsimf);
        }

        private ExportDatabaseToExcel edte;
        private void 数据库导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edte == null || edte.IsDisposed)
            {
                edte = new ExportDatabaseToExcel();
                edte.MdiParent = this;
            }

            //edte.WindowState = FormWindowState.Maximized;
            edte.BringToFront();
            edte.Show();

            allForm.Add(edte);
        }

        private BufferFaultMBStoreForm bfmbsf;
        private void bufferMB转不良品库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bfmbsf == null || bfmbsf.IsDisposed)
            {
                bfmbsf = new BufferFaultMBStoreForm();
                bfmbsf.MdiParent = this;
            }

            bfmbsf.WindowState = FormWindowState.Maximized;
            bfmbsf.BringToFront();
            bfmbsf.Show();

            allForm.Add(bfmbsf);
        }

        private ReceiveOrderExport roexport;
        private void 收货信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (roexport == null || roexport.IsDisposed)
            {
                roexport = new ReceiveOrderExport();
                roexport.MdiParent = this;
            }

           // roexport.WindowState = FormWindowState.Maximized;
            roexport.BringToFront();
            roexport.Show();

            allForm.Add(roexport);
        }

        private DatabaseForm databaseForm;
        private void 数据库备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (databaseForm == null || databaseForm.IsDisposed)
            {
                databaseForm = new DatabaseForm();
                databaseForm.MdiParent = this;
            }

          //  databaseForm.WindowState = FormWindowState.Maximized;
            databaseForm.BringToFront();
            databaseForm.Show();

            allForm.Add(databaseForm);
        }

        private RepairRecordExport repairRecordExport;
        private void 导出维修记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repairRecordExport == null || repairRecordExport.IsDisposed)
            {
                repairRecordExport = new RepairRecordExport();
                repairRecordExport.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            repairRecordExport.BringToFront();
            repairRecordExport.Show();

            allForm.Add(repairRecordExport);
        }

        private StationCheckForm stationcheckform;
        private void 站别检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stationcheckform == null || stationcheckform.IsDisposed)
            {
                stationcheckform = new StationCheckForm();
                stationcheckform.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            stationcheckform.BringToFront();
            stationcheckform.Show();

            allForm.Add(stationcheckform);
        }

        private void 站别检查ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            站别检查ToolStripMenuItem_Click(null,null);
        }

        private DPKExport dpkExport;
        private void dPK信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dpkExport == null || dpkExport.IsDisposed)
            {
                dpkExport = new DPKExport();
                dpkExport.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            dpkExport.BringToFront();
            dpkExport.Show();

            allForm.Add(dpkExport);
        }
        private MaterialConsumeExport materialConsumeExport;
        private void 库存消耗查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (materialConsumeExport == null || materialConsumeExport.IsDisposed)
            {
                materialConsumeExport = new MaterialConsumeExport();
                materialConsumeExport.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            materialConsumeExport.BringToFront();
            materialConsumeExport.Show();

            allForm.Add(materialConsumeExport);
        }

        private FlexIdExport flexIdExport;
        private void flexidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flexIdExport == null || flexIdExport.IsDisposed)
            {
                flexIdExport = new FlexIdExport();
                flexIdExport.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            flexIdExport.BringToFront();
            flexIdExport.Show();

            allForm.Add(flexIdExport);
        }

        private ChartForm chartForm;
        private void lCD显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chartForm == null || chartForm.IsDisposed)
            {
                chartForm = new ChartForm();
               // lcdDisplay.MdiParent = this;
            }

            chartForm.WindowState = FormWindowState.Maximized;
            chartForm.BringToFront();
            chartForm.Show();

            allForm.Add(chartForm);
        }

        private LCDDisplay lcdDisplay;
        private void 最近一个月内容汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lcdDisplay == null || lcdDisplay.IsDisposed)
            {
                lcdDisplay = new LCDDisplay();
                // lcdDisplay.MdiParent = this;
            }

            lcdDisplay.WindowState = FormWindowState.Maximized;
            lcdDisplay.BringToFront();
            lcdDisplay.Show();

            allForm.Add(lcdDisplay);
        }

        private CIDExport cidExport;
        private void cID信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cidExport == null || cidExport.IsDisposed)
            {
                cidExport = new CIDExport();
                cidExport.MdiParent = this;
            }

            //  repairRecordExport.WindowState = FormWindowState.Maximized;
            cidExport.BringToFront();
            cidExport.Show();

            allForm.Add(cidExport);
        }

        private MBLifeRecord mbLifeRecord;
        private void 主板生命周期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbLifeRecord == null || mbLifeRecord.IsDisposed)
            {
                mbLifeRecord = new MBLifeRecord();
                mbLifeRecord.MdiParent = this;
            }

            mbLifeRecord.WindowState = FormWindowState.Maximized;
            mbLifeRecord.BringToFront();
            mbLifeRecord.Show();

            allForm.Add(mbLifeRecord);
        }

        private FaultMBUnRepairForm faultMBUnRepairForm;
        private void mB未休出不良品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultMBUnRepairForm == null || faultMBUnRepairForm.IsDisposed)
            {
                faultMBUnRepairForm = new FaultMBUnRepairForm();
                faultMBUnRepairForm.MdiParent = this;
            }

            faultMBUnRepairForm.WindowState = FormWindowState.Maximized;
            faultMBUnRepairForm.BringToFront();
            faultMBUnRepairForm.Show();

            allForm.Add(faultMBUnRepairForm);

        }

        private PackageForm packageform;  
        private void 包装操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (packageform == null || packageform.IsDisposed)
            {
                packageform = new PackageForm();
                packageform.MdiParent = this;
            }

            //packageform.WindowState = FormWindowState.Maximized;
            packageform.BringToFront();
            packageform.Show();

            allForm.Add(packageform);
        }

        private void 预领料申请ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            库房领料申请ToolStripMenuItem_Click(null, null);
        }

        private ReturnOrderExport reorexport;
        private void 还货表信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (reorexport == null || reorexport.IsDisposed)
            {
                reorexport = new ReturnOrderExport();
                reorexport.MdiParent = this;
            }

            // reorexport.WindowState = FormWindowState.Maximized;
            reorexport.BringToFront();
            reorexport.Show();

            allForm.Add(reorexport);
        }

        private MBLifeExport mbLifeExport;
        private void 主板流水导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbLifeExport == null || mbLifeExport.IsDisposed)
            {
                mbLifeExport = new MBLifeExport();
                mbLifeExport.MdiParent = this;
            }

            mbLifeExport.WindowState = FormWindowState.Maximized;
            mbLifeExport.BringToFront();
            mbLifeExport.Show();

            allForm.Add(mbLifeExport);
        }

        private OutWaitRepairByHand outWaitRepairByHand;
        private void 手动出待维修库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outWaitRepairByHand == null || outWaitRepairByHand.IsDisposed)
            {
                outWaitRepairByHand = new OutWaitRepairByHand();
                outWaitRepairByHand.MdiParent = this;
            }

            outWaitRepairByHand.WindowState = FormWindowState.Maximized;
            outWaitRepairByHand.BringToFront();
            outWaitRepairByHand.Show();

            allForm.Add(outWaitRepairByHand);
        }
		   private UnlockForm unlockForm;
        private void 解锁板子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unlockForm == null || unlockForm.IsDisposed)
            {
                unlockForm = new UnlockForm();
                unlockForm.MdiParent = this;
            }

           // unlockForm.WindowState = FormWindowState.Maximized;
            unlockForm.BringToFront();
            unlockForm.Show();

            allForm.Add(unlockForm);
        }
        private EcoForm ecoForm;
        private void eCOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ecoForm == null || ecoForm.IsDisposed)
            {
                ecoForm = new EcoForm();
                ecoForm.MdiParent = this;
            }

            ecoForm.WindowState = FormWindowState.Maximized;
            ecoForm.BringToFront();
            ecoForm.Show();

            allForm.Add(ecoForm);
        }

        private Test_Outlook.TakePhotoForm takePhotoForm;
        private void 拍照ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (takePhotoForm == null || takePhotoForm.IsDisposed)
            {
                takePhotoForm = new Test_Outlook.TakePhotoForm();
                takePhotoForm.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            takePhotoForm.BringToFront();
            takePhotoForm.Show();

            allForm.Add(takePhotoForm);
        }

        private ObeRateForm obeRateForm;
        private void oBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (obeRateForm == null || obeRateForm.IsDisposed)
            {
                obeRateForm = new ObeRateForm();
                obeRateForm.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            obeRateForm.BringToFront();
            obeRateForm.Show();

            allForm.Add(obeRateForm);
        }

        private void 主板流水ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            主板生命周期ToolStripMenuItem_Click(null, null);
        }

        private void 主板流水ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            主板生命周期ToolStripMenuItem_Click(null, null);
        }

        private RepairOtherMaterialInputForm repairOtherMaterialInputForm;
        private void 其他用料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repairOtherMaterialInputForm == null || repairOtherMaterialInputForm.IsDisposed)
            {
                repairOtherMaterialInputForm = new RepairOtherMaterialInputForm();
                repairOtherMaterialInputForm.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            repairOtherMaterialInputForm.BringToFront();
            repairOtherMaterialInputForm.Show();

            allForm.Add(repairOtherMaterialInputForm);
        }

        private ObeCheckExport obeCheckExport;
        private void oBE抽检信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obeCheckExport == null || obeCheckExport.IsDisposed)
            {
                obeCheckExport = new ObeCheckExport();
                obeCheckExport.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            obeCheckExport.BringToFront();
            obeCheckExport.Show();

            allForm.Add(obeCheckExport);
        }

        private LiangpinMaterialOutForm liangpinMaterialOutForm;
        private void 良品报关出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (liangpinMaterialOutForm == null || liangpinMaterialOutForm.IsDisposed)
            {
                liangpinMaterialOutForm = new LiangpinMaterialOutForm();
                liangpinMaterialOutForm.MdiParent = this;
            }

            //faultOutForm.WindowState = FormWindowState.Maximized;
            liangpinMaterialOutForm.BringToFront();
            liangpinMaterialOutForm.Show();

            allForm.Add(liangpinMaterialOutForm);
        }

        private ModifyErrorTrackNoForm modifyErrorTrackNoForm;
        private void 修改收错的板子ToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            if (liangpinMaterialOutForm == null || liangpinMaterialOutForm.IsDisposed)
            {
                modifyErrorTrackNoForm = new ModifyErrorTrackNoForm();
                modifyErrorTrackNoForm.MdiParent = this;
            }

            //faultOutForm.WindowState = FormWindowState.Maximized;
            modifyErrorTrackNoForm.BringToFront();
            modifyErrorTrackNoForm.Show();

            allForm.Add(modifyErrorTrackNoForm);
        }

        private SITaxExport sITaxExport;
        private void sI交税报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (sITaxExport == null || sITaxExport.IsDisposed)
            {
                sITaxExport = new SITaxExport();
                sITaxExport.MdiParent = this;
            }

            //faultOutForm.WindowState = FormWindowState.Maximized;
            sITaxExport.BringToFront();
            sITaxExport.Show();

            allForm.Add(sITaxExport);
        }
    }
}
