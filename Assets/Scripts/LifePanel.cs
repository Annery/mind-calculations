using System;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    [SerializeField] private Text _life = default;
    
    private Save _save;

    private void Update()
    {
        UpdatePanel();
    }

    public void Enable()
    {
        _save = Save.Instance;
    }

    private void UpdatePanel()
    {
        _life.text = $"Life: {_save.Lives}";
        if (_save.Lives < _save.TotalLives)
        {
            _life.text += $"{Environment.NewLine}Time: {(int)_save.TimeLeftToGetLife}";
        }
    }
}
