using System.Collections;
using System.IO;
using ARPGCommon.Model;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Photon;
using Assets.Scripts.Model.Photon.Controller;
using Assets.Scripts.View.MainMenu.Knapscak;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerInfo PlayerInfo;

        public static PlayerManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void TakeDamage(int value)
        {
            PlayerInfo.TakeDamage(value);
            if (PlayerInfo.HP < 0)
            {
                OnDeadEvent(new EnemyState());
            }
        }

        public void AddInfoChangeEventToState(OnInfoChangeEvent onInfoChangeEvent)
        {
            PlayerInfo.OnInfoChangeEvent += onInfoChangeEvent;
        }

        public void DeleteInfoChangeEventToState(OnInfoChangeEvent onInfoChangeEvent)
        {
            PlayerInfo.OnInfoChangeEvent -= onInfoChangeEvent;
        }

        public event OnDeadEvent OnDeadEvent;
    }
}