using System;
using UnityEngine.UI;

public static class Extensions
{
    public static void ReplaceOnClick(this Button button, Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClick.Invoke);
    }

    public static void ReplaceOnValueChanged(this Slider slider, Action<float> onValueChanged)
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(onValueChanged.Invoke);
    }
}