using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qr_Code_Attendence_System_2019
{
    public partial class qrhistory : Form
    {
        public qrhistory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultypannel fp = new facultypannel();
            fp.Show();
        }

        private void qrhistory_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void qrhistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                try
                {
                    label4.Text = "System is processing please wait ....";
                    work obj = new work(textBox1.Text);
                    obj.gen(pictureBox1);
                    label4.Text = "";
                }
                catch
                {
                    label4.Text = "";
                    MessageBox.Show("Invalid Day input \n Or incorrect data connection");
                }
            }
            else
            {
                label3.Text = "This Filed is Required";
            }
        }
    }
    class work
    {
        string qrpin;
        public work(string l3)
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["classDay"] = ""+l3;

                var response = wb.UploadValues("https://guarded-river-89855.herokuapp.com/classes/getqrcode", "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInString.ToString());

                string[] keys = list.Keys.ToArray();

                qrpin = list[keys[1]].ToString();
            }
        }
        public void gen(PictureBox pb)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pb.Image = qrcode.Draw(qrpin, 50);
        }
    }
}
