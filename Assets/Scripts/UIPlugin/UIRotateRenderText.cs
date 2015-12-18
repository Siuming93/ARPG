using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UIPlugin
{
    /// <summary>
    /// 让RenderTexture的模型旋转
    /// </summary>
    public class UiRotateRenderText : MonoBehaviour, IDragHandler
    {
        public Transform Origin;

        //通过设置模型的旋转来使RenderTexture旋转
        public void OnDrag(PointerEventData eventData)
        {
            float target = Mathf.Clamp(Origin.eulerAngles.y - eventData.delta.x, 90, 280);
            Origin.eulerAngles = new Vector3(0f, target, 0f);
        }
    }
}