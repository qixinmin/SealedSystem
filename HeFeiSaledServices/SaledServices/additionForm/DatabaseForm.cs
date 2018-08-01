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

namespace SaledServices.additionForm
{
    public partial class DatabaseForm : Form
    {
        public DatabaseForm()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                string path = "D:\\DatabaseBackup\\";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                string filename = path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";

                cmd.CommandText = "BACKUP DATABASE  SaledService TO DISK = '" + filename + "'";
                cmd.ExecuteNonQuery();
                mConn.Close();

                MessageBox.Show("备份成功到服务器的 " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
