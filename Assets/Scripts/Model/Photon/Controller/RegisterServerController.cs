using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Presenter.Start;
using Assets.Scripts.UIPlugin;
using ExitGames.Client.Photon;
using LitJson;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 注册操作处理器
    /// </summary>
    public class RegisterServerController : ServerControllerBase
    {
        protected override OperationCode OpCode
        {
            get { return OperationCode.Register; }
        }

        public void SendRegisterRequest(string username, string password)
        {
            //1.初始化参数
            var json = JsonMapper.ToJson(new User {Username = username, Password = password});
            var parameters = new Dictionary<byte, object> {{(byte) ParameterCode.UserCheckInfo, json}};

            //2.通过PhotonEngine发送
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Register, parameters);
        }

        public override void OnOperationResponse(OperationResponse response)
        {
            switch (response.ReturnCode)
            {
                case (short) ReturnCode.Success:
                    //1.注册成功后直接登录
                    StartMenuController.Instance.OnRegisterSuccess();
                    break;
                case (short) ReturnCode.Fail:
                    //2.提示
                    MessageUiManger.Instance.SetMessage(response.DebugMessage);
                    break;
            }
        }
    }
}