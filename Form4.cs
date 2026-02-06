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
    public partial class Form4 : Form
    {
        public SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Курсовая работа\\Курсовая работа\\DB.mdf;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
        }
        decimal summ = 0;
        private void Form4_Load(object sender, EventArgs e)
        {
            conn.Open();
            string query = $"SELECT price FROM [Order]";
            SqlCommand com = new SqlCommand(query, conn);
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    summ += reader.GetDecimal(0);
                }
            }
            textBoxSumm.Text = Convert.ToString(summ);
            conn.Close();
        }
    }
}
