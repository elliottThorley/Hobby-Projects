using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlayer : MonoBehaviour
{
    public playerMovement mov;
    private float rotSpeed=10;
    public Animator anim;
    public bool bossFight = false;
    public GameObject boss;
    private Vector3 multiTarget;
    public GameObject playerParent;

    // Update is called once per frame
    void LateUpdate()
    {
        if (bossFight == false)
        {
            if (mov.movement.x != 0 || mov.movement.z != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(mov.movement), Time.deltaTime * rotSpeed);
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }
            
            playerParent.transform.rotation = Quaternion.Slerp(playerParent.transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 2f);
        }
        if (bossFight == true) {
            multiTarget = (transform.position + boss.transform.position) / 2;
            var rotation = Quaternion.LookRotation(multiTarget - transform.position);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
            playerParent.transform.rotation = Quaternion.Slerp(playerParent.transform.rotation, rotation, Time.deltaTime * 2f);
        }
    }
}
