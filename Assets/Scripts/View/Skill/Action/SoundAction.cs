using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    /// <summary>
    /// 声音行为,播放技能音效
    /// </summary>
    public class SoundAction : ActionBase
    {
        /// <summary>
        /// 技能音效
        /// </summary>
        public AudioClip AudioClip;

        /// <summary>
        /// 播放音效的对象
        /// </summary>
        private GameObject _playerGameObject;


        // Update is called once per frame
        public override ActionType ActionType
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            _playerGameObject = playerGameObject;
        }

        protected override void Play()
        {
            var audioSource = _playerGameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(AudioClip);
            Finish();
        }
    }
}