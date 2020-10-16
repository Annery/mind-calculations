using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public sealed class GamePanel : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _backspace = default;
    [SerializeField] private Button _options = default;
    [SerializeField] private Button[] _numbers = default;
    [SerializeField] private Text _expression = default;
    [SerializeField] private Text _userResult = default;
    [SerializeField] private Text _score = default;
    [SerializeField] private Text _time = default;
    [SerializeField] private Slider _slider = default;
    [SerializeField] private Image _sliderFill = default;
    [SerializeField] private Gradient _sliderColor = default;

    private int _currentScore;
    private float _timeToEndMatch;
    private Operation _currentOperation;
    private LevelConfig _config;
    private SaveStrategy _save;
    private GameScreen _screen;

    public void Initialize(LevelConfig config, SaveStrategy save, GameScreen screen)
    {
        _screen = screen;
        _options.ReplaceOnClick(screen.ShowOptionsPanel);
        Initialize(config, save);
    }

    public void Initialize(LevelConfig config, SaveStrategy save)
    {
        _save = save;
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].ReplaceOnClick(() => OnButtonClick(num));
        }

        _config = config;
        _slider.maxValue = _config.MatchDuration;
        _timeToEndMatch = _config.MatchDuration;

        _clear.ReplaceOnClick(ClearUserResult);
        _backspace.ReplaceOnClick(OnBackspace);

        _currentScore = 0; //TODO: refactoring
        ShowNewExpression();
        ShowScore();
    }

    private void Update()
    {
        if (IsMatchEnded())
        {
            return;
        }
        if (IsTimerActive())
        {
            UpdateTimer();
        }
        CheckResult();
    }

    private bool IsTimerActive() => gameObject.activeSelf;

    private void GameEnd(string result) => 
        _screen.GameEnd($"You {result}{ Environment.NewLine}" + 
                        (IsWon() ? $"Score: {_currentScore}" : string.Empty) + 
                        $"{ Environment.NewLine}Stars: { _config.GetStarsByTime(_timeToEndMatch)}");

    private void CheckResult()
    {
        if (IsWon())
        {
            _save.Win(_config, _timeToEndMatch);
            GameEnd("win!");
        }
        else if (IsLost())
        {
            _save.Loose();
            GameEnd("lose");
        }
        else if (IsUserAnswerCorrect())
        {
            ShowNewExpression();
            UpdateScore();
            ShowScore();
        }
    }

    private bool IsLost() => _currentScore < _config.ExpressionCount && _timeToEndMatch <= 0;

    private bool IsWon() => _currentScore == _config.ExpressionCount && _timeToEndMatch > 0;

    private void UpdateTimer()
    {
        _timeToEndMatch -= Time.deltaTime;
        _sliderFill.color = _sliderColor.Evaluate(Mathf.Lerp(0, 1, _slider.value / _slider.maxValue));
        _time.text = $"Time: {(int)_timeToEndMatch}";
        _slider.value = (int)_timeToEndMatch;
    }

    private bool IsMatchEnded() => _timeToEndMatch <= 0;

    private void OnButtonClick(int number)
    {
        if (IsMatchEnded())
        {
            return;
        }

        int.TryParse(_userResult.text, out var result);
        if (number == 0 && string.IsNullOrEmpty(_userResult.text))
        {
            SetUserResult(number);
            return;
        }

        switch (result)
        {
            case 0 when number == 0:
                return;
            case 0 when number != 0:
                ClearUserResult();
                break;
        }

        SetUserResult(number);
    }

    private void SetUserResult(int result)
    {
        if (_userResult.text.Length < Operation.MaxAnswerLength)
        {
            _userResult.text += result.ToString();
        }
    }

    private void OnBackspace()
    {
        _userResult.text = string.IsNullOrEmpty(_userResult.text)
            ? string.Empty
            : _userResult.text.Substring(0, _userResult.text.Length - 1);
    }

    private void ShowScore() => _score.text = $"Score: {_currentScore}";

    private void UpdateScore() => _currentScore++;
    
    public void ShowNewExpression()
    {
        ClearUserResult();
        var signIndex = Random.Range(0, _config.GetOperations().Count);
        GenerateAndPrintExpression(_config.GetOperations()[signIndex]);
    }

    private bool IsUserAnswerCorrect() => int.TryParse(_userResult.text, out var result)
                                          && result == _currentOperation.Result;

    private void ClearUserResult() => _userResult.text = string.Empty;

    private void GenerateAndPrintExpression(Operation operation)
    {
        _currentOperation = operation;
        _currentOperation.GenerateExpression();
        _expression.text = $"{operation.Number1.ToString()} {operation.Name} " +
                           $"{operation.Number2.ToString()} = ?";
    }

    public void Restart() => Initialize(_config, _save);
}