using System;
using System.Collections.Generic;
using ARPGCommon;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public static PhotonEngine Instance { get; private set; }

    public ConnectionProtocol Protocol;
    public string ServerAddress = "127.0.0.1:4530";
    public string ServerApplicationName = "ARPGServer";

    public delegate void OnConnectToServerEvent();

    public event OnConnectToServerEvent OnConnectToServer;

    public bool IsConnected;
    private PhotonPeer _peer;
    private readonly Dictionary<byte, ControllerBase> _controllers = new Dictionary<byte, ControllerBase>();

    /// <summary>
    /// 每个Controller需要注册之后才能被PhotonEngine调用.
    /// </summary>
    /// <param name="opCode"></param>
    /// <param name="controller"></param>
    public void RegisterController(OperationCode opCode, ControllerBase controller)
    {
        _controllers.Add((byte) opCode, controller);
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
        //throw new NotImplementedException();
    }

    public void OnEvent(EventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase controller;
        print("OnOpertaionResponse,OperationCode:" + operationResponse.OperationCode);
        if (_controllers.TryGetValue(operationResponse.OperationCode, out controller))
        {
            controller.OnOperationResponse(operationResponse);
        }
        else
        {
            print("Unknow OperationCode:" + operationResponse.OperationCode);
        }
    }

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

    private void Awake()
    {
        Instance = this;

        _peer = new PhotonPeer(this, Protocol);
        _peer.Connect(ServerAddress, ServerApplicationName);

        //while (!_isConnected)
        //{
        //    _peer.Service();
        //    Debug.Log("connected.");
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        _peer.Service();
    }
}