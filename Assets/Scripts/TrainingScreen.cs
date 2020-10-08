using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class TrainingScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private StartScreen _startScreen = default;
    [SerializeField] private Button _start = default;
    [SerializeField] private Button _return = default;
    [SerializeField] private OperationSettings _operationSettings = default;
    [SerializeField] private RectTransform _toggleRoot = default;
    [SerializeField] private InputField _matchDuration = default;
    [SerializeField] private InputField _expressionCount = default;

    private readonly List<OperationSettings> _toggles = new List<OperationSettings>();

    private void Awake()
    {
        _start.ReplaceOnClick(StartGame);
        _return.ReplaceOnClick(ShowStartScreen);

       foreach (var operation in Utility.GetAllInstances<Operation>())
        {
            var toggle = Instantiate(_operationSettings, _toggleRoot);
            toggle.Initialize(operation);
            _toggles.Add(toggle);
        }
    }

    private void ShowStartScreen()
    {
        _startScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private static bool IsValid(string text) => text.Trim().Length > 0 && text.All(char.IsDigit);

    private void StartGame()
    {
        var selectedOperations = _toggles
            .Where(t => t.IsSelected)
            .Select(t => t.Operation)
            .ToList();

        if (selectedOperations.Count == 0 || !IsValid(_matchDuration.text) 
                                          || !IsValid(_expressionCount.text))
        {
            string log = string.Empty;
            if (selectedOperations.Count == 0)
            {
                log += "Operation is not selected" + Environment.NewLine;
            }
            if (!IsValid(_matchDuration.text))
            {
               log += "Match Duration is not valid" + Environment.NewLine;
            }
            if (!IsValid(_expressionCount.text))
            {
                log += "Max Score is not valid" + Environment.NewLine;
            }

            Debug.Log(log);
            return;
        }

        _game.Initialize(new LevelConfig(selectedOperations, 
            float.Parse(_matchDuration.text),
            int.Parse(_expressionCount.text)));
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}