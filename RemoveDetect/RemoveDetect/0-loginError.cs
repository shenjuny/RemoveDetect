using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class errorLogin : Form
    {
        public errorLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //faceRecog fr = new faceRecog();
            //fr = (faceRecog)this.Owner;
            //fr.Close();
            this.Close();
            Thread thread = new Thread(showform);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform()
        {
            Application.Run(new faceRecog());
        }
        private void label2_MouseHover(object sender, EventArgs e)
        {
            this.label2.Font = new Font("宋体", 24, FontStyle.Underline);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.label2.Font = new Font("宋体", 10);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            faceRecog fr = new faceRecog();
            fr = (faceRecog)this.Owner;
            fr.Close();
            this.Close();
            Thread thread = new Thread(showform1);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform1()
        {
            Application.Run(new idCardLogin());
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            this.label3.Font = new Font("宋体", 24, FontStyle.Underline);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            this.label3.Font = new Font("宋体", 10);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            faceRecog fr = new faceRecog();
            fr = (faceRecog)this.Owner;
            fr.Close();
            this.Close();
            Thread thread = new Thread(showform2);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform2()
        {
            Application.Run(new logon());
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            this.label4.Font = new Font("宋体", 24, FontStyle.Underline);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            this.label4.Font = new Font("宋体", 10);
        }
    }
}
