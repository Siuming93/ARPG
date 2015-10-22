using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBar : MonoBehaviour
{
    #region 控件
    public Text name;
    public Text level;
    public Text energyText;
    public Text toughenText;

    public Slider energySlider;
    public Slider toughenSlider;

    public Image headPortrait;
    #endregion

    public string AssetPath = "HeadPortraitPrefabs\\";

    /// <summary>
    /// 开始时注册InfoChanged函数
    /// </summary>
    void Start()
    {
        PlayerInfo.Instance.InfoChanged += InfoChanged;
    }

    /// <summary>
    /// 被销毁时取消注册
    /// </summary>
    void Destory()
    {
        PlayerInfo.Instance.InfoChanged -= InfoChanged;
    }

    /// <summary>
    /// 当角色信息变更时,触发此函数来更改显示的playerBar
    /// </summary>
    /// <param name="infoType"></param>
    void InfoChanged(InfoType infoType)
    {
        PlayerInfo info = PlayerInfo.Instance;

        switch (infoType)
        {
            case InfoType.Name:
                name.text = info.name;
                break;
            case InfoType.Level:
                level.text = info.Level.ToString();
                break;
            case InfoType.Energy:
                energySlider.value = info.Energy / (float)info.MaxEnergy;
                energyText.text = info.Energy.ToString() + "/" + info.MaxEnergy.ToString();
                break;
            case InfoType.Toughen:
                toughenSlider.value = info.Toughen / (float)info.MaxToughen;
                toughenText.text = info.Toughen + "/" + info.MaxToughen.ToString();
                break;
            case InfoType.HeadPortrait:
                //通过Prefab来获得目标精灵
                GameObject obj = Resources.Load<GameObject>(AssetPath + info.HeadPortrait);
                headPortrait.sprite = obj.GetComponent<Image>().sprite;
                break;
            default:
                break;
        }
    }
}
