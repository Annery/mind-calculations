using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CampaignScreen : MonoBehaviour
{
    [SerializeField] private StartScreen _start = default;
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private LifePanel _lifePanel = default;
    [SerializeField] private Button _return = default;
    [SerializeField] private LevelSettings _level = default;
    [SerializeField] private RectTransform _levelRoot = default;

    private readonly List<LevelSettings> _listLevels = new List<LevelSettings>();

    private void Awake()
    {
        _return.ReplaceOnClick(ShowStartScreen);
        
        var configs = LevelConfigs.Instance.Levels;
        for (int i = 0; i < configs.Count; i++)
        {
            var lvl = Instantiate(_level, _levelRoot);
            lvl.Initialize(i + 1, configs[i], OnLevelButtonClick);
            lvl.LevelButton.interactable = i == 0; //TODO: hard to read, refactor
            _listLevels.Add(lvl);
        }
    }

    public void Initialize()
    {
        var completedLevels = Save.Instance.CompletedLevels;
        for (int i = 0; i < completedLevels.Count; i++)
        {
            _listLevels[i].StarsCount = completedLevels[i].StarsCount.ToString();
            if (i + 1 != _listLevels.Count) //TODO: Hook. NRE on last element
            {
                _listLevels[i + 1].LevelButton.interactable = true;
            }
        }
    }

    private void OnLevelButtonClick(LevelConfig config)
    {
        if (Save.Instance.Lives <= 0)
        {
            return;
        }
        _game.gameObject.SetActive(true);
        _game.Initialize(config, new CampaignSaveStrategy());
        gameObject.SetActive(false);
    }

    private void ShowStartScreen()
    {
        _start.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
       _lifePanel.Enable();
    }
}
