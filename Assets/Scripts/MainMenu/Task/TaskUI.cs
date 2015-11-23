using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TaskUI : MonoBehaviour
{
    public GameObject TaskGridUI;
    public GameObject TaskItemUI;
    public Button closeButton;
    public UIScale thisPanel;


    private void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public void OnSyncTaskDbComplete()
    {
        print("OnSyncTaskDbComplete");
        InitTaskListUI();
    }

    /// <summary>
    /// 初始化任务列表
    /// </summary>
    public void InitTaskListUI()
    {
        Dictionary<int, Task> taskDic = TaskManager.Instance.TaskDic;

        foreach (Task task in taskDic.Values)
        {
            GameObject obj = GameObject.Instantiate(TaskItemUI) as GameObject;
            obj.transform.parent = TaskGridUI.transform;
            TaskItemUI taskUI = obj.GetComponent<TaskItemUI>();
            taskUI.SetTask(task);
        }
    }

    private void OnCloseButtonClick()
    {
        thisPanel.SetActive(false);
    }
}