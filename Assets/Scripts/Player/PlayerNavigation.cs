﻿using UnityEngine;
using System.Collections;

public class PlayerNavigation : MonoBehaviour
{  
    private NavMeshAgent nav;
    private Animator animator;

    public float MinDistance;
    void Start()
    {
        nav = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
        nav.stoppingDistance = MinDistance;
    }

    void Update()
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