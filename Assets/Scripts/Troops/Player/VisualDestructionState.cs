using System.Collections;
using UnityEngine;

internal class VisualDestructionState : IState
{
    [SerializeField] private Transform _visual;

    public override void OnStateEnter()
    {
        _visual.gameObject.SetActive(true);
    }

    public override void OnStateExit()
    {
        _visual.gameObject.SetActive(false);
    }
}
