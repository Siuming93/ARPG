using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    public UIScale StartScale;
    public UIScale LoginScale;
    public UIScale RegisterScale;
    public UIScale ServerScale;

    public UIMove CharcterSelectMove;
    public UIMove StartMove;
    public UIMove CharcterChnageMove;

    public Text Name;
    public Text Level;
    public Text newName;

    public Transform ServerGridVerticalCell1;
    public Transform ServerGridVerticalCell2;
    public Transform CharcterSelectedParent;//当前选中的角色模型的父GameObject

    public GameObject ServerSelectedButton; //已选择的服务器Button
    public GameObject ServerShowButton;     //显示服务器的按钮
    public GameObject UserNameButton;       //显示用户名称的按钮


    public GameObject[] charcterArray;

    //Prefabs
    public GameObject ServerItem;

    bool haveInitServerList = false;    //是不是已经加载过服务器了
    ServerProperty curServerInfo;       //已选择的服务器
    UIScale ChoiceCharcterUIScale;
    int index;                          //创建角色时所选中的角色的索引


    void Start()
    {
        InitServerList();
    }

    /// <summary>
    /// 创建角色确认按钮点击处理
    /// </summary>
    public void OnCharcterCreateButtonClick()
    {
        //1.连接服务器,验证昵称是否可用
        //TODO
        //2.选择了角色
        //TODO

        //3.将选中的角色替换掉
        Destroy(CharcterSelectedParent.GetComponentInChildren<Animation>().gameObject);
        GameObject newCharcter = GameObject.Instantiate(charcterArray[index], CharcterSelectedParent.position, CharcterSelectedParent.rotation) as GameObject;
        newCharcter.transform.parent = CharcterSelectedParent;

        //4.更新角色信息
        Name.text = newName.text;
        Level.text = "Lv.1";

        //5.跳转回去
        OnChangeCharcterBackButtonClick();
    }

    /// <summary>
    /// 显示切换角色面板    
    /// </summary>
    public void OnChangeCharcterButtonClick()
    {
        CharcterChnageMove.Target = CharcterSelectMove.transform.position.x;
        CharcterChnageMove.Duration = CharcterSelectMove.Duration;
        CharcterSelectMove.Target = -CharcterChnageMove.transform.position.x;

        CharcterSelectMove.MoveAndSetActive(false);
        CharcterChnageMove.MoveAndSetActive(true);
    }

    /// <summary>
    /// 显示选择角色隐藏切换角色面板    
    /// </summary>
    public void OnChangeCharcterBackButtonClick()
    {
        CharcterSelectMove.Target = CharcterChnageMove.transform.position.x;
        CharcterSelectMove.Duration = CharcterChnageMove.Duration;
        CharcterChnageMove.Target = -CharcterSelectMove.transform.position.x;

        CharcterChnageMove.MoveAndSetActive(false);
        CharcterSelectMove.MoveAndSetActive(true);
    }

    /// <summary>
    /// 选中的角色要放大显示
    /// </summary>
    /// <param name="selectedUI"></param>
    public void OnChoiceCharcterClick(GameObject selectedUI)
    {
        if (ChoiceCharcterUIScale != null)
        {
            ChoiceCharcterUIScale.Scale(new Vector3(1f, 1f, 1f), 0.5f);
        }

        if (selectedUI.name.Contains("Boy"))
            index = 0;
        else
            index = 1;

        UIScale selectedUIScale = selectedUI.GetComponent<UIScale>();
        selectedUIScale.Scale(new Vector3(1.4f, 1.4f, 1.4f), 0.5f);
        ChoiceCharcterUIScale = selectedUIScale;
    }

    /// <summary>
    /// 点击用户名,显示登陆面板
    /// </summary>
    public void OnUserNameButtonClick()
    {
        //1.跳转
        StartScale.SetActive(false);
        LoginScale.SetActive(true);
    }

    /// <summary>
    /// 点击进入游戏
    /// </summary>
    public void OnEnterGameClick()
    {
        //1.连接服务器，验证用户名和服务器
        //TODO

        //2.进入角色选择页面

        //设定移动的目标
        //float distance = StartMoveOut.transform.position.x - CharcterMoveIn.transform.position.x;
        CharcterSelectMove.Target = StartMove.transform.position.x;
        StartMove.Target = -CharcterSelectMove.transform.position.x;

        StartMove.MoveAndSetActive(false);
        CharcterSelectMove.MoveAndSetActive(true);
    }

    /// <summary>
    /// 点击登录之后
    /// </summary>
    /// <param name="text"></param>
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

    /// <summary>
    /// 关闭登录面板
    /// </summary>
    public void OnLoginPanelCloseButtonClick()
    {
        //1.跳转
        LoginScale.SetActive(false);
        StartScale.SetActive(true);
    }

    /// <summary>
    /// 显示注册面板
    /// </summary>
    public void OnRegisterShowButtonClick()
    {
        //1.跳转
        LoginScale.SetActive(false);
        RegisterScale.SetActive(true);
    }

    /// <summary>
    /// 关闭注册面板
    /// </summary>
    public void OnRegisterCloseButtonClick()
    {
        //1.跳转
        RegisterScale.SetActive(false);
        LoginScale.SetActive(true);
    }

    /// <summary>
    /// 注册账号
    /// </summary>
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

    /// <summary>
    /// 显示服务器面板
    /// </summary>
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
        ServerSelectedButton.SetActive(true);
        ServerProperty serverButtonSelectedSP = ServerSelectedButton.GetComponent<ServerProperty>();
        serverButtonSelectedSP.Set(curServerInfo.ip, curServerInfo.name, curServerInfo.count);

    }

    /// <summary>
    /// 选择当前服务器
    /// </summary>
    public void OnServerSelectedButtonClick()
    {
        //1.跳转
        ServerScale.SetActive(false);
        StartScale.SetActive(true);

        //2.设置开始面板上的服务器名称
        ServerShowButton.GetComponentInChildren<Text>().text = curServerInfo.name;
    }

    /// <summary>
    /// 初始化服务器列表
    /// </summary>
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
