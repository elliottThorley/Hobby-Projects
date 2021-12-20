using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeThrower : MonoBehaviour
{
    public float timer;
    private float time;
    private GameObject currentCube;
    private GameObject currentCube2;
    public GameObject cube;
    public GameObject[] spawnPoints;
    private int i;
    private int j;
    // Start is called before the first frame update
    void Start()
    {
        time = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) { time = time - Time.deltaTime; }
        if (time <= 0)
        {
            currentCube = Instantiate(cube);
            currentCube2 = Instantiate(cube);
            i =Random.Range(0, spawnPoints.Length);
            j = Random.Range(0, spawnPoints.Length);
            while (i == j)
            {
                j = Random.Range(0, spawnPoints.Length);
            }

            currentCube.transform.position = spawnPoints[i].transform.position;
            currentCube2.transform.position = spawnPoints[j].transform.position;
            time = 1f ;
        }
    }
}
