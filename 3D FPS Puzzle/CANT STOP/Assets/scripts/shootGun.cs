using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    public GameObject bulletParent;
    private GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            temp=Instantiate(bullet, shotPoint);
            temp.transform.parent = bulletParent.transform;
        }
    }
}
