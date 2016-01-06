/*与服务器相连的PeerListener*/

using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Model.Photon.Controller;
using Assets.Scripts.UIPlugin;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Assets.Scripts.Model.Photon
{
    public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
    {
        //实例
        public static PhotonEngine Instance { get; private set; }

        //连接配置
        public ConnectionProtocol Protocol = ConnectionProtocol.Tcp;
        public string ServerAddress = "127.0.0.1:4530";
        public string ServerApplicationName = "ARPGServer";
        //连接事件
        public delegate void OnConnectToServerEvent();

        public event OnConnectToServerEvent OnConnectToServer;
        //连接状态
        public bool IsConnected;
        //连接peer
        private PhotonPeer _peer;
        //控制器的存储
        private readonly Dictionary<byte, ServerControllerBase> _controllers =
            new Dictionary<byte, ServerControllerBase>();

        //选中的角色
        public Role CurRole { get; private set; }

        /// <summary>
        /// 设置当前的Role
        /// </summary>
        /// <param name="role"></param>
        public void SetCurRole(Role role)
        {
            CurRole = role;
        }

        /// <summary>
        /// 每个Controller需要注册之后才能被PhotonEngine调用.
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="serverController"></param>
        public void RegisterController(OperationCode opCode, ServerControllerBase serverController)
        {
            _controllers.Add((byte) opCode, serverController);
        }

        /// <summary>
        /// 取消Controller的注册
        /// </summary>
        /// <param name="opCode"></param>
        public void UnRegisterController(OperationCode opCode)
        {
            _controllers.Remove((byte) opCode);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            print("DebugReturn:" + message);
        }

        public void OnEvent(EventData eventData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 当服务器响应了请求
        /// </summary>
        /// <param name="operationResponse"></param>
        public void OnOperationResponse(OperationResponse operationResponse)
        {
            //根据响应的Code找到相应的Controller,并将请求数据转送
            ServerControllerBase serverController;
            print("OnOpertaionResponse,OperationCode:" + (OperationCode) operationResponse.OperationCode);
            if (_controllers.TryGetValue(operationResponse.OperationCode, out serverController))
            {
                serverController.OnOperationResponse(operationResponse);
            }
            else
            {
                print("Unknow OperationCode:" + operationResponse.OperationCode);
            }
        }

        /// <summary>
        /// 当连接状态发生更改
        /// </summary>
        /// <param name="statusCode"></param>
        public void OnStatusChanged(StatusCode statusCode)
        {
            print("OnStatusChanged:" + statusCode);
            switch (statusCode)
            {
                case StatusCode.Connect:
                    IsConnected = true;
                    if (OnConnectToServer != null) OnConnectToServer();
                    break;
            }
        }

        /// <summary>
        /// 向服务器发送操作请求
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="parameters"></param>
        public void SendOperationRequest(OperationCode opCode, Dictionary<byte, object> parameters)
        {
            print("send request to server,OperationCode:" + opCode);
            _peer.OpCustom((byte) opCode, parameters, true);
        }

        /// <summary>
        /// 发送带子操作的请求.
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="subCode"></param>
        /// <param name="parameters"></param>
        public void SendOperationRequest(OperationCode opCode, SubCode subCode, Dictionary<byte, object> parameters)
        {
            MessageUiManger.Instance.Print("OpCOde:" + opCode + "SubCode:" + subCode);

            print("send request to server,OperationCode:" + opCode + ",SubCode:" + subCode);
            ParameterTool.AddParameter(parameters, ParameterCode.SubCode, subCode, false);
            _peer.OpCustom((byte) opCode, parameters, true);
        }

        /// <summary>
        /// 构造函数,构造时就进行连接
        /// </summary>
        private void Awake()
        {
            Instance = this;
            _peer = new PhotonPeer(this, Protocol);
            _peer.Connect(ServerAddress, ServerApplicationName);

            //调试用的默认角色
            CurRole = new Role {Name = "siuming", Isman = false, Level = 10};
            //while (!IsConnected)
            //{
            //    _peer.Service();
            //    //Debug.Log("connected.");
            //}
        }

        // Update is called once per frame
        public void Update()
        {
            _peer.Service();
        }

        /// <summary>
        /// 将信息发送的到控制台
        /// </summary>
        /// <param name="message"></param>
        private void print(string message)
        {
            MonoBehaviour.print(message);
        }
    }
}