using UnityEngine;
using UnityEngine.UI;

public sealed class Menu : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _enter = default;
    [SerializeField] private Button[] _numbers = default;
    [SerializeField] private Text _expression = default;
    [SerializeField] private Text _userResult = default;
    [SerializeField] private Text _score = default;
    [SerializeField] private Text _time = default;
    [SerializeField] private Slider _slider = default;

    private int _number1;
    private int _number2;
    private int _result;
    private int _currentScore;
    private float _timeRemaining = 20f;
    private const int MaxScore = 3;
    private const int MaxAnswerLength = 4;

    private void Awake()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].onClick.AddListener(() => OnButtonClick(num));
        }
        _slider.maxValue = MaxScore;

        _clear.onClick.AddListener(ClearUserResult);
        _enter.onClick.AddListener(OnEnter);

        ShowNewExpression();
        ShowScore();
    }

    private void Update()
    {
        if (IsMatchEnded())
        {
            return;
        }
        ShowTimer();
        UpdateTimer();
        CheckResult();
    }

    private void ShowTimer()
    {
        _time.text = $"Time: {(int)_timeRemaining}";
    }

    private void CheckResult()
    {
        if (Win())
        {
            ShowResult("win");
            ClearAll();
        }
        else if (Lose())
        {
            ShowResult("lose");
            ClearAll();
        }
    }

    private void ClearAll()
    {
        ClearUserResult();
        ClearScore();
        ClearTimer();
        ShowScore();
        ShowTimer();
    }

    private void ClearTimer()
    {
        _timeRemaining = 0;
    }

    private void ClearScore()
    {
        _currentScore = 0;
    }

    private void ShowResult(string result)
    {
        Debug.Log($"You {result}. Score: {_currentScore}");
    }

    private bool Lose()
    {
        return _currentScore < MaxScore && _timeRemaining <= 0;
    }

    private bool Win()
    {
        return _currentScore == MaxScore && _timeRemaining >= 0;
    }

    private void UpdateTimer()
    {
        _timeRemaining -= Time.deltaTime;
    }

    private bool IsMatchEnded()
    {
        return _timeRemaining <= 0;
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

    private void ShowScore()
    {
        _score.text = $"Score: {_currentScore}";
        _slider.value = _currentScore;
    }

    private void UpdateScore()
    {
        _currentScore++;
    }

    //TODO: green result, block input for 2 sec
    private void ShowNewExpression()
    {
        ClearUserResult();
        switch (Random.Range(0, 3))
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
            default:
                Debug.LogErrorFormat("Unsupported sign : [{0}]", sign);
                break;
        }
        _expression.text = $"{_number1.ToString()} {sign} {_number2.ToString()} = ?";
    }
}