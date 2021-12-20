using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordThrowAttack : MonoBehaviour
{
    public GameObject[] swords;
    public Transform[] swordTriangle;
    public bossMovement movement;
    public Transform[] swordRecallPos;
    public Transform[] antiCrossPos;
    public Transform player;
    private Vector3 newDirection;
    public float timer=0;
    private bool start = false;
    private bool moveSwords = false;
    public int i = 0;
    private int k = 20;//k is the end of the array
    private int attackPatternNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) { timer = timer- Time.deltaTime; }


        if (attackPatternNumber == 1) {
            attackPattern1();
        }
        if (attackPatternNumber == 2)
        {
            attackPattern2();
        }
        if (attackPatternNumber == 3)
        {
            attackPattern3();
        }
        if (i > 20 || (i==10 && attackPatternNumber==3)) {
            //can reset variables here, as this marks an attack pattern has ended
            moveSwords = false;
            attackPatternNumber = Random.Range(1,4);
            k = 20;
            movement.switchPosition();
            i = 0;
        }
    }
    private void attackPattern1() {

        //this will reset the timer every 2.5 seconds, giving the sword time to get into position
        if (start == false)
        {
            timer = 2.5f;
            start = true;
        }

        if (timer > 0)
        {
            //move the swords into position
            swords[i].transform.position = Vector3.Lerp(swords[i].transform.position, swordTriangle[0].position, Time.deltaTime * 2);
            swords[i + 1].transform.position = Vector3.Lerp(swords[i + 1].transform.position, swordTriangle[1].position, Time.deltaTime * 2);
            swords[i + 2].transform.position = Vector3.Lerp(swords[i + 2].transform.position, swordTriangle[2].position, Time.deltaTime * 2);
            //get the angle for the swords to aim at
            Vector3 targetDirection = player.position - swords[i + 1].transform.position;
            float singleStep = Time.deltaTime;
            newDirection = Vector3.RotateTowards(swords[i + 1].transform.forward, targetDirection, singleStep, 0.0f);
            //rotate the sword to that angle
            swords[i].transform.rotation = Quaternion.LookRotation(newDirection);
            swords[i + 1].transform.rotation = Quaternion.LookRotation(newDirection);
            swords[i + 2].transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (timer <= 0 && start == true)
        {
            //give the swords momentum
            swords[i].GetComponent<swordThrown>().go = true;
            swords[i + 1].GetComponent<swordThrown>().go = true;
            swords[i + 2].GetComponent<swordThrown>().go = true;
            //increment i
            i = i + 3;
            start = false;
        }
    }
    private void attackPattern2() {
        //moving all swords into position
        if (timer > 0)
        {
            for (int ii = i; ii <= 20; ii++)
            {
                swords[ii].transform.position = Vector3.Lerp(swords[ii].transform.position, swordRecallPos[ii].position, Time.deltaTime * 2);
                swords[ii].GetComponent<facePlayer>().enabled = true;
            }
        }

        //this will reset the timer every 2.5 seconds, giving the sword time to get into position
        if (start == false)
        {
            if (moveSwords == false) {
                timer = 3.5f;
            }
            if (moveSwords == true)
            {
                timer = .8f;
            }
            start = true;
        }
        
        if (timer <= 0 && start == true)
        {
            moveSwords = true;
            //give the swords momentum and turn off aiming for it
            swords[i].GetComponent<swordThrown>().go = true;
            swords[i].GetComponent<facePlayer>().enabled = false;
            //increment i
            i++;
            start = false;
        }
    }
    private void attackPattern3()
    {
        //moving all swords into position
        if (timer > 0)
        {
            for (int ii = i; ii <= k; ii++)
            {
                swords[ii].transform.position = Vector3.Lerp(swords[ii].transform.position, antiCrossPos[ii].position, Time.deltaTime * 2);
                swords[ii].GetComponent<facePlayer>().enabled = true;
            }
        }

        //this will reset the timer every 2.5 seconds, giving the sword time to get into position
        if (start == false)
        {
            if (moveSwords == false)
            {
                timer = 3.5f;
            }
            if (moveSwords == true)
            {
                timer = 1f;
            }
            start = true;
        }

        if (timer <= 0 && start == true)
        {
            moveSwords = true;
            //give the swords momentum and turn off aiming for it
            swords[i].GetComponent<swordThrown>().go = true;
            swords[i].GetComponent<facePlayer>().enabled = false;

            swords[k].GetComponent<swordThrown>().go = true;
            swords[k].GetComponent<facePlayer>().enabled = false;
            //increment i and k
            i++;
            k--;
            start = false;
        }
    }
}
