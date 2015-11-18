using System.Collections.Generic;
using ARPGCommon.Model;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public static StartMenuController Instance { get; private set; }

    //UI缩放动画
    public UIScale StartScale;
    public UIScale LoginScale;
    public UIScale RegisterScale;
    public UIScale ServerScale;
    //UI移动动画
    public UIMove CharcterSelectMove;
    public UIMove StartMove;
    public UIMove CharcterChnageMove;
    //PhotonEngine的控制器
    public LoginController LoginController;
    public RegisterController RegisterController;
    public RoleController RoleController;
    //登录输入框
    public InputField UsernameInputField;
    public InputField PasswordInputField;
    //注册输入框
    public InputField RegisterUsernameInputField;
    public InputField RegisterPasswordInputField;
    public InputField RegisterPasswordConfirmInputField;
    //角色信息?
    public Text Name;
    public Text Level;
    public Text NewName;
    //创建角色时名称输入框
    public InputField CharcterNameInputField;
    //不知道了..囧
    public Transform CharcterSelectedParent; //当前选中的角色模型的父GameObject
    //已选择的服务器Button
    public GameObject ServerSelectedButton; //
    //开始面板的两个按钮
    public Text ServerShowButtonText; //显示服务器的按钮
    public Text UserNameButtonText; //显示用户名称的按钮

    /// <summary>
    /// 从创建角色返回选择角色的按钮
    /// </summary>
    public GameObject BackToSelectCharcterGameObject;

    //当前可用的角色模型
    public GameObject[] CharcterArray;
    //私有属性
    private ServerUiProperty _curServerUiInfo; //已选择的服务器
    private UIScale _choiceCharcterUiScale;
    private int _index = -1; //创建角色时所选中的角色的索引

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        RoleController.OnAddRole += OnAddRole;
        RoleController.OnGetRole += OnGetRole;
    }

    private void Destory()
    {
        RoleController.OnAddRole -= OnAddRole;
        RoleController.OnGetRole -= OnGetRole;
    }

    public void OnAddRole(Role role)
    {
    }

    public void OnGetRole(List<Role> roles)
    {
        if (roles != null && roles.Count > 0)
        {
            UpdateCharcterSelected(roles[0]);
            StartMove.MoveOut(Direction.Right);
            CharcterSelectMove.MoveIn(Direction.Left);
        }
        else
        {
            BackToSelectCharcterGameObject.SetActive(false);
            StartMove.MoveOut(Direction.Right);
            CharcterChnageMove.MoveIn(Direction.Left);
        }
    }

    private void UpdateCharcterSelected(Role role)
    {
        //1.将之前的模型销毁
        Destroy(CharcterSelectedParent.GetComponentInChildren<Animation>().gameObject);

        //2.创建新的角色模型
        var index = role.Isman ? 0 : 1;
        var newCharcter =
            Instantiate(CharcterArray[index], CharcterSelectedParent.position, CharcterSelectedParent.rotation) as
                GameObject;
        newCharcter.transform.parent = CharcterSelectedParent;

        //.3更新角色信息
        Name.text = role.Name;
        Level.text = "Lv" + role.Level;
    }

    /// <summary>
    /// 创建角色确认按钮点击处理
    /// </summary>
    public void OnCharcterCreateButtonClick()
    {
        //1.本地验证
        if (_index == -1)
        {
            MessageManger.Instance.SetMessage("请选择一个角色!");
            return;
        }
        if (CharcterNameInputField.text == "" || CharcterNameInputField.text.Length > 20)
        {
            MessageManger.Instance.SetMessage("昵称不合法!");
            return;
        }
        //2.连接服务器,验证昵称是否可用
        //TODO
        //2.选择了角色
        //TODO

        //3.将选中的角色替换掉
        UpdateCharcterSelected(new Role {Name = "0", Level = 0, Isman = _index == 0});

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
    /// 获得选中的模型id
    /// </summary>
    /// <param name="selectedUi"></param>
    public void OnChoiceCharcterClick(int id)
    {
        _index = id;
    }

    /// <summary>
    /// 选中的角色要放大显示
    /// </summary>
    /// <param name="selectedUiScale"></param>
    public void OnChoiceCharcterClick(UIScale selectedUiScale)
    {
        if (_choiceCharcterUiScale != null)
        {
            _choiceCharcterUiScale.Scale(new Vector3(1f, 1f, 1f), 0.5f);
        }
        selectedUiScale.Scale(new Vector3(1.4f, 1.4f, 1.4f), 0.5f);
        _choiceCharcterUiScale = selectedUiScale;
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


        //2.进入角色选择页面
        //3.加载角色信息
        RoleController.GetRole();

        //设定移动的目标
        //float distance = StartMoveOut.transform.position.x - CharcterMoveIn.transform.position.x;
        //CharcterSelectMove.Target = StartMove.transform.position.x;
        //StartMove.Target = -CharcterSelectMove.transform.position.x;

        //StartMove.MoveAndSetActive(false);
        //CharcterSelectMove.MoveAndSetActive(true);
    }

    /// <summary>
    /// 点击登录之后
    /// </summary>
    /// <param name="text"></param>
    public void OnLoginButtonClick(Text text)
    {
        //1.验证用户名和密码
        LoginController.LoginRequest(UsernameInputField.text, PasswordInputField.text);

        //3.验证失败
        //TODO
    }

    /// <summary>
    ///  验证成功,返回开始界面
    /// </summary>
    public void OnLoginSuccess()
    {
        //1.设定用户名称
        UserNameButtonText.text = UsernameInputField.text;
        //2.跳转到选择服务器
        LoginScale.SetActive(false);
        StartScale.SetActive(true);
        //3.选择服务器
        //TODO
    }

    public void OnSelectServer()
    {
        //1.加载角色信息
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
        if (RegisterUsernameInputField.text.Length < 3)
        {
            MessageManger.Instance.SetMessage("用户名不能少于三个字符");
            return;
        }

        if (RegisterUsernameInputField.text.Length < 3)
        {
            MessageManger.Instance.SetMessage("密码不能少于三个字符");
            return;
        }

        if (RegisterPasswordInputField.text != RegisterPasswordConfirmInputField.text)
        {
            MessageManger.Instance.SetMessage("两次输入密码不同");
            return;
        }


        //2.校验失败
        RegisterController.SendRegisterRequest(RegisterUsernameInputField.text, RegisterPasswordConfirmInputField.text);

        //3.校验成功
        //登陆TOD
    }

    /// <summary>
    /// 注册成功后,和和登录一个操作
    /// </summary>
    public void OnRegisterSuccess()
    {
        //跳转至开始界面
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
    public void OnServerButtonClick(ServerUiProperty serverUi)
    {
        //1.记录
        _curServerUiInfo = serverUi;

        //2.设置已选择的服务器
        ServerSelectedButton.SetActive(true);
        var serverButtonSelectedSp = ServerSelectedButton.GetComponent<ServerUiProperty>();
        serverButtonSelectedSp.Set(_curServerUiInfo.Ip, _curServerUiInfo.Name, _curServerUiInfo.Count);

        //3.设置开始面板上的服务器名称
        ServerShowButtonText.text = _curServerUiInfo.Name;
    }

    /// <summary>
    /// 选择当前服务器
    /// </summary>
    public void OnServerSelectedButtonClick()
    {
        //1.跳转
        ServerScale.SetActive(false);
        StartScale.SetActive(true);
    }
}