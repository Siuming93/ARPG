using System;
using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Presenter.Manager;
using Assets.Scripts.UIPlugin;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.Knapscak
{
    /// <summary>
    /// 背包类的Ui管理器
    /// </summary>
    public class KanpsackView : MonoBehaviour
    {
        /// <summary>
        /// 背包Item列表
        /// </summary>
        public List<KnapsackItemUi> Items = new List<KnapsackItemUi>();

        /// <summary>
        /// 背包格子数
        /// </summary>
        public Text CounText;

        public UIScale ItemMessageScale;

        public static KanpsackView Instance { get; private set; }

        /// <summary>
        /// 选中的道具
        /// </summary>
        private InventoryItem _selectedItem;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InventoryManger.Instrance.AddPlayerInventoryItem += OnAddPlayerInventoryItem;
            gameObject.SetActive(false);
        }

        public void OnAddPlayerInventoryItem(InventoryItem item)
        {
            Items[item.Index].SetItem(item);
        }

        public void DeleteInventoryItem(int index)
        {
            Items[index].Clear();
        }

        /// <summary>
        /// 选中道具,显示详细信息栏
        /// </summary>
        /// <param name="item"></param>
        public void SelectItem(InventoryItem item)
        {
            if (_selectedItem != null && item != null)
                return;

            _selectedItem = item;
            ItemMessageScale.ScaleIn();
            KnapsackMessageBoxView.Instance.UpdateInfo(item);
            //播放声音
            //TODO
        }

        public void SelectItemById(int id)
        {
            //TODO
            throw new NotSupportedException();
        }

        /// <summary>
        /// 整理
        /// </summary>
        public void OnTrimButtonClick()
        {
            InventoryManger.Instrance.InventoryTrim();
        }

        /// <summary>
        /// 使用道具
        /// </summary>
        public void OnUseButtonClick()
        {
            InventoryManger.Instrance.UseItem(_selectedItem);
            OnMessageBoxCloseButtonClick();
        }

        /// <summary>
        /// 关闭详细信息
        /// </summary>
        public void OnMessageBoxCloseButtonClick()
        {
            ItemMessageScale.ScaleOut();
            _selectedItem = null;
        }
    }
}