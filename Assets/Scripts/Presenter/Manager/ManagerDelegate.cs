﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Presenter.Manager
{
    public delegate void OnDeadEvent(EnemyState enemyState);

    public delegate void OnInfoChangeEvent();

    public delegate void OnTakeDamageEvent(GameObject source, string trigger);

    public delegate void OnComboAddEvent();

    public delegate void OnPassTranscriptEvent();
}