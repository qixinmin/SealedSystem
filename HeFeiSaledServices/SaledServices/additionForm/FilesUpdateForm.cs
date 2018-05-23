using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace SaledServices
{
    public partial class FilesUpdateForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "TestCpu";

        public FilesUpdateForm()
        {
            InitializeComponent();
        }

        public byte[] File2Bytes(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            byte[] fileDatas = new byte[fs.Length];
            fs.Read(fileDatas, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return fileDatas;
        }    

        private void add_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();

            string fileName = openFileDialog.FileName;
            this.cpupnfileName.Text = fileName;
            if (this.cpupnfileName.Text.Trim() == "")
            {
                MessageBox.Show("CPUPN内容为空!");
                return;
            }
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            string chkcpu = openFileDialog1.FileName;
            this.chkcpufilename.Text = chkcpu;

            if (this.chkcpufilename.Text.Trim() == "")
            {
                MessageBox.Show("CHKCPU内容为空!");
                return;
            }
        }

        private void upload_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + tableName + " where id='1'";
                    SqlDataReader sq = cmd.ExecuteReader();
                    bool exist = false;
                    if (sq.HasRows)
                    {
                        exist = true;
                    }
                    sq.Close();
                    if (exist == false)
                    {
                        cmd.CommandText = "insert into " + tableName + " values('1','1','1')";
                        cmd.ExecuteNonQuery();
                    }
                    
                    if (this.cpupnfileName.Text != "")
                    {
                        cmd.CommandText = "update " + tableName + " set cpupn=@cpupn where id='1'";
                        SqlParameter cpupnFile = new SqlParameter("@cpupn", SqlDbType.Image);
                        cpupnFile.Value = File2Bytes(this.cpupnfileName.Text);
                        cmd.Parameters.Add(cpupnFile);
                        cmd.ExecuteNonQuery();
                    }
                    if (this.chkcpufilename.Text != "")
                    {
                        cmd.CommandText = "update " + tableName + " set chkcpu=@chkcpu where id='1'";
                        SqlParameter chkcpu = new SqlParameter("@chkcpu", SqlDbType.Image);
                        chkcpu.Value = File2Bytes(this.chkcpufilename.Text);
                        cmd.Parameters.Add(chkcpu);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                this.cpupnfileName.Text = "";
                this.chkcpufilename.Text = "";
                MessageBox.Show("上传成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT cpupn, chkcpu FROM "+tableName;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        byte[] cpupn = (byte[])reader.GetValue(0);
                        byte[] chkcpu = (byte[])reader.GetValue(1);

                        string saveFileName = @"D:\CPUPN.txt";
                        int arraysize = new int();
                        arraysize = cpupn.GetUpperBound(0);
                        FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(cpupn, 0, arraysize);
                        fs.Close();

                        saveFileName = @"D:\CHKCPU.BAT";
                        arraysize = chkcpu.GetUpperBound(0);
                        fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(chkcpu, 0, arraysize);
                        fs.Close();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                this.cpupnfileName.Text = "";
                this.chkcpufilename.Text = "";
                MessageBox.Show("下载成功，放在D:\\下！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
