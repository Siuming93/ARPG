using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Presenter.Start;
using Assets.Scripts.UIPlugin;
using ExitGames.Client.Photon;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 登录的请求发送和响应处理器
    /// </summary>
    public class LoginServerController : ServerControllerBase
    {
        protected override OperationCode OpCode
        {
            get { return OperationCode.Login; }
        }

        public void LoginRequest(string username, string password)
        {
            //1.初始化参数
            var parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameters, ParameterCode.UserCheckInfo,
                new User {Username = username, Password = password});
            //2.发送登录请求与帐号密码
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Login, parameters);
        }

        public override void OnOperationResponse(OperationResponse response)
        {
            //获得响应的数据
            switch (response.ReturnCode)
            {
                case (short) ReturnCode.Success:
                    //返回开始
                    StartMenuController.Instance.OnLoginSuccess();
                    break;

                case (short) ReturnCode.Fail:
                    //登录失败,提示
                    MessageUiManger.Instance.SetMessage(response.DebugMessage);
                    break;
            }
        }
    }
}