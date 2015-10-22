using UnityEngine;
using System.Collections;

public enum TaskType
{
    Main,
    Reward,
    Daily
}

public enum TaskState
{
    //i.	未开始
    UnStarted,
    //ii.	接受任务
    Accepted,
    //iii.	任务完成
    Accomplished,
    //iv.	获取奖励（结束）
    Achieved
}

public class Task
{
    //a)	Id
    public int _id;
    //b)	任务类型
    public TaskType _taskType;
    //c)	名称
    public string _name;
    //d)	图标
    public string _icon;
    //e)	任务描述
    public string _description;
    //f)	获得的金币奖励
    public int _coin;
    //g)	获得的钻石奖励
    public int _diamond;
    //h)	跟npc交谈的话语
    public string _talkContent;
    //i)	Npc的id
    public int _npcId;
    //j)	副本id
    public int _transcriptId;
    //k)	任务的状态
    public TaskState _taskState;
}
