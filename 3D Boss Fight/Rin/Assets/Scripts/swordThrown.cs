using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordThrown : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public bool go = false;
    public cameraShake cs;
    private TrailRenderer trail;
    private float timer=1;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<facePlayer>().enabled = false;
        trail = gameObject.GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer = timer - Time.deltaTime;
        }
        if (timer <= 0) {
            trail.enabled = false;
        }
        if (go == true)
        {
            timer = 8f;
            trail.enabled = true;
            gameObject.transform.parent = null;
            rb.AddForce(transform.forward * 900);
            gameObject.GetComponent<facePlayer>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rb != null && go==true)
        {
            rb.velocity = Vector3.zero;
            gameObject.GetComponent<facePlayer>().enabled = false;
            StartCoroutine(cs.Shake(0.15f, .2f));
            go = false;
        }
    }
}
