using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class StatisticsScreen : MonoBehaviour
{
    [SerializeField] private StartScreen _start = default;
    [SerializeField] private Button _return = default;
    [SerializeField] private Text _statistics = default;

    private Save _save;

    private void Awake()
    {
        _return.ReplaceOnClick(ShowStartScreen);
    }

    internal void Initialize()
    {
        _save = Resources.Load<Save>("Save");
        _statistics.text = $"TotalScore {_save.TotalScore}{Environment.NewLine}" +
                           $"BestTime {_save.BestTime}{Environment.NewLine}" +
                           $"SessionCount {_save.SessionCount}{Environment.NewLine}" +
                           $"WinCount {_save.WinCount}{Environment.NewLine}";
    }

    private void ShowStartScreen()
    {
        _start.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
