using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameStateManager: MonoBehaviour {
    private static GameStateManager _instance;
    public static GameStateManager Instance {
        get {
            if (_instance == null) {
                throw new Exception("No GameStateManager instance found");
            }
            return _instance;
        }
    }
    void Awake() {
        _instance = this;
    }

    private GameStates _gameState;
    private GameStateManager() {
        _gameState = GameStates.Menu;
    }
    public GameStates GetGameState() {
        return _gameState;
    }
    public void SetGameState(GameStates newState) {
        Debug.Log("Changing game state from " + _gameState + " to " + newState);
        _gameState = newState;
    }
    
    public enum GameStates {
        Menu,
        Playing,
        GameOver
    }
}