using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSwing : MonoBehaviour
{
    public Animator anim;
    private bool attacked = false;
    private float attackTimer;
    private float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0) {
            attackTimer -= Time.deltaTime;
        }
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (attackTimer <= 0) {
            attacked = false;
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2",false);
        }
        if (Input.GetMouseButtonDown(0) && attacked==false && coolDown<=0)
        {
            anim.SetBool("Attack1", true);
            attackTimer = 1f;
            attacked = true;
        }
        else if (Input.GetMouseButtonDown(0) && attacked == true)
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
            coolDown = 1f;
        }

    }
}
