using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawner : MonoBehaviour
{
    public GameObject[] clouds;
    private GameObject[] madeClouds;
    public float cloudWidth;
    public float cloudHeight;
    private Vector2 spawnPoint=new Vector3(-30f,-16f);
    private float widthSpace=4f;
    private float heightSpace=3f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        for(int i=1;i<cloudWidth+1;i++){
            for(int ii=1;ii<cloudHeight+1;ii++){
                Instantiate(clouds[Random.Range(0,clouds.Length)],new Vector2((spawnPoint.x+Random.Range(-2,2))+(i*widthSpace),(spawnPoint.y+Random.Range(-2,2))+(ii*heightSpace)),Quaternion.identity,transform);
            }
        }
    }
    void Update(){
        rb.velocity = new Vector2(-0.5f,0);
    }
}
