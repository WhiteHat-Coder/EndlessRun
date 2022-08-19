using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>, IGameController
{
    [SerializeField] private UIManager _uiManager;
    public GameState _gameState { get; private set; }
    private PlayerController _playerController;

    private void Start()
    {
        _gameState = GameState.Idle;
    }

    public void SetUp()
    {
        _uiManager.SetUp();
       PoolObjects.Instance.SpawnGround();
    }

    private void bind()
    {
        unbind();
        _playerController.OnCollectedGreenCube += handleGreenCubeCollect;
        _playerController.OnCollectedRedCube += handleRedCubeCollect;
    }
    
    private void unbind()
    {
        _playerController.OnCollectedGreenCube -= handleGreenCubeCollect;
        _playerController.OnCollectedRedCube -= handleRedCubeCollect;
    }

    private void handleRedCubeCollect()
    {
       _gameState = GameState.Over;
       UIManager.Instance.GameOverPanel(true);
    }

    private void handleGreenCubeCollect(int value)
    {
        UIManager.Instance.SetScoreTxt(value);
    }

    public void Play()
    {
        _gameState = GameState.Play;
        var playerObj = PoolObjects.Instance.SpawnPlayer();
        _playerController = playerObj.GetComponent<PlayerController>();
        _playerController.SetUp();
        bind();
    }

    public void ReStart()
    {
        LoadScene(Constants.Game);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}