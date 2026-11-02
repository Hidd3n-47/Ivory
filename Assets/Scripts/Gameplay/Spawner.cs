using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

[Serializable]
public class TroopUnit
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private string    _friendlyName  = "Troop";
    [SerializeField] private float     _spawnCooldown = 1.0f;
    [SerializeField] private int       _maxTroopCount = 1;

    public Transform   Prefab        => _prefab;
    public string      FriendlyName  => _friendlyName;
    public float       SpawnCooldown => _spawnCooldown;
    public int         MaxTroopCount => _maxTroopCount;
    public float       CooldownTimer { get; set; }
    public Troop[]     Pool          { get; set; }

    public bool TryGetNextItemFromPool(out Transform nextFreeItem)
    {
        nextFreeItem = Pool.FirstOrDefault(x => !x.gameObject.activeInHierarchy)?.transform;

        return nextFreeItem;
    }
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private TroopUnit[] _troops;
    [SerializeField] private float       _troopSpeedMultiplier = 2.0f;
    public TroopUnit[] Troops => _troops;

    private GameObject _mainCamera;
    private Clickable _clickableComponent;

    public void SpawnTroop(int poolIndex, SplineContainer path)
    {
        var selectedTroop = _troops[poolIndex];

        if (selectedTroop.CooldownTimer > 0.0f)
        {
            return;
        }

        if (selectedTroop.TryGetNextItemFromPool(out var troop))
        {
            troop.SetPositionAndRotation(transform.position, Quaternion.identity);
            troop.gameObject.SetActive(true);

            troop.GetComponentInChildren<MovementState>().MovementSpline = path;
            troop.GetComponentInChildren<Reset>().TriggerReset();

            selectedTroop.CooldownTimer = selectedTroop.SpawnCooldown;
        }
    }

    private void Awake()
    {
        foreach (var troop in _troops)
        {
            troop.Pool = new Troop[troop.MaxTroopCount];

            for (int i = 0; i < troop.MaxTroopCount; ++i)
            {
                troop.Pool[i] = Instantiate(troop.Prefab, Vector3.zero, Quaternion.identity).GetComponent<Troop>();
                troop.Pool[i].gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main.gameObject;
        _clickableComponent = GetComponent<Clickable>();
    }

    private void Update()
    {
        foreach (var troop in _troops)
        {
            troop.CooldownTimer -= Time.deltaTime;
        }
    }

    public void IncreaseTroopSpeeds()
    {
        foreach (var troopUnit in _troops)
        {
            foreach (var troop in troopUnit.Pool)
            {
                troop.MovementSpeedMultiplier = _troopSpeedMultiplier;
            }
        }
    }
}
