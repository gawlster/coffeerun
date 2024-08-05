using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    public void OnPlayButtonClicked() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameStates.Playing);
    }
    
    public void OnExitButtonClicked() {
        Application.Quit();
    }
}
