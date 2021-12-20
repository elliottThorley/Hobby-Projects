using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSequence : MonoBehaviour
{
    public GameObject bridge;
    private GameObject temp;
    public GameObject gate;
    public Transform bridgeSpawnPoint;
    private Vector3 gateEndPoint = new Vector3(0, -300f, 0);
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        gateEndPoint = gate.transform.position + gateEndPoint;
    }

    // Update is called once per frame
    void Update()
    {
        gate.transform.position = Vector3.Lerp(gate.transform.position,gateEndPoint,Time.deltaTime);
        if (done == false)
        {
            temp = Instantiate(bridge, null);
            temp.transform.position = bridgeSpawnPoint.position;
            done = true;
        }
    }
}
