using System;
using UnityEngine;

public class TerrainSectionController: MonoBehaviour {
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject coinLine;

    private GameObject floor;
    
    public event Action OnCrossHalfwayPoint;
    private GameObject player;
    
    private void Start() {
        floor = transform.GetChild(0).gameObject;
        if (obstacle != null) {
            Instantiate(obstacle, floor.transform, false);
        }

        var coinInstantiationData = new CoinLineInstantiationData();
        if (coinInstantiationData.shouldSpawn) {
            var coins = Instantiate(coinLine, floor.transform, false);
            coins.transform.position = new Vector3(coinInstantiationData.lane * 3.75f, coins.transform.position.y, coins.transform.position.z);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            OnCrossHalfwayPoint?.Invoke();
        }
    }
    
    private class CoinLineInstantiationData {
        public int lane = UnityEngine.Random.Range(-1, 2);
        public Boolean shouldSpawn = UnityEngine.Random.Range(0, 3) == 0;
    }
}