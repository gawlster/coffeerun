using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObstacleSpawnerManager : MonoBehaviour {
    [SerializeField] private GameObject jumpObstaclePrefab;
    [SerializeField] private GameObject slideObstaclePrefab;
    [SerializeField] private Transform obstacleRootPoint;

    void Start() {
        ObstacleData obstacleData = getRandomObstacleData();
        if (obstacleData.Obstacle != null) {
            Instantiate(obstacleData.Obstacle, obstacleData.Position, Quaternion.identity, obstacleRootPoint);
        }
    }

    private ObstacleData getRandomObstacleData() {
        GameObject obstaclePrefab = null;
        int randomI = Random.Range(0, 3);
        switch (randomI) {
            case 1:
                obstaclePrefab = jumpObstaclePrefab;
                break;
            case 2:
                obstaclePrefab = slideObstaclePrefab;
                break;
            default:
                // If random is 0, we spawn no obstacle
                break;
        }

        float randomF = Random.Range(0f, 5f);
        Vector3 obstaclePosition = obstacleRootPoint.position + new Vector3(0, 0, randomF - 2.5f);
        
        return new ObstacleData() {
            Obstacle = obstaclePrefab,
            Position = obstaclePosition,
        };
    }

    struct ObstacleData {
        [CanBeNull] public GameObject Obstacle;
        public Vector3 Position;
    }
}