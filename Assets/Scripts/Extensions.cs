using System;
using UnityEngine.UI;

public static class Extensions
{
    public static void ReplaceOnClick(this Button button, Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClick.Invoke);
    }
}