using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presenter.Manager
{
    public class GameManager : MonoBehaviour
    {
        //单例实例
        public static GameManager Instance { get; private set; }
        public LoadProgressBar LoadProgressBar;

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

        private void SetPlayerModel()
        {
            var playerMoelName = CurRole.Isman ? "Boy" : "Girl";
            var model = Resources.Load("CharcterPrefabs/" + playerMoelName);
            var playerModel = Instantiate(model,
                PlayerTransform.position, PlayerTransform.rotation) as GameObject;
            playerModel.transform.parent = PlayerTransform;
        }

        public void PassTranscript(string name)
        {
            OnPassTranscriptEvent();
        }

        public void LoadScene(string name)
        {
            DontDestroyOnLoad(PhotonEngine.Instance);
            var operation = Application.LoadLevelAsync(name);
            LoadProgressBar.Show(operation);
        }

        public event OnPassTranscriptEvent OnPassTranscriptEvent;
    }
}