using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowZoom : MonoBehaviour
{
    public Transform target;
    private TrailRenderer trail;
    private Rigidbody rb;
    private bool shot = false;
    private float time = 20f;
    private arrowShooter ars;
    public GameObject arrowTip;
    public GameObject arrowBack;
    public Material arrowTipColor;
    // Start is called before the first frame update
    void Start()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
        rb= gameObject.GetComponent<Rigidbody>();
        trail.enabled = false;
        ars = GameObject.FindGameObjectWithTag("Player").GetComponent<arrowShooter>();
        if (ars.target != null) {
            target = ars.target.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shot == false&&target!=null)
        {
            transform.LookAt(target);
        }
        if (shot == true) {
            time = time - Time.deltaTime;
        }
        if (time <= 0) {
            Destroy(gameObject);
        }
    }
    public void shoot() {
        rb.AddForce(transform.forward * 5000);
        trail.enabled = true;
        shot = true;
    }
    public void ready()
    {
        arrowTip.GetComponent<Renderer>().material = arrowTipColor;
        arrowBack.GetComponent<Renderer>().material = arrowTipColor;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }
}
