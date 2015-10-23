using UnityEngine;
using System.Collections;

public class PlayerNavigation : MonoBehaviour
{
    public Vector3 targetPosition;

    private NavMeshAgent nav;

    void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.destination = targetPosition;
    }

    void OnGizmos()
    {
        if (gameObject.activeSelf)
            Gizmos.DrawLine(transform.position, nav.desiredVelocity);
    }

}
