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
   



    public partial class login : Form
    {



        public login()
        {
            InitializeComponent();
         
            
        }
       



        private void button1_Click(object sender, EventArgs e)
        {
           // Login checking
           
            logininfo obj = new logininfo();
            int i = obj.logininfos("" + textBox1.Text + "", "" + textBox2.Text + "");
            if (i == 1)
            {
                label3.Text = null;
                this.Hide();
            }
            else if(i==0)
            {
               label3.Text = "please fill complete informantion";
                
            }
            else if(i==2)
            {
                label3.Text = "Incorrect Email or Password";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            home f1 = new home();
            f1.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ck();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ck();
        }


        public void ck()
        {
            if(textBox1.Text!=null && textBox2.Text !=null)
            {
                label3.Text = "";
            }
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

    }

    class logininfo
    {
        string user;
        string pass;
        public int logininfos(string un, string pa)
        {
            if (un == "" || pa == "")
            {
                return 0;
            }
            else
            {
                user = un;
                pass = pa;

                if (user == "admin" && pass == "1234")
                {
                    facultypannel f3 = new facultypannel();
                    f3.Show();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}
