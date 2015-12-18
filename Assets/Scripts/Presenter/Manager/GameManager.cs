using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using Assets.Scripts.View.Start;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presenter.Manager
{
    /// <summary>
    /// 游戏管理器,控制场景加载等
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        //单例实例
        public static GameManager Instance { get; private set; }
        public LoadProgressBarView LoadProgressBarView;

        //Player模型的父物体
        public Transform PlayerTransform;
        //当前角色
        public Role CurRole { get; private set; }

        private void Awake()
        {
            Instance = this;
            CurRole = PhotonEngine.Instance.CurRole;
            //1.根据当前的Role加载模型;
            SetPlayerModel();
        }

        /// <summary>
        /// 根据角色信息,为角色加载模型
        /// </summary>
        private void SetPlayerModel()
        {
            var playerMoelName = CurRole.Isman ? "Boy" : "Girl";
            var model = Resources.Load("CharcterPrefabs/" + playerMoelName);
            var playerModel = Instantiate(model,
                PlayerTransform.position, PlayerTransform.rotation) as GameObject;
            playerModel.transform.parent = PlayerTransform;
        }

        /// <summary>
        /// 选择副本加载场景
        /// </summary>
        /// <param name="name"></param>
        public void PassTranscript(string name)
        {
            OnPassTranscriptEvent();
        }

        /// <summary>
        /// 加载目标场景
        /// </summary>
        /// <param name="name"></param>
        public void LoadScene(string name)
        {
            DontDestroyOnLoad(PhotonEngine.Instance);
            var operation = Application.LoadLevelAsync(name);
            LoadProgressBarView.Show(operation);
        }

        /// <summary>
        /// 通关副本事件
        /// </summary>
        public event OnPassTranscriptEvent OnPassTranscriptEvent;
    }
}