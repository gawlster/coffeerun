using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            GameStateManager.Instance.SetGameState(GameStateManager.GameStates.GameOver);
        }
    }
}
