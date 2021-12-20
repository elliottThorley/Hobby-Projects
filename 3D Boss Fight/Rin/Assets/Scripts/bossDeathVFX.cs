using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDeathVFX : MonoBehaviour
{
    public GameObject piece;
    public GameObject spawnPoint;
    public bool go = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go == true) {
            for (int i = 0; i < 200; i++)
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.x = Random.Range(-360, 360);
                rotationVector.y = Random.Range(-360, 360);
                rotationVector.z = Random.Range(-360,360);
                spawnPoint.transform.rotation = Quaternion.Euler(rotationVector);
                Instantiate(piece,spawnPoint.transform.position,spawnPoint.transform.rotation);
            }
            go = false;
        }
    }
}
