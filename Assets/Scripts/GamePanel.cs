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
    private Operation _currentOperation;
    private LevelConfig _config;
    private SaveStrategy _save;
    private GameScreen _screen;
    private Timer _timer;

    public void Initialize(LevelConfig config, SaveStrategy save, GameScreen screen)
    {
        _screen = screen;
        _options.ReplaceOnClick(() =>
        {
            _timer.IsPaused = true;
            screen.ShowOptionsPanel();
        });
        Initialize(config, save);
    }

    public void Initialize(LevelConfig config, SaveStrategy save)
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].ReplaceOnClick(() => OnButtonClick(num));
        }

        _save = save;
        _config = config;
        _timer = new Timer(_config.MatchDuration);
        _slider.maxValue = _config.MatchDuration;

        _clear.ReplaceOnClick(ClearUserResult);
        _backspace.ReplaceOnClick(OnBackspace);

        _currentScore = 0; //TODO: refactoring
        ShowNewExpression();
        ShowScore();
    }

    private void Update()
    {
        if (_timer.IsPaused)
        {
            return;
        }
        UpdateProgress();
        CheckResult();
    }

    private void GameEnd(string result) =>
        _screen.GameEnd($"You {result}{Environment.NewLine}" +
                        (IsWon()
                            ? $"Score: {_currentScore}{Environment.NewLine}" +
                              $"Stars: { _config.GetStarsByTime(_timer.TimeToEnd)}"
                            : string.Empty));

    private void CheckResult()
    {
        if (IsWon())
        {
            _save.Win(_config, _timer.TimeToEnd);
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

    private bool IsLost() => _currentScore < _config.ExpressionCount && _timer.TimeToEnd <= 0;

    private bool IsWon() => _currentScore == _config.ExpressionCount && _timer.TimeToEnd > 0;

    private void UpdateProgress()
    {
        _timer.Update();
        _sliderFill.color = _sliderColor.Evaluate(Mathf.Lerp(0, 1, _slider.value / _slider.maxValue));
        _time.text = $"Time: {(int)_timer.TimeToEnd}";
        _slider.value = (int)_timer.TimeToEnd;
    }

    private void OnButtonClick(int number)
    {
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

    private void ShowNewExpression()
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

    public void Resume()
    {
        _timer.IsPaused = false;
        ShowNewExpression();
    }
}