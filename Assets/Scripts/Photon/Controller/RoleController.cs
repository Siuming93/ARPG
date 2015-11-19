using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ExitGames.Client.Photon;
using LitJson;

public class RoleController : ControllerBase
{
    protected override OperationCode OpCode
    {
        get { return OperationCode.Role; }
    }

    public void GetRole()
    {
        var parameter = new Dictionary<byte, object> {{(byte) ParameterCode.SubCode, SubCode.GetRole}};

        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    public void AddRole(Role role)
    {
        var parameter = new Dictionary<byte, object>
        {
            {(byte) ParameterCode.SubCode, SubCode.AddRole},
            {(byte) ParameterCode.Role, JsonMapper.ToJson(role)}
        };
        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    /// <summary>
    /// 选择进入游戏之后的角色
    /// </summary>
    /// <param name="role"></param>
    public void SelectRole(Role role)
    {
        var parameter = new Dictionary<byte, object>
        {
            {(byte) ParameterCode.SubCode, SubCode.SelectRole},
            {(byte) ParameterCode.Role, JsonMapper.ToJson(role)}
        };
        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        var subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);
        switch (subCode)
        {
            case SubCode.GetRole:
                var roleListDb = ParameterTool.GetParameter<List<Role>>(response.Parameters, ParameterCode.RoleList);
                OnGetRole(roleListDb);
                break;
            case SubCode.AddRole:
                switch (response.ReturnCode)
                {
                    case (short) ReturnCode.Success:
                        var role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                        OnAddRole(role);
                        break;
                    case (short) ReturnCode.Fail:
                        MessageManger.Instance.SetMessage(response.DebugMessage);
                        break;
                }

                break;
            case SubCode.SelectRole:
                if (OnSelectRole != null)
                    OnSelectRole();
                break;
        }
    }

    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvent OnAddRole;
    public event OnSelectRoleEvent OnSelectRole;
}