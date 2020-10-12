using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private Button _button = default;
    [SerializeField] private Text _levelNumber = default;
    [SerializeField] private Text _starsCount = default;

    public string StarsCount
    {
        get => _starsCount.text;
        set => _starsCount.text = $"Stars: {value}";
    }

    public Button LevelButton => _button;

    public void Initialize(int levelNumber, LevelConfig config, Action<LevelConfig> onClick)
    {
        _levelNumber.text = levelNumber.ToString();
        _button.ReplaceOnClick(() => onClick.Invoke(config));
        _starsCount.text = string.Empty;
    }
}
