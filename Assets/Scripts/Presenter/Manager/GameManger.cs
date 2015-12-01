using ARPGCommon.Model;
using Assets.Scripts.Model.Photon;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public class GameManger : MonoBehaviour
    {
        //单例实例
        public static GameManger Instance { get; private set; }

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
    }
}