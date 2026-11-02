using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTroopContainer : MonoBehaviour
{
    public UnityEvent OnAllTowersAndDefencesDown;

    [SerializeField] private Transform _towersParent;
    [SerializeField] private Transform _defencesParent;

    public bool AllTowersDestroyed   => !_towers.Where(t => !t.IsDestroyed).Any();
    public bool AllDefencesDestroyed => !_defences.Where(t => !t.IsDestroyed).Any();

    private Tower[] _towers;
    private Tower[] _defences;

    private bool _allTowersAndDefencesDown = false;

    private void Start()
    {
        _towers = new Tower[_towersParent.childCount];
        for (int i = 0; i < _towersParent.childCount; ++i)
        {
            _towers[i] = _towersParent.GetChild(i).GetComponent<Tower>();
        }

        _defences = new Tower[_defencesParent.childCount];
        for (int i = 0; i < _defencesParent.childCount; ++i)
        {
            _defences[i] = _defencesParent.GetChild(i).GetComponent<Tower>();
        }
    }

    private void Update()
    {
        if(AllTowersDestroyed && AllDefencesDestroyed && !_allTowersAndDefencesDown)
        {
            OnAllTowersAndDefencesDown?.Invoke();
            _allTowersAndDefencesDown = true;
            return;
        }

        if (_allTowersAndDefencesDown && (!AllTowersDestroyed || !AllDefencesDestroyed))
        {
            _allTowersAndDefencesDown = false;
        }
    }
}
