using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour,IPointerClickHandler
{
    public AudioClip clip;      //要播放的声音
   
    AudioSource source;  //播放声音的源头

    void Start()
    {
        //初始化声音播放源
        source = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<AudioSource>();
    }

    /// <summary>
    /// 鼠标点击之后,audioSource播放声音
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}
