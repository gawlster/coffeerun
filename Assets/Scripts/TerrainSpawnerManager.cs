using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject emptySectionPrefab;
    [SerializeField] private Boolean disableObstacles = false;
    [SerializeField] private int sectionsToRender = 7;
    [SerializeField] private GameObject[] sectionPrefabs; 
    
    private Queue<GameObject> terrainSections = new Queue<GameObject>();
    private GameObject lastAddedSection;
    
    void Start() {
        spawnAndDespawnSections();
    }
    
    void Update() {
        spawnAndDespawnSections();
    }
    
    private void spawnAndDespawnSections() {
        for (int i = terrainSections.Count; i < sectionsToRender; i++) {
            var section = getNewSection(terrainSections.Count == 0);
            var spawnPosition = getSpawnPosition(section);
            GameObject gameObject = Instantiate(section, spawnPosition, Quaternion.identity);
            if (terrainSections.Count >= 2) {
                var despawnController = gameObject.GetComponent<TerrainSectionDespawnController>();
                despawnController.OnCrossHalfwayPoint += () => {
                    var furthestSection = terrainSections.Dequeue();
                    Destroy(furthestSection);
                };
            }
            terrainSections.Enqueue(gameObject);
            lastAddedSection = gameObject;
        }
    }
    
    private GameObject getNewSection(Boolean forceEmptySection) {
        if (disableObstacles || forceEmptySection) {
            return emptySectionPrefab;
        }
        return sectionPrefabs[UnityEngine.Random.Range(0, sectionPrefabs.Length)];
    }
     
    private Vector3 getSpawnPosition(GameObject section) {
        if (!lastAddedSection) {
            return Vector3.zero;
        }
        return new Vector3(0, 0, lastAddedSection.transform.position.z + 30);
    }
}
