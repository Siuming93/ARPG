using UnityEngine;
using System.Collections;

public class BottomBar : MonoBehaviour
{
    public UIScale TaskUIScale;
    public UIScale KnapscakUIScale;

    public TaskUI taskUI;

    void Start()
    {
        taskUI.InitTaskListUI();
    }

    public void OnTaskButtonClick()
    {
        TaskUIScale.SetActive(!TaskUIScale.gameObject.activeSelf);
    }
}
