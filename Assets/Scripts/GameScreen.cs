using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public sealed class GameScreen : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _enter = default;
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
    private const int MaxAnswerLength = 4;
    private int _number1;
    private int _number2;
    private int _result;
    private int _currentScore;
    private float _timeToEndMatch;

    private void Awake()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].onClick.AddListener(() => OnButtonClick(num));
        }

        _slider.maxValue = MatchDuration;
        _timeToEndMatch = MatchDuration;

        _clear.onClick.AddListener(ClearUserResult);
        _enter.onClick.AddListener(OnEnter);
        _backspace.onClick.AddListener(OnBackspace);

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
        _endScreen.SetResult($"You {result}{Environment.NewLine}Score: {_currentScore}");
        ClearTimer();
    }

    private void ShowTimer()
    {
        _time.text = $"Time: {(int) _timeToEndMatch}";
        _slider.value = (int) _timeToEndMatch;
    }

    private void CheckResult()
    {
        if (Win())
        {
            ShowResult("win!");
        }
        else if (Lose())
        {
            ShowResult("lose");
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
        if (_userResult.text.Length < MaxAnswerLength)
        {
            _userResult.text += result.ToString();
        }
    }

    private void OnEnter()
    {
        if (IsUserAnswerCorrect())
        {
            ShowNewExpression();
            UpdateScore();
            ShowScore();
        }
        else
        {
            ClearUserResult();
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
        switch (Random.Range(0, 4))
        {
            case 0:
                GenerateAndPrintExpression("+");
                break;
            case 1:
                GenerateAndPrintExpression("-");
                break;
            case 2:
                GenerateAndPrintExpression("*");
                break;
            case 3:
                GenerateAndPrintExpression("/");
                break;
        }
    }

    private bool IsUserAnswerCorrect()
    {
        return int.TryParse(_userResult.text, out var result) && result == _result;
    }

    private void ClearUserResult()
    {
        _userResult.text = string.Empty;
    }

    private void GenerateAndPrintExpression(string sign)
    {
        _number1 = Random.Range(1, 10);
        _number2 = Random.Range(1, 10);
        switch (sign)
        {
            case "+":
                _result = _number1 + _number2;
                break;
            case "-":
                if (_number1 > _number2)
                {
                    _result = _number1 - _number2;
                }
                else
                {
                    GenerateAndPrintExpression(sign);
                }

                break;
            case "*":
                _result = _number1 * _number2;
                break;
            case "/":
                if (_number1 % _number2 == 0)
                {
                    _result = _number1 / _number2;
                }
                else
                {
                    GenerateAndPrintExpression(sign);
                }

                break;
            default:
                Debug.LogErrorFormat("Unsupported sign : [{0}]", sign);
                break;
        }

        _expression.text = $"{_number1.ToString()} {sign} {_number2.ToString()} = ?";
    }
}