using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    public UIScalePanel StartPanel;
    public UIScalePanel LoginPanel;
    public UIScalePanel RegisterPanel;
    public UIScalePanel ServerPanel;

    public Transform ServerGridVerticalCell1;
    public Transform ServerGridVerticalCell2;

    public GameObject ServerItemRed;
    public GameObject ServerItemGreen;

    bool haveInitServerList = false;    //是不是已经加载过服务器了

    void Start()
    {
        InitServerList();
    }

    public void OnUserNameButtonClick()
    {
        //1.跳转
        StartPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    /// <summary>
    /// 按钮点击后执行的函数
    /// </summary>
    public void OnEnterGameClick()
    {
        //1.连接服务器，验证用户名和服务器
        //TODO

        //2.进入角色选择页面
        //TODO
    }

    public void OnLoginButtonClick()
    {
        //1.验证用户名和密码
        //TODO

        //2.验证成功
        //返回开始界面
        LoginPanel.SetActive(false);
        StartPanel.SetActive(true);

        //3.验证失败
        //TODO
    }

    public void OnLoginPanelCloseButtonClick()
    {
        //1.跳转
        LoginPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void OnRegisterShowButtonClick()
    {
        //1.跳转
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnRegisterCloseButtonClick()
    {
        //1.跳转
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(true);
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
        RegisterPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void OnServerShowButtonClikc()
    {
        //1.跳转
        StartPanel.SetActive(false);
        ServerPanel.SetActive(true);
    }

    /// <summary>
    /// 服务器选择按钮点击事件
    /// </summary>
    public void OnServerButtonClick(GameObject sender)
    {
        //1.记录
        //TODO
        //2.跳转
        ServerPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    IEnumerator InitServerListDelay()
    {
        while (true)
        {
            if (ServerPanel.isPlaying)
                yield return null;
            else
            {
                InitServerList();
                break;
            }
        }
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
            GameObject ServerItem;
            if (count > 50)
            {
                //火爆
                ServerItem = GameObject.Instantiate(ServerItemRed) as GameObject;

            }
            else
            {
                //流程
                ServerItem = GameObject.Instantiate(ServerItemGreen) as GameObject;
            }

            if (i % 2 == 0)
            {
                ServerItem.transform.parent = ServerGridVerticalCell1;
            }
            else
            {
                ServerItem.transform.parent = ServerGridVerticalCell2;
            }

            ServerProperty sp = ServerItem.GetComponent<ServerProperty>();
            sp.Set(ip, name);
        }
    }
}
