using Assets.Scripts.Model;
using Assets.Scripts.Presenter.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.MainMenu.Knapscak
{
    public class KnapscakCharcter : MonoBehaviour
    {
        public Text name;
        public Text HP;
        public Text damage;
        public Text ExpText;
        public Slider ExpSlider;

        public KnapscakUiSprite helmEquip;
        public KnapscakUiSprite clothEquip;
        public KnapscakUiSprite weaponEquip;
        public KnapscakUiSprite shoesEquip;
        public KnapscakUiSprite necklaceEquip;
        public KnapscakUiSprite braceletEquip;
        public KnapscakUiSprite ringEquip;
        public KnapscakUiSprite wingEquip;

        private void Start()
        {
            UpdateInfoShow();
            PlayerManager.Instance.OnInfoChange += OnInfoChanged;
        }

        private void OnInfoChanged()
        {
            UpdateInfoShow();
        }

        private void UpdateInfoShow()
        {
            var info = PlayerInfo.Instance;
            name.text = info.Name;

            HP.text = info.HP.ToString();

            damage.text = info.Damage.ToString();

            ExpText.text = info.Exp + "/" + info.MaxExp;
            ExpSlider.value = (float) info.Exp/info.MaxExp;

            //更新装备显示
            helmEquip.SetID(info.HelmID);
            clothEquip.SetID(info.ClothID);
            weaponEquip.SetID(info.WeaponID);
            shoesEquip.SetID(info.ShoesID);
            necklaceEquip.SetID(info.NecklaceID);
            braceletEquip.SetID(info.BraceletID);
            ringEquip.SetID(info.RingID);
            wingEquip.SetID(info.WingID);
        }
    }
}