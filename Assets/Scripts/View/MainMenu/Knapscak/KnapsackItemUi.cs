using Assets.Scripts.Model;
using Assets.Scripts.Presenter.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.Knapscak
{
    /// <summary>
    /// 背包上的道具显示子对象
    /// </summary>
    public class KnapsackItemUi : MonoBehaviour, IPointerClickHandler
    {
        private static string assetPath = "ItemPrefabs/";
        private static string defaultSpriteName = "bg_道具";


        /// <summary>
        /// 道具数量
        /// </summary>
        public Text CountText;

        private InventoryItem InventoryItem;

        /// <summary>
        /// 设定道具的信息
        /// </summary>
        /// <param name="inventoryItem"></param>
        public void SetItem(InventoryItem inventoryItem)
        {
            if (inventoryItem.Count > 1)
                CountText.text = inventoryItem.Count.ToString();
            SetSprite(inventoryItem.Icon);
            InventoryItem = inventoryItem;
        }

        /// <summary>
        /// 设定背景图片
        /// </summary>
        /// <param name="Icon"></param>
        public void SetSprite(string Icon)
        {
            GameObject obj = Resources.Load<GameObject>(assetPath + Icon);
            Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;

            Image image = gameObject.GetComponent<Image>();
            image.sprite = sprite;
        }

        /// <summary>
        /// 图片的位置
        /// </summary>
        public void SetItemById(int id)
        {
            if (id == 0)
                return;
            print(id);
            InventoryItem i = InventoryManger.Instrance.InventoryDict[id];

            GameObject obj = Resources.Load<GameObject>(assetPath + i.Icon);
            Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;

            Image image = gameObject.GetComponent<Image>();
            image.sprite = sprite;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            CountText.text = "";
            SetSprite(defaultSpriteName);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            KanpsackView.Instance.SelectItem(InventoryItem);
        }
    }
}