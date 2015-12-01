using ARPGCommon;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Assets.Scripts.Model.Photon.Controller
{
    public abstract class ServerControllerBase : MonoBehaviour
    {
        protected abstract OperationCode OpCode { get; }

        public abstract void OnOperationResponse(OperationResponse response);

        public virtual void Start()
        {
            PhotonEngine.Instance.RegisterController(OpCode, this);
        }


        public virtual void OnDestroy()
        {
            PhotonEngine.Instance.UnRegisterController(OpCode);
        }
    }
}