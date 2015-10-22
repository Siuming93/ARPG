using UnityEngine;
using System.Collections;

public enum InfoType
{
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,

    HP,
    Damage,

    All
}

public class PlayerInfo : MonoBehaviour
{
    static PlayerInfo _instance;

    public static PlayerInfo Instance
    {
        get { return PlayerInfo._instance; }
    }

    public delegate void InfoChange(InfoType infoType);
    public event InfoChange InfoChanged;

    #region 主角属性
    //姓名
    private string _name;
    //头像
    private string _headPortrait;
    //等级
    private int _level;
    //战斗力
    private int _power;
    //经验数
    private int _exp;
    private int _maxExp;
    //钻石数
    private int _diamond;
    //金币数
    private int _coin;
    //体力数
    private int _energy;
    private int _maxEnergy;
    //历练数
    private int _tougher;
    private int _maxToughen;


    //生命值 hp
    private int _hp;
    //伤害值 damage
    private int _damage;

    //8个装备(存储ID)
    private int _helmID;


    private int _clothID;


    private int _weaponID;


    private int _shoesID;


    private int _necklaceID;


    private int _braceletID;


    private int _ringID;


    private int _wingID;


    #endregion


    #region 属性设置
    public string name
    {
        get { return _name; }
        private set
        {
            _name = value;
            InfoChanged(InfoType.Name);
        }
    }

    public string HeadPortrait
    {
        get { return _headPortrait; }
        private set
        {
            _headPortrait = value;
            InfoChanged(InfoType.HeadPortrait);
        }
    }

    public int Level
    {
        get { return _level; }
        private set
        {
            _level = value;
            InfoChanged(InfoType.Level);
        }
    }

    public int Power
    {
        get { return _power; }
        private set
        {
            _power = value;
            InfoChanged(InfoType.Power);
        }
    }

    public int Exp
    {
        get { return _exp; }
        private set
        {
            _exp = value;
            InfoChanged(InfoType.Exp);
        }
    }

    public int MaxExp
    {
        get { return _maxExp; }
        private set
        {
            _maxExp = value;
            InfoChanged(InfoType.Exp);
        }
    }

    public int Diamond
    {
        get { return _diamond; }
        private set
        {
            _diamond = value;
            InfoChanged(InfoType.Diamond);
        }
    }

    public int Coin
    {
        get { return _coin; }
        private set
        {
            _coin = value;
            InfoChanged(InfoType.Coin);
        }
    }

    public int Energy
    {
        get { return _energy; }
        private set
        {
            _energy = value;
            InfoChanged(InfoType.Energy);
        }
    }
    public int MaxEnergy
    {
        get { return _maxEnergy; }
        private set
        {
            _maxEnergy = value;
            InfoChanged(InfoType.Energy);
        }
    }

    public int Toughen
    {
        get { return _tougher; }
        private set
        {
            _tougher = value;
            InfoChanged(InfoType.Toughen);
        }
    }

    public int MaxToughen
    {
        get { return _maxToughen; }
        private set
        {
            _maxToughen = value;
            InfoChanged(InfoType.Toughen);
        }
    }

    public int HP
    {
        get { return _hp; }
        private set
        {
            _hp = value;
            InfoChanged(InfoType.HP);
        }
    }

    public int Damage
    {
        get { return _damage; }
        set { 
            _damage = value;
            InfoChanged(InfoType.Damage);
        }
    }

    public int HelmID
    {
        get { return _helmID; }
        set { 
            _helmID = value;
            InfoChanged(InfoType.All);
        }
    }

    public int ClothID
    {
        get { return _clothID; }
        set { _clothID = value; }
    }

    public int WeaponID
    {
        get { return _weaponID; }
        set { _weaponID = value; }
    }

    public int ShoesID
    {
        get { return _shoesID; }
        set { _shoesID = value; }
    }

    public int NecklaceID
    {
        get { return _necklaceID; }
        set { _necklaceID = value; }
    }

    public int BraceletID
    {
        get { return _braceletID; }
        set { _braceletID = value; }
    }

    public int RingID
    {
        get { return _ringID; }
        set { _ringID = value; }
    }

    public int WingID
    {
        get { return _wingID; }
        set { _wingID = value; }
    }
    #endregion



    public float energyTimer = 0f;
    public float toughenTimer = 0f;



    public void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        //1.计时增加体力
        if (this.Energy < 100)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer > 60)
            {
                Energy += 1;
                energyTimer = 0;
            }
        }
        else
        {
            energyTimer = 0f;
        }

        //2.计时,历练自动增长
        if (this.Toughen < 50)
        {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60)
            {
                Toughen += 1;
                toughenTimer = 0f;
            }
        }
        else
        {
            toughenTimer = 0f;
        }
    }

    void Init()
    {
        this.Coin = 9870;
        this.Diamond = 1234;
        this.Energy = 78;
        this.MaxEnergy = 100;
        this.HeadPortrait = "女性角色头像";
        this.Level = 12;
        this.Exp = 123;
        this.MaxExp = (Level - 1) * (100 + 100 + 10 * (Level - 1)) / 2;
        this.name = "王尼美";
        this.Power = 1745;
        this.Toughen = 34;
        this.MaxToughen = 50;

        InitHPDamageandPower();
    }

    private void InitHPDamageandPower()
    {
        this.HP = 100 * this.Level;
        this.Damage = 50 * this.Level;
        this.Power = this.HP + this.Damage;

        this.HelmID = 1013;
    }

    void PutonEquip(int id)
    {
        Inventory inventory = null;
        InventoryManger.Instrance.InventoryDict.TryGetValue(id, out inventory);
        this.HP += inventory.HP;
        this.Damage += inventory.Damage;
    }

    void PutoffEquip(int id)
    {
        if (id == 0)
            return;

        Inventory inventory = null;
        InventoryManger.Instrance.InventoryDict.TryGetValue(id, out inventory);
        this.HP -= inventory.HP;
        this.Damage -= inventory.Damage;
    }

}
