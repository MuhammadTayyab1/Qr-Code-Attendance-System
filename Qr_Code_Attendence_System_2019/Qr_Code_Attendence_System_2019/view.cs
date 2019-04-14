using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace Qr_Code_Attendence_System_2019
{
    public partial class view : Form
    {
        public view()
        {
            InitializeComponent();
        }

        private void view_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            richTextBox1.ReadOnly = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            facultypannel f3 = new facultypannel();
            f3.Show();
        }

        private void view_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    label3.Text = "Processing ...";
                    viewdata vb = new viewdata();
                    richTextBox1.Text = vb.view(textBox1.Text);
                    label3.Text = "";
                }
                catch
                {
                    label3.Text = "";
                    MessageBox.Show("Invalid day input please input correct day \n  Or  \n Check your internet connection");

                }
            }
            else
            {
                label2.Text = "Please enter day";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }
    }
    class viewdata
    {
        public string view(string d)
        {
            string savehold = "";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["classDay"] = "" + d;

                var response = wb.UploadValues("https://guarded-river-89855.herokuapp.com/classes/getattendace", "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
                Dictionary<string, object> list = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseInString.ToString());

                string[] keys = list.Keys.ToArray();
                int sno = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    sno++;
                    Dictionary<string, object> list1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(list[keys[i]].ToString());
                    string[] keys1 = list1.Keys.ToArray();
                    savehold += "                                                      S.No  " + sno + "\n";
                    savehold += "                                                                         Student Name          :   " + list1[keys1[2]].ToString() + "\n";
                    savehold += "                                                                         Student Id                :   " + list1[keys1[3]].ToString() + "\n";
                    savehold += "                                                                         Attendance Status   :   " + list1[keys1[0]].ToString() + "\n";



                    savehold += "                                                                             ________________________________________\n";
                    savehold += "\n\n";
                }
                return savehold;
            }
        }
    }
}
