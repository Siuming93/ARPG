using Assets.Scripts.Presenter.Start;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Start
{
    /// <summary>
    /// 服务器属性,用来连接服务器button与服务器属性
    /// </summary>
    public class ServerUiViewProperty : MonoBehaviour
    {
        //组件信息
        public Text Text;
        public Image Image;
        public Sprite ServerItemSpriteRed;
        public Sprite ServerItemSpriteGreen;

        //服务器信息
        [HideInInspector] public string Ip = "127.0.0.1:8090";
        [HideInInspector] public string Name = "一区 马达加斯加";
        [HideInInspector] public int Count = 100;

        /// <summary>
        /// 设定显示值
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        public void Set(string ip, string name, int count)
        {
            this.Ip = ip;
            this.Name = name;
            this.Count = count;

            Text.text = name;
            Image.sprite = Count > 50 ? ServerItemSpriteRed : ServerItemSpriteGreen;
        }

        /// <summary>
        /// 服务器Button被点击之后
        /// </summary>
        public void SelectButtonClick()
        {
            StartMenuController.Instance.OnServerButtonClick(this);
        }
    }
}