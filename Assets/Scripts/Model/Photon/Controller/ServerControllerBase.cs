using ARPGCommon;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 所有服务器处理器的基类
    /// </summary>
    public abstract class ServerControllerBase : MonoBehaviour
    {
        /// <summary>
        /// 当前处理器的所处理的操作代码
        /// </summary>
        protected abstract OperationCode OpCode { get; }

        /// <summary>
        /// 处理服务器响应
        /// </summary>
        /// <param name="response"></param>
        public abstract void OnOperationResponse(OperationResponse response);

        /// <summary>
        /// 在PeerListener中注册
        /// </summary>
        public virtual void Start()
        {
            PhotonEngine.Instance.RegisterController(OpCode, this);
        }

        /// <summary>
        ///  在PeerListener中取消注册
        /// </summary>
        public virtual void OnDestroy()
        {
            PhotonEngine.Instance.UnRegisterController(OpCode);
        }
    }
}