using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelConfigs), menuName = "Persistence/LevelConfigs")]
public class LevelConfigs : ScriptableObject
{
    public List<LevelConfig> Levels;

    public static LevelConfigs Instance => Resources.Load<LevelConfigs>(nameof(LevelConfigs));
}
