using Assets.Scripts.Presenter.Manager;
using UnityEngine;

namespace Assets.Scripts.View.Player
{
    public class PlayerNavigation : MonoBehaviour
    {
        private NavMeshAgent nav;
        private Animator _animator;

        public float MinDistance;

        private void Start()
        {
            nav = transform.GetComponent<NavMeshAgent>();
            _animator = transform.GetComponentInChildren<Animator>();
            nav.stoppingDistance = MinDistance;
        }

        private void Update()
        {
            if (nav.enabled)
            {
                if (nav.remainingDistance != 0 && nav.remainingDistance < MinDistance)
                {
                    nav.enabled = false;
                    TaskManager.Instance.OnArriveDestination();
                }
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0f || v != 0f)
            {
                nav.enabled = false;
            }
        }

        public void SetDestination(Vector3 targPos)
        {
            nav.enabled = true;
            nav.destination = targPos;
        }
    }
}