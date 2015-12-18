using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Common;
using ExitGames.Client.Photon;

namespace Assets.Scripts.Model.Photon.Controller
{
    /// <summary>
    /// 任务信息的处理器
    /// </summary>
    public class TaskDbServerController : ServerControllerBase
    {
        protected override OperationCode OpCode
        {
            get { return OperationCode.Task; }
        }

        /// <summary>
        /// 请求任务列表
        /// </summary>
        public void GetTaskDbList()
        {
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Task, SubCode.GetTask,
                new Dictionary<byte, object>());
        }

        /// <summary>
        /// 请求增加任务
        /// </summary>
        /// <param name="taskDb"></param>
        public void AddTaskDb(TaskDb taskDb)
        {
            var parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDb);
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Task, SubCode.AddTask, parameters);
        }

        /// <summary>
        /// 请求更新任务信息
        /// </summary>
        /// <param name="taskDb"></param>
        public void UpdateTaskDb(TaskDb taskDb)
        {
            var parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDb);
            PhotonEngine.Instance.SendOperationRequest(OperationCode.Task, SubCode.UpdateTask, parameters);
        }

        public override void OnOperationResponse(OperationResponse response)
        {
            var subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);
            switch (subCode)
            {
                case SubCode.GetTask:
                    //成功获得任务列表
                    var taskDbs = ParameterTool.GetParameter<List<TaskDb>>(response.Parameters, ParameterCode.TaskDb);
                    OnGetTaskDb(taskDbs);
                    break;
                case SubCode.AddTask:
                    //成功增加任务
                    var taskDb = ParameterTool.GetParameter<TaskDb>(response.Parameters, ParameterCode.TaskDb);
                    OnAddTaskDb(taskDb);
                    break;
                case SubCode.UpdateTask:
                    //成功更新任务
                    taskDb = ParameterTool.GetParameter<TaskDb>(response.Parameters, ParameterCode.TaskDb);
                    OnUpdateTaskDb(taskDb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 从服务器获得了任务信息
        /// </summary>
        public event OnGetTaskDbEvent OnGetTaskDb;

        /// <summary>
        /// 在服务器中更新了任务信息
        /// </summary>
        public event OnUpdateTaskDbEvent OnUpdateTaskDb;

        /// <summary>
        /// 在服务器中增添了任务信息
        /// </summary>
        public event OnAddTaskDbEvent OnAddTaskDb;
    }
}