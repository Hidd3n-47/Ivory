using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum GameState
{
    Overworld,
    Fps,
    Count
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [Header("Debug Only, Do Not Set/Change.")]
    [SerializeField] private GameState _state;

    public GameState State 
    { 
        get => _state;
        set => _state = value;
    }

    public bool IsInOverworldState => State == GameState.Overworld;
    public bool ShouldChangeBackToOverworld => !IsInOverworldState && !GetFpsTroop();

    public bool CanGoIntoFps()
    {
        var activeFpsCameraTroops = _spawner.Troops
            .SelectMany(x => x.Pool)
            .Where(t => t.gameObject.activeInHierarchy)
            .Where(t => t.FpsCamera);

        return activeFpsCameraTroops.Any();
    }

    public Troop GetFpsTroop()
    {
        return _spawner.Troops
            .SelectMany(x => x.Pool)
            .Where(t => t.gameObject.activeInHierarchy)
            .Where(t => t.FpsCamera)
            .FirstOrDefault();
    }

    public void SwitchState()
    {
        State = (GameState)(((int)State + 1) % (int)GameState.Count);
    }
}
