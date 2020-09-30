using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private StatisticsScreen _statisticsScreen = default;
    [SerializeField] private Button _start = default;
    [SerializeField] private Button _statistics = default;
    [SerializeField] private OperationSettings _operationSettings = default;
    [SerializeField] private RectTransform _toggleRoot = default;

    private readonly List<Operation> _operations = new List<Operation>
    {
        new AdditionOperation(),
        new SubtractionOperation(), 
        new MultiplicationOperation(),
        new DivisionOperation()
    };

    private readonly List<OperationSettings> _toggles = new List<OperationSettings>();

    private void Awake()
    {
        _start.ReplaceOnClick(StartGame);
        _statistics.ReplaceOnClick(ShowStatisticsScreen);

        for (int i = 0; i < _operations.Count; i++)
        {
            var toggle = Instantiate(_operationSettings, _toggleRoot);
            toggle.Initialize(_operations[i]);
            _toggles.Add(toggle);
        }
    }

    private void ShowStatisticsScreen()
    {
        _statisticsScreen.gameObject.SetActive(true);
        _statisticsScreen.Initialize();
        gameObject.SetActive(false);
    }

    private void StartGame()
    {
        var selectedOperations = _toggles
            .Where(t => t.IsSelected)
            .Select(t => t.Operation)
            .ToList();
        
        if (selectedOperations.Count == 0)
        {
            return;
        }

        _game.Initialize(selectedOperations);
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
