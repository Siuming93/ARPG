using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UIPlugin
{
    /// <summary>
    /// Ui的拖拽组件
    /// </summary>
    public class UiDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// 是否正拖拽
        /// </summary>
        private bool isDrag = false;

        /// <summary>
        /// 鼠标位置与对象位置的偏移
        /// </summary>
        private Vector3 offsetVector3;

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            isDrag = true;
            offsetVector3 = Input.mousePosition - transform.position;
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            isDrag = false;
        }

        private void Update()
        {
            if (!isDrag)
                return;

            transform.position = Input.mousePosition - offsetVector3;
        }
    }
}