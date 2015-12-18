using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIPlugin
{
    /// <summary>
    /// 通知信息的UI管理器
    /// </summary>
    public class MessageUiManger : MonoBehaviour
    {
        public Text MessageText;

        public static MessageUiManger Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        /// <summary>
        /// 设定通知信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer">存留时间</param>
        public void SetMessage(string message, float timer = 2f)
        {
            MessageText.text = message;
            StartCoroutine(ClearMessage(timer));
        }
        /// <summary>
        /// 到时间后清空信息
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        private IEnumerator ClearMessage(float timer)
        {
            yield return new WaitForSeconds(timer);

            MessageText.text = "";
        }
    }
}