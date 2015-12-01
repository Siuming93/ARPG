using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Presenter.Start;
using Assets.Scripts.View.Start;
using LitJson;
using UnityEngine;

namespace Assets.Scripts.Model.Photon.Controller
{
    public class ServerListServerController : ServerControllerBase
    {
        public override void Start()
        {
            base.Start();
            PhotonEngine.Instance.OnConnectToServer += GetServerListRequest;
            GetServerListRequest();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
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
            var serverList = ParameterTool.GetParameter<List<ServerProperty>>(response.Parameters,
                ParameterCode.ServerList);

            //2.初始化服务器列表
            OnGetServerList(serverList);
        }

        protected override OperationCode OpCode
        {
            get { return OperationCode.GetServer; }
        }

        public event OnGetServerListEvent OnGetServerList;
    }
}