/*逻辑层与UI层的回调事件*/

using System.Collections;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Charcter;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public delegate void OnDeadEvent(EnemyState enemyState);

    public delegate void OnInfoChangeEvent();

    public delegate void OnTakeDamageEvent(GameObject source, string trigger);

    public delegate void OnComboAddEvent();

    public delegate void OnPassTranscriptEvent();

    public delegate void OnAddPlayerInventoryItem(InventoryItem item);

    public delegate IEnumerator DelayExcute();
}