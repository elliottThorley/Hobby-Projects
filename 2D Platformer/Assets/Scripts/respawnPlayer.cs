using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayer : MonoBehaviour
{
    public float x=0f;
    public float y=0f;
    public Animator anim;
    private float animTimerFalse=0;
    private float animTimerTrue=0;
    private bool move=false;
    private movement mov;

    void Start(){
        mov=GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
    }
    void Update(){
        if(animTimerFalse>0){animTimerFalse-=Time.deltaTime;}
        else{
            if(move==true){
                transform.position=new Vector2(x,y);
                move=false;
            }
            anim.SetBool("go",false);
        }

        if(animTimerTrue>0){animTimerTrue-=Time.deltaTime;}
        else{
            if(move==true){
                anim.SetBool("go",true);
            }
        }
    }
    public void Respawn(){
        if(move==false){
            mov.vibrate(0.7f,0.65f);
            animTimerTrue=.5f;
            animTimerFalse=.9f;
            //anim.SetBool("go",true);
            move=true;
        }
    }
}
