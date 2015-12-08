using UnityEngine;

namespace Assets.Scripts.View.Skill.Action
{
    public class SoundAction : ActionBase
    {
        public AudioClip AudioClip;


        // Update is called once per frame
        public override ActionType ActionType
        {
            get { throw new System.NotImplementedException(); }
        }

        public override void Init(GameObject playerGameObject)
        {
            //throw new System.NotImplementedException();
        }

        protected override void Play()
        {
            var audioSource = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<AudioSource>();
            audioSource.PlayOneShot(AudioClip);
            Finish();
        }
    }
}