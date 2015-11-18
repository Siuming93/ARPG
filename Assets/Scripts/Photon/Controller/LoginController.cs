using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ExitGames.Client.Photon;
using LitJson;

public class LoginController : ControllerBase
{
    protected override OperationCode OpCode
    {
        get { return OperationCode.Login; }
    }

    public void LoginRequest(string username, string password)
    {
        //1.初始化参数
        var json = JsonMapper.ToJson(new User {Username = username, Password = password});
        var parameters = new Dictionary<byte, object> {{(byte) ParameterCode.UserCheckInfo, json}};

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
                MessageManger.Instance.SetMessage(response.DebugMessage);
                break;
        }
    }
}