using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ExitGames.Client.Photon;
using LitJson;

namespace Assets.Scripts.Model.Photon.Controller
{
    public class RoleServerController : ServerControllerBase
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

        public void UpdateRole(Role role)
        {
            var parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameters, ParameterCode.Role, role);
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Role, SubCode.UpdateRole, parameters);
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
            switch (response.ReturnCode)
            {
                case (short) ReturnCode.Success:
                    var subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);
                    switch (subCode)
                    {
                        case SubCode.GetRole:
                            var roleListDb = ParameterTool.GetParameter<List<Role>>(response.Parameters,
                                ParameterCode.RoleList);
                            if (OnGetRole != null) OnGetRole(roleListDb);
                            break;
                        case SubCode.AddRole:
                            var role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                            if (OnAddRole != null) OnAddRole(role);
                            break;


                            break;
                        case SubCode.SelectRole:
                            role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                            PhotonEngine.Instance.SetCurRole(role);
                            if (OnSelectRole != null)
                                OnSelectRole();
                            break;
                        case SubCode.UpdateRole:
                            role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                            PhotonEngine.Instance.SetCurRole(role);
                            if (OnUpdateRole != null)
                            {
                                OnUpdateRole();
                            }
                            break;
                    }
                    break;
                case (short) ReturnCode.Fail:
                    MessageManger.Instance.SetMessage(response.DebugMessage);
                    break;
            }
        }

        public event OnGetRoleEvent OnGetRole;
        public event OnAddRoleEvent OnAddRole;
        public event OnSelectRoleEvent OnSelectRole;
        public event OnUpdateRoleEvent OnUpdateRole;
    }
}