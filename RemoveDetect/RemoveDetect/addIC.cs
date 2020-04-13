using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RemoveDetect
{
    public delegate void setICTextValue(string textValue);
    public partial class addIC : Form
    {
        [DllImport("umf.DLL", EntryPoint = "fw_init")]
        public static extern Int32 fw_init(Int16 port, Int32 baud);
        [DllImport("umf.DLL", EntryPoint = "fw_exit")]
        public static extern Int32 fw_exit(Int32 icdev);

        [DllImport("umf.DLL", EntryPoint = "fw_card")]
        public static extern Int32 fw_card(Int32 icdev, Byte _Mode, ulong[] _Snr);

        Int32 ihdev;
        
        public event setICTextValue setICTextValue;
        public addIC()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ihdev = fw_init(100, 0);//baud 115200

            if (ihdev > 0)
            {
                ulong[] cardnumber = new ulong[3];
                int state = fw_card(ihdev, 0, cardnumber);
                if (state != 0)
                {
                    
                    fw_exit(ihdev);
                    MessageBox.Show("读卡失败！");
                }
                else
                {
                    txtbox_number.Text = cardnumber[0].ToString();
                    setICTextValue(cardnumber[0].ToString());
                    fw_exit(ihdev);
                    this.Close();
                }
            }
        }
    }
}
