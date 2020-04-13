using RemoveDetectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RDService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "logon?name={name}&password={password}&IC={IC}&face={face}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutputT<user> logon(string name, string password, string IC, string face);

        /*--------------用户编辑-----------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "editUser?id={id}&name={name}&password={password}&IC={IC}&face={face}&phone={phone}&number={number}&groupNum={groupNum}&department={department}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput editUser(string id, string name, string password, string IC, string face, string phone, string number, string groupNum, string department);

        /*--------------新增用户-----------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "addUser?name={name}&password={password}&IC={IC}&face={face}&phone={phone}&number={number}&groupNum={groupNum}&department={department}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput addUser(string name, string password, string IC, string face, string phone, string number, string groupNum, string department);

        /*-------------获取列表------------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getUserList?", ResponseFormat = WebMessageFormat.Json)]
        PageRows<user[]> getUserList();
        /*-------------获取列表------------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "searchUsers?name={name}&department={department}", ResponseFormat = WebMessageFormat.Json)]
        PageRows<user[]> searchUsers(string name, string department);
        /*-------------删除用户信息------------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "deleteUser?id={id}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput deleteUser(string id);



        /*--------------新增用户-----------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "addDevice?IP={IP}&name={name}&mask={mask}&gate={gate}&type={type}&size={size}&Num={Num}&model={model}&note={note}&layer={layer}&seats={seats}&limitTIme={limitTIme}&hostName={hostName}&IPVersion={IPVersion}&DNS={DNS}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput addDevice(string IP, string name, string mask, string gate, string type, string size, string Num, string model, string note, string layer, string seats, string limitTIme, string hostName, string IPVersion, string DNS);

        /*--------------用户编辑-----------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "editDevice?id={id}&IP={IP}&name={name}&mask={mask}&gate={gate}&type={type}&size={size}&Num={Num}&model={model}&note={note}&layer={layer}&seats={seats}&limitTIme={limitTIme}&hostName={hostName}&IPVersion={IPVersion}&DNS={DNS}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput editDevice(string id, string IP, string name, string mask, string gate, string type, string size, string Num, string model, string note, string layer, string seats, string limitTIme, string hostName, string IPVersion, string DNS);


        /*-------------获取列表------------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getDeviceList?", ResponseFormat = WebMessageFormat.Json)]
        PageRows<device[]> getDeviceList();

        /*--------------新增用户-----------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "addToolsPosition?layer={layer}&seat={seat}&isok={isok}", ResponseFormat = WebMessageFormat.Json)]
        CommonOutput addToolsPosition(string layer, string seat, string isok);

        /*-------------获取列表------------*/
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "searchTime?deviceID={deviceID}", ResponseFormat = WebMessageFormat.Json)]
        string searchTime(string deviceID);

    }
}
