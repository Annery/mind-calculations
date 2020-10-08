using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    [SerializeField] private float _matchDuration;
    [SerializeField] private int _expressionCount;
    [SerializeField] private List<OperationConfig> _operationConfigs;

    public float MatchDuration => _matchDuration;
    public int ExpressionCount => _expressionCount;

    private readonly List<Operation> _operations;

    public LevelConfig(List<Operation> operations, float matchDuration, int expressionCount)
    {
        _operations = operations;
        _matchDuration = matchDuration;
        _expressionCount = expressionCount;
    }

    public List<Operation> GetOperations() => _operations ?? _operationConfigs
        .Select(t => t.GetOperation())
        .ToList();
}