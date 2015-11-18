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

    public void AddRole()
    {
        var parameter = new Dictionary<byte, object> {{(byte) ParameterCode.SubCode, SubCode.AddRole}};

        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        var subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode);
        switch (subCode)
        {
            case SubCode.GetRole:
                var json = ParameterTool.GetParameter<object>(response.Parameters, ParameterCode.RoleList);
                var roleListDb = JsonMapper.ToObject<List<Role>>(json.ToString());
                OnGetRole(roleListDb);
                break;
            case SubCode.AddRole:
                var role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                OnAddRole(role);
                break;
        }
    }

    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvent OnAddRole;
}