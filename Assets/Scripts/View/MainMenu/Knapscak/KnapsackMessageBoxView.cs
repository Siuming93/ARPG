using Assets.Scripts.Model;
using Assets.Scripts.UIPlugin;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.Knapscak
{
    public class KnapsackMessageBoxView : MonoBehaviour
    {
        private static string assetPath = "ItemPrefabs/";
        public static KnapsackMessageBoxView Instance;

        private void Awake()
        {
            Instance = this;
        }

        //按钮面板
        public GameObject ItemPanel;
        public GameObject EquipPutOnPanel;
        public GameObject EquipPutOffPanel;

        //缩放组件
        public UIScale UiScale;

        /// <summary>
        /// 道具图片
        /// </summary>
        public Image IconImage;

        /// <summary>
        /// 描述
        /// </summary>
        public Text DescriptionText;

        /// <summary>
        /// 名字
        /// </summary>
        public Text NameText;

        /// <summary>
        /// 种类
        /// </summary>
        public Text CategroyText;

        private InventoryItem curItem;

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="item"></param>
        public void UpdateInfo(InventoryItem item)
        {
            ItemPanel.SetActive(false);
            EquipPutOnPanel.SetActive(false);
            EquipPutOffPanel.SetActive(false);

            curItem = item;
            SetSprite(item.Icon);
            DescriptionText.text = item.Description;
            NameText.text = item.Name;
            //显示种类,根据种类显示不同的Button
            //TODO
            CategroyText.text = "道具";
            ItemPanel.SetActive(true);
        }

        /// <summary>
        /// 设定背景图片
        /// </summary>
        /// <param name="Icon"></param>
        private void SetSprite(string Icon)
        {
            GameObject obj = Resources.Load<GameObject>(assetPath + Icon);
            Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;
            IconImage.sprite = sprite;
        }
    }
}