using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    public Transform[] bossPositions;
    private int posNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, bossPositions[posNum].position, Time.deltaTime);
    }
    public void switchPosition() {
        posNum = Random.Range(0, bossPositions.Length);
    }
}
