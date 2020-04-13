using RemoveDetectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace RDService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service1 : IService1
    {
        
        #region 登录
        /*--------------登录-----------*/
        public CommonOutputT<user> logon(string name, string password, string IC, string face)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutputT<user> returnData = new CommonOutputT<user>();
            loginLog log = new loginLog();
            if (!String.IsNullOrEmpty(IC))
            {
                IQueryable<user> x = from a in rd.user
                                     where IC == a.IC
                                     select a;
                if (x.Count() != 0)
                {

                    log.id = Guid.NewGuid().ToString();
                    log.isOK = "success";
                    log.time = DateTime.Now.ToString("");
                    log.userID = x.FirstOrDefault().id;

                    rd.loginLog.InsertOnSubmit(log);
                    rd.SubmitChanges();
                    returnData.success = true;
                    returnData.message = "success";
                    returnData.data = x.FirstOrDefault();
                    rd.Dispose(); return returnData;
                }
                return returnData;
            }
            if (!String.IsNullOrEmpty(face))
            {
                IQueryable<user> x = from a in rd.user
                                     where face == a.face
                                     select a;
                if (x.Count() != 0)
                {
                    log.id = Guid.NewGuid().ToString();
                    log.isOK = "success";
                    log.time = DateTime.Now.ToString("");
                    log.userID = x.FirstOrDefault().id;
                    rd.loginLog.InsertOnSubmit(log);
                    rd.SubmitChanges();
                    returnData.success = true;
                    returnData.message = "success";
                    returnData.data = x.FirstOrDefault();
                    rd.Dispose(); return returnData;
                }
                return returnData;
            }
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(password))
            {
                IQueryable<user> x = from a in rd.user
                                     where (name == a.name && password == a.password)
                                     select a;
                if (x.Count() != 0)
                {
                    log.id = Guid.NewGuid().ToString();
                    log.isOK = "success";
                    log.time = DateTime.Now.ToString("");
                    log.userID = x.FirstOrDefault().id;
                    rd.loginLog.InsertOnSubmit(log);
                    rd.SubmitChanges();
                    returnData.success = true;
                    returnData.message = "success";
                    returnData.data = x.FirstOrDefault();
                    rd.Dispose(); return returnData;
                }
                return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "账号为空，请重新登录！";
                rd.Dispose(); return returnData;
            }
        }

        /*--------------登录日志-----------*/


        #endregion

        #region 设备管理

        /*--------------添加-----------*/
        public CommonOutput addDevice(string IP, string name, string mask, string gate, string type, string size, string Num, string model, string note, string layer, string seats, string limitTIme, string hostName, string IPVersion, string DNS)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if ((!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(Num)) || !string.IsNullOrEmpty(size))
            {
                device x = new device();

                x.id = Guid.NewGuid().ToString();
                x.IP = IP;
                x.name = name;
                x.mask = mask;
                x.gate = gate;
                x.type = type;
                x.size = size;
                x.Num = Num;
                x.model = model;
                x.note = note;
                x.layer = layer;
                x.seats = seats;
                x.limitTIme = limitTIme;
                x.hostName = hostName;
                x.IPVersion = IPVersion;
                x.DNS = DNS;
                rd.device.InsertOnSubmit(x);
                rd.SubmitChanges();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "用户信息不全！";
                rd.Dispose(); return returnData;
            }
        }

        /*--------------编辑-----------*/
        public CommonOutput editDevice(string id, string IP, string name, string mask, string gate, string type, string size, string Num, string model, string note, string layer, string seats, string limitTIme, string hostName, string IPVersion, string DNS)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if ((!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(Num)) || !string.IsNullOrEmpty(size))
            {
                var x = rd.device.SingleOrDefault(c => c.id == id);
                x.IP = IP;
                x.name = name;
                x.mask = mask;
                x.gate = gate;
                x.type = type;
                x.size = size;
                x.Num = Num;
                x.model = model;
                x.note = note;
                x.layer = layer;
                x.seats = seats;
                x.limitTIme = limitTIme;
                x.hostName = hostName;
                x.IPVersion = IPVersion;
                x.DNS = DNS;
                rd.SubmitChanges();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "用户信息不全！";
                rd.Dispose(); return returnData;
            }
        }
        /*--------------显示-----------*/
        public PageRows<device[]> getDeviceList()
        {
            RDDataContext rd = new RDDataContext();
            PageRows<device[]> returnData = new PageRows<device[]>();
            var temp = from a in rd.device
                       select a;
            returnData.message = "success";
            returnData.success = true;
            returnData.total = temp.Count();
            returnData.rows = temp.ToArray();
            rd.Dispose(); return returnData;
        }

        /*--------------添加-----------*/
        public CommonOutput addToolsPosition(string layer, string seat, string isok)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if ((!string.IsNullOrEmpty(layer) && !string.IsNullOrEmpty(seat)) || !string.IsNullOrEmpty(isok))
            {
                deviceState x = new deviceState();

                x.id = Guid.NewGuid().ToString();
                x.layerNum = layer;
                x.seatNum = seat;
                x.isok = isok;
                rd.deviceState.InsertOnSubmit(x);
                rd.SubmitChanges();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "用户信息不全！";
                rd.Dispose(); return returnData;
            }
        }


        public string searchTime(string deviceID)
        {
            RDDataContext rd = new RDDataContext();
            if (!string.IsNullOrEmpty(deviceID))
            {
                var x = rd.device.SingleOrDefault(a => a.id == deviceID);
                return x.limitTIme;
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region 设备取放&盘点

        #endregion

        #region 人员管理

        /*--------------添加用户-----------*/
        public CommonOutput addUser(string name, string password, string IC, string face, string phone, string number, string groupNum, string department)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if ((!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password)) || !string.IsNullOrEmpty(password))
            {
                user x = new user();

                x.id = Guid.NewGuid().ToString();
                x.department = department;
                x.groupNum = groupNum;
                x.number = number;
                x.phone = phone;
                x.face = face;
                x.IC = IC;
                x.password = password;
                x.name = name;
                rd.user.InsertOnSubmit(x);
                rd.SubmitChanges();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "用户信息不全！";
                rd.Dispose(); return returnData;
            }
        }

        /*--------------人员查找-----------*/
        public PageRows<user[]> searchUsers(string name, string department)
        {
            RDDataContext rd = new RDDataContext();
            PageRows<user[]> returnData = new PageRows<user[]>();

            if (name != "" && department != "")
            {
                var temp = from a in rd.user
                           where a.name.Contains(name) && a.department.Contains(department)
                           select a;
                returnData.message = "success";
                returnData.success = true;
                returnData.total = temp.Count();
                returnData.rows = temp.ToArray();
                rd.Dispose(); return returnData;
            }
            if (name == "" && department != "")
            {
                var temp = from a in rd.user
                           where a.department.Contains(department)
                           select a;
                returnData.message = "success";
                returnData.success = true;
                returnData.total = temp.Count();
                returnData.rows = temp.ToArray();
                rd.Dispose(); return returnData;
            }
            if (name == "" && department == "")
            {
                var temp = from a in rd.user
                           select a;
                returnData.message = "success";
                returnData.success = true;
                returnData.total = temp.Count();
                returnData.rows = temp.ToArray();
                rd.Dispose(); return returnData;
            }
            if (name != "" && department == "")
            {
                var temp = from a in rd.user
                           where a.name.Contains(name)
                           select a;
                returnData.message = "success";
                returnData.success = true;
                returnData.total = temp.Count();
                returnData.rows = temp.ToArray();
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "筛选信息缺失！";
                rd.Dispose(); return returnData;
            }

        }

        /*--------------显示人员列表-----------*/
        public PageRows<user[]> getUserList()
        {
            RDDataContext rd = new RDDataContext();
            PageRows<user[]> returnData = new PageRows<user[]>();
            var temp = from a in rd.user
                       select a;
            returnData.message = "success";
            returnData.success = true;
            returnData.total = temp.Count();
            returnData.rows = temp.ToArray();
            rd.Dispose(); return returnData;
        }
        /*--------------编辑人员信息-----------*/
        public CommonOutput editUser(string id, string name, string password, string IC, string face, string phone, string number, string groupNum, string department)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if (!string.IsNullOrEmpty(name))
            {
                var x = rd.user.SingleOrDefault(c => c.id == id);
                x.department = department;
                x.groupNum = groupNum;
                x.number = number;
                x.phone = phone;
                x.face = face;
                x.IC = IC;
                x.password = password;
                x.name = name;
                rd.SubmitChanges();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                returnData.success = false;
                returnData.message = "字典类型不能为空";
                rd.Dispose(); return returnData;
            }
        }
        /*--------------删除人员-----------*/
        public CommonOutput deleteUser(string id)
        {
            RDDataContext rd = new RDDataContext();
            CommonOutput returnData = new CommonOutput();
            if (!string.IsNullOrEmpty(id))
            {
                var thisguy = rd.user.SingleOrDefault(c => c.id == id);
                if (thisguy != null)
                {
                    rd.user.DeleteOnSubmit(thisguy);
                }
                rd.SubmitChanges();
                rd.Dispose();
                returnData.success = true;
                returnData.message = "success";
                rd.Dispose(); return returnData;
            }
            else
            {
                rd.Dispose();
                returnData.success = false;
                returnData.message = "未选中信息";
                rd.Dispose(); return returnData;
            }
        }
        /*--------------权限管理-----------*/
        #endregion
    }
}
