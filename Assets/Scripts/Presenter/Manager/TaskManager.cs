using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Model.Photon.Controller;
using Assets.Scripts.UIPlugin;
using Assets.Scripts.View.Charcter.Player;
using Assets.Scripts.View.MainMenu.Task;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Assets.Scripts.Presenter.Manager
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class TaskManager : MonoBehaviour
    {
        /// <summary>
        /// 存储任务信息的文档
        /// </summary>
        public TextAsset TaskInfoList;

        /// <summary>
        /// 副本入口
        /// </summary>
        public Transform TranscriptEnterTransform;

        /// <summary>
        /// 任务对话窗口
        /// </summary>
        public UIScale NpcTalkPanelScale;

        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<int, Task> TaskDic = new Dictionary<int, Task>();

        /// <summary>
        /// 当前任务
        /// </summary>
        private Task _curTask;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static TaskManager Instance { get; private set; }

        /// <summary>
        /// 玩家导航插件
        /// </summary>
        public PlayerNavigationView PlayerNavigationView { get; private set; }

        /// <summary>
        /// 任务数据库控制器
        /// </summary>
        public TaskDbServerController TaskDbController;

        /*就得这么写才能达到手动添加委托的目的*/

        /// <summary>
        /// 获取数据库任务列表同步完成事件
        /// </summary>
        [SerializeField] [FormerlySerializedAs("onSyncTaskDbComplete")] private OnSyncTaskDbCompleteEvent
            _OnSyncTaskDbComplete = new OnSyncTaskDbCompleteEvent();

        public OnSyncTaskDbCompleteEvent OnSyncTaskDbComplete
        {
            get { return _OnSyncTaskDbComplete; }
            set { _OnSyncTaskDbComplete = value; }
        }

        /// <summary>
        /// 执行任务,即开始导航
        /// </summary>
        /// <param name="task"></param>
        public void Excute(Task task)
        {
            _curTask = task;
            switch (task.TaskState)
            {
                case TaskState.UnStarted:
                    var targetPos = NpcManager.Instance.GetNpc(task.NpcId).transform.position;
                    PlayerNavigationView.SetDestination(targetPos);
                    break;
                case TaskState.Accepted:
                    PlayerNavigationView.SetDestination(TranscriptEnterTransform.position);
                    break;
                case TaskState.Accomplished:
                    break;
                case TaskState.Achieved:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 当到达导航位置
        /// </summary>
        public void OnArriveDestination()
        {
            var taskDb = _curTask.GetTaskDb();
            switch (_curTask.TaskState)
            {
                case TaskState.UnStarted:
                    TaskDbController.AddTaskDb(taskDb);
                    break;
                case TaskState.Accepted:
                    break;
            }
        }

        /// <summary>
        /// 当接受任务Button点击
        /// </summary>
        public void OnAcceptButtonClick()
        {
            TaskDbController.UpdateTaskDb(_curTask.GetTaskDb());
        }

        /// <summary>
        /// 当从服务器得到任务列表
        /// </summary>
        /// <param name="taskDbs"></param>
        public void OnGetTaskDbList(List<TaskDb> taskDbs)
        {
            for (var i = 0; taskDbs != null && i < taskDbs.Count; i++)
            {
                Task task = null;
                var key = taskDbs[i].TaskId;
                if (TaskDic.TryGetValue(key, out task))
                {
                    task.SyncTaskDb(taskDbs[i]);
                }
            }
            if (OnSyncTaskDbComplete != null)
                OnSyncTaskDbComplete.Invoke();
        }

        /// <summary>
        /// 当服务器添加任务成功
        /// </summary>
        /// <param name="taskDb"></param>
        public void OnAddTaskDb(TaskDb taskDb)
        {
            var task = TaskDic[taskDb.TaskId];
            task.SyncTaskDb(taskDb);
            NpcTalkPanelScale.SetActive(true);
        }

        /// <summary>
        /// 更新任务信息成功
        /// </summary>
        /// <param name="taskDb"></param>
        public void OnUpdateTaskDb(TaskDb taskDb)
        {
            var task = TaskDic[taskDb.TaskId];
            task.SyncTaskDb(taskDb);
            switch (taskDb.TaskState)
            {
                case (byte) TaskState.UnStarted:
                    break;
                case (byte) TaskState.Accepted:
                    PlayerNavigationView.SetDestination(TranscriptEnterTransform.position);
                    NpcTalkPanelScale.SetActive(false);
                    break;
                case (byte) TaskState.Accomplished:
                    break;
                case (byte) TaskState.Achieved:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Awake()
        {
            Instance = this;
            PlayerNavigationView = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerNavigationView>();
            InitTaskDic();
            //为任务处理器添加监听函数
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

        /// <summary>
        /// 读取所有的任务
        /// </summary>
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
                task.TaskState = TaskState.UnStarted;

                TaskDic.Add(task.Id, task);
            }
        }

        [Serializable]
        public class OnSyncTaskDbCompleteEvent : UnityEvent
        {
        }
    }
}