using Assets.Scripts.Presenter.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.Task
{
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
            InitTaskListUi();
        }

        /// <summary>
        /// 初始化任务列表
        /// </summary>
        public void InitTaskListUi()
        {
            var taskDic = TaskManager.Instance.TaskDic;

            foreach (var task in taskDic.Values)
            {
                var obj = Instantiate(TaskItemUI) as GameObject;
                obj.transform.parent = TaskGridUI.transform;
                var taskUi = obj.GetComponent<TaskItemUI>();
                //初始化,不初始化不能用
                taskUi.TaskUiScale = thisPanel;
                taskUi.SetTask(task);
            }
        }

        private void OnCloseButtonClick()
        {
            thisPanel.SetActive(false);
        }
    }
}