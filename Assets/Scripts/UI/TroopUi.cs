using System;
using TMPro;
using UnityEngine;

internal class TroopUi : GameplayUiButton
{
    [SerializeField] private TextMeshProUGUI _troopNameText;
    [SerializeField] private TextMeshProUGUI _troopCountText;

    public void SetTroopName(string name)
    {
        _troopNameText.text = name;
    }

    public void SetTroopCount(int count, int max)
    {
        _troopCountText.text = $"{count}/{max}";
    }
}
