using UnityEngine;
using System.Collections;

public enum InventoryType
{
    Equip,
    Drug,
    box
}

public enum EquipType
{
    Helm,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing
}

public class Inventory
{
    #region 属性
    //ID
    private int _id; 
    //名称
    private string _name;
    //图标
    private string _icon;
    //类型（Equip，Drug）
    private InventoryType _inventoryType;
    //装备类型
    private EquipType _equipType;
    //售价 
    private int _price = 0;
    //星级
    private int _startLevel = 1;
    //品质
    private int _quality = 1;
    //伤害
    private int _damage = 0;
     //生命
    private int _hp = 0;
     //战斗力
    private int _power = 0;
   //作用类型 表示作用在人物的哪个属性之上
    private InfoType _infoType;
    //作用值
    private int _applyValue;   
    //描述
    private string _description;
    #endregion    

    #region 访问器
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public InventoryType InventoryType
    {
        get { return _inventoryType; }
        set { _inventoryType = value; }
    }

    public EquipType EquipType
    {
        get { return _equipType; }
        set { _equipType = value; }
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public int StartLevel
    {
        get { return _startLevel; }
        set { _startLevel = value; }
    }

    public int Quality
    {
        get { return _quality; }
        set { _quality = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    public int Power
    {
        get { return _power; }
        set { _power = value; }
    }

    public InfoType InfoType
    {
        get { return _infoType; }
        set { _infoType = value; }
    }

    public int ApplyValue
    {
        get { return _applyValue; }
        set { _applyValue = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    #endregion
}
