using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    static PlayerInfo _instance;

    public static PlayerInfo Instance
    {
        get { return PlayerInfo._instance; }
    }

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
    //钻石数
    private int _diamond; 
    //金币数
    private int _coin;
    //体力数
    private int _energy;
    //历练数
    private int _tougher;



    //生命值 hp
    private int hp;
    //伤害值 damage
    //8个装备(存储ID)
    #endregion

    private float energyTimer = 0f;
    private float toughenTimer = 0f;

    #region 属性设置
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string HeadPortrait
    {
        get { return _headPortrait; }
        set { _headPortrait = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int Power
    {
        get { return _power; }
        set { _power = value; }
    }

    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    public int Diamond
    {
        get { return _diamond; }
        set { _diamond = value; }
    }

    public int Coin
    {
        get { return _coin; }
        set { _coin = value; }
    }

    public int Energy
    {
        get { return _energy; }
        set { _energy = value; }
    }

    public int Toughen
    {
        get { return _tougher; }
        set { _tougher = value; }
    }

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    #endregion 

    public void Awake()
    {
        _instance = this;
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
        this.Exp = 123;
        this.HeadPortrait = "头像底版女性";
        this.Level = 12;
        this.Name = "王尼美";
        this.Power = 1745;
        this.Toughen = 34;
    }

}
