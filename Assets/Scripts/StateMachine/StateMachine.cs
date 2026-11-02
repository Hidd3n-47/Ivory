using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class StateMachine : MonoBehaviour
{
    [SerializeField] protected IState[]  _stateComponents;
    [Header("Debug Visual Only, Do Not Set.")]
    [SerializeField] protected IState    _activeState;

    public void ChangeToState(IState newState)
    {
        _activeState?.OnStateExit();

        _activeState = newState;

        _activeState.OnStateEnter();
    }

    private void Start()
    {
        ChangeToState(_stateComponents.FirstOrDefault());
    }

    private void Update()
    {
        _activeState?.StateUpdate();

        foreach (var condition in _activeState.Conditions)
        {
            if (condition.ShouldChangeState())
            {
                ChangeToState(condition.StateToChangeTo);
            }
        }
    }
}
