using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFightCamera : MonoBehaviour
{
    public cameraZoomOut cz;
    public rotatePlayer rotplayer;
    public swordThrowAttack sa;
    public arrowShooter arrs;
    public GameObject boss;
    public GameObject[] tiles;
    public Material glowRed;
    public int stage = 0;
    private int i = 0;
    private float timer = 0;
    public GameObject floor;
    public GameObject floorSkyPos;
    public GameObject player;
    public GameObject Boss;
    public GameObject walls;

    // Start is called before the first frame update
    void Start()
    {
        walls.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) { timer -= Time.deltaTime; }

        if (stage == 1) {
            cz.bossFight = true;
            rotplayer.bossFight = true;
            arrs.target = boss;
            if (i < tiles.Length&&timer<=0)
            {
                tiles[i].GetComponent<Renderer>().material = glowRed;
                i++;
                timer = .07f;
            }
            if (i == tiles.Length)
            {
                walls.active = true;
                timer = 1; 
                stage = 2;
            }
        }

        if (stage == 2&&timer<=0)
        {
            floor.transform.position=Vector3.Lerp(floor.transform.position, floorSkyPos.transform.position, Time.deltaTime/2);
            player.GetComponent<playerMovement>().enabled = false;
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(player.transform.position.x,floorSkyPos.transform.position.y+6,player.transform.position.z), Time.deltaTime / 2);
            boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(boss.transform.position.x, floorSkyPos.transform.position.y + 50, boss.transform.position.z), Time.deltaTime / 2);
            if ((floorSkyPos.transform.position.y - floor.transform.position.y) < 38)
            {
                timer = .4f;
                stage = 3;
            }
        }
        if (stage == 3&&timer<=0) {
            player.GetComponent<playerMovement>().enabled = true;
            sa.enabled = true;
            stage++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true&&stage==0)
        {
            stage = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
