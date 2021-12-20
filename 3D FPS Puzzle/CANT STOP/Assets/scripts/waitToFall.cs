using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToFall : MonoBehaviour
{
    private Rigidbody rb;
    private float fallTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fallTimer > 0) {
            fallTimer = fallTimer - Time.deltaTime;
        }
        if (fallTimer <= 0) {
            rb.useGravity = true;
        }
    }
}
