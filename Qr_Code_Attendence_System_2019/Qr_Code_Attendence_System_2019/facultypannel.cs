using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qr_Code_Attendence_System_2019
{
    public partial class facultypannel : Form
    {
        public facultypannel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            view f5 = new view();
            f5.Show(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            qr f4 = new qr();
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            login f2 = new login();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            login fs = new login();
            fs.Show();
        }
    }
}
