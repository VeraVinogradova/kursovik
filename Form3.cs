using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа
{
    public partial class Form3 : Form
    {
        public SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Курсовая работа\\Курсовая работа\\DB.mdf;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            string query = $"INSERT INTO [Account] (Login, Password) VALUES (@Login, @Password)";
            SqlCommand com = new SqlCommand(query, conn);

            com.Parameters.Add("@Login", System.Data.SqlDbType.NVarChar);
            com.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar);

            com.Parameters["@Login"].Value = RegLoginTextBox.Text;
            com.Parameters["@Password"].Value = RegPasswordTextBox.Text;

            com.ExecuteNonQuery();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            string query = $"SELECT Login, Password FROM [Account]";
            SqlCommand com = new SqlCommand(query, conn);

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetValue(0).ToString() == LogLoginTextBox.Text && reader.GetValue(1).ToString() == LogPasswordTextBox.Text)
                    {
                        Form3 form3 = new Form3();
                        form3.Close();
                        Form1 form = new Form1();
                        form.Show();
                    }
                }
            }
        }
    }
}
