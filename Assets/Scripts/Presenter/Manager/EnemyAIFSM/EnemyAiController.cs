using UnityEngine;
using Assets.Scripts.Presenter.Manager.Charcter;
using Assets.Scripts.Presenter.Manager.EnemyAIFSM;


public class EnemyAiController : FSM
{
    /// <summary>
    /// 玩家
    /// </summary>
    private Transform _player;

    public SkillManager SkillManager;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.Player).transform;

        InitStates();
    }

    /// <summary>
    /// 初始化状态机的状态列表
    /// </summary>
    private void InitStates()
    {
        var chase = new ChaseState(new Transform[] {transform});
        chase.AddTransition(Transition.LostPlayer, FSMStateId.Patrolling);
        chase.AddTransition(Transition.ReachPlayer, FSMStateId.Attacking);
        chase.AddTransition(Transition.NoHealth, FSMStateId.Dead);

        var attack = new AttackState() {SkillManager = SkillManager};
        attack.AddTransition(Transition.LostPlayer, FSMStateId.Chasing);
        attack.AddTransition(Transition.NoHealth, FSMStateId.Dead);

        fsmStates.Add(chase);
        fsmStates.Add(attack);

        CurFsmState = chase;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CurFsmState.Reason(_player, transform);
        CurFsmState.Act(_player, transform);
    }


    public void TransitionState(Transition transition)
    {
        PerformTransition(transition);
    }
}