using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Save), menuName = "Persistence/Save")]
public sealed class Save : ScriptableObject
{
    public int TotalScore;
    public float BestTime;
    public int SessionCount;
    public int WinCount;
    public List<CompletedLevel> CompletedLevels;

    //LivesTimer
    public int Lives;
    public int TotalLives;
    public float TimeToGetLife;
    public float TimeLeftToGetLife;
    public long TimeLeftGame;

    public IEnumerator RestoreLivesTracking()
    {
        RestoreLivesByLastTimeOnline();
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeLeftGame = DateTime.Now.ToFileTime();
            RestoreLivesOverTime();
        }
    }

    public static Save Instance => Resources.Load<Save>(nameof(Save));

    //TODO: refactor
    public void RestoreLivesByLastTimeOnline()
    {
        var timeDifference = (float)(DateTime.Now - DateTime.FromFileTime(TimeLeftGame)).TotalSeconds;
        if (Lives == TotalLives)
        {
            return;
        }
        while (Lives < TotalLives && timeDifference > TimeToGetLife)
        {
            Lives++;
            timeDifference -= TimeToGetLife;
        }
        if (Lives == TotalLives)
        {
            TimeLeftToGetLife = 0;
            return;
        }
        TimeLeftToGetLife = Mathf.Clamp((int)(TimeToGetLife - timeDifference), 0, TimeToGetLife);
    }
    
    //TODO: refactor
    private void RestoreLivesOverTime()
    {
        if (Lives == TotalLives)
        {
            TimeLeftToGetLife = 0;
        }
        else if (Lives < TotalLives && Mathf.Approximately(TimeLeftToGetLife, 0))
        {
            TimeLeftToGetLife = TimeToGetLife;
        }
        else if (TimeLeftToGetLife > 0)
        {
            TimeLeftToGetLife--;
            if (Lives < TotalLives && Mathf.Approximately(TimeLeftToGetLife, 0))
            {
                Lives++;
                if (Lives < TotalLives)
                {
                    TimeLeftToGetLife = TimeToGetLife;
                }
            }
        }
    }
}