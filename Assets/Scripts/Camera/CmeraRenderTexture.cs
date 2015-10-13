using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CmeraRenderTexture : MonoBehaviour
{
    public RawImage targetImage;

    private RenderTexture rtt;

    void Start()
    {
        rtt = new RenderTexture(512, 512, 1);

        camera.targetTexture = rtt;

        targetImage.texture = camera.targetTexture;
    }
}
