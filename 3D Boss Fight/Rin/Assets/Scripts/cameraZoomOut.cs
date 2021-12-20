using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoomOut : MonoBehaviour
{
    public Vector3 newRot = new Vector3(20.3f, -44.7f, 0f);
    public GameObject camera;
    public bool go = false;
    public bool goIn = false;
    public Transform target;

    public Vector3 oldRot;
    public Transform oldPos;

    public GameObject player;
    public bool bossFight = false;
    public GameObject boss;
    private Vector3 multiTarget;
    public Transform bossPos;
    public Transform bossPosRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (go == true && bossFight==false)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, target.position, Time.deltaTime*0.5f);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, Quaternion.LookRotation(newRot), Time.deltaTime *0.5f);
        }
        if (go == false && bossFight == false)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, oldPos.position, Time.deltaTime * 0.5f);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.EulerAngles(oldRot.x,oldRot.y,oldRot.z), Time.deltaTime * 0.5f);
        }
        if (bossFight == true) {
            multiTarget = (player.transform.position + boss.transform.position) / 2;

            
            camera.transform.position = Vector3.Lerp(camera.transform.position, bossPos.position, Time.deltaTime * 0.5f);
            var rotation = Quaternion.LookRotation(multiTarget - camera.transform.position);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, rotation, Time.deltaTime * 2f);
            bossPosRot.transform.rotation = Quaternion.Slerp(bossPosRot.transform.rotation, rotation, Time.deltaTime * 2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            go = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            go = false;
        }
    }
}
