using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Model;
using Assets.Scripts.Presenter.Manager;

public class TopBar : MonoBehaviour
{
    #region 控件

    public Text CoinText;
    public Text DiamondText;

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

        CoinText.text = info.Coin.ToString();
        DiamondText.text = info.Diamond.ToString();
    }
}