using UnityEngine;
using UnityEngine.UI;

public sealed class Menu : MonoBehaviour
{
    [SerializeField] private Button _clear = default;
    [SerializeField] private Button _enter = default;
    [SerializeField] private Button[] _numbers = default;
    [SerializeField] private Text _expression = default;
    [SerializeField] private Text _result = default;

    private int _term1;
    private int _term2;

    private void Awake()
    {
        for (var i = 0; i < _numbers.Length; i++)
        {
            var num = i;
            _numbers[i].onClick.AddListener(() => { _result.text += num.ToString(); });
        }

        _clear.onClick.AddListener(ClearResult);
        _enter.onClick.AddListener(OnEnter);

        ClearResult();
        ShowExpression();
    }

    private void OnEnter()
    {
        if (IsUserInputValid())
        {
            ShowNewExpression();
        }
    }

    //TODO: green result, block input for 2 sec
    private void ShowNewExpression()
    {
        ClearResult();
        ShowExpression();
    }

    private bool IsUserInputValid()
    {
        return int.TryParse(_result.text, out var result) && result == _term1 + _term2;
    }

    private void ClearResult()
    {
        _result.text = string.Empty;
    }

    private void ShowExpression()
    {
        _term1 = Random.Range(1, 10);
        _term2 = Random.Range(1, 10);
        _expression.text = $"{_term1.ToString()} + {_term2.ToString()} = ?";
    }
}