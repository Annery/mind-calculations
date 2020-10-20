using UnityEngine;

public sealed class GameScreen : MonoBehaviour
{
    [SerializeField] private OptionsPanel _optionsPanel = default;
    [SerializeField] private GamePanel _gamePanel = default;
    [SerializeField] private StartScreen _startScreen = default;
    [SerializeField] private EndScreen _endScreen = default;

    public void Initialize(LevelConfig config, SaveStrategy save)
    {
        _gamePanel.Initialize(config, save, this);
        _optionsPanel.Initialize(this);
        ShowGamePanel();
    }

    public void ShowOptionsPanel()
    {
        _optionsPanel.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
    }

    private void ShowGamePanel()
    {
        _optionsPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        _gamePanel.Resume();
        ShowGamePanel();
    }

    public void RestartGame()
    {
        _gamePanel.Restart();
        ShowGamePanel();
    }

    public void ExitGame()
    {
        _startScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GameEnd(string message)
    {
        _endScreen.SetResult(message);
        _endScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
