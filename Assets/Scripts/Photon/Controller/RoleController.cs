using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;

public class RoleController : ControllerBase
{
    protected override ARPGCommon.OperationCode OpCode
    {
        get { return OperationCode.Role; }
    }

    public void GetRole()
    {
        var parameter = new Dictionary<byte, object> { { (byte)ParameterCode.SubCode, SubCode.GetRole } };

        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    public void AddRole()
    {
        var parameter = new Dictionary<byte, object> { { (byte)ParameterCode.SubCode, SubCode.AddRole } };

        PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        var subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode);
        switch (subCode)
        {
            case SubCode.AddRole:
                var roleListDb = ParameterTool.GetParameter<List<Role>>(response.Parameters, ParameterCode.RoleList);
                OnGetRole(roleListDb);
                break;
            case SubCode.GetRole:
                var role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                OnAddRole(role);
                break;
        }
    }

    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvent OnAddRole;
}