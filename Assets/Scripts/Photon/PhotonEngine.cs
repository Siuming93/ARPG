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

    private bool _isConnected;
    private PhotonPeer _peer;
    private readonly Dictionary<byte, ControllerBase> _controllers = new Dictionary<byte, ControllerBase>();

    public void RegisterController(OperationCode opCode, ControllerBase controller)
    {
        _controllers.Add((byte) opCode, controller);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new NotImplementedException();
    }

    public void OnEvent(EventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        ControllerBase controller;
        if (_controllers.TryGetValue((byte) operationResponse.OperationCode, out controller))
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
                _isConnected = true;
                break;
        }
    }

    private void Awake()
    {
        Instance = this;

        _peer = new PhotonPeer(this, Protocol);
        _peer.Connect(ServerAddress, ServerApplicationName);

        while (!_isConnected)
        {
            _peer.Service();
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}