using System;
using UnityEngine;

public class InventoryManager: MonoBehaviour {
    private static InventoryManager instance;
    public static InventoryManager Instance {
        get {
            if (instance == null) {
                throw new Exception("No instance of InventoryManager found");
            }
            return instance;
        }
    }
    
    private void Awake() {
        instance = this;
    }

    public int CoinCount = 0;
    public void CollectCoin() {
        CoinCount++;
        notifyCoinCountChanged();
    }
   private Action<int> _onCoinCountChanged; 
    public void SubscribeToCoinCountChanged(Action<int> callback) {
        _onCoinCountChanged += callback;
    }
    
    private void notifyCoinCountChanged() {
        if (_onCoinCountChanged != null) {
            foreach(var callback in _onCoinCountChanged.GetInvocationList()) {
                callback?.DynamicInvoke(CoinCount);
            }
        }
    }
}