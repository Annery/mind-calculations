using UnityEngine;
using UnityEngine.UI;

public sealed class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Button _back = default;
    [SerializeField] private Button _restart = default;
    [SerializeField] private Button _exit = default;

    public void Initialize(GameScreen screen)
    {
        _back.ReplaceOnClick(screen.ResumeGame);
        _restart.ReplaceOnClick(screen.RestartGame);
        _exit.ReplaceOnClick(screen.ExitGame);
    }
}
