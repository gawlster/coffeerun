using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayerController : MonoBehaviour {
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -10);
    private GameObject player;
    private Vector3 lastPos = Vector3.zero;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update() {
        var pos = player.IsDestroyed() ? lastPos : player.transform.position + offset;
        if (GameStateManager.Instance.GetGameState() == GameStateManager.GameStates.Dying) {
            pos += Vector3.up * 0.01f + Vector3.forward * 0.01f;
            transform.Rotate(Vector3.right * 0.03f);
        }
        transform.position = pos;
        lastPos = pos;
    }
}
