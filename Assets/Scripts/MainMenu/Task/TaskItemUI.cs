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

    public void SetTask(Task task)
    {
        nameText.text = task._name;
        descriptionText.text = task._description;
        coinText.text = task._coin.ToString();
        diamondText.text = task._diamond.ToString();

        GameObject iconSpritePre = Resources.Load<GameObject>(AssetPath + task._icon);
        icon.sprite = iconSpritePre.GetComponent<SpriteRenderer>().sprite;

        GameObject typeSpritePre = Resources.Load<GameObject>(AssetPath + task._taskType.ToString());
        taskType.sprite = typeSpritePre.GetComponent<SpriteRenderer>().sprite;

        

    }
}
