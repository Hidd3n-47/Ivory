using UnityEngine;
using UnityEngine.Splines;

public class BaseSpawning : MonoBehaviour
{
    [SerializeField] private Transform _troopPrefab;
    [SerializeField] private SplineContainer[] _paths;
    [SerializeField] private float _spawnCooldown = 5.0f;
    [SerializeField] private bool _startAtEnd = false;

    private float _spawnTimer;

    private void Awake()
    {
        _spawnTimer = _spawnCooldown;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer > 0.0f)
        {
            return;
        }

        foreach (var path in _paths)
        {
            var troop = Instantiate(_troopPrefab, transform.position, Quaternion.identity);
            var move = troop.GetComponentInChildren<MovementState>();
            move.MovementSpline = path;

            if (_startAtEnd)
            {
                move.NegatePath();
            }
        }

        _spawnTimer = _spawnCooldown;
    }
}
