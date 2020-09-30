using System;
using UnityEngine;
using UnityEngine.UI;

public class OperationSettings : MonoBehaviour
{
    [SerializeField] private Text _description = default;
    [SerializeField] private Toggle _toggle = default;
    [SerializeField] private Slider _slider = default;

    public bool IsSelected => _toggle.isOn;
    public Operation Operation { get; private set; }

    public void Initialize(Operation operation)
    {
        Operation = operation;
        _description.text = operation.Name;
        _slider.minValue = Operation.MinDigitValue;
        _slider.maxValue = Operation.MaxDigitValue;
        _slider.ReplaceOnValueChanged(value => operation.DigitCapacity = (int)value);
    }
}
