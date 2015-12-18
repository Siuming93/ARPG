/*逻辑层与UI层的回调事件*/

using Assets.Scripts.Model.Charcter;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public delegate void OnDeadEvent(EnemyState enemyState);

    public delegate void OnInfoChangeEvent();

    public delegate void OnTakeDamageEvent(GameObject source, string trigger);

    public delegate void OnComboAddEvent();

    public delegate void OnPassTranscriptEvent();
}