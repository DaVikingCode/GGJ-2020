using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    BaseState _currentState;

    public void Initialize()
    {

    }

    public void SwitchToState<T>() where T : BaseState
    {
        if(_currentState != null)
        {
            Destroy(_currentState);
        }

        _currentState = (T)this.gameObject.AddComponent<T>();
        _currentState.Initialize();
    }
}
