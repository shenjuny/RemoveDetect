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
    public partial class _2_toolPickBack : Form
    {
        public _2_toolPickBack()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 工具取用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform()
        {
            Application.Run(new _2_toolPickUp());
        }
        /// <summary>
        /// 工具归还
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform1);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform1()
        {
            Application.Run(new _2_toolBack());
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform3);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform3()
        {
            Application.Run(new _1_menuList());
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _1_menuLogout exit = new _1_menuLogout();
            exit.ShowDialog(this);
        }
    }
}
