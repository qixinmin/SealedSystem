﻿using System;
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
    public partial class ModifyErrorTrackNoForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
       // private String tableName = "sourceTable";

        public ModifyErrorTrackNoForm()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.rightTrackNoTextBox.Text.Trim() == "" || this.errorTrackNoTextBox.Text.Trim() == "")
            {
                MessageBox.Show("内容为空!");
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    string right= this.rightTrackNoTextBox.Text.ToUpper().Trim();
                    string error = this.errorTrackNoTextBox.Text.ToUpper().Trim();

                    cmd.CommandText = " update DeliveredTable set track_serial_no='"+right+"' where track_serial_no='"+error+"'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set track_serial_no='"+right+"' where track_serial_no='"+error+"'";
                    cmd.ExecuteNonQuery();
  
                    cmd.CommandText = " update flexidRecord set track_serial_no='"+right+"' where track_serial_no='"+error+"'";
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "update wait_repair_in_house_table set track_serial_no='"+right+"' where track_serial_no='"+error+"'";
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "update wait_repair_out_house_table set track_serial_no='" + right + "' where track_serial_no='" + error + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update need_to_lock set track_serial_no='" + right + "' where track_serial_no='" + error + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update mb_repair_status_record set track_serial_no='" + right + "' where track_serial_no='" + error + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
              //  query_Click(null, null);
                MessageBox.Show("更新成功!");
                this.rightTrackNoTextBox.Text = "";
                this.errorTrackNoTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                //mConn = new SqlConnection(Constlist.ConStr);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = mConn;
                //cmd.CommandText = "select * from  " + tableName;
                //cmd.CommandType = CommandType.Text;

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

            string[] hTxt = { "ID", "来源" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            //DataTable dt = ds.Tables[tableName];
            //sda.FillSchema(dt, SchemaType.Mapped);
            //DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());
            //dr["source"] = this.sourceTextBox.Text.Trim();            

            //SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            //sda.Update(dt);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(Constlist.ConStr);
                //conn.Open();

                //if (conn.State == ConnectionState.Open)
                //{
                //    SqlCommand cmd = new SqlCommand();
                //    cmd.Connection = conn;
                //    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                //    cmd.CommandType = CommandType.Text;
                //    cmd.ExecuteNonQuery();
                //}
                //else
                //{
                //    MessageBox.Show("SaledService is not opened");
                //}

                //conn.Close();
                //MessageBox.Show("删除完毕!");
                //query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.errorTrackNoTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.rightTrackNoTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();

        }
    }
}