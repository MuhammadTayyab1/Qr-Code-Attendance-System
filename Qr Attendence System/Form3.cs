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
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace Qr_Attendence_System
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int a = r.Next(10000, 99999);

            textBox1.Text = Convert.ToString(a);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                label5.Text = "First generate unique pin";
            }
            else
            {
                if(textBox2.Text=="")
                {
                    label6.Text = "This is required field";
                    label5.Text = "";
                }
                else
                {
                    string n = "";
                    string p = "";
                    string d = "";
                    string a = "";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();
                        data["username"] = "myUser";
                        data["password"] = "myPassword";

                        var response = wb.UploadValues("https://jsonplaceholder.typicode.com/posts", "POST", data);
                        string responseInString = Encoding.UTF8.GetString(response);
                        //Console.WriteLine(responseInString);
                    }

                    using (var wb = new WebClient())
                    {
                        var response = wb.DownloadString("https://jsonplaceholder.typicode.com/todos/1");
                        Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString());

                        string[] keys = list.Keys.ToArray();

                        
                        n = list[keys[0]].ToString();
                        p = list[keys[1]].ToString();
                        d = list[keys[2]].ToString();
                        a = list[keys[3]].ToString();

                        //Console.WriteLine(response);
                    }








                    string pin = textBox1.Text;
                    string course = textBox2.Text;
                    string comment = textBox3.Text;
                    string date=DateTime.Now.ToString("MM/dd/yyyy");
                    string Time= DateTime.Now.ToString("HH:mm:ss");
                    string save = "pin:" + pin + "\nCourse:" + course + "\nDate:" + date + "\nTime:" + Time + "\nComment:" + comment;
                    save += "\nJSON\nVar1 :"+n+"\nVar2 :"+p+"\nVar3 :"+d+"\nVar4 :"+a;


                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    pictureBox1.Image = qrcode.Draw(save, 50);
                    label5.Text = "";
                    label6.Text = "";



                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
