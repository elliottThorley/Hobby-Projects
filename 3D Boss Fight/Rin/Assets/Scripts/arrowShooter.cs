using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowShooter : MonoBehaviour
{
    public GameObject arrow;
    public Transform spawnPoint;
    public Camera cam;
    private bool aiming = false;
    private float cooldown=0;
    private float fov=77;
    private float zoomfov=65;
    private GameObject currentArrow;
    private float fovTimer = .003f;
    private bool zoomReady = false;
    public GameObject target;
    private bool fullyZoomed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) {
            cooldown = cooldown - Time.deltaTime;
        }
        if (fovTimer > 0) {
            fovTimer = fovTimer - Time.deltaTime;
        }
        if (fovTimer <= 0) {
            zoomReady = true;
            fovTimer = .005f;
        }
        if (aiming == true && cam.fieldOfView>zoomfov && zoomReady==true) {
            cam.fieldOfView = cam.fieldOfView - .2f;
            zoomReady = false;
        }
        if (aiming == true && cam.fieldOfView <= zoomfov) {
            fullyZoomed = true;
        }
        if (aiming == false && cam.fieldOfView < fov && zoomReady == true)
        {
            cam.fieldOfView = cam.fieldOfView + .2f;
            zoomReady = false;
        }
        if (Input.GetMouseButtonDown(1)&&cooldown<=0)
        {
            aiming = true;
            currentArrow=Instantiate(arrow,spawnPoint);
        }
        if (Input.GetMouseButtonUp(1) && aiming==true && fullyZoomed==true)
        {
            currentArrow.transform.parent = null;
            currentArrow.GetComponent<arrowZoom>().shoot();
            cooldown = .7f;
            fullyZoomed = false;
            aiming = false;
        }
        if (fullyZoomed == true) {
            currentArrow.GetComponent<arrowZoom>().ready();
        }
        if (Input.GetMouseButtonUp(1) && aiming == true && fullyZoomed == false)
        {
            Destroy(currentArrow);
            fullyZoomed = false;
            aiming = false;
        }
    }
}
