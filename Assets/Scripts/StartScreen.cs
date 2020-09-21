using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private Button _start = default;
    [SerializeField] private ToggleWithDescription _toggleWithDescription = default;
    [SerializeField] private RectTransform _toggleRoot = default;

    private readonly string[] Operations = { "+", "-", "*", "/" };
    private readonly List<ToggleWithDescription> _toggles = new List<ToggleWithDescription>();

    private void Awake()
    {
        _start.ReplaceOnClick(StartGame);
        for (int i = 0; i < Operations.Length; i++)
        {
            var toggle = Instantiate(_toggleWithDescription, _toggleRoot);
            toggle.Initialize(Operations[i]);
            _toggles.Add(toggle);
        }
    }

    private void StartGame()
    {
        var operations = GetSelectedOperations();
        if (operations.Length == 0)
        {
            return;
        }
        _game.Initialize(operations);
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private string[] GetSelectedOperations()
    {
        return _toggles.Where(t => t.IsSelected)
            .Select(t => t.Description)
            .ToArray();
    }
}
