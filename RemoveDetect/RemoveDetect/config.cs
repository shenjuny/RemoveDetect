using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net;
using System.Management;

namespace RemoveDetect
{
    public delegate void setControlValue(bool control);
    public partial class config : Form
    {
        public List<device> listDevice = new List<device>();
        public List<user> listUser = new List<user>();
        public List<tool> listTool = new List<tool>();
        public int num;
        string ip;
        string ipsubnet;
        string ipgateway;
        public static Bitmap bmp;
        public static Bitmap roibmp;
        string user_ID;
        string tool_ID;
        const int cTimeOutMs = 500;
        List<int> roi = new List<int>();
        public event setControlValue setControlValue;
        Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        string rectengle = "";
        public config()
        {
            InitializeComponent();
            //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection nics = mc.GetInstances();
            //foreach (ManagementObject nic in nics)
            //{
            //    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
            //    {
            //        //string mac = nic["MacAddress"].ToString();//Mac地址
            //        ip = (nic["IPAddress"] as String[])[0];//IP地址
            //        ipsubnet = (nic["IPSubnet"] as String[])[0];//子网掩码
            //        ipgateway = (nic["DefaultIPGateway"] as String[])[0];//默认网关
            //    }
            //}
            //this.txtbox_IP.Text = ip;
            //this.txtbox_mask.Text = ipsubnet;
            //this.txtbox_gate.Text = ipgateway;

            //listDevice = Common.ToDataList<device>(DBSet.ReadCSV(@"..\device.csv"));
            //dataGridView_device.DataSource = DBSet.ReadCSV(@"..\device.csv");

            //listUser = Common.ToDataList<user>(DBSet.ReadCSV(@"..\user.csv"));
            //dataGridViewUser.DataSource = DBSet.ReadCSV(@"..\user.csv");

            //listTool = Common.ToDataList<tool>(DBSet.ReadCSV(@"..\tool.csv"));
            //dataGridViewTool.DataSource = DBSet.ReadCSV(@"..\tool.csv");

        }
        public void configNew()
        {
            //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection nics = mc.GetInstances();
            //foreach (ManagementObject nic in nics)
            //{
            //    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
            //    {
                    //string mac = nic["MacAddress"].ToString();//Mac地址
                    //ip = (nic["IPAddress"] as String[])[0];//IP地址
                    //ipsubnet = (nic["IPSubnet"] as String[])[0];//子网掩码
                    //ipgateway = (nic["DefaultIPGateway"] as String[])[0];//默认网关
            //    }
            //}
            this.txtbox_IP.Text = "";
            this.txtbox_mask.Text = "";
            this.txtbox_gate.Text = "";

            listDevice = Common.ToDataList<device>(DBSet.ReadCSV(@"..\device.csv"));
            dataGridView_device.DataSource = DBSet.ReadCSV(@"..\device.csv");

            listUser = Common.ToDataList<user>(DBSet.ReadCSV(@"..\user.csv"));
            dataGridViewUser.DataSource = DBSet.ReadCSV(@"..\user.csv");

            listTool = Common.ToDataList<tool>(DBSet.ReadCSV(@"..\tool.csv"));
            dataGridViewTool.DataSource = DBSet.ReadCSV(@"..\tool.csv");
        }
        private void config_Load(object sender, EventArgs e)
        {
            configNew();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

        }
        //设备管理--新增
        #region 设备管理
        private void btn_add_device_Click(object sender, EventArgs e)
        {
            try
            {
                //表格新增
                //int index = this.dataGridView_device.Rows.Add();
                //this.dataGridView_device.Rows[index].Cells[1].Value = txtbox_IP.Text;
                //this.dataGridView_device.Rows[index].Cells[2].Value = txtbox_mask.Text;
                //this.dataGridView_device.Rows[index].Cells[3].Value = txtbox_gate.Text;
                //this.dataGridView_device.Rows[index].Cells[4].Value = cboModel.SelectedText;
                //this.dataGridView_device.Rows[index].Cells[5].Value = txtbox_name.Text;
                //this.dataGridView_device.Rows[index].Cells[6].Value = txtbox_type.Text;
                //this.dataGridView_device.Rows[index].Cells[7].Value = txtbox_size.Text;
                //this.dataGridView_device.Rows[index].Cells[8].Value = txtbox_Num.Text;
                //this.dataGridView_device.Rows[index].Cells[9].Value = txtbox_note.Text;
                //this.dataGridView_device.Refresh();
                //数据库新增
                device device = new device();
                device.IP = txtbox_IP.Text.Trim();
                device.mask = txtbox_mask.Text.Trim();
                device.gate = txtbox_gate.Text.Trim();
                device.model = cboModel.SelectedItem==null?"":cboModel.SelectedItem.ToString();
                device.name = txtbox_name.Text.Trim();
                device.type = txtbox_type.Text.Trim();
                device.size = txtbox_size.Text.Trim();
                device.Num = txtbox_Num.Text.Trim();
                device.note = txtbox_note.Text.Trim();
                if (check_device(device))
                {
                    //listDevice = new List<device>();
                    listDevice.Add(device);
                    DataTable dataTable = Common.FillDataTable<device>(listDevice);
                    this.dataGridView_device.DataSource = dataTable;
                    DBSet.dataTableToCsvT(dataTable, @"..\device.csv");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_del_device_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.dataGridView_device.Rows.Count; i++)
                {
                    if (dataGridView_device.Rows[i].Cells[0].Value != null)
                    {
                        if ((bool)dataGridView_device.Rows[i].Cells[0].Value)
                        {
                            listDevice.RemoveAt(i);
                        }
                    }
                }
                dataGridView_device.DataSource = Common.FillDataTable<device>(listDevice);
                DataTable dataTable = Common.FillDataTable<device>(listDevice);
                DBSet.dataTableToCsvT(dataTable, @"..\device.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_search_device_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView_device.DataSource = DBSet.ReadCSV(@"..\device.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_device_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //int num = this.dataGridView_device.SelectedCells[0].RowIndex;
                //this.txtbox_IP.Text = listDevice[num].IP;
                //this.txtbox_mask.Text = listDevice[num].mask;
                //this.txtbox_gate.Text = listDevice[num].gate;
                //this.cboModel.SelectedItem = listDevice[num].model;
                //this.txtbox_gate.Text = listDevice[num].gate;
                //this.txtbox_name.Text = listDevice[num].name;
                //this.txtbox_type.Text = listDevice[num].type;
                //this.txtbox_size.Text = listDevice[num].size;
                //this.txtbox_Num.Text = listDevice[num].Num;
                //this.txtbox_note.Text = listDevice[num].note;
                num = this.dataGridView_device.SelectedCells[0].RowIndex;
                deviceChange device = new deviceChange(this);
                device.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool check_device(device device)
        {
            if (string.IsNullOrEmpty(device.IP))
            {
                MessageBox.Show("IP地址不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.mask))
            {
                MessageBox.Show("子网掩码不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.gate))
            {
                MessageBox.Show("网关不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.name))
            {
                MessageBox.Show("名称不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.type))
            {
                MessageBox.Show("型号不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.Num))
            {
                MessageBox.Show("编号不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.size))
            {
                MessageBox.Show("大小不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(device.model))
            {
                MessageBox.Show("使用模式不能为空");
                return false;
            }
            return true;
        }
        #endregion

        #region 用户管理
        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //表格新增
                //int index = this.dataGridViewUser.Rows.Add();
                //this.dataGridViewUser.Rows[index].Cells[0].Value = txtUserName.Text;
                //this.dataGridViewUser.Rows[index].Cells[1].Value = txtUserPassword.Text;
                //this.dataGridViewUser.Rows[index].Cells[2].Value = txtUserIC.Text;
                //this.dataGridViewUser.Rows[index].Cells[3].Value = txtUserFace.Text;
                //this.dataGridViewUser.Refresh();
                //数据库新增
                user user = new user();
                user.name = txtUserName.Text.Trim();
                user.password = txtUserPassword.Text.Trim();
                user.IC = txtUserIC.Text.Trim();
                user.face = txtUserFace.Text.Trim();
                if (check_user(user))
                {
                    //List<user> listUser = new List<user>();
                    listUser.Add(user);
                    DataTable dataTable = Common.FillDataTable<user>(listUser);
                    this.dataGridViewUser.DataSource = dataTable;
                    DBSet.dataTableToCsvT(dataTable, @"..\user.csv");
                    model t = new model();
                    t.SetTrainedFaceRecognizer(model.FaceRecognizerType.LBPHFaceRecognizer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.dataGridViewUser.Rows.Count; i++)
                {
                    if (dataGridViewUser.Rows[i].Cells[0].Value != null)
                    {
                        
                        if ((bool)dataGridViewUser.Rows[i].Cells[0].Value)
                        {
                            if (dataGridViewUser.Rows[i].Cells[5].Value != null)
                            {
                                DirectoryInfo di = new DirectoryInfo("source//" + dataGridViewUser.Rows[i].Cells[5].Value + "//");
                                di.Delete(true);
                            }
                            listUser.RemoveAt(i);
                        }
                    }
                }
                DataTable dataTable = Common.FillDataTable<user>(listUser);
                dataGridViewUser.DataSource = dataTable;
                DBSet.dataTableToCsvT(dataTable, @"..\user.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUserSearch_Click(object sender, EventArgs e)
        {
            this.dataGridViewUser.DataSource = DBSet.ReadCSV(@"..\user.csv");

        }

        private void dataGridViewUser_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int num = this.dataGridViewUser.SelectedCells[0].RowIndex;
            //this.txtUserName.Text = listUser[num].admin;
            //this.txtUserPassword.Text = listUser[num].password;
            //this.txtUserIC.Text = listUser[num].IC;
            //this.txtUserFace.Text = listUser[num].face;
            num = this.dataGridViewUser.SelectedCells[0].RowIndex;
            userChange user = new userChange(this);
            user.Show();
        }

        public bool check_user(user user)
        {
            if (string.IsNullOrEmpty(user.name))
            {
                MessageBox.Show("用户名不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(user.password))
            {
                MessageBox.Show("密码不能为空");
                return false;
            }

            //if (string.IsNullOrEmpty(user.IC))
            //{
            //    MessageBox.Show("IC不能为空");
            //}
            //if (string.IsNullOrEmpty(user.face))
            //{
            //    MessageBox.Show("人脸登记不能为空");
            //}
            return true;
        }

        private void btn_UserFace_Click(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            user_ID = "user" + Convert.ToInt64(ts.TotalSeconds).ToString();
            addFace face = new addFace(user_ID);
            face.Show();
            txtUserFace.Text = user_ID;
        }
        #endregion

        #region 工具管理
        private void btnToolAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //
                //表格新增
                //int index = this.dataGridViewTool.Rows.Add();
                //this.dataGridViewTool.Rows[index].Cells[0].Value = txtToolNo.Text;
                //this.dataGridViewTool.Rows[index].Cells[1].Value = txtToolName.Text;
                //this.dataGridViewTool.Rows[index].Cells[2].Value = txtToolAddress.Text;
                //this.dataGridViewTool.Rows[index].Cells[3].Value = txtToolPic.Text;
                //this.dataGridViewTool.Refresh();
                //数据库新增
                tool tool = new tool();
                tool.No = txtToolNo.Text.Trim();
                tool.name = txtToolName.Text.Trim();
                tool.address = txtToolAddress.Text.Trim();
                tool.picture = txtToolPic.Text.Trim();
                tool.x = rectengle.Split(',')[0];
                tool.y = rectengle.Split(',')[1];
                tool.w = rectengle.Split(',')[2];
                tool.h = rectengle.Split(',')[3];

                if (check_tool(tool))
                {
                    //List<tool> listTool = new List<tool>();
                    listTool.Add(tool);
                    DataTable dataTable = Common.FillDataTable<tool>(listTool);
                    dataGridViewTool.DataSource = dataTable;
                    DBSet.dataTableToCsvT(dataTable, @"..\tool.csv");
                }
                rectengle = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnToolDel_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.dataGridViewTool.Rows.Count; i++)
                {
                    if (dataGridViewTool.Rows[i].Cells[0].Value != null)
                    {
                        if ((bool)dataGridViewTool.Rows[i].Cells[0].Value)
                        {
                            if (dataGridViewTool.Rows[i].Cells[4].Value != null)
                            {
                                DirectoryInfo di = new DirectoryInfo("tools//" + dataGridViewTool.Rows[i].Cells[4].Value + "//");
                                di.Delete(true);
                            }
                            listTool.RemoveAt(i);
                        }
                    }
                }

                DataTable dataTable = Common.FillDataTable<tool>(listTool);
                dataGridViewTool.DataSource = dataTable;
                DBSet.dataTableToCsvT(dataTable, @"..\tool.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnToolSearch_Click(object sender, EventArgs e)
        {
            this.dataGridViewTool.DataSource = DBSet.ReadCSV(@"..\tool.csv");
        }

        private void dataGridViewTool_CellContentDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int num = this.dataGridViewTool.SelectedCells[0].RowIndex;
            //this.txtToolNo.Text = listTool[num].No;
            //this.txtToolName.Text = listTool[num].name;
            //this.txtToolAddress.Text = listTool[num].address;
            //this.txtToolPic.Text = listTool[num].picture;
            num = this.dataGridViewTool.SelectedCells[0].RowIndex;
            toolChange tool = new toolChange(this);
            tool.Show();
        }

        public bool check_tool(tool tool)
        {
            if (string.IsNullOrEmpty(tool.No))
            {
                MessageBox.Show("编号不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(tool.name))
            {
                MessageBox.Show("物品名称不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(tool.address))
            {
                MessageBox.Show("存储位置不能为空");
                return false;
            }
            //if (string.IsNullOrEmpty(tool.picture))
            //{
            //    MessageBox.Show("物品图片不能为空");
            //}
            return true;
        }

        #endregion

        private void btn_tool_add_Click(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970,1,1,0,0,0,0);
            tool_ID = "tool" + Convert.ToInt64(ts.TotalSeconds).ToString();
            txtToolPic.Text = tool_ID;
            ROIconfig roi = new ROIconfig(tool_ID);
            roi.setToolTextValue += new setToolTextValue(_setToolTextValue);
            roi.Show();
        }

        public void _setToolTextValue(string textValue)
        {
            rectengle = textValue;
        }

        private void btn_addIC_Click(object sender, EventArgs e)
        {
            addIC ic = new addIC();
            ic.setICTextValue += new setICTextValue(_setICTextValue);
            ic.Show();
        }
        public void _setICTextValue(string textValue)
        {
            txtUserIC.Text = textValue;
        }

        private void config_FormClosed(object sender, FormClosedEventArgs e)
        {
            setControlValue(false);
        }
    }
}
