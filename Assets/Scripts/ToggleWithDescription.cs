using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWithDescription : MonoBehaviour
{
    [SerializeField] private Text _description = default;
    [SerializeField] private Toggle _toggle = default;

    public bool IsSelected => _toggle.isOn;
    public string Description => _description.text;

    public void Initialize(string description)
    {
        _description.text = description;
    }
}
