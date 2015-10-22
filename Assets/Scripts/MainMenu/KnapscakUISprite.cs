using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KnapscakUISprite : MonoBehaviour
{
    string assetPath = "ItemPrefabs/";

    public void SetID(int ID)
    {
        if (ID == 0)
            return;
        print(ID);
        Inventory i = InventoryManger.Instrance.InventoryDict[ID];

        GameObject obj = Resources.Load<GameObject>(assetPath + i.Icon);
        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;

        Image image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
    }

    public void SetSprite(string Icon)
    {
        GameObject obj = Resources.Load<GameObject>(assetPath + Icon);
        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;

        Image image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
    }
}
