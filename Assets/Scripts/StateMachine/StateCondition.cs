using System;
using UnityEngine;

[Serializable]
internal class StateCondition : MonoBehaviour
{
    protected StateMachine _stateMachine;

    [SerializeField]        protected IState     _stateToChangeTo;
    [SerializeReference,
     ConditionTypeSelector] protected ICondition _condition;

    public IState StateToChangeTo { get => _stateToChangeTo; }

    private void OnAwake()
    {
        _stateMachine = GetComponentInParent<StateMachine>();
    }

    public virtual bool ShouldChangeState()
    {
        return _condition.Evaluate();
    }

    protected void ChangeState()
    {
        _stateMachine.ChangeToState(_stateToChangeTo);
    }
}
