using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

    public Transform Parent;
    public float Speed;

    void Update()
    {
        transform.RotateAround(Parent.position, Parent.up, Speed * Time.deltaTime);
    }
}
