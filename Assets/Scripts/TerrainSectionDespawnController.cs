using System;
using UnityEngine;

public class TerrainSectionDespawnController: MonoBehaviour {
    public event Action OnCrossHalfwayPoint;
    private GameObject player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            OnCrossHalfwayPoint?.Invoke();
        }
    }
}