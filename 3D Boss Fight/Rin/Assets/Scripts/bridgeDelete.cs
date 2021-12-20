using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeDelete : MonoBehaviour
{
    private float timer = 1f;
    private Vector3 loweredPos;
    // Start is called before the first frame update
    void Start()
    {

        loweredPos = new Vector3(transform.position.x, transform.position.y - 90, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, loweredPos, Time.deltaTime *2);
        if (timer > 0) { timer -= Time.deltaTime; }
        if (timer <= 0) { Destroy(gameObject); }
    }
}
