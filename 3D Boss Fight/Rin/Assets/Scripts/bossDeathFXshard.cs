using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDeathFXshard : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer rend;
    public Material glowRed;
    private float fastTime = 2;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 900);
        rend = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (fastTime > 0)
        {
            fastTime -= Time.deltaTime;
            rb.AddForce(transform.forward * -1);
        }
        else
        {
            rb.AddForce(transform.forward * -40);
        }
        rend.material.color = Color.Lerp(rend.material.color, glowRed.color, Mathf.PingPong(0f,1f) );
    }
}
