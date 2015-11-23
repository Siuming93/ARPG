using UnityEngine;
using System.Collections;

public class BottomBar : MonoBehaviour
{
    public UIScale TaskUIScale;
    public UIScale KnapscakUIScale;


    private void Start()
    {
    }

    public void OnTaskButtonClick()
    {
        TaskUIScale.SetActive(!TaskUIScale.gameObject.activeSelf);
    }
}