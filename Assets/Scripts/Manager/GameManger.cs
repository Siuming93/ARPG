using UnityEngine;
using System.Collections;
using ARPGCommon.Model;

public class GameManger : MonoBehaviour
{
    //Player模型的父物体
    public Transform PlayerTransform;
    //当前角色
    private Role _curRole;

    private void Awake()
    {
        _curRole = PhotonEngine.Instance.CurRole;
        //1.根据当前的Role加载模型;
        SetPlayerModel();
    }

    private void SetPlayerModel()
    {
        var playerMoelName = _curRole.Isman ? "Boy" : "Girl";
        var model = Resources.Load("CharcterPrefabs/" + playerMoelName);
        var playerModel = Instantiate(model,
            PlayerTransform.position, PlayerTransform.rotation) as GameObject;
        playerModel.transform.parent = PlayerTransform;
    }
}