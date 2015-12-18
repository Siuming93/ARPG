using Assets.Scripts.Model.Charcter;
using Assets.Scripts.Presenter.Manager.Charcter;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.PlayerStatus
{
    /// <summary>
    /// 玩家详细信息
    /// </summary>
    public class PlayerStatusUiView : MonoBehaviour
    {
        #region 控件

        public Text Name;
        public Text Power;
        public Text Level;
        public Text ExpText;
        public Slider ExpSlider;
        public Image HeadPortrait;

        public Text Coin;
        public Text Diamond;

        public Text Energy;
        public Text EnergyRecoveryNextTime;
        public Text EnergyRecoveryAllTime;

        public Text Toughen;
        public Text ToughenRecoveryNextTime;
        public Text ToughenRecoveryAllTime;

        #endregion

        public string AssetPath = "HeadPortraitPrefabs/";

        /// <summary>
        /// 开始时注册InfoChanged函数
        /// </summary>
        private void Start()
        {
            UpdateInfoShow();
            PlayerManager.Instance.AddInfoChangeEventToState(OnInfoChange);
        }

        private void Update()
        {
            UpdateEnergyAndToughenShow();
        }

        /// <summary>
        /// 被销毁时取消注册
        /// </summary>
        private void Destory()
        {
            PlayerManager.Instance.DeleteInfoChangeEventToState(OnInfoChange);
        }

        /// <summary>
        /// 当角色信息变更时,触发此函数来更改显示的playerStatus窗口
        /// </summary>
        /// <param name="infoType"></param>
        private void OnInfoChange()
        {
            UpdateInfoShow();
        }

        private void UpdateInfoShow()
        {
            PlayerInfo info = PlayerInfo.Instance;

            Name.text = info.Name;

            Level.text = info.Level.ToString();

            Energy.text = info.Energy.ToString() + "/" + info.MaxEnergy.ToString();

            Toughen.text = info.Toughen + "/" + info.MaxToughen.ToString();

            //通过Prefab来获得目标精灵
            GameObject obj = Resources.Load<GameObject>(AssetPath + info.HeadPortrait);
            HeadPortrait.sprite = obj.GetComponent<Image>().sprite;

            Power.text = info.Power.ToString();

            ExpText.text = info.Exp.ToString() + "/" + info.MaxExp.ToString();
            ExpSlider.value = info.Exp/(float) info.MaxExp;

            Coin.text = info.Coin.ToString();

            Diamond.text = info.Diamond.ToString();
        }

        /// <summary>
        /// 更新PlayerStatus中体力和历练的回复倒计时
        /// </summary>
        private void UpdateEnergyAndToughenShow()
        {
            PlayerInfo info = PlayerInfo.Instance;
            if (info.Energy >= info.MaxEnergy)
            {
                EnergyRecoveryNextTime.text = "00:00:00";
                EnergyRecoveryAllTime.text = "00:00:00";
            }
            else
            {
                int nextReaminTime = 59 - (int) info.EnergyTimer;
                EnergyRecoveryNextTime.text = string.Format("00:00:{0}", (nextReaminTime).ToString("00"));
                int allRemainTime = 60*(info.MaxEnergy - info.Energy - 1) + nextReaminTime;
                EnergyRecoveryAllTime.text = string.Format("{0}:{1}:{2}", (allRemainTime/3600).ToString("00"),
                    (allRemainTime/60).ToString("00"), (allRemainTime%60).ToString("00"));
            }

            if (info.Toughen >= info.MaxToughen)
            {
                ToughenRecoveryNextTime.text = "00:00:00";
                ToughenRecoveryAllTime.text = "00:00:00";
            }
            else
            {
                int nextReaminTime = 59 - (int) info.ToughenTimer;
                ToughenRecoveryNextTime.text = string.Format("00:00:{0}", (nextReaminTime).ToString("00"));
                int allRemainTime = 60*(info.MaxToughen - info.Toughen - 1) + nextReaminTime;
                ToughenRecoveryAllTime.text = string.Format("{0}:{1}:{2}", (allRemainTime/3600).ToString("00"),
                    (allRemainTime/60).ToString("00"), (allRemainTime%60).ToString("00"));
            }
        }
    }
}