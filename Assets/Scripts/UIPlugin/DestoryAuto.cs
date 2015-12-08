using UnityEngine;
using System.Collections;

public class DestoryAuto : MonoBehaviour
{
    public float WaitTime;
    // Use this for initialization
    private void Start()
    {
        StartCoroutine(DelayDestory());
    }


    private IEnumerator DelayDestory()
    {
        yield return new WaitForSeconds(WaitTime);

        Destroy(gameObject);
    }
}