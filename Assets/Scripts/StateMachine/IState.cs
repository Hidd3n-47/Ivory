using System;
using UnityEngine;

[Serializable]
internal class IState : MonoBehaviour
{
    [SerializeField] private StateCondition[] _conditions;
    public StateCondition[] Conditions { get => _conditions; }

    public virtual void StateUpdate()
    {

    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }
}
