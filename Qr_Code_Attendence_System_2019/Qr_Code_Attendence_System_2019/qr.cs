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
            int num = r.Next(100000, 999999);
            textBox1.Text = Convert.ToString(num);
        }
        public void ck()
        {
            if (textBox1.Text != null)
            {
                label6.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 8)
            {
                MessageBox.Show("Class id must be less then 9 digits");
            }
            else
            {
                if (textBox3.Text.Length > 68)
                {
                    MessageBox.Show("comment must be less then 68 characters \n your recent comment length is "+textBox3.Text.Length);
                }
                else
                {

                    string save = "";
                    if (textBox1.Text == "")
                    {
                        label5.Text = "First generate unique pin";
                    }
                    else
                    {
                        if (textBox2.Text == "")
                        {
                            label6.Text = "Please enter course id";
                        }
                        else
                        {
                            label6.Text = "";
                            save += "Unique pin : " + textBox1.Text + "\n";
                            save += "Course Id  : " + textBox2.Text + "\n";
                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();
                                data["username"] = "ali";
                                data["password"] = "1234";

                                try
                                {
                                    var response = wb.UploadValues("https://jsonplaceholder.typicode.com/posts", "POST", data);
                                    string responseInString = Encoding.UTF8.GetString(response);
                                    Console.WriteLine(responseInString);

                                    Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInString.ToString());

                                    string[] keys = list.Keys.ToArray();

                                    save += "Post id  : " + list[keys[2]].ToString() + "\n";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Your internet is not working \n PleaseCheck your connection \n and try again \n\n" + ex.Message);
                                }
                            }
                            if (textBox3.Text != "")
                            {
                                save += "Comment  : " + textBox3.Text + "\n";
                            }

                            try
                            {
                                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                                pictureBox1.Image = qrcode.Draw(save, 50);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            random();
        }

        private void qr_Load(object sender, EventArgs e)
        {
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
            ck();
        }
    }
}
