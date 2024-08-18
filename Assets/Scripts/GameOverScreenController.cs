using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {
    private GameObject score;
    private TextMeshProUGUI scoreText;
    
    private void Start() {
        score = GameObject.Find("GameOverScoreText");
        scoreText = score.GetComponent<TextMeshProUGUI>();
        
        GameStateManager.Instance.SubscribeToGameStateChanged(OnGameStateChanged);
        gameObject.SetActive(false);
    }

    private void OnGameStateChanged(GameStateManager.GameStates gameState) {
        scoreText.SetText($"Score: {InventoryManager.Instance.CoinCount}");
        gameObject.SetActive(gameState == GameStateManager.GameStates.GameOver);
    }

    public void OnRestartButtonClicked() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameStates.Playing);
    }
    
    public void OnMainMenuButtonClicked() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameStates.Menu);
    }
}
