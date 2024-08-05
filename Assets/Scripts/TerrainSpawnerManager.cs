using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject emptySectionPrefab;
    [SerializeField] private int sectionsToRender = 3;
    
    private Camera mainCamera;
    private Renderer renderer;
    private Queue<TerrainSection> terrainSections = new Queue<TerrainSection>();
    private float lastSectionZ = -30;
    
    void Start() {
        mainCamera = Camera.main;
        spawnAndDespawnSections();
    }
    
    void Update() {
        spawnAndDespawnSections();
    }
    
    private void spawnAndDespawnSections() {
        for (int i = terrainSections.Count; i < sectionsToRender; i++) {
            GameObject gameObject = Instantiate(emptySectionPrefab, new Vector3(0, 0, lastSectionZ + 30), Quaternion.identity);
            terrainSections.Enqueue(new TerrainSection {
                GameObject = gameObject,
            });
            lastSectionZ += 30;
        }

        TerrainSection firstSection = terrainSections.Peek();
        if (firstSection.IsBehindCamera(mainCamera)) {
            TerrainSection section = terrainSections.Dequeue();
            Destroy(section.GameObject);
        }
    }

    private struct TerrainSection {
        public GameObject GameObject;

        public bool IsBehindCamera(Camera camera, int offset = 15) {
            return GameObject.transform.position.z + offset < camera.transform.position.z;
        }
        
        public override string ToString() {
            return "GameObject: " + GameObject;
        }
    }
}
