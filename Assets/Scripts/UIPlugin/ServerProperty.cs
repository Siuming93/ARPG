using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerProperty : MonoBehaviour
{
    public Text text;
    public Image image;   
    public Sprite ServerItemSpriteRed;
    public Sprite ServerItemSpriteGreen;

    [HideInInspector]
    public string ip = "127.0.0.1:8090";
    [HideInInspector]
    public string name = "一区 马达加斯加";
    [HideInInspector]
    public int count = 100;

    public void Set(string ip, string name, int count)
    {
        this.ip = ip;
        this.name = name;
        this.count = count;

        text.text = name;
        if (count > 50)
            image.sprite = ServerItemSpriteRed;
        else
            image.sprite = ServerItemSpriteGreen;
    }

    public void SelectButtonClick()
    {
        transform.root.SendMessage("OnServerButtonClick", this.gameObject);
    }
}
