using System;
using UnityEngine;
using UnityEngine.Splines;

public class OnSplineClicked : MonoBehaviour, IOnClick
{
    [SerializeField] Spawner   _spawner;
    [SerializeField] UiManager _uiManager;

    private SplineContainer   _spline;
    private SplineInstantiate _splineInstantiate;

    private void Start()
    {
        _spline            = GetComponent<SplineContainer>();
        _splineInstantiate = GetComponent<SplineInstantiate>();
    }

    public void OnClick()
    {
        if (_spawner.SpawnTroop(_uiManager.SelectedTroopIndex, _spline))
        {
            _splineInstantiate.enabled = true;
        }
    }
}