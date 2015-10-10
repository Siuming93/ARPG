using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour,IPointerClickHandler
{
    public AudioClip clip;      //要播放的声音
    public AudioSource source;  //播放声音的源头

    /// <summary>
    /// 鼠标点击之后,audioSource播放声音
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}
