using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    private playerMovement pm;
    public float dashSpeed;
    public float dashTime;
    private bool go = false;
    private float coolDown = 0;
    public TrailRenderer[] trails;
    // Start is called before the first frame update
    void Start()
    {
        pm = gameObject.GetComponent<playerMovement>();
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].emitting = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0) {
            coolDown -= Time.deltaTime;
        }
        if (coolDown <= 0) {
            
            go = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&go==false) {

            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].emitting = true;
            }
            StartCoroutine(dashed());
            coolDown = .75f;
            
            go = true;
        }
    }
    IEnumerator dashed() {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime) {
            pm.characterController.Move(pm.movement*dashSpeed*Time.deltaTime);
            yield return null;
        }
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].emitting = false;
        }
    }
}
