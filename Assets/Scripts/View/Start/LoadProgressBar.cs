using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadProgressBar : MonoBehaviour
{
    public Slider ProgressSlider;
    public Text SliderText;

    private AsyncOperation _operation;

    public void Update()
    {
        ProgressSlider.value = _operation.progress;
        SliderText.text = (int) (_operation.progress*100f) + "%";
    }

    public void Show(AsyncOperation operation)
    {
        gameObject.SetActive(true);
        this._operation = operation;
    }
}