using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerController : MonoBehaviour {
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -10);
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
