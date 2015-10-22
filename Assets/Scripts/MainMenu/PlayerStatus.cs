using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    #region 控件
    public Text name;
    public Text power;
    public Text level;
    public Text expText;
    public Slider expSlider;
    public Image headPortrait;

    public Text coin;
    public Text diamond;

    public Text energy;
    public Text energyRecoveryNextTime;
    public Text energyRecoveryAllTime;

    public Text toughen;
    public Text toughenRecoveryNextTime;
    public Text toughenRecoveryAllTime;
    #endregion
    public string AssetPath = "HeadPortraitPrefabs/";

    /// <summary>
    /// 开始时注册InfoChanged函数
    /// </summary>
    void Start()
    {
        UpdateInfoShow();
        PlayerInfo.Instance.InfoChanged += InfoChanged;
    }

    void Update()
    {
        UpdateEnergyAndToughenShow();
    }

    /// <summary>
    /// 被销毁时取消注册
    /// </summary>
    void Destory()
    {
        PlayerInfo.Instance.InfoChanged -= InfoChanged;
    }

    /// <summary>
    /// 当角色信息变更时,触发此函数来更改显示的playerStatus窗口
    /// </summary>
    /// <param name="infoType"></param>
    void InfoChanged(InfoType infoType)
    {
        if (infoType == InfoType.Name || infoType == InfoType.Level || infoType == InfoType.Energy ||
            infoType == InfoType.Toughen || infoType == InfoType.HeadPortrait || infoType == InfoType.Power ||
            infoType == InfoType.Exp || infoType == InfoType.Coin || infoType == InfoType.Diamond)
            UpdateInfoShow();
    }

    void UpdateInfoShow()
    {
        PlayerInfo info = PlayerInfo.Instance;

        name.text = info.name;

        level.text = info.Level.ToString();

        energy.text = info.Energy.ToString() + "/" + info.MaxEnergy.ToString();

        toughen.text = info.Toughen + "/" + info.MaxToughen.ToString();

        //通过Prefab来获得目标精灵
        GameObject obj = Resources.Load<GameObject>(AssetPath + info.HeadPortrait);
        headPortrait.sprite = obj.GetComponent<Image>().sprite;

        power.text = info.Power.ToString();

        expText.text = info.Exp.ToString() + "/" + info.MaxExp.ToString();
        expSlider.value = info.Exp / (float)info.MaxExp;

        coin.text = info.Coin.ToString();

        diamond.text = info.Diamond.ToString();

    }

    /// <summary>
    /// 更新PlayerStatus中体力和历练的回复倒计时
    /// </summary>
    void UpdateEnergyAndToughenShow()
    {
        PlayerInfo info = PlayerInfo.Instance;
        if (info.Energy >= info.MaxEnergy)
        {
            energyRecoveryNextTime.text = "00:00:00";
            energyRecoveryAllTime.text = "00:00:00";
        }
        else
        {
            int nextReaminTime = 59 - (int)info.energyTimer;
            energyRecoveryNextTime.text = string.Format("00:00:{0}", (nextReaminTime).ToString("00"));
            int allRemainTime = 60 * (info.MaxEnergy - info.Energy - 1) + nextReaminTime;
            energyRecoveryAllTime.text = string.Format("{0}:{1}:{2}", (allRemainTime / 3600).ToString("00"), (allRemainTime / 60).ToString("00"), (allRemainTime % 60).ToString("00"));
        }

        if (info.Toughen >= info.MaxToughen)
        {
            toughenRecoveryNextTime.text = "00:00:00";
            toughenRecoveryAllTime.text = "00:00:00";
        }
        else
        {
            int nextReaminTime = 59 - (int)info.toughenTimer;
            toughenRecoveryNextTime.text = string.Format("00:00:{0}", (nextReaminTime).ToString("00"));
            int allRemainTime = 60 * (info.MaxToughen - info.Toughen - 1) + nextReaminTime;
            toughenRecoveryAllTime.text = string.Format("{0}:{1}:{2}", (allRemainTime / 3600).ToString("00"), (allRemainTime / 60).ToString("00"), (allRemainTime % 60).ToString("00"));
        }
    }
}
