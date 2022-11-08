using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 velocity;

    void Start(){
        player=GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate(){
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0f,1.5f,-10f), ref velocity, 0.1f);
    }
}
