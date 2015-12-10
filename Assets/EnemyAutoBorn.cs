using UnityEngine;
using System.Collections;
using Assets.Scripts.Presenter.Manager.Charcter;

public class EnemyAutoBorn : MonoBehaviour
{
    public int SumCount;
    public int MaxCount;
    public float DurationTime;
    public GameObject EnemyPerfab;

    private int _curCount = 0;
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

    private void OnEnemyDead(EnemyState enemyState)
    {
        timer = 0f;
        _curCount--;
    }

    private bool ShouldCreateEnemy()
    {
        if (_curCount < MaxCount && timer < DurationTime)
        {
            timer += Time.deltaTime;
            return false;
        }

        return _curCount < MaxCount && SumCount > 0;
    }

    private bool IsClear()
    {
        return SumCount == 0 && _curCount == 0;
    }
}