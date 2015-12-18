using System.Collections.Generic;
using Assets.Scripts.View.MainMenu.Knapscak;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    /// <summary>
    /// 背包系统管理器
    /// </summary>
    public class InventoryManger : MonoBehaviour
    {
        public static InventoryManger Instrance { get; private set; }

        public TextAsset ListInfo;

        /// <summary>
        /// 所有物品的字典
        /// </summary>
        public Dictionary<int, Inventory> InventoryDict = new Dictionary<int, Inventory>();

        /// <summary>
        /// 记录了当前角色拥有的物品
        /// </summary>
        public List<InventoryItem> InventoryItemDict = new List<InventoryItem>();

        /// <summary>
        /// 背包Item列表
        /// </summary>
        public List<KnapsackItem> Items = new List<KnapsackItem>();


        private void Awake()
        {
            Instrance = this;

            ReadInventroyInfo();
            ReadInventroyItem();
        }

        /// <summary>
        /// 根据文本文档初始化所有物品年列表
        /// </summary>
        private void ReadInventroyInfo()
        {
            string str = ListInfo.ToString();
            string[] itemStrArray = str.Split('\n');
            for (int i = 0; i < itemStrArray.Length; i++)
            {
                //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing) 
                string itemStr = itemStrArray[i];

                string[] proArray = itemStr.Split('|');
                Inventory inventory = new Inventory();
                inventory.ID = int.Parse(proArray[0]);
                inventory.Name = proArray[1];
                inventory.Icon = proArray[2];
                switch (proArray[3])
                {
                    case "Equip":
                        inventory.InventoryType = InventoryType.Equip;
                        break;
                    case "Drug":
                        inventory.InventoryType = InventoryType.Drug;
                        break;
                    case "Box":
                        inventory.InventoryType = InventoryType.Box;
                        break;
                }
                if (inventory.InventoryType == InventoryType.Equip)
                {
                    switch (proArray[4])
                    {
                        case "Helm":
                            inventory.EquipType = EquipType.Helm;
                            break;
                        case "Cloth":
                            inventory.EquipType = EquipType.Cloth;
                            break;
                        case "Weapon":
                            inventory.EquipType = EquipType.Weapon;
                            break;
                        case "Shoes":
                            inventory.EquipType = EquipType.Shoes;
                            break;
                        case "Necklace":
                            inventory.EquipType = EquipType.Necklace;
                            break;
                        case "Bracelet":
                            inventory.EquipType = EquipType.Bracelet;
                            break;
                        case "Ring":
                            inventory.EquipType = EquipType.Ring;
                            break;
                        case "Wing":
                            inventory.EquipType = EquipType.Wing;
                            break;
                    }
                }
                //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
                inventory.Price = int.Parse(proArray[5]);
                if (inventory.InventoryType == InventoryType.Equip)
                {
                    inventory.StartLevel = int.Parse(proArray[6]);
                    inventory.Quality = int.Parse(proArray[7]);
                    inventory.Damage = int.Parse(proArray[8]);
                    inventory.HP = int.Parse(proArray[9]);
                }

                if (inventory.InventoryType == InventoryType.Drug)
                {
                    inventory.ApplyValue = int.Parse(proArray[12]);
                }
                inventory.Description = proArray[13];
                InventoryDict.Add(inventory.ID, inventory);
            }
        }

        /// <summary>
        /// 从服务器读取当前角色的道具信息
        /// </summary>
        private void ReadInventroyItem()
        {
            //1.连接服务器,取得角色拥有的物品信息
            //TODO

            //2.暂时随机生成
            for (int i = 0; i < 20; i++)
            {
                int id = Random.Range(1001, 1020);
                Inventory inventroy;
                InventoryDict.TryGetValue(id, out inventroy);
                InventoryItem it = new InventoryItem();

                if (inventroy.InventoryType == InventoryType.Equip)
                {
                    it.Inventory = inventroy;
                    it.Level = Random.Range(1, 10);
                    it.Count = 1;
                    InventoryItemDict.Add(it);
                }
                else
                {
                    //判断背包里面是否已经存在,存在则+1
                    it.Inventory = inventroy;
                    InventoryItemDict.Add(it);
                    it.Level = 0;
                    it.Count = Random.Range(1, 20);
                }

                Items[i].SetItem(it);
            }
        }
    }
}