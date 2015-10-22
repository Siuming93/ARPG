using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBar : MonoBehaviour
{
    #region 控件
    public Text coinText;
    public Text diamondText;
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
            case InfoType.Coin:
                coinText.text = info.Coin.ToString();
                break;
            case InfoType.Diamond:
                diamondText.text = info.Diamond.ToString();
                break;
            default:
                break;
        }
    }
}