using UnityEngine;
using System.Collections;

public class PlayerNavigation : MonoBehaviour
{  
    private NavMeshAgent nav;

    public float MinDistance;
    void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
        nav.stoppingDistance = MinDistance;
    }


    public void SetDestination(Vector3 targPos)
    {
        nav.destination = targPos;
    }

}
