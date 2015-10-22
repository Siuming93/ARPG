
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KnapsackItem : MonoBehaviour
{
    public Text CountText;
    public KnapscakUISprite UISprite;

    public void SetItem(InventoryItem inventoryItem)
    {
        if (inventoryItem.Count > 1)
            CountText.text = inventoryItem.Count.ToString();
        UISprite.SetSprite(inventoryItem.Inventory.Icon);
    }
}
