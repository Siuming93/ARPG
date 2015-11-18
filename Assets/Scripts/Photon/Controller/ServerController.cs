using System.Collections.Generic;
using ARPGCommon;
using UnityEngine;
using LitJson;

public class ServerController : ControllerBase
{
    public GameObject ServerItem;
    public Transform ServerGridVerticalCell1;
    public Transform ServerGridVerticalCell2;

    public override void Start()
    {
        base.Start();
        PhotonEngine.Instance.OnConnectToServer += GetServerListRequest;
        GetServerListRequest();
    }

    public override void OnDestory()
    {
        base.OnDestory();
        PhotonEngine.Instance.OnConnectToServer -= GetServerListRequest;
    }

    /// <summary>
    /// 发送获得服务器列表的请求
    /// </summary>
    public void GetServerListRequest()
    {
        PhotonEngine.Instance.SendOperationRequest(OperationCode.GetServer, new Dictionary<byte, object>());
    }

    /// <summary>
    /// 响应服务器发送回来的获得服务器响应
    /// </summary>
    /// <param name="response"></param>
    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        //1.取得返回的json
        object json;
        List<ARPGCommon.Model.ServerProperty> serverList = null;
        if (response.Parameters.TryGetValue((byte) ParameterCode.ServerList, out json))
        {
            serverList = JsonMapper.ToObject<List<ARPGCommon.Model.ServerProperty>>(json.ToString());
        }

        //2.初始化服务器列表
        for (var i = 0; i < serverList.Count; i++)
        {
            var ip = serverList[i].Ip;
            var serverName = serverList[i].Name;
            var count = serverList[i].Count;
            var curServerItem = GameObject.Instantiate(ServerItem) as GameObject;
            curServerItem.transform.parent = i%2 == 0 ? ServerGridVerticalCell1 : ServerGridVerticalCell2;

            //设置名称和ip
            var sp = curServerItem.GetComponent<ServerUiProperty>();
            sp.Set(ip, serverName, count);
            StartMenuController.Instance.OnServerButtonClick(sp);
        }
    }

    protected override OperationCode OpCode
    {
        get { return OperationCode.GetServer; }
    }
}