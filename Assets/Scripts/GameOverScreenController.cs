using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {
    private void Start() {
        GameStateManager.Instance.SubscribeToGameStateChanged(OnGameStateChanged);
        gameObject.SetActive(false);
    }

    private void OnGameStateChanged(GameStateManager.GameStates gameState) {
        gameObject.SetActive(gameState == GameStateManager.GameStates.GameOver);
    }

    public void OnRestartButtonClicked() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameStates.Playing);
    }
    
    public void OnMainMenuButtonClicked() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameStates.Menu);
    }
}
