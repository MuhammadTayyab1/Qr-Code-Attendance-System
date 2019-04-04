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

namespace Qr_Code_Attendence_System_2019
{
    public partial class view : Form
    {
        public view()
        {
            InitializeComponent();
        }
        public class car
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Status { get; set; }

        }

        private void view_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            try
            {
                using (var wb = new WebClient())
                {
                    var response = wb.DownloadString("https://api.myjson.com/bins/a4vru");

                    Console.WriteLine(response);

                    Dictionary<string, car> test = JsonConvert.DeserializeObject<Dictionary<string, car>>(response);

                    string savehold = "";
                    string[] nam = new string[test.Keys.ToArray().Length];
                    string[] id = new string[test.Keys.ToArray().Length];
                    string[] sta = new string[test.Keys.ToArray().Length];


                    string[] keys = test.Keys.ToArray();

                    int index = 0;
                    foreach (var item in keys)
                    {
                        car temp = test[item];

                        nam[index] = temp.Name;

                        id[index] = temp.Id;

                        sta[index] = temp.Status;

                        index++;
                    }

                    int countstu = 0;
                    // checking arrays data
                    for (int i = 0; i < nam.Length; i++)
                    {
                        countstu++;
                        savehold += "                                                                            Student " + countstu + "\n";
                        savehold += "                                                                                       Name   :   " + nam[i] + "\n";
                        savehold += "                                                                                       Id     :   " + id[i] + "\n";
                        savehold += "                                                                                       Status :   " + sta[i] + "\n";
                        savehold += "                                                                            _______________________________________\n\n\n\n";
                    }

                    richTextBox1.Text = savehold;



                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Your internet is not working \n PleaseCheck your connection \n and try again \n\n" + ex.Message);
            }

            
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
    }
}
