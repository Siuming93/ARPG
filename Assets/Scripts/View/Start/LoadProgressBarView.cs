using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Start
{
    /// <summary>
    /// 显示场景加载进度
    /// </summary>
    public class LoadProgressBarView : MonoBehaviour
    {
        public Slider ProgressSlider;
        public Text SliderText;

        private AsyncOperation _operation;

        public void Update()
        {
            //显示进度
            ProgressSlider.value = _operation.progress;
            SliderText.text = (int) (_operation.progress*100f) + "%";
        }

        /// <summary>
        /// 显示进度面板
        /// </summary>
        /// <param name="operation"></param>
        public void Show(AsyncOperation operation)
        {
            gameObject.SetActive(true);
            this._operation = operation;
        }
    }
}