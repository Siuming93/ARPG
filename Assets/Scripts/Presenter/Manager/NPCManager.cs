using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    /// <summary>
    /// NPC的管理器
    /// </summary>
    public class NpcManager : MonoBehaviour
    {
        /// <summary>
        /// NpcGameObject的列表
        /// </summary>
        public List<GameObject> NpcList;

        public static NpcManager Instance { get; private set; }

        /// <summary>
        /// NpcGameObject的字典
        /// </summary>
        private readonly Dictionary<int, GameObject> _npcDic = new Dictionary<int, GameObject>();

        private void Awake()
        {
            Instance = this;
            Init();
        }

        /// <summary>
        /// 将列表中数据挪到字典中,方便查找
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < NpcList.Count; i++)
            {
                int id = int.Parse(NpcList[i].name);
                _npcDic[id] = NpcList[i];
            }
        }

        /// <summary>
        /// 根据id获得NPC的GameObject
        /// </summary>
        /// <param name="id">Npc的id</param>
        /// <returns></returns>
        public GameObject GetNpc(int id)
        {
            GameObject obj;
            _npcDic.TryGetValue(id, out obj);
            return obj;
        }
    }
}