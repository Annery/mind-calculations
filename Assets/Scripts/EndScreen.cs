using UnityEngine;
using UnityEngine.UI;

public sealed class EndScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private Button _restart = default;
    [SerializeField] private Button _exit = default;
    [SerializeField] private Text _gameResult = default;

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _exit.onClick.AddListener(Application.Quit);
    }

    public void SetResult(string text)
    {
        _gameResult.text = text;
    }

    private void RestartGame()
    {
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _game.ResetTimer();
    }
}
