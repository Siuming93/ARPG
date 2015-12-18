using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Common;
using Assets.Scripts.UIPlugin;
using ExitGames.Client.Photon;
using LitJson;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 角色信息处理器
    /// </summary>
    public class RoleServerController : ServerControllerBase
    {
        protected override OperationCode OpCode
        {
            get { return OperationCode.Role; }
        }

        /// <summary>
        /// 请求得到用户的所有角色
        /// </summary>
        public void GetRole()
        {
            var parameter = new Dictionary<byte, object> {{(byte) ParameterCode.SubCode, SubCode.GetRole}};

            PhotonEngine.Instance.SendOperationRequest(OpCode, parameter);
        }

        /// <summary>
        /// 为当前用户增添角色
        /// </summary>
        /// <param name="role"></param>
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
        /// 更新当前role的信息
        /// </summary>
        /// <param name="role"></param>
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
                            //获得角色列表成功
                            var roleListDb = ParameterTool.GetParameter<List<Role>>(response.Parameters,
                                ParameterCode.RoleList);
                            if (OnGetRole != null) OnGetRole(roleListDb);
                            break;
                        case SubCode.AddRole:
                            //增加角色成功
                            var role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                            if (OnAddRole != null) OnAddRole(role);
                            break;
                        case SubCode.SelectRole:
                            //选择角色成功
                            role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.Role);
                            PhotonEngine.Instance.SetCurRole(role);
                            if (OnSelectRole != null)
                                OnSelectRole();
                            break;
                        case SubCode.UpdateRole:
                            //更新角色信息成功
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
                    MessageUiManger.Instance.SetMessage(response.DebugMessage);
                    break;
            }
        }

        /// <summary>
        /// 获得了当前用户的Role List
        /// </summary>
        public event OnGetRoleEvent OnGetRole;

        /// <summary>
        /// 为当前用户增添Role
        /// </summary>
        public event OnAddRoleEvent OnAddRole;

        /// <summary>
        /// 在服务器中选中了当前Role
        /// </summary>
        public event OnSelectRoleEvent OnSelectRole;

        /// <summary>
        /// 在服务器中更新了role信息
        /// </summary>
        public event OnUpdateRoleEvent OnUpdateRole;
    }
}