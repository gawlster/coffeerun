using Unity.VisualScripting;
using UnityEngine;

public class CoinController: MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            InventoryManager.Instance.CollectCoin();
            Destroy(gameObject);
        }
    }
}