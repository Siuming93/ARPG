using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public List<GameObject> NPCList;

    private static NPCManager _instance;

    public static NPCManager Instance
    {
        get { return _instance; }
    }

    private Dictionary<int, GameObject> npcDic = new Dictionary<int, GameObject>();

    void Awake()
    {
        _instance = this;
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < NPCList.Count; i++)
        {
            int id = int.Parse(NPCList[i].name);
            npcDic[id] = NPCList[i];
        }
    }

    public GameObject GetNpc(int id)
    {
        GameObject obj;
        npcDic.TryGetValue(id, out obj);
        return obj;
    }
}
