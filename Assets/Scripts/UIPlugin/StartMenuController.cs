using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    public UIScale StartScale;
    public UIScale LoginScale;
    public UIScale RegisterScale;
    public UIScale ServerScale;
    public UIMove CharcterMoveIn;
    public UIMove StartMoveOut;

    public Transform ServerGridVerticalCell1;
    public Transform ServerGridVerticalCell2;

    public GameObject ServerSelectedButton; //已选择的服务器Button
    public GameObject ServerShowButton;     //显示服务器的按钮
    public GameObject UserNameButton;       //显示用户名称的按钮

    //Prefabs
    public GameObject ServerItem;

    bool haveInitServerList = false;    //是不是已经加载过服务器了
    ServerProperty curServerInfo;       //已选择的服务器

    void Start()
    {
        InitServerList();
    }

    public void OnUserNameButtonClick()
    {
        //1.跳转
        StartScale.SetActive(false);
        LoginScale.SetActive(true);
    }

    /// <summary>
    /// 按钮点击后执行的函数
    /// </summary>
    public void OnEnterGameClick()
    {
        //1.连接服务器，验证用户名和服务器
        //TODO

        //2.进入角色选择页面

        //设定移动的距离
        //float distance = StartMoveOut.transform.position.x - CharcterMoveIn.transform.position.x;

        float target = Screen.width / 2;
        StartMoveOut.Target = Screen.width + target;
        CharcterMoveIn.Target = target;

        StartMoveOut.SetActive(true);
        CharcterMoveIn.SetActive(true);
    }

    public void OnLoginButtonClick(Text text)
    {
        //1.验证用户名和密码
        //TODO

        //2.验证成功
        //返回开始界面
        LoginScale.SetActive(false);
        StartScale.SetActive(true);
        //设定用户名称
        UserNameButton.GetComponentInChildren<Text>().text = text.text;

        //3.验证失败
        //TODO
    }

    public void OnLoginPanelCloseButtonClick()
    {
        //1.跳转
        LoginScale.SetActive(false);
        StartScale.SetActive(true);
    }

    public void OnRegisterShowButtonClick()
    {
        //1.跳转
        LoginScale.SetActive(false);
        RegisterScale.SetActive(true);
    }

    public void OnRegisterCloseButtonClick()
    {
        //1.跳转
        RegisterScale.SetActive(false);
        LoginScale.SetActive(true);
    }

    public void OnRegisterButtonClick()
    {
        //1.本地校验，连接服务器校验
        //TODO

        //2.校验失败
        //TODO

        //3.校验成功
        //登陆TOD

        //4.跳转至开始界面
        RegisterScale.SetActive(false);
        StartScale.SetActive(true);
    }

    public void OnServerShowButtonClikc()
    {
        //1.跳转
        StartScale.SetActive(false);
        ServerScale.SetActive(true);
    }

    /// <summary>
    /// 服务器选择按钮点击事件
    /// </summary>
    public void OnServerButtonClick(GameObject sender)
    {
        //1.记录
        curServerInfo = sender.GetComponent<ServerProperty>();
        Image curImage = sender.GetComponent<Image>();

        //2.设置已选择的服务器
        ServerProperty serverButtonSelectedSP = ServerSelectedButton.GetComponent<ServerProperty>();
        serverButtonSelectedSP.Set(curServerInfo.ip, curServerInfo.name, curServerInfo.count);

    }

    public void OnServerSelectedButtonClick()
    {
        //1.跳转
        ServerScale.SetActive(false);
        StartScale.SetActive(true);

        //2.设置开始面板上的服务器名称
        ServerShowButton.GetComponentInChildren<Text>().text = curServerInfo.name;
    }

    public void InitServerList()
    {
        if (haveInitServerList)
            return;
        //1.连接服务器, 获取列表信息
        //TODO
        //2.根据上面的信息初始化
        for (int i = 0; i < 20; i++)
        {
            string ip = "127.0.0.1";
            string name = (i + 1).ToString() + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject curServerItem = GameObject.Instantiate(ServerItem) as GameObject;

            if (i % 2 == 0)
            {
                curServerItem.transform.parent = ServerGridVerticalCell1;
            }
            else
            {
                curServerItem.transform.parent = ServerGridVerticalCell2;
            }

            //设置名称和ip
            ServerProperty sp = curServerItem.GetComponent<ServerProperty>();
            sp.Set(ip, name, count);
        }
    }
}
