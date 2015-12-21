using Assets.Scripts.Model.Charcter;
using Assets.Scripts.Presenter.Manager.Charcter;
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

        public KnapsackItemUi helmEquip;
        public KnapsackItemUi clothEquip;
        public KnapsackItemUi weaponEquip;
        public KnapsackItemUi shoesEquip;
        public KnapsackItemUi necklaceEquip;
        public KnapsackItemUi braceletEquip;
        public KnapsackItemUi ringEquip;
        public KnapsackItemUi wingEquip;

        private void Start()
        {
            UpdateInfoShow();
            PlayerManager.Instance.AddInfoChangeEventToState(OnInfoChanged);
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
            helmEquip.SetItemById(info.HelmID);
            clothEquip.SetItemById(info.ClothID);
            weaponEquip.SetItemById(info.WeaponID);
            shoesEquip.SetItemById(info.ShoesID);
            necklaceEquip.SetItemById(info.NecklaceID);
            braceletEquip.SetItemById(info.BraceletID);
            ringEquip.SetItemById(info.RingID);
            wingEquip.SetItemById(info.WingID);
        }
    }
}