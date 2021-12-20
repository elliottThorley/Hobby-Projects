using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour
{
    public GameObject[] targets;
    public bool hit = false;
    private int i = 0;
    public bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < targets.Length; j++) {
            if (j != 0) { targets[j].active = false; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true) {
            i++;
            if (i < targets.Length)
            {
                targets[i].active = true;
            }
            hit = false;
        }
        if (i == targets.Length)
        {
            win = true;
        }
    }
}
