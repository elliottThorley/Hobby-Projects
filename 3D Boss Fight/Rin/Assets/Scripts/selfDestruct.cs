using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float timeTilDeath;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTilDeath > 0) { timeTilDeath -= Time.deltaTime; }
        if (timeTilDeath <= 0) { Destroy(gameObject); }
    }
}
