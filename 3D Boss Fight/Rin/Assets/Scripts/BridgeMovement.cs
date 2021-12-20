using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovement : MonoBehaviour
{
    private Vector3 raisedPos;
    private float speed = 6;
    // Start is called before the first frame update
    void Start()
    {
        raisedPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - 90, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,raisedPos  ,Time.deltaTime*speed);
    }
}
