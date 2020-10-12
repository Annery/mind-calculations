using System;
using UnityEngine;

[Serializable]
public class CompletedLevel
{
    [SerializeField] private int _starsCount = default;

    public int StarsCount
    {
        get => _starsCount;
        set => _starsCount = value;
    }

    public CompletedLevel(int starsCount)
    {
        _starsCount = starsCount;
    }
}
