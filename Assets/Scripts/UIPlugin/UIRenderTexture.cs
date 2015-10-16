using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRenderTexture : MonoBehaviour
{
    public Camera OriginCamera;

    void Start()
    {
        this.GetComponent<RawImage>().texture = OriginCamera.targetTexture;
    }

}
