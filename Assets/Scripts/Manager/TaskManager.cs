using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TaskManager : MonoBehaviour
{
    //存储任务信息的文档
    public TextAsset TaskInfoList;
    //副本入口
    public Transform TranscriptEnterTransform;
    //任务对话窗口
    public UIScale NpcTalkPanelScale;
    //任务字典
    public Dictionary<int, Task> TaskDic = new Dictionary<int, Task>();
    //当前任务
    private Task _curTask;
    //单例实例
    public static TaskManager Instance { get; private set; }
    //玩家导航插件
    public PlayerNavigation PlayerNavigation { get; private set; }
    //任务数据库控制器
    public TaskDbController TaskDbController;
    //获取数据库任务列表同步完成事件
    [SerializeField] [FormerlySerializedAs("onSyncTaskDbComplete")] private OnSyncTaskDbCompleteEvent
        _OnSyncTaskDbComplete = new OnSyncTaskDbCompleteEvent();

    public OnSyncTaskDbCompleteEvent OnSyncTaskDbComplete
    {
        get { return _OnSyncTaskDbComplete; }
        set { _OnSyncTaskDbComplete = value; }
    }

    public void Excute(Task task)
    {
        _curTask = task;
        var targetPos = NPCManager.Instance.GetNpc(task.NpcId).transform.position;
        PlayerNavigation.SetDestination(targetPos);
    }

    public void OnArriveDestination()
    {
        var taskDb = _curTask.GetTaskDb();
        switch (_curTask.TaskProgress)
        {
            case TaskProgress.UnStarted:
                TaskDbController.AddTaskDb(taskDb);
                break;
            case TaskProgress.Accepted:
                break;
        }
    }

    public void OnAcceptButtonClick()
    {
        TaskDbController.UpdateTaskDb(_curTask.GetTaskDb());
    }

    public void OnGetTaskDbList(List<TaskDb> taskDbs)
    {
        for (var i = 0; taskDbs != null && i < taskDbs.Count; i++)
        {
            Task task = null;
            if (TaskDic.TryGetValue(taskDbs[i].Id, out task))
            {
                task.SyncTaskDb(taskDbs[i]);
            }
        }
        if (OnSyncTaskDbComplete != null)
            OnSyncTaskDbComplete.Invoke();
    }

    public void OnAddTaskDb(TaskDb taskDb)
    {
        var task = TaskDic[taskDb.TaskId];
        task.SyncTaskDb(taskDb);
        NpcTalkPanelScale.SetActive(true);
    }

    public void OnUpdateTaskDb(TaskDb taskDb)
    {
        var task = TaskDic[taskDb.TaskId];
        task.SyncTaskDb(taskDb);
        switch (taskDb.TaskState)
        {
            case TaskState.UnStarted:
                break;
            case TaskState.Accepted:
                PlayerNavigation.SetDestination(TranscriptEnterTransform.position);
                NpcTalkPanelScale.SetActive(false);
                break;
            case TaskState.Accomplished:
                break;
            case TaskState.Achieved:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        Instance = this;
        PlayerNavigation = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerNavigation>();
        InitTaskDic();

        TaskDbController.OnGetTaskDb += OnGetTaskDbList;
        TaskDbController.OnAddTaskDb += OnAddTaskDb;
        TaskDbController.OnUpdateTaskDb += OnUpdateTaskDb;
    }

    private void Start()
    {
        TaskDbController.GetTaskDbList();
    }

    private void Destroy()
    {
        TaskDbController.OnGetTaskDb -= OnGetTaskDbList;
        TaskDbController.OnAddTaskDb -= OnAddTaskDb;
        TaskDbController.OnUpdateTaskDb -= OnUpdateTaskDb;
    }


    private void InitTaskDic()
    {
        var taskArray = TaskInfoList.ToString().Split('\n');

        for (int i = 0; i < taskArray.Length; i++)
        {
            var infos = taskArray[i].Split('|');

            var task = new Task {Id = int.Parse(infos[0])};
            switch (infos[1])
            {
                case "Main":
                    task.TaskType = TaskType.Main;
                    break;
                case "Daily":
                    task.TaskType = TaskType.Daily;
                    break;
                case "Reward":
                    task.TaskType = TaskType.Reward;
                    break;
            }
            task.Name = infos[2];
            task.Icon = infos[3];
            task.Description = infos[4];
            task.Coin = int.Parse(infos[5]);
            task.Diamond = int.Parse(infos[6]);
            task.TalkContent = infos[7];
            task.NpcId = int.Parse(infos[8]);
            task.TranscriptId = int.Parse(infos[9]);
            task.TaskProgress = TaskProgress.UnStarted;

            TaskDic.Add(task.Id, task);
        }
    }

    [Serializable]
    public class OnSyncTaskDbCompleteEvent : UnityEvent
    {
    }
}