using TMPro;
using UnityEngine;

public class InventoryUIController: MonoBehaviour {
    private GameObject coinCountObject;
    private TextMeshProUGUI coinCountText;
    
    private void Start() {
        coinCountObject = GameObject.Find("CoinsCountText");
        coinCountText = coinCountObject.GetComponent<TextMeshProUGUI>();
        coinCountText.SetText(InventoryManager.Instance.CoinCount.ToString());
        InventoryManager.Instance.SubscribeToCoinCountChanged(OnCoinCountChanged);
        GameStateManager.Instance.SubscribeToGameStateChanged(OnGameStateChanged);
        gameObject.SetActive(GameStateManager.Instance.GetGameState() == GameStateManager.GameStates.Playing);
    }

    private void OnGameStateChanged(GameStateManager.GameStates gameState) {
        gameObject.SetActive(gameState == GameStateManager.GameStates.Playing);
    }
    
    private void OnCoinCountChanged(int coinCount) {
        coinCountText.SetText(coinCount.ToString());
    }
}