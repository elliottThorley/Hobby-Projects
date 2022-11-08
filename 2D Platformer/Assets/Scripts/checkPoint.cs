using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private respawnPlayer respawn;
    public float newX;
    public float newY;
    // Start is called before the first frame update
    void Start()
    {
        respawn=GameObject.FindGameObjectWithTag("Player").GetComponent<respawnPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="Player"){
            respawn.x=newX;
            respawn.y=newY;
        }
    }
}
