using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float deathTime=20;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTime > 0) { deathTime = deathTime - Time.deltaTime; }
        if (deathTime <= 0) { Destroy(gameObject); }
    }
}
