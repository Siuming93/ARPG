
using UnityEngine;

class PhotonServerComponent : MonoBehaviour
{
    void Update()
    {
        PhotonEngine.Instance.Service();
    }
}

