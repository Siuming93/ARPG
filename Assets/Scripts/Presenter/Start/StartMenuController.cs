using System.Collections.Generic;
using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using Assets.Scripts.Model.Photon.Controller;
using Assets.Scripts.View.Start;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presenter.Start
{
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
        public LoginServerController LoginController;
        public RegisterServerController RegisterController;
        public RoleServerController RoleController;
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
        //加载场景的进度条
        public LoadProgressBar LoadProgressBar;
        //加载服务器列表的位置
        public GameObject ServerItem;
        public Transform ServerGridVerticalCell1;
        public Transform ServerGridVerticalCell2;
        public ServerListServerController ServerListServerController;

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
        //选择的角色
        public Role CurRole { get; private set; }


        private void Awake()
        {
            Instance = this;
            ServerListServerController.OnGetServerList += OnGetServerList;
        }

        private void Start()
        {
            RoleController.OnAddRole += OnAddRole;
            RoleController.OnGetRole += OnGetRole;
            RoleController.OnSelectRole += OnSelectRole;
        }

        private void Destroy()
        {
            RoleController.OnAddRole -= OnAddRole;
            RoleController.OnGetRole -= OnGetRole;
            RoleController.OnSelectRole -= OnSelectRole;
        }

        public void OnAddRole(Role role)
        {
            //1.设定角色
            CurRole = role;
            //2.向服务器发送选择了当前角色的请求
            RoleController.SelectRole(CurRole);
        }

        public void OnGetServerList(List<ServerProperty> serverList)
        {
            for (var i = 0; i < serverList.Count; i++)
            {
                var ip = serverList[i].Ip;
                var serverName = serverList[i].Name;
                var count = serverList[i].Count;
                var curServerItem = GameObject.Instantiate(ServerItem) as GameObject;
                curServerItem.transform.parent = i%2 == 0 ? ServerGridVerticalCell1 : ServerGridVerticalCell2;

                //设置名称和ip
                var sp = curServerItem.GetComponent<ServerUiProperty>();
                sp.Set(ip, serverName, count);
                OnServerButtonClick(sp);
            }
        }

        public void OnGetRole(List<Role> roles)
        {
            if (roles != null && roles.Count > 0)
            {
                //1.创建过角色
                CurRole = roles[0];
                UpdateCharcterSelected(CurRole);
                StartMove.MoveOut(Direction.RighttoCenter);
                CharcterSelectMove.MoveIn(Direction.LefttoCenter);
            }
            else
            {
                //2.没有创建过角色
                BackToSelectCharcterGameObject.SetActive(false);
                StartMove.MoveOut(Direction.RighttoCenter);
                CharcterChnageMove.MoveIn(Direction.LefttoCenter);
            }
        }

        /// <summary>
        /// 服务器响应了选择角色的请求
        /// </summary>
        public void OnSelectRole()
        {
            DontDestroyOnLoad(PhotonEngine.Instance);
            CharcterSelectMove.SetActiveFalse();
            CharcterChnageMove.SetActiveFalse();
            PhotonEngine.Instance.SetCurRole(CurRole);
            var operation = Application.LoadLevelAsync(Scenes.loadLevel);
            LoadProgressBar.Show(operation);
        }

        /// <summary>
        /// 选择完角色需要进入游戏时
        /// </summary>
        public void OnAfterSelectRole()
        {
            //1.向服务器发送选择了当前角色的请求
            RoleController.SelectRole(CurRole);
        }

        private void UpdateCharcterSelected(Role role)
        {
            //1.将之前的模型销毁
            Destroy(CharcterSelectedParent.GetComponentInChildren<Animator>().gameObject);

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
            //2.连接服务器,验证昵称
            RoleController.AddRole(new Role {Name = CharcterNameInputField.text, Level = 0, Isman = _index == 0});
        }

        /// <summary>
        /// 显示切换角色面板    
        /// </summary>
        public void OnChangeCharcterButtonClick()
        {
            CharcterChnageMove.MoveIn(Direction.RighttoCenter);
            CharcterSelectMove.MoveOut(Direction.LefttoCenter);
        }

        /// <summary>
        /// 显示选择角色隐藏切换角色面板    
        /// </summary>
        public void OnChangeCharcterBackButtonClick()
        {
            CharcterSelectMove.MoveIn(Direction.LefttoCenter);
            CharcterChnageMove.MoveOut(Direction.RighttoCenter);
        }

        /// <summary>
        /// 获得选中的模型id
        /// </summary>
        /// <param name="id"></param>
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
            //1.加载当前用户的角色信息
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
            //1.本地校验
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

            //2.连接服务器校验
            RegisterController.SendRegisterRequest(RegisterUsernameInputField.text,
                RegisterPasswordConfirmInputField.text);
        }

        /// <summary>
        /// 注册成功后,和和登录一个操作
        /// </summary>
        public void OnRegisterSuccess()
        {
            //1.跳转至开始界面
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
}