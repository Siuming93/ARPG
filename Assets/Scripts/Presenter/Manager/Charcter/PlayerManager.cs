using Assets.Scripts.Model;
using Assets.Scripts.Model.Charcter;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager.Charcter
{
    /// <summary>
    /// 玩家角色管理器
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public PlayerInfo PlayerInfo;

        public static PlayerManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 接受伤害
        /// </summary>
        /// <param name="value"></param>
        public void TakeDamage(int value)
        {
            print(value);
            PlayerInfo.TakeDamage(value);
            if (PlayerInfo.HP < 0)
            {
                if (OnDeadEvent != null) OnDeadEvent(new EnemyState());
            }
        }

        /// <summary>
        /// 注册角色信息改变事件
        /// </summary>
        /// <param name="onInfoChangeEvent"></param>
        public void AddInfoChangeEventToState(OnInfoChangeEvent onInfoChangeEvent)
        {
            PlayerInfo.OnInfoChangeEvent += onInfoChangeEvent;
        }

        /// <summary>
        /// 取消角色信息改变事件注册
        /// </summary>
        /// <param name="onInfoChangeEvent"></param>
        public void DeleteInfoChangeEventToState(OnInfoChangeEvent onInfoChangeEvent)
        {
            PlayerInfo.OnInfoChangeEvent -= onInfoChangeEvent;
        }

        /// <summary>
        /// 角色死亡事件
        /// </summary>
        public event OnDeadEvent OnDeadEvent;

        public void UseItem(InventoryItem item)
        {
            //根据道具属性,改变人物状态
            //TODO
        }
    }
}