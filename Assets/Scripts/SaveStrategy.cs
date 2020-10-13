using System.Collections.Generic;
using UnityEngine;

public abstract class SaveStrategy
{
    private readonly Save _save;
    private readonly List<LevelConfig> _levelsConfigs;

    protected SaveStrategy()
    {
        _save = Save.Instance;
        _levelsConfigs = LevelConfigs.Instance.Levels;
    }

    public void Win(LevelConfig config, float timeToEndMatch)
    {
        SaveGlobalProgress(config, timeToEndMatch);
        SaveLevelProgress(_levelsConfigs.IndexOf(config), config.GetStarsByTime(timeToEndMatch));
    }

    public void Loose()
    {
        _save.SessionCount++;
    }

    private void SaveGlobalProgress(LevelConfig config, float timeToEndMatch)
    {
        _save.WinCount++;
        _save.TotalScore += config.ExpressionCount;
        _save.SessionCount++;
        if (config.MatchDuration - (int)timeToEndMatch < _save.BestTime
            || Mathf.Approximately(_save.BestTime, 0))
        {
            _save.BestTime = config.MatchDuration - (int)timeToEndMatch;
        }
    }
    
    protected virtual void SaveLevelProgress(int levelIndex, int starsCount)
    {
        if (levelIndex > _save.CompletedLevels.Count - 1)
        {
            _save.CompletedLevels.Add(new CompletedLevel(starsCount));
        }
        else
        {
            if (_save.CompletedLevels[levelIndex].StarsCount < starsCount)
            {
                _save.CompletedLevels[levelIndex].StarsCount = starsCount;
            }
        }
    }
}
