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
    public partial class loginChoice : Form
    {
        public loginChoice()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
            this.Close();
            Thread thread = new Thread(showform);
            thread.IsBackground = false;
            thread.Start();

        }
        void showform()
        {
            Application.Run(new faceRecog());
        }
        private void label3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
            this.Close();
            Thread thread = new Thread(showform1);
            thread.IsBackground = false;
            thread.Start();

        }
        void showform1()
        {
            Application.Run(new idCardLogin());
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
            this.Close();
            Thread thread = new Thread(showform2);
            thread.IsBackground = false;
            thread.Start();

        }
        void showform2()
        {
            Application.Run(new logon());
        }
    
    }
}
