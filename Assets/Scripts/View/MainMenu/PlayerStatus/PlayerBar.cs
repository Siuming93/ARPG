using Assets.Scripts.Model;
using Assets.Scripts.Presenter.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.PlayerStatus
{
    public class PlayerBar : MonoBehaviour
    {
        #region 控件

        //头像状态显示文字
        public Text Name;
        public Text Level;
        public Text EnergyText;
        public Text ToughenText;
        //进度条
        public Slider EnergySlider;
        public Slider ToughenSlider;
        //头像
        public Image HeadPortrait;

        #endregion

        public string AssetPath = "HeadPortraitPrefabs\\";

        /// <summary>
        /// 开始时注册InfoChanged函数
        /// </summary>
        private void Start()
        {
            PlayerManager.Instance.AddInfoChangeEventToState(OnInfoChange);
        }

        /// <summary>
        /// 被销毁时取消注册
        /// </summary>
        private void Destory()
        {
            PlayerManager.Instance.DeleteInfoChangeEventToState(OnInfoChange);
        }

        /// <summary>
        /// 当角色信息变更时,触发此函数来更改显示的playerBar
        /// </summary>
        /// <param name="infoType"></param>
        private void OnInfoChange()
        {
            var info = PlayerInfo.Instance;


            Name.text = info.Name;
            Level.text = info.Level.ToString();
            EnergySlider.value = info.Energy/(float) info.MaxEnergy;
            EnergyText.text = info.Energy.ToString() + "/" + info.MaxEnergy.ToString();
            ToughenSlider.value = info.Toughen/(float) info.MaxToughen;
            ToughenText.text = info.Toughen + "/" + info.MaxToughen.ToString();
            //通过Prefab来获得目标精灵
            var obj = Resources.Load<GameObject>(AssetPath + info.HeadPortrait);
            HeadPortrait.sprite = obj.GetComponent<Image>().sprite;
        }
    }
}