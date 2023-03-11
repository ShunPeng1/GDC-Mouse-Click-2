using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : PersistentSingleton<GameManager>
{
    public enum GameState
    {
        GameInitial,
        GamePlaying,
        GameOver,
    }

    private GameState _gameState;

    protected override void Awake()
    {
        base.Awake();
        _gameState = GameState.GameInitial;
    }

    public void ChangeGameState(GameState changeGameState)
    {
        switch (changeGameState)
        {
            case GameState.GameInitial:
                OnGameInitial();
                break;
            case GameState.GamePlaying:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(changeGameState), changeGameState, null);
        }
    }
    private void OnGameInitial()
    {
        
    }

    private void OnGameOver()
    {
        
    }

    private void OnGamePlaying()
    {
        
    }
    
    
}
