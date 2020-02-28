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
    [SerializeField] private Slider _slider = default;

    private int _number1;
    private int _number2;
    private int _result;
    private int _counterClick;
    private int _currentScore;
    private const int MaxScore = 10;
    private const int MaxAnswerLength = 4;

    private void Awake()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].onClick.AddListener(() => OnButtonClick(num));
        }

        _clear.onClick.AddListener(Clear);
        _enter.onClick.AddListener(OnEnter);

        ShowNewExpression();
        ShowAndUpdateScore();
    }

    private void OnButtonClick(int number)
    {
        int.TryParse(_userResult.text, out var result);
        if (number == 0 && string.IsNullOrEmpty(_userResult.text))
        {
            SetUserResult(ref number);
            return;
        }
        switch (result)
        {
            case 0 when number == 0:
                return;
            case 0 when number != 0:
                Clear();
                break;
        }
        SetUserResult(ref number);
    }

    private void SetUserResult(ref int result)
    {
        if (_counterClick < MaxAnswerLength)
        {
            _userResult.text += result.ToString();
            _counterClick++;
        }
    }

    private void OnEnter()
    {
        if (IsUserAnswerCorrect())
        {
            ShowNewExpression();
            ShowAndUpdateScore();
        }
        else
        {
            Clear();
        }
    }

    private void ShowAndUpdateScore()
    {
        _score.text = $"Score: {_currentScore}";
        _slider.value = _currentScore /(float) MaxScore;
        _currentScore++;
    }

    //TODO: green result, block input for 2 sec
    private void ShowNewExpression()
    {
        Clear();
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

    private void Clear()
    {
        _userResult.text = string.Empty;
        _counterClick = 0;
        if (_currentScore == MaxScore)
        {
            _currentScore = 0;
        }
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