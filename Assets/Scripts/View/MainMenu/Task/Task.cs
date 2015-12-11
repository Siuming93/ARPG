using System;
using ARPGCommon;
using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using Assets.Scripts.Presenter.Manager;

namespace Assets.Scripts.View.MainMenu.Task
{
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
        public TaskState TaskState;
        //任务的数据库记录
        public TaskDb TaskDb = new TaskDb();

        public void SyncTaskDb(TaskDb taskDb)
        {
            TaskDb = taskDb;
            TaskState = (TaskState) taskDb.TaskState;
        }

        public TaskDb GetTaskDb()
        {
            TaskDb.TaskType = (byte) TaskType;
            TaskDb.TaskId = Id;
            TaskDb.Role = GameManager.Instance.CurRole;
            TaskDb.LastUpdateTime = new DateTime();
            TaskDb.TaskState = (byte) TaskState;
            return TaskDb;
        }
    }
}