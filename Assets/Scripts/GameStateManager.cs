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
    private Action<GameStates> _onGameStateChanged;
    private GameStateManager() {
        _gameState = GameStates.Playing; // TODO change back to menu
    }
    public GameStates GetGameState() {
        return _gameState;
    }
    public void SetGameState(GameStates newState) {
        Debug.Log("Changing game state from " + _gameState + " to " + newState);
        _gameState = newState;
        Time.timeScale = newState == GameStates.Playing ? 1 : 0;
        foreach(Action<GameStates> callback in _onGameStateChanged.GetInvocationList()) {
            callback.DynamicInvoke(_gameState);
        }
    }
    public void SubscribeToGameStateChanged(Action<GameStates> callback) {
        _onGameStateChanged += callback;
    }

    public enum GameStates {
        Menu,
        Playing,
        GameOver
    }
}