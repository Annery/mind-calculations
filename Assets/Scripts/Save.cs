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

    public static Save Instance => Resources.Load<Save>(nameof(Save));
}