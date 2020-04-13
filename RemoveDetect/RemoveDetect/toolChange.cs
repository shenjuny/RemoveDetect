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
    public partial class toolChange : Form
    {
        private config config;
        public toolChange()
        {
            InitializeComponent();
        }
        public toolChange(config config)
        {
            InitializeComponent();
            this.config = config;
        }
        private void toolChange_Load(object sender, EventArgs e)
        {
            this.txtToolNo.Text = this.config.listTool[this.config.num].No;
            this.txtToolName.Text = this.config.listTool[this.config.num].name;
            this.txtToolAddress.Text = this.config.listTool[this.config.num].address;
            this.txtToolPic.Text = this.config.listTool[this.config.num].picture;
        }
        private void btn_save_device_Click(object sender, EventArgs e)
        {
            try
            {
                tool tool = new tool();
                tool.name = txtToolNo.Text.Trim();
                tool.No = txtToolName.Text.Trim();
                tool.address = txtToolAddress.Text.Trim();
                tool.picture = txtToolPic.Text.Trim();
                this.config.listTool.RemoveAt(this.config.num);
                this.config.listTool.Add(tool);

                DataTable dataTable = Common.FillDataTable<tool>(this.config.listTool);
                DBSet.dataTableToCsvT(dataTable, @"..\tool.csv");

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
