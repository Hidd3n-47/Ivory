using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private Vector2   _padding;

    [Header("Troops")]
    [SerializeField] private Spawner   _spawner;
    [SerializeField] private Transform _troopUiPrefab;
    private TroopUi[] _troopUiButtons;
    private int _selectedTroopButton = 0;

    [Header("Path")]
    [SerializeField] private Transform _pathsParent;
    [SerializeField] private Transform _pathUiPrefab;
    private PathUi[] _pathUiButtons;
    private int _selectedPathButton = 0;


    void Start()
    {
        _troopUiButtons = new TroopUi[_spawner.Troops.Length];
        for (int i = 0; i < _spawner.Troops.Length; ++i)
        {
            TroopUnit troop = _spawner.Troops[i];

            RectTransform troopRect = Instantiate(_troopUiPrefab, Vector3.zero, Quaternion.identity, _canvas) as RectTransform;
            TroopUi       troopUi   = troopRect.GetComponent<TroopUi>();
            troopUi.Index = i;

            troopRect.GetComponent<Button>().onClick.AddListener(() => OnTroopButtonPress(troopUi.Index));

            troopUi.SetTroopName(troop.FriendlyName);
            troopUi.SetTroopCount(troop.MaxTroopCount, troop.MaxTroopCount);

            troopRect.anchoredPosition = new Vector3(-(_padding.x * (i + 1) + troopRect.sizeDelta.x * i), _padding.y, 0.0f);

            _troopUiButtons[i] = troopUi;
        }

        _troopUiButtons.First()?.EnableBorder();

        _pathUiButtons = new PathUi[_pathsParent.childCount];
        for (int i = 0; i < _pathsParent.childCount; ++i)
        {
            RectTransform pathRect = Instantiate(_pathUiPrefab, Vector3.zero, Quaternion.identity, _canvas) as RectTransform;
            PathUi        pathUi   = pathRect.GetComponent<PathUi>();
            pathUi.Index = i;
            pathUi.Path  = _pathsParent.GetChild(i).GetComponent<SplineContainer>();

            pathRect.GetComponent<Button>().onClick.AddListener(() => OnPathButtonPress(pathUi.Index, pathUi.Path));

            pathUi.SetPathName($"Lane {i + 1}");

            pathRect.anchoredPosition = new Vector3(_padding.x * (i + 1) + pathRect.sizeDelta.x * i, _padding.y, 0.0f);

            _pathUiButtons[i] = pathUi;
        }

        _pathUiButtons.First()?.EnableBorder();
    }

    void Update()
    {
        for (int i = 0; i < _spawner.Troops.Length; ++i)
        {
            TroopUnit troop = _spawner.Troops[i];

            int activeCount = troop.Pool.Where(t => t.gameObject.activeInHierarchy).Count();

            _troopUiButtons[i].SetTroopCount(troop.MaxTroopCount - activeCount, troop.MaxTroopCount);
        }
    }

    private void OnTroopButtonPress(int selectedTroopIndex)
    {
        _troopUiButtons[_selectedTroopButton].DisableBorder();

        _selectedTroopButton = selectedTroopIndex;

        _troopUiButtons[_selectedTroopButton].EnableBorder();
    }

    private void OnPathButtonPress(int selectedPathIndex, SplineContainer path)
    {
        if (selectedPathIndex == _selectedPathButton)
        {
            _spawner.SpawnTroop(_selectedTroopButton, path);
            return;
        }


        _pathUiButtons[_selectedPathButton].DisableBorder();

        _selectedPathButton = selectedPathIndex;

        _pathUiButtons[_selectedPathButton].EnableBorder();
    }
}
