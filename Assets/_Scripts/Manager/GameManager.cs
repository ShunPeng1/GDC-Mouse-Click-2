using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public enum GameState
    {
        GameInitial,
        GamePlaying,
        GameOver,
    }

    private GameState _currentGameState;

    
    protected override void Awake()
    {
        base.Awake();
        ChangeGameState(GameState.GameInitial);
    }

    public void ChangeGameState(GameState changeGameState)
    {
        switch (changeGameState)
        {
            case GameState.GameInitial:
                OnGameInitial();
                break;
            case GameState.GamePlaying:
                OnGamePlaying();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(changeGameState), changeGameState, null);
        }

        _currentGameState = changeGameState;
    }
    private void OnGameInitial()
    {
        Time.timeScale = 1;
    }

    
    private void OnGamePlaying()
    {
        
    }
    
    public void OnGameOver()
    {
        Time.timeScale = 0;
        SoundManager.Instance.OnGameOver();
        UIManager.Instance.OnGameOver();
    }
}
