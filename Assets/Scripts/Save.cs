using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Persistence/Save")]
public sealed class Save : ScriptableObject
{
    public int TotalScore;
    public float BestTime;
    public int SessionCount;
    public int WinCount;
}