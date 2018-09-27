using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class MBLifeRecord : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "stationInfoRecord";

        public MBLifeRecord()
        {
            InitializeComponent();
        }       

        private void query_Click(object sender, EventArgs e)
        {
            if (this.trackNoTextBox.Text.Trim() == "")
            {
                MessageBox.Show("跟踪条目为空！");
                return;
            }
            try
            {
                this.dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                List<stationInfoRecord> stationList = new List<stationInfoRecord>();
                mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id, trackno, station,recordstatus,recorddate,repairplace1,repairnum1,repairMaterial1,inputer from  " + tableName + " where trackno='" + this.trackNoTextBox.Text.Trim() + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    stationInfoRecord temp = new stationInfoRecord();
                    temp.id = querySdr[0].ToString();
                    temp.trackno = querySdr[1].ToString();
                    temp.station = querySdr[2].ToString();
                    temp.recordstatus = querySdr[3].ToString();
                    temp.recorddate = querySdr[4].ToString();
                    temp.repairplace1 = querySdr[5].ToString();
                    temp.repairnum1 = querySdr[6].ToString();
                    temp.repairMaterial1 = querySdr[7].ToString();
                    temp.inputer = querySdr[8].ToString();
                    stationList.Add(temp);
                }
                querySdr.Close();

                //List<string> distintTrackNo = new List<string>();
                //cmd.CommandText = "select distinct trackno from  " + tableName;
                //querySdr = cmd.ExecuteReader();
                //while (querySdr.Read())
                //{
                //    distintTrackNo.Add(querySdr[0].ToString());
                //}
                //querySdr.Close();


               // foreach (string trackno in distintTrackNo)
                {
                    cmd.CommandText = "select input_date from repaired_out_house_excel_table where track_serial_no='" + this.trackNoTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        stationInfoRecord temp = new stationInfoRecord();
                        temp.trackno =this.trackNoTextBox.Text.Trim();
                        temp.station = "还货";
                        temp.recordstatus = "OK";
                        temp.recorddate = querySdr[0].ToString();
                        stationList.Add(temp);
                    }
                    querySdr.Close();
                }

                mConn.Close();

                dataGridView1.DataSource = stationList;
                dataGridView1.RowHeadersVisible = false;

                //sda = new SqlDataAdapter();
                //sda.SelectCommand = cmd;
                //ds = new DataSet();
                //sda.Fill(ds, tableName);
                //dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.RowHeadersVisible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "跟踪条码","站别","状态","日期","维修位置","数量","维修材料","輸入人" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }
        }      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
