using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerProperty : MonoBehaviour
{
    public Text text;
    public int count = 100;

    string ip = "127.0.0.1:8090";
    string name = "一区 马达加斯加";

    public void Set(string ip, string name)
    {
        this.ip = ip;
        this.name = name;

        text.text = name;
    }

    public void SelectButtonClick()
    {
        transform.root.SendMessage("OnServerButtonClick", this.gameObject);
    }
}
