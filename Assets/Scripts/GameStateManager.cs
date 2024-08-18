using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Awake() {
        _instance = this;
    }

    private GameStates _gameState;
    private Action<GameStates> _onGameStateChanged;
    private GameStateManager() {
        _gameState = GameStates.Playing;
    }
    public GameStates GetGameState() {
        return _gameState;
    }
    public void SetGameState(GameStates newState) {
        Debug.Log("Changing game state from " + _gameState + " to " + newState);
        _gameState = newState;
        Time.timeScale = newState == GameStates.Playing ? 1 : 0;
        if (_onGameStateChanged != null) {
            foreach(var callback in _onGameStateChanged.GetInvocationList()) {
                callback?.DynamicInvoke(_gameState);
            }
        }

        if (newState == GameStates.Playing) {
            SceneManager.LoadScene(_scenes.Game);
        }
        if (newState == GameStates.Menu) {
            SceneManager.LoadScene(_scenes.Menu);
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

    private readonly Scenes _scenes = new Scenes();
    private class Scenes {
        public readonly string Menu = "MainMenu";
        public readonly string Game = "Game";
    }
}