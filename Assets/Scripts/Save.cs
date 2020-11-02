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

    public IEnumerator RestoreLivesTracing() //TODO: save timer progress after close game
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
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

    public static Save Instance => Resources.Load<Save>(nameof(Save));
}