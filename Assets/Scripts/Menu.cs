using UnityEngine;
using UnityEngine.UI;

public sealed class Menu : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _enter = default;
    [SerializeField] private Button[] _numbers = default;
    [SerializeField] private Text _expression = default;
    [SerializeField] private Text _userResult = default;

    private int _number1;
    private int _number2;
    private int _result;

    private void Awake()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].onClick.AddListener(() => OnButtonClick(num));
        }

        _clear.onClick.AddListener(ClearResult);
        _enter.onClick.AddListener(OnEnter);

       ShowNewExpression();
    }

    private void OnButtonClick(int num)
    {
        int.TryParse(_userResult.text, out int result);
        if (num == 0 && string.IsNullOrEmpty(_userResult.text))
        {
            _userResult.text += num.ToString();
            return;
        }
        switch (result)
        {
            case 0 when num == 0:
                return;
            case 0 when num != 0:
                ClearResult();
                break;
        }

        _userResult.text += num.ToString();
    }

    private void OnEnter()
    {
        if (IsUserAnswerCorrect())
        {
            ShowNewExpression();
        }
        else
        {
            ClearResult();
        }
    }

    //TODO: green result, block input for 2 sec
    private void ShowNewExpression()
    {
        ClearResult();
        switch (Random.Range(0, 2))
        {
            case 0:
                GenerateAndPrintExpression("+");
                break;
            case 1:
                GenerateAndPrintExpression("-");
                break;
        }
    }

    private bool IsUserAnswerCorrect()
    {
        return int.TryParse(_userResult.text, out var result) && result == _result;
    }

    private void ClearResult()
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
            default:
                Debug.LogErrorFormat("Unsupported sign : [{0}]", sign);
                break;
        }
        _expression.text = $"{_number1.ToString()} {sign} {_number2.ToString()} = ?";
    }
}