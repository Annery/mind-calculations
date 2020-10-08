using UnityEngine;
using UnityEngine.UI;

public sealed class EndScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private StartScreen _start = default;
    [SerializeField] private Button _restart = default;
    [SerializeField] private Button _exit = default;
    [SerializeField] private Text _gameResult = default;

    private void Awake()
    {
        _restart.ReplaceOnClick(RestartGame);
        _exit.ReplaceOnClick(ShowStartScreen);
    }

    private void ShowStartScreen()
    {
        _start.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _game.ResetTimer();
    }

    public void SetResult(string textResult)
    {
        _gameResult.text = textResult;
    }

    private void RestartGame()
    {
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _game.ResetTimer();
    }
}
