using UnityEngine;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour
{
    public abstract void OnOperationResponse(OperationResponse response);
}