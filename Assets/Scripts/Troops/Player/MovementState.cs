using UnityEngine;
using UnityEngine.Splines;

internal class MovementState : IState
{
    [SerializeField] private float     _movementSpeed = 1.5f;
    [SerializeField] private Transform _visual;

    private SplineContainer _movementSpline;
    public SplineContainer  MovementSpline
    {
        get => _movementSpline;
        set => _movementSpline = value;
    }

    public float PathProgressPecentage => _tSplineParameter;

    private float _tSplineParameter = 0.0f;
    private float _tDeltaOnMove     = 0.0f;
    private float _splineLength     = 0.0f;

    private float _tStart    = 0.0f;
    private float _direction = 1.0f;

    private Vector3 _previousPosition;

    private Troop _troop;

    private float SpeedModifier => _troop?.MovementSpeedMultiplier ?? 1.0f;

    public void NegatePath()
    {
        _tStart           =  1.0f;
        _tSplineParameter =  1.0f;
        _direction        = -1.0f;
    }

    public void Reset()
    {
        _splineLength = _movementSpline.CalculateLength();
        _tDeltaOnMove = _movementSpeed / _splineLength;

        _tSplineParameter = _tStart;
        _previousPosition = _movementSpline.EvaluatePosition(_tSplineParameter);
    }

    private void Start()
    {
        Reset();

        _troop = transform.parent.parent.GetComponent<Troop>();
    }

    public override void StateUpdate()
    {
        Vector3 lookDirection = _visual.parent.position - _previousPosition;
        if (lookDirection.sqrMagnitude > 0.0f)
        {
            _visual.rotation = Quaternion.LookRotation(lookDirection);
        }

        _previousPosition       = _visual.parent.transform.position;
        _visual.parent.position = _movementSpline.EvaluatePosition(_tSplineParameter);

        _tSplineParameter += _tDeltaOnMove * SpeedModifier * _direction * Time.deltaTime;
    }
}
