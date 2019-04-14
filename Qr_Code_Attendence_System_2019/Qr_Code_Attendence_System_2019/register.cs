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
using System.Net;
using System.Collections.Specialized;
using System.Threading;

namespace Qr_Code_Attendence_System_2019
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bt obj = new bt();
            int hold =obj.checkempty(textBox1.Text, textBox2.Text,progressBar1);

            if (hold==1)
            {
                label3.Text = "All fields are required";
            }
            else if(hold==2)
            {
                label3.Text = "";
                label4.Text = "Please enter correct email format";
            }
            else if (hold == 0)
            {
                label4.Text = "";
                label3.Text = "";
            }

            int err = reg.regmsg();
            if(err==101)
            {
                label5.Text = "Sucessfully Registered";
                pictureBox1.Show();
                progressBar1.Hide();
            }
            else if(err==102)
            {
                label5.Text = "";
                pictureBox1.Hide();
                label6.Text = "This email is already registered";
                progressBar1.Hide();
            }
        }

        private void register_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            pictureBox1.Hide();
            progressBar1.Hide();
            progressBar1.Minimum = 1;
            progressBar1.Maximum = 187;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultypannel fp = new facultypannel();
            fp.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label6.Text = null;
            label4.Text = null;
            label3.Text = "";
            
        }

        private void register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

    class reg
    {
        static string ch;
        public reg(string nam,string em,ProgressBar ok)
        {
            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["email"] = em;
                    data["fullName"] = nam;
                    ok.Show();
                    for (int i = 1; i <= 160; i++)
                    {
                        Thread.Sleep(50);
                        ok.Value = i;
                    }
                    var response = wb.UploadValues("https://guarded-river-89855.herokuapp.com/teacher/createstudent", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                    for (int i = 161; i < 187; i++)
                    {
                        Thread.Sleep(50);
                        ok.Value = i;
                    }

                    Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInString.ToString());

                    string[] keys = list.Keys.ToArray();

                    ch = list[keys[0]].ToString();
                }
            }
            catch
            {
                MessageBox.Show("Check your internet connection");
            }
        }
        public static int regmsg()
        {
            if(ch=="True")
            {
                return 101;
            }
            else if(ch=="False")
            {
                return 102;
            }
            else
            {
                return 103;
            }
        }
    }
    class bt
    {
        string name;
        string email;
        bool flag;
        public int checkempty(string n,string e,ProgressBar ok1)
        {
            if(n=="" || e=="")
            {
                return 1;
            }
            else
            {
                try
                {
                    string[] tayyab = e.Split('@');
                    if (tayyab[0].Length > 0 && tayyab[1].Length > 0)
                    {
                        string sub = e.Substring(e.Length - 4);
                        if (sub == ".com")
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }

                if(flag==true)
                {
                    name = n;
                    email = e;

                    reg log = new reg(name,email,ok1);
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}








