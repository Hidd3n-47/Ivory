using UnityEngine;

internal class FpsState : IState
{
    [SerializeField] private Spawner          _spawner;
    [SerializeField] private GameStateManager _gameStateManager;

    private FpsShootingInfo _shootingInfo;
    private FpsArmAnimation _armAnimation;
    private FpsCamera       _fpsCamera;
    private FpsCrosshair    _fpsCrosshair;

    private void Start()
    {
        _shootingInfo = GetComponent<FpsShootingInfo>();
        _armAnimation = GetComponent<FpsArmAnimation>();
        _fpsCamera    = GetComponent<FpsCamera>();
        _fpsCrosshair = GetComponent<FpsCrosshair>();
    }

    public override void OnStateEnter()
    {
        var troop = _gameStateManager.GetFpsTroop();

        troop.FpsCamera.gameObject.SetActive(true);

        _armAnimation.Setup(troop.ArmRotation);
        _fpsCamera.Setup(troop);
        _fpsCrosshair.Activate();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void StateUpdate()
    {
        if (Input.GetMouseButtonDown(1) || _gameStateManager.ShouldChangeBackToOverworld)
        {
            _gameStateManager.SwitchState();
        }

        _shootingInfo.UpdateShootingInfo();
        _armAnimation.UpdateAnimation();
        _fpsCamera.UpdateCamera();
        _fpsCrosshair.UpdateCrosshair();
    }

    public override void OnStateExit()
    {
        Cursor.lockState = CursorLockMode.None;

        _fpsCrosshair.Deactivate();

        var fps = _gameStateManager.GetFpsTroop();
        if (fps)
        {
            fps.FpsCamera.gameObject.SetActive(false);
        }

    }
}
