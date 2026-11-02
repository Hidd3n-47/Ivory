using UnityEngine;

internal class OverworldState : IState
{
    [SerializeField] private Transform        _overworldCamera;
    [SerializeField] private GameStateManager _gameStateManager;

    public override void OnStateEnter()
    {
        _overworldCamera.gameObject.SetActive(true);
    }

    public override void StateUpdate()
    {
        if (Input.GetMouseButtonDown(1) && _gameStateManager.CanGoIntoFps())
        {
            _gameStateManager.SwitchState();
        }
    }

    public override void OnStateExit()
    {
        _overworldCamera.gameObject.SetActive(false);
    }
}
