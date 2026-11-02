using System;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

internal class PathUi : GameplayUiButton
{
    public SplineContainer Path { get; set; }

    [SerializeField] TextMeshProUGUI _pathName;

    public void SetPathName(string pathName)
    {
        _pathName.text = pathName;
    }
}
