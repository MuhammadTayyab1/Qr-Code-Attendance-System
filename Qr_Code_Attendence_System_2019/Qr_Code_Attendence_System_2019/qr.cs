using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Drawing.Imaging;

namespace Qr_Code_Attendence_System_2019
{
    public partial class qr : Form
    {
        public qr()
        {
            InitializeComponent();
        }
        public void random()
        {
            Random r = new Random();
            long num = r.Next(100000000, 999999999);
            textBox1.Text = Convert.ToString(num);

            Random r1 = new Random();
            long num1 = r1.Next(100000000, 999999999);
            textBox1.Text += Convert.ToString(num);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(2019, 4, 11))).TotalSeconds;
                string save = "";
                save += textBox1.Text;

                
                try
                {
                    label6.Text = "Please Wait System is processing ....";
                    genqrcd myobj = new genqrcd(textBox1.Text);
                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    pictureBox1.Image = qrcode.Draw(save, 50);
                    label6.Text = "";
                }
                catch
                {
                    MessageBox.Show("check your internet connection");
                    label6.Text = "";
                }
            }
            else
            {
                label5.Text = "First generate unique pin";
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            random();
        }

        private void qr_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultypannel f2 = new facultypannel();
            f2.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void qr_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
    class genqrcd
    {
        public genqrcd(string qrpin)
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["qrcode"] = "" + qrpin;
                var response = wb.UploadValues("https://guarded-river-89855.herokuapp.com/attendace/create", "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInString.ToString());
                string[] keys = list.Keys.ToArray();
                MessageBox.Show("Qr Code Sucessfully generated for \n Day " + list[keys[2]].ToString() + "\nUsing day you can get access to overall attendance");

            }
        }
    }
}
