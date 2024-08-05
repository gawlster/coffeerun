using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject emptySectionPrefab;
    
    [SerializeField] private int sectionsToRender = 3;
    
    private LinkedList<GameObject> terrainSections = new LinkedList<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < sectionsToRender; i++) {
            spawnNewSection();
        }
    }
    
    private void spawnNewSection()
    {
        if (terrainSections.Count == 0) {
            terrainSections.AddFirst(Instantiate(emptySectionPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            return;
        }

        if (terrainSections.Count >= sectionsToRender) {
            GameObject firstSection = terrainSections.First.Value;
            bool firstSectionIsStillVisible = true; // TODO
            if (firstSectionIsStillVisible) {
                return;
            }
            Destroy(firstSection);
            terrainSections.RemoveFirst();
        }
        
        GameObject lastSection = terrainSections.Last.Value;
        Vector3 lastSectionPosition = lastSection.transform.position;
        Vector3 newSectionPosition = new Vector3(lastSectionPosition.x, lastSectionPosition.y, lastSectionPosition.z + 30); // TODO: Change 10 to the length of the section
        terrainSections.AddAfter(terrainSections.Last, Instantiate(emptySectionPrefab, newSectionPosition, Quaternion.identity));
    }
}
