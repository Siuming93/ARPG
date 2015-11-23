using System;
using UnityEngine;
using System.Collections;
using ARPGCommon;
using ARPGCommon.Model;

public enum TaskProgress : byte
{
    UnStarted,
    Accepted,
    Accomplished,
    Achieved
}

public class Task
{
    //Id
    public int Id;
    //任务类型
    public TaskType TaskType;
    //名称
    public string Name;
    //图标
    public string Icon;
    //任务描述
    public string Description;
    //获得的金币奖励
    public int Coin;
    //获得的钻石奖励
    public int Diamond;
    //跟npc交谈的话语
    public string TalkContent;
    //Npc的id
    public int NpcId;
    //副本id
    public int TranscriptId;
    //任务的状态
    public TaskProgress TaskProgress;
    //任务的数据库记录
    public TaskDb TaskDb = new TaskDb();

    public void SyncTaskDb(TaskDb taskDb)
    {
        TaskDb = taskDb;
        switch (taskDb.TaskState)
        {
            case TaskState.UnStarted:
                TaskProgress = TaskProgress.UnStarted;
                break;
            case TaskState.Accepted:
                TaskProgress = TaskProgress.Accepted;
                break;
            case TaskState.Accomplished:
                TaskProgress = TaskProgress.Accomplished;
                break;
            case TaskState.Achieved:
                TaskProgress = TaskProgress.Achieved;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public TaskDb GetTaskDb()
    {
        TaskDb.TaskType = TaskType;
        TaskDb.TaskId = Id;
        TaskDb.Role = PhotonEngine.Instance.CurRole;
        TaskDb.LastUpdateTime = new DateTime();
        TaskDb.TaskState = (TaskState) TaskProgress;
        return TaskDb;
    }
}