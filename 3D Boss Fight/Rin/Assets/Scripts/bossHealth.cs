using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    public cameraZoomOut cz;
    public rotatePlayer rotplayer;
    public arrowShooter arrs;
    public bossDeathVFX fx;
    public swordThrowAttack sa;
    public float health;
    private bool gone = false;
    private float timer = 0;
    public Material glowRed;
    private Color col;
    public Renderer rend1;
    public Renderer rend2;
    public Renderer rend3;
    public Renderer rend4;
    public Renderer rend5;
    public Renderer rend6;
    public GameObject walls;
    private float timer2;
    public GateSequence gates;
    // Start is called before the first frame update
    void Start()
    {
        col = rend1.material.color;
        gates.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer2 > 0) { timer2 -= Time.deltaTime; }
        if (timer2 <= 0&&health<=0&&gone==true)
        {
            cz.bossFight = false;
            rotplayer.bossFight = false;
            arrs.target = null;
            gates.enabled = true;
            walls.active = false;
        }
        if (timer > 0) {
            timer = timer - Time.deltaTime;
            rend1.material.color = glowRed.color;
            rend2.material.color = glowRed.color;
            rend3.material.color = glowRed.color;
            rend4.material.color = glowRed.color;
            rend5.material.color = glowRed.color;
            rend6.material.color = glowRed.color;
        }
        if (timer <= 0) {
            rend1.material.color = col;
            rend2.material.color = col;
            rend3.material.color = col;
            rend4.material.color = col;
            rend5.material.color = col;
            rend6.material.color = col;
        }
        if (health <= 0&&gone==false) {

            timer2 = 5f;
            sa.enabled = false;
            fx.go = true;
            gone = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerShot") {
            health = health - 1;
            timer = .25f;
            Destroy(collision.gameObject);
        }
    }
}
