using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class cubeSpawner : MonoBehaviour
{
    public GameObject cube;
    private GameObject currentCube;
    private float spawnTimer=0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > 0) { spawnTimer = spawnTimer - Time.deltaTime; }

        if (spawnTimer <= 0) {
            currentCube=Instantiate(cube);
            currentCube.transform.position = transform.position;
            spawnTimer = 3f;
        }
    }
}
