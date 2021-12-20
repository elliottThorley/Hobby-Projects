using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathContact : MonoBehaviour
{
    private gameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm= GameObject.FindGameObjectWithTag("gm").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "death") {
            gm.death = true;
        }
    }
}
