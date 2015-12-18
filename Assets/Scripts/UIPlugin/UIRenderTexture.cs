using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIPlugin
{
    /// <summary>
    /// 设定图片为相机的渲染结果
    /// </summary>
    public class UIRenderTexture : MonoBehaviour
    {
        /// <summary>
        /// 源相机
        /// </summary>
        public Camera OriginCamera;

        private void Start()
        {
            this.GetComponent<RawImage>().texture = OriginCamera.targetTexture;
        }
    }
}