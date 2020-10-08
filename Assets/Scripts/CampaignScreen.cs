using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignScreen : MonoBehaviour
{
    [SerializeField] private StartScreen _start = default;
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private Button _return = default;
    [SerializeField] private LevelSettings _level = default;
    [SerializeField] private RectTransform _levelRoot = default;

    private void Awake()
    {
        _return.ReplaceOnClick(ShowStartScreen);

        var configs = Resources.Load<LevelConfigs>("LevelConfigs").Levels;
        for (int i = 0; i < configs.Count; i++)
        {
            Instantiate(_level, _levelRoot)
                .Initialize(i + 1, configs[i], OnLevelButtonClick);
        }
    }

    private void OnLevelButtonClick(LevelConfig config)
    {
        _game.gameObject.SetActive(true);
        _game.Initialize(config);
        gameObject.SetActive(false);
    }

    private void ShowStartScreen()
    {
        _start.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
