using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect_4
{
    public partial class Form2 : Form
    {
        string first = "";
        string second = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            first = txt1.Text;
            second = txt2.Text;
            Form1 main = new Form1(first, second);
            this.Hide();
            main.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
