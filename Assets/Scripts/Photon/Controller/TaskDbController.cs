using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ExitGames.Client.Photon;

public class TaskDbController : ControllerBase
{
    protected override OperationCode OpCode
    {
        get { return OperationCode.Task; }
    }

    public void GetTaskDbList()
    {
        PhotonEngine.Instance.SendOperationRequest(OperationCode.Task, SubCode.GetTask, new Dictionary<byte, object>());
    }

    public void AddTaskDb(TaskDb taskDb)
    {
        var parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDb);
        PhotonEngine.Instance.SendOperationRequest(OperationCode.Task, SubCode.AddTask, parameters);
    }

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
                var taskDbs = ParameterTool.GetParameter<List<TaskDb>>(response.Parameters, ParameterCode.TaskDb);
                OnGetTaskDb(taskDbs);
                break;
            case SubCode.AddTask:
                var taskDb = ParameterTool.GetParameter<TaskDb>(response.Parameters, ParameterCode.TaskDb);
                OnAddTaskDb(taskDb);
                break;
            case SubCode.UpdateTask:
                taskDb = ParameterTool.GetParameter<TaskDb>(response.Parameters, ParameterCode.TaskDb);
                OnUpdateTaskDb(taskDb);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public event OnGetTaskDbEvent OnGetTaskDb;
    public event OnUpdateTaskDbEvent OnUpdateTaskDb;
    public event OnAddTaskDbEvent OnAddTaskDb;
}