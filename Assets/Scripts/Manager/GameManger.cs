using UnityEngine;
using System.Collections;
using ARPGCommon.Model;

public class GameManger: MonoBehaviour {
    //当前角色
    private Role _curRole;
    void Awake()
    {
        _curRole = PhotonEngine.Instance.CurRole;
        //1.根据当前的Role加载模型;
        //TODO
    }
}
