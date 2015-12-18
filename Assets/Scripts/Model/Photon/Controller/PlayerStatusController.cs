using ARPGCommon;
using ExitGames.Client.Photon;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 角色信息的请求发送和响应处理器
    /// </summary>
    public class PlayerStatusServerController : ServerControllerBase
    {
        protected override OperationCode OpCode
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void OnOperationResponse(OperationResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}