using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KnapscakCharcter : MonoBehaviour
{
    public Text name;
    public Text HP;
    public Text damage;
    public Text ExpText;
    public Slider ExpSlider;

    public KnapscakUISprite helmEquip;
    public KnapscakUISprite clothEquip;
    public KnapscakUISprite weaponEquip;
    public KnapscakUISprite shoesEquip;
    public KnapscakUISprite necklaceEquip;
    public KnapscakUISprite braceletEquip;
    public KnapscakUISprite ringEquip;
    public KnapscakUISprite wingEquip;

    void Start()
    {
        UpdateInfoShow();
        PlayerInfo.Instance.InfoChanged += OnInfoChanged;
    }

    void OnInfoChanged(InfoType infoType)
    {
        if (infoType == InfoType.All || infoType == InfoType.Name || infoType == InfoType.HP || infoType == InfoType.Damage || infoType == InfoType.Exp)
            UpdateInfoShow();
    }

    void UpdateInfoShow()
    {
        PlayerInfo info = PlayerInfo.Instance;
        name.text = info.name;

        HP.text = info.HP.ToString();

        damage.text = info.Damage.ToString();

        ExpText.text = info.Exp + "/" + info.MaxExp;
        ExpSlider.value = (float)info.Exp / info.MaxExp;

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
