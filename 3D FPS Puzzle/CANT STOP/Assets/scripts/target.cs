using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public GameObject sound;
    private targetController tc;
    // Start is called before the first frame update
    void Start()
    {
        tc = GameObject.FindGameObjectWithTag("tc").GetComponent<targetController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Instantiate(sound);
            tc.hit=true;
            gameObject.active = false;
        }
    }
}
