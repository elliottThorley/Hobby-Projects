using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float DestroyAfter=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DestroyAfter>0){DestroyAfter-=Time.deltaTime;}
        else{Destroy(gameObject);}
    }
}
