using System.Collections;
using System.IO;
using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using Assets.Scripts.Model.Photon.Controller;
using Assets.Scripts.View.MainMenu.Knapscak;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public class PlayerInfo : MonoBehaviour
    {
        public static PlayerInfo Instance { get; private set; }

        public delegate void OnInfoChangeEvent();

        public event OnInfoChangeEvent OnInfoChange;

        public RoleServerController RoleServerController;

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

        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPlayerInfoSet();
            }
        }

        public string HeadPortrait
        {
            get { return _headPortrait; }
            private set
            {
                _headPortrait = value;
                OnPlayerInfoSet();
            }
        }

        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                OnPlayerInfoSet();
            }
        }

        public int Power
        {
            get { return _power; }
            private set
            {
                _power = value;
                OnPlayerInfoSet();
            }
        }

        public int Exp
        {
            get { return _exp; }
            private set
            {
                _exp = value;
                OnPlayerInfoSet();
            }
        }

        public int MaxExp
        {
            get { return _maxExp; }
            private set
            {
                _maxExp = value;
                OnPlayerInfoSet();
            }
        }

        public int Diamond
        {
            get { return _diamond; }
            private set
            {
                _diamond = value;
                OnPlayerInfoSet();
            }
        }

        public int Coin
        {
            get { return _coin; }
            private set
            {
                _coin = value;
                OnPlayerInfoSet();
            }
        }

        public int Energy
        {
            get { return _energy; }
            private set
            {
                _energy = value;
                OnPlayerInfoSet();
            }
        }

        public int MaxEnergy
        {
            get { return _maxEnergy; }
            private set
            {
                _maxEnergy = value;
                OnPlayerInfoSet();
            }
        }

        public int Toughen
        {
            get { return _tougher; }
            private set
            {
                _tougher = value;
                OnPlayerInfoSet();
            }
        }

        public int MaxToughen
        {
            get { return _maxToughen; }
            private set
            {
                _maxToughen = value;
                OnPlayerInfoSet();
            }
        }

        public int HP
        {
            get { return _hp; }
            private set
            {
                _hp = value;
                OnPlayerInfoSet();
            }
        }

        public int Damage
        {
            get { return _damage; }
            set
            {
                _damage = value;
                OnPlayerInfoSet();
            }
        }

        public int HelmID
        {
            get { return _helmID; }
            set
            {
                _helmID = value;
                OnPlayerInfoSet();
            }
        }

        public int ClothID
        {
            get { return _clothID; }
            set
            {
                _clothID = value;
                OnPlayerInfoSet();
            }
        }

        public int WeaponID
        {
            get { return _weaponID; }
            set
            {
                _weaponID = value;
                OnPlayerInfoSet();
            }
        }

        public int ShoesID
        {
            get { return _shoesID; }
            set
            {
                _shoesID = value;
                OnPlayerInfoSet();
            }
        }

        public int NecklaceID
        {
            get { return _necklaceID; }
            set
            {
                _necklaceID = value;
                OnPlayerInfoSet();
            }
        }

        public int BraceletID
        {
            get { return _braceletID; }
            set
            {
                _braceletID = value;
                OnPlayerInfoSet();
            }
        }

        public int RingID
        {
            get { return _ringID; }
            set
            {
                _ringID = value;
                OnPlayerInfoSet();
            }
        }

        public int WingID
        {
            get { return _wingID; }
            set
            {
                _wingID = value;
                OnPlayerInfoSet();
            }
        }

        #endregion

        public float EnergyTimer { get; private set; }
        public float ToughenTimer { get; private set; }


        public void Awake()
        {
            Instance = this;
            RoleServerController.OnUpdateRole += OnUpdatePlayerInfo;
        }


        private void Start()
        {
            Init();
        }

        private void Update()
        {
            //1.计时增加体力
            if (this.Energy < 100)
            {
                EnergyTimer += Time.deltaTime;
                if (EnergyTimer > 60)
                {
                    Energy += 1;
                    EnergyTimer = 0;
                }
            }
            else
            {
                EnergyTimer = 0f;
            }

            //2.计时,历练自动增长
            if (this.Toughen < 50)
            {
                ToughenTimer += Time.deltaTime;
                if (ToughenTimer > 60)
                {
                    Toughen += 1;
                    ToughenTimer = 0f;
                }
            }
            else
            {
                ToughenTimer = 0f;
            }
        }

        private void OnPlayerInfoSet()
        {
            //Tip:不能访问访问其以防止出现调用堆栈错误
            //1.检测经验,若经验高了则升级.
            if (_exp >= _maxExp)
            {
                _level++;
                _exp -= _maxExp;
                InitPropertiesBaseOnLevel();
            }
            UpdatePlayerInfo();
        }

        private void Init()
        {
            var role = PhotonEngine.Instance.CurRole;
            this._name = role.Name;
            this._coin = role.Coin;
            this._diamond = role.Diamond;
            this._level = role.Level;
            this._energy = role.Energy;
            this._tougher = role.Toughen;
            this._exp = role.Exp;
            this._headPortrait = role.Isman ? "男性角色头像" : "女性角色头像";


            this._maxEnergy = 100;
            this._maxToughen = 50;

            InitPropertiesBaseOnLevel();
            if (OnInfoChange != null) OnInfoChange();
        }

        private void UpdatePlayerInfo()
        {
            var role = PhotonEngine.Instance.CurRole;
            role.Coin = this.Coin;
            role.Diamond = this.Diamond;
            role.Level = this.Level;
            role.Energy = this.Energy;
            role.Toughen = this.Toughen;
            role.Exp = this.Exp;

            RoleServerController.UpdateRole(role);
        }

        private void OnUpdatePlayerInfo()
        {
            if (OnInfoChange != null) OnInfoChange();
        }

        private void InitPropertiesBaseOnLevel()
        {
            this._hp = 100*this.Level;
            this._damage = 50*this.Level;
            this._power = this.HP + this.Damage;
            this._maxExp = Level*(100 + 100 + 10*(Level - 1))/2;

            this._helmID = 1013;
        }

        private void PutonEquip(int id)
        {
            Inventory inventory = null;
            InventoryManger.Instrance.InventoryDict.TryGetValue(id, out inventory);
            this.HP += inventory.HP;
            this.Damage += inventory.Damage;
        }

        private void PutoffEquip(int id)
        {
            if (id == 0)
                return;

            Inventory inventory = null;
            InventoryManger.Instrance.InventoryDict.TryGetValue(id, out inventory);
            this.HP -= inventory.HP;
            this.Damage -= inventory.Damage;
        }
    }
}