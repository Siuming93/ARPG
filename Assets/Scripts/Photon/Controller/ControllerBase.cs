using ARPGCommon;
using UnityEngine;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour
{
    protected abstract OperationCode OpCode { get; }

    public abstract void OnOperationResponse(OperationResponse response);

    public virtual void Start()
    {
        PhotonEngine.Instance.RegisterController(OpCode, this);
    }

    public virtual void OnDestory()
    {
        PhotonEngine.Instance.UnRegisterController(OpCode);
    }
}