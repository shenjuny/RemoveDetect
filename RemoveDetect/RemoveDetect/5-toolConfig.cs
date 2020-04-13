using RDService;
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
    public partial class _5_toolConfig : Form
    {
        Service1 x = new Service1();
        public _5_toolConfig()
        {
            InitializeComponent();
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform1);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform1()
        {
            Application.Run(new _1_menuList());
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _1_menuLogout exit = new _1_menuLogout();
            exit.ShowDialog(this);
        }

        private void seatGenerate(object sender, EventArgs e)
        {
            
            if (txtBoxLayer.Text != "" && txtBoxSeats.Text != "")
            {
                int layer = Convert.ToInt32(txtBoxLayer.Text);
                int seats = Convert.ToInt32(txtBoxSeats.Text);
                this.dataGridView1.ColumnCount = 1;
                this.dataGridView1.RowCount = layer;
                
                for (int j = 0; j < seats; j++)
                {
                    //为dgv增加复选框列
                    DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
                    //列显示名称
                    checkbox.HeaderText = (j + 1).ToString();

                    //checkbox.Name = "IsChecked";
                    //checkbox.TrueValue = true;
                    //checkbox.FalseValue = false;
                    //checkbox.DataPropertyName = "IsChecked";
                    //列大小不改变
                    //checkbox.Resizable = DataGridViewTriState.False;
                    this.dataGridView1.Columns.Add(checkbox);
                }
                for (int i = 0; i < layer; i++)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    this.dataGridView1.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    for (int j = 1; j <= seats; j++)
                    {
                        this.dataGridView1.Rows[i].Cells[j].Value = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //int layerNum = Convert.ToInt32(txtBoxLayer.Text);
            //int seatsNum = Convert.ToInt32(txtBoxSeats.Text);
            //for (int i = 0; i < layerNum; i++)
            //{
            //    for (int j = 1; j < seatsNum + 1; j++)
            //    {
            //        //addToolsPosition(i, j, this.dataGridView1.Rows[i].Cells[j].Value);
            //    }
            //}
            string IP = txtBoxIP.Text;
            string name = txtBoxName.Text;
            string mask = txtBoxmask.Text;
            string gate = txtBoxgate.Text;
            string type = txtBoxType.Text;
            string size = txtBoxSize.Text;
            string Num = txtBoxNum.Text;
            string model = "";
            string note = "";
            string layer = txtBoxLayer.Text;
            string seats = txtBoxSeats.Text;
            string limitTIme = domainUpDown1.Text;
            string hostName = txtBoxHostName.Text;
            string IPVersion = ComboIPVersion.Text;
            string DNS = txtBoxDNS.Text;
            string id = txtBoxId.Text;
            var tmp = x.editDevice(id, IP, name, mask, gate, type, size, Num, model, note, layer, seats, limitTIme, hostName, IPVersion, DNS);
            if (tmp.success)
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void _5_toolConfig_Load(object sender, EventArgs e)
        {
            try
            {
                var tmp = x.getDeviceList();
                if (tmp.success)
                {
                    txtBoxId.Text = tmp.rows[0].id;
                    txtBoxName.Text = tmp.rows[0].name;
                    txtBoxNum.Text = tmp.rows[0].Num;
                    txtBoxType.Text = tmp.rows[0].type;
                    txtBoxSize.Text = tmp.rows[0].size;
                    txtBoxLayer.Text = tmp.rows[0].layer;
                    txtBoxSeats.Text = tmp.rows[0].seats;

                    txtBoxHostName.Text = tmp.rows[0].hostName;
                    txtBoxIP.Text = tmp.rows[0].IP;
                    txtBoxmask.Text = tmp.rows[0].mask;
                    txtBoxgate.Text = tmp.rows[0].gate;
                    txtBoxDNS.Text = tmp.rows[0].DNS;
                    ComboIPVersion.Text = tmp.rows[0].IPVersion;
                    domainUpDown1.Text = tmp.rows[0].limitTIme;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据读取异常:"+ex.Message);
            }
        }
    }
}
