using System;
using UnityEditor;
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
        _instance._gameState = SceneManager.GetActiveScene().name == _scenes.Menu ? GameStates.Menu : GameStates.Playing;
    }

    private float _gameOverFrameCount;
    private GameStates _gameState;
    private Action<GameStates> _onGameStateChanged;
    public GameStates GetGameState() {
        return _gameState;
    }
    public void SetGameState(GameStates newState) {
        _gameState = newState;
        if (_onGameStateChanged != null) {
            foreach(var callback in _onGameStateChanged.GetInvocationList()) {
                callback?.DynamicInvoke(_gameState);
            }
        }
        
        _gameOverFrameCount = newState == GameStates.Dying ? Time.frameCount : 0;

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

    private void Update() {
        Time.timeScale = _gameState switch {
            GameStates.Menu => 0,
            GameStates.Playing => 1,
            GameStates.Dying => Mathf.MoveTowards(Time.timeScale, 0, Time.unscaledDeltaTime),
            GameStates.GameOver => 0,
            _ => throw new ArgumentOutOfRangeException()
        };
        if (_gameState == GameStates.Dying && Time.timeScale <= 0) {
            SetGameState(GameStates.GameOver);
        }
    }

    public enum GameStates {
        Menu,
        Playing,
        Dying,
        GameOver
    }

    private readonly Scenes _scenes = new Scenes();
    private class Scenes {
        public readonly string Menu = "MainMenu";
        public readonly string Game = "Game";
    }
}