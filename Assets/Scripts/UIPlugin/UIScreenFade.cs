using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using DG.Tweening;

public class UIScreenFade : MonoBehaviour
{
    public Image Image;
    public float Duration;
    public Color _targetColor;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        _targetColor = Color.clear;
        StartCoroutine(ColorChange());
    }

    public void FadeOut()
    {
        _targetColor = Color.black;
        StartCoroutine(ColorChange());
    }

    public void DamageFlash()
    {
    }

    private IEnumerator ColorChange()
    {
        while (Mathf.Abs(Image.color.a - _targetColor.a) > 0.1f)
        {
            Image.color = Color.Lerp(Image.color, _targetColor, Duration);
            yield return 0;
        }

        Image.color = _targetColor;
        gameObject.SetActive(false);
    }
}