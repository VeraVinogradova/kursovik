using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Generation generation;

        private void Form1_Load(object sender, EventArgs e)
        {
            generation = new Generation(textBox1);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            generation.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            generation.Stop();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            generation.conn.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
    }
}
