using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public TextAsset taskInfoList;

    public Dictionary<int, Task> TaskDic = new Dictionary<int, Task>();

    private static TaskManager _instance;

    public static TaskManager Instance
    {
        get { return _instance; }
    }

    private PlayerNavigation _playerNavigation;

    public PlayerNavigation mPlayerNavigation
    {
        get
        {
            if (_playerNavigation == null)
                _playerNavigation = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerNavigation>();
            return _playerNavigation;
        }
    }


    void Awake()
    {
        _instance = this;
        InitTaskDic();
    }

    void InitTaskDic()
    {
        string[] taskArray = taskInfoList.ToString().Split('\n');

        for (int i = 0; i < taskArray.Length; i++)
        {
            string[] infos = taskArray[i].Split('|');

            Task task = new Task();
            task._id = int.Parse(infos[0]);
            switch (infos[1])
            {
                case "Main":
                    task._taskType = TaskType.Main;
                    break;
                case "Daily":
                    task._taskType = TaskType.Daily;
                    break;
                case "Reward":
                    task._taskType = TaskType.Reward;
                    break;
            }
            task._name = infos[2];
            task._icon = infos[3];
            task._description = infos[4];
            task._coin = int.Parse(infos[5]);
            task._diamond = int.Parse(infos[6]);
            task._talkContent = infos[7];
            task._npcId = int.Parse(infos[8]);
            task._transcriptId = int.Parse(infos[9]);

            TaskDic.Add(task._id, task);
        }
    }

    public void Excute(Task task)
    {
        Vector3 targetPos = NPCManager.Instance.GetNpc(task._npcId).transform.position;
        mPlayerNavigation.SetDestination(targetPos);
    }


}
