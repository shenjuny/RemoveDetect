using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class deviceChange : Form
    {
        private config config;
        public deviceChange()
        {
            InitializeComponent();
        }
        public deviceChange(config config)
        {
            InitializeComponent();
            this.config = config;
        }
        private void deviceChange_Load(object sender, EventArgs e)
        {
            this.txtbox_IP.Text = this.config.listDevice[this.config.num].IP;
            this.txtbox_mask.Text = this.config.listDevice[this.config.num].mask;
            this.txtbox_gate.Text = this.config.listDevice[this.config.num].gate;
            this.cboModel.SelectedItem = this.config.listDevice[this.config.num].model;
            this.txtbox_gate.Text = this.config.listDevice[this.config.num].gate;
            this.txtbox_name.Text = this.config.listDevice[this.config.num].name;
            this.txtbox_type.Text = this.config.listDevice[this.config.num].type;
            this.txtbox_size.Text = this.config.listDevice[this.config.num].size;
            this.txtbox_Num.Text = this.config.listDevice[this.config.num].Num;
            this.txtbox_note.Text = this.config.listDevice[this.config.num].note;
        }
        private void btn_save_device_Click(object sender, EventArgs e)
        {
            try
            {
                device device = new device();
                device.IP = txtbox_IP.Text.Trim();
                device.mask = txtbox_mask.Text.Trim();
                device.gate = txtbox_gate.Text.Trim();
                device.model = cboModel.SelectedItem.ToString();
                device.name = txtbox_name.Text.Trim();
                device.type = txtbox_type.Text.Trim();
                device.size = txtbox_size.Text.Trim();
                device.Num = txtbox_Num.Text.Trim();
                device.note = txtbox_note.Text.Trim();
                this.config.listDevice.RemoveAt(this.config.num);
                this.config.listDevice.Add(device);

                DataTable dataTable = Common.FillDataTable<device>(this.config.listDevice);
                DBSet.dataTableToCsvT(dataTable, @"..\device.csv");

                MessageBox.Show("保存成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_device_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
