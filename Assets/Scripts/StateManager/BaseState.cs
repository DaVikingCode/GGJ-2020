﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    protected GameManager game;

    public virtual void Initialize(params object[] arguments)
    {
        this.game = GameManager.instance;
    }

    public virtual void OnDestroy()
    {

    }

}
