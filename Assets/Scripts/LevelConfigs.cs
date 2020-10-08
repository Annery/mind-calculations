using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "Persistence/LevelConfigs")]
public class LevelConfigs : ScriptableObject
{
    public List<LevelConfig> Levels;
}
