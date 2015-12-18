using Assets.Scripts.Model.Charcter;
using Assets.Scripts.Presenter.Manager.Charcter;
using UnityEngine;

namespace Assets.Scripts.View
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public class EnemyAutoBornView : MonoBehaviour
    {
        /// <summary>
        /// 总量
        /// </summary>
        public int SumCount;

        /// <summary>
        /// 同时存在的最大数量
        /// </summary>
        public int MaxCount;

        /// <summary>
        /// 生成间隔
        /// </summary>
        public float DurationTime;

        /// <summary>
        /// 敌人的预制
        /// </summary>
        public GameObject EnemyPerfab;

        /// <summary>
        /// 当前数量
        /// </summary>
        private int _curCount = 0;

        //计时器
        private float timer = 0f;

        private void Update()
        {
            //告诉管理器,这个点的怪物都死光了
            if (IsClear())
            {
                EnemyManager.Instance.RemoveEnemyBornPoint(this);
                Destroy(gameObject);
                return;
            }

            //生成怪物
            if (ShouldCreateEnemy())
            {
                var enemyObj = Instantiate(EnemyPerfab, transform.position, transform.rotation) as GameObject;
                var enemyState = enemyObj.GetComponent<EnemyState>();
                enemyState.OnDeadEvent += OnEnemyDead;
                //在管理器中注册
                EnemyManager.Instance.AddEnemy(enemyState);
                _curCount++;
                SumCount--;
            }
        }

        /// <summary>
        /// 开始计时,生成怪物
        /// </summary>
        /// <param name="enemyState"></param>
        private void OnEnemyDead(EnemyState enemyState)
        {
            timer = 0f;
            _curCount--;
        }

        /// <summary>
        /// 是否生成怪物
        /// </summary>
        /// <returns></returns>
        private bool ShouldCreateEnemy()
        {
            if (_curCount < MaxCount && timer < DurationTime)
            {
                timer += Time.deltaTime;
                return false;
            }

            return _curCount < MaxCount && SumCount > 0;
        }

        /// <summary>
        /// 当前点是否已经清空了
        /// </summary>
        /// <returns></returns>
        private bool IsClear()
        {
            return SumCount == 0 && _curCount == 0;
        }
    }
}