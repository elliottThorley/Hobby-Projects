using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeWalk : MonoBehaviour
{
    private GameObject bridgeSegment;
    public GameObject previousSegment;
    public GameObject nextSegment;
    private Vector3 prevPos = new Vector3(0f, 0f, -80f);
    private Vector3 nextPos = new Vector3(0f, 0f, 80f);
    private GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        bridgeSegment = gameObject;
        prevPos = gameObject.transform.position + prevPos;
        nextPos = gameObject.transform.position + nextPos;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextSegment != null)
            {
                if (nextSegment.GetComponent<bridgeWalk>().nextSegment != null)
                {
                    temp = nextSegment;
                    while (nextSegment.GetComponent<bridgeWalk>().nextSegment != null)
                    {
                        nextSegment = nextSegment.GetComponent<bridgeWalk>().nextSegment;
                    }
                    nextSegment.GetComponent<bridgeWalk>().previousSegment.GetComponent<bridgeWalk>().nextSegment = null;
                    nextSegment.GetComponent<BridgeMovement>().enabled = false;
                    nextSegment.GetComponent<bridgeDelete>().enabled = true;
                    nextSegment = temp;
                }
                else
                {
                    nextSegment.GetComponent<BridgeMovement>().enabled = false;
                    nextSegment.GetComponent<bridgeDelete>().enabled = true;
                }
            }
            else
            {
                nextSegment = Instantiate(bridgeSegment);
                nextSegment.transform.position = nextPos;
                nextSegment.GetComponent<bridgeWalk>().previousSegment = gameObject;
                nextSegment.GetComponent<BridgeMovement>().enabled = true;
            }
        }
    }
}
