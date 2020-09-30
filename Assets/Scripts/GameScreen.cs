using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public sealed class GameScreen : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _backspace = default;
    [SerializeField] private Button[] _numbers = default;
    [SerializeField] private Text _expression = default;
    [SerializeField] private Text _userResult = default;
    [SerializeField] private Text _score = default;
    [SerializeField] private Text _time = default;
    [SerializeField] private Slider _slider = default;
    [SerializeField] private Image _sliderFill = default;
    [SerializeField] private Gradient _sliderColor = default;
    [SerializeField] private EndScreen _endScreen = default;

    private const float MatchDuration = 20f;
    private const int MaxScore = 3;
    private int _currentScore;
    private float _timeToEndMatch;
    private List<Operation> _signs;
    private Save _save;
    private Operation _currentOperation;

    public void Initialize(List<Operation> signs)
    {
        _save = Resources.Load<Save>("Save");

        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].ReplaceOnClick(() => OnButtonClick(num));
        }

        _slider.maxValue = MatchDuration;
        _timeToEndMatch = MatchDuration;
        _signs = signs;

        _clear.ReplaceOnClick(ClearUserResult);
        _backspace.ReplaceOnClick(OnBackspace);

        ShowNewExpression();
        ShowScore();
    }

    private void Update()
    {
        if (IsMatchEnded())
        {
            ShowEndScreen();
            return;
        }

        ShowTimer();
        UpdateTimer();
        CheckResult();
    }

    private void ShowEndScreen()
    {
        _endScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResetTimer()
    {
        ClearScore();
        ShowScore();
        ShowNewExpression();
        _timeToEndMatch = MatchDuration;
    }

    private void ShowResult(string result)
    {
        _endScreen.SetResult($"You {result}{Environment.NewLine}"
                             + (Win() ? $"Score: {_currentScore}" : string.Empty));
        ClearTimer();
    }

    private void ShowTimer()
    {
        _time.text = $"Time: {(int)_timeToEndMatch}";
        _slider.value = (int)_timeToEndMatch;
    }

    private void CheckResult()
    {
        if (Win())
        {
            _save.WinCount++;
            _save.SessionCount++;
            _save.TotalScore += MaxScore;
            if ((int)_timeToEndMatch > _save.BestTime)
            {
                _save.BestTime = (int)_timeToEndMatch;
            }
            ShowResult("win!");
        }
        else if (Lose())
        {
            _save.SessionCount++;
            ShowResult("lose");
        }
        else if (IsUserAnswerCorrect())
        {
            ShowNewExpression();
            UpdateScore();
            ShowScore();
        }
    }

    private void ClearTimer()
    {
        _timeToEndMatch = 0;
    }

    private void ClearScore()
    {
        _currentScore = 0;
    }

    private bool Lose()
    {
        return _currentScore < MaxScore && _timeToEndMatch <= 0;
    }

    private bool Win()
    {
        return _currentScore == MaxScore && _timeToEndMatch >= 0;
    }

    private void UpdateTimer()
    {
        _timeToEndMatch -= Time.deltaTime;
        _sliderFill.color = _sliderColor.Evaluate(Mathf.Lerp(0, 1, _slider.value / _slider.maxValue));
    }

    private bool IsMatchEnded()
    {
        return _timeToEndMatch <= 0;
    }

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

    private void ShowScore()
    {
        _score.text = $"Score: {_currentScore}";
    }

    private void UpdateScore()
    {
        _currentScore++;
    }

    private void ShowNewExpression()
    {
        ClearUserResult();
        var signIndex = Random.Range(0, _signs.Count);
        GenerateAndPrintExpression(_signs[signIndex]);
    }

    private bool IsUserAnswerCorrect()
    {
        return int.TryParse(_userResult.text, out var result) 
               && result == _currentOperation.Result;
    }

    private void ClearUserResult()
    {
        _userResult.text = string.Empty;
    }

    private void GenerateAndPrintExpression(Operation operation)
    {
        _currentOperation = operation;
        _currentOperation.GenerateExpression();
        _expression.text = $"{operation.Number1.ToString()} {operation.Name} " +
                           $"{operation.Number2.ToString()} = ?";
    }
}