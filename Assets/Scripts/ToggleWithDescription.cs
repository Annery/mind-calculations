using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWithDescription : MonoBehaviour
{
    [SerializeField] private Text _description = default;
    [SerializeField] private Toggle _toggle = default;

    public bool IsSelected => _toggle.isOn;
    public Operation Operation { get; private set; }

    public void Initialize(Operation operation)
    {
        Operation = operation;
        _description.text = operation.Name;
    }
}
