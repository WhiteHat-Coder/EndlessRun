using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>, IUIManager
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Text scoreTxtField;
    [SerializeField] private Text currentScoreTxtField;
    [SerializeField] private Text bestScoreTxtField;
    [SerializeField] private Text totalCoinstxtField;
    [SerializeField] private int _scoreData;

    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _reStartBtn;

    [SerializeField] private int _cubesCollected;

    public void SetUp()
    {
        bind();
        _cubesCollected = DataSave.Instance.GetIntData(Constants.TotalScore);
        totalCoinstxtField.text = _cubesCollected.ToString();
    }

    private void bind()
    {
        unbind();
        _playBtn.onClick.AddListener(handlePlayBtnClicked);
        _reStartBtn.onClick.AddListener(handleRestartBtnClicked);
    }

    private void unbind()
    {
        _playBtn.onClick.RemoveAllListeners();
        _reStartBtn.onClick.RemoveAllListeners();
    }

    private void handleRestartBtnClicked()
    {
        GameController.Instance.ReStart();
    }

    private void handlePlayBtnClicked()
    {
        MainMenuPanel(false);
        GameController.Instance.Play();
    }

    public void GameOverPanel(bool status)
    {
        gameOverPanel.SetActive(status);
        totalCollectedCubes(_scoreData);
    }

    public void MainMenuPanel(bool status)
    {
        mainMenuPanel.SetActive(status);
    }

    public void SetScoreTxt(int score)
    {
        _scoreData += score;
        scoreTxtField.text = _scoreData.ToString();
        currentScoreTxtField.text = scoreTxtField.text;
        if (_scoreData > DataSave.Instance.GetIntData(Constants.BestScore))
        {
            bestScoreTxtField.text = _scoreData.ToString();
            DataSave.Instance.SetIntData(Constants.BestScore, _scoreData);
        }

        bestScoreTxtField.text = DataSave.Instance.GetIntData(Constants.BestScore).ToString();
    }

    private void totalCollectedCubes(int score)
    {
        _cubesCollected += score;
        totalCoinstxtField.text = _cubesCollected.ToString();
        DataSave.Instance.SetIntData(Constants.TotalScore, _cubesCollected);
    }
}