using UnityEngine;
using UnityEngine.UI;

public sealed class OptionsScreen : MonoBehaviour
{
    [SerializeField] private Button _back = default;
    [SerializeField] private Button _restart = default;
    [SerializeField] private Button _exit = default;
    [SerializeField] private StartScreen _start = default;
    [SerializeField] private GameScreen _game = default;

    private void Awake()
    {
        _back.ReplaceOnClick(ShowGameScreen);
        _restart.ReplaceOnClick(ShowNewGameScreen);
        _exit.ReplaceOnClick(ShowStartScreen);
    }

    private void ShowNewGameScreen()
    {
        _game.Restart();
        gameObject.SetActive(false);
    }

    private void ShowStartScreen()
    {
        _start.gameObject.SetActive(true);
        _game.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void ShowGameScreen()
    {
        _game.ShowNewExpression();
        gameObject.SetActive(false);
    }
}
