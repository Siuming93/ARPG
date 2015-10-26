using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskItemUI : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Text coinText;
    public Text diamondText;
    public Image icon;
    public Image taskType;
    public Button button;

    private string AssetPath = "ItemPrefabs/";
    private Task task;

    public void SetTask(Task task)
    {
        this.task=task;

        nameText.text = task._name;
        descriptionText.text = task._description;
        coinText.text = task._coin.ToString();
        diamondText.text = task._diamond.ToString();

        GameObject iconSpritePre = Resources.Load<GameObject>(AssetPath + task._icon);
        icon.sprite = iconSpritePre.GetComponent<SpriteRenderer>().sprite;

        GameObject typeSpritePre = Resources.Load<GameObject>(AssetPath + task._taskType.ToString());
        taskType.sprite = typeSpritePre.GetComponent<SpriteRenderer>().sprite;

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        print("click");
        TaskManager.Instance.Excute(task);
    }
}
