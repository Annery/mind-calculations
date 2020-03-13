using UnityEngine;
using UnityEngine.UI;

public sealed class StartScreen : MonoBehaviour
{
    [SerializeField] private GameScreen _game = default;
    [SerializeField] private Button _start = default;
    
    private void Awake()
    {
        _start.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _game.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
