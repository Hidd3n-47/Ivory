using System;
using UnityEngine;

internal class GameplayUiButton : MonoBehaviour
{
    [SerializeField] private Transform _border;

    public int Index { get; set; }

    public void EnableBorder()
    {
        _border.gameObject.SetActive(true);
    }

    public void DisableBorder()
    {
        _border.gameObject.SetActive(false);
    }
}
