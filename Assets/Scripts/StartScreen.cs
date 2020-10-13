using UnityEngine;
using UnityEngine.UI;

public sealed class StartScreen : MonoBehaviour
{
    [SerializeField] private TrainingScreen _trainingScreen = default;
    [SerializeField] private StatisticsScreen _statisticsScreen = default;
    [SerializeField] private CampaignScreen _campaignScreen = default;
    [SerializeField] private Button _training = default;
    [SerializeField] private Button _campaign = default;
    [SerializeField] private Button _statistics = default;

    private void Awake()
    {
        _training.ReplaceOnClick(ShowTrainingScreen);
        _statistics.ReplaceOnClick(ShowStatisticsScreen);
        _campaign.ReplaceOnClick(ShowCampaignScreen);
    }

    private void ShowTrainingScreen()
    {
        _trainingScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ShowCampaignScreen()
    {
        _campaignScreen.gameObject.SetActive(true);
        _campaignScreen.Initialize();
        gameObject.SetActive(false);
    }

    private void ShowStatisticsScreen()
    {
        _statisticsScreen.gameObject.SetActive(true);
        _statisticsScreen.Initialize();
        gameObject.SetActive(false);
    }
}