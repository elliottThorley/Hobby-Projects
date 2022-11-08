using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length;
    private Vector2 startPos;
    public float parallaxEffect;
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam=GameObject.FindGameObjectWithTag("MainCamera");
        startPos=new Vector2(transform.position.x,transform.position.y);
        length=GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float temp=(cam.transform.position.x*(1-parallaxEffect));
        float dist=(cam.transform.position.x*parallaxEffect);
        float dist2=(cam.transform.position.y*parallaxEffect);
        transform.position=new Vector2(startPos.x+dist,startPos.y+dist2);
        if(temp>startPos.x+length)startPos=new Vector2(startPos.x+length,startPos.y);
        else if(temp<startPos.x-length)startPos=new Vector2(startPos.x-length,startPos.y);
    }
}
