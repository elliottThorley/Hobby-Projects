using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float groundCheckLength=0.5f;
    private float fallMult=2.5f;
    private float lowJumpMult=2.5f;
    private float vibTimer=0;
    private float wallJumpTimer=0f;
    private float wallSlideTimer=0f;
    private float deathAnimTimer=0f;
    private float jumpsLeft=2f;
    public bool grounded=false;
    public bool onWall=false;
    public bool onSpike=false;
    public bool onPlatform=false;
    private bool wallJump=false;
    private bool wallSlide=false;
    private bool setWallSlideTimer=false;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask spikeLayer;
    public LayerMask platFormLayer;
    private Animator anim;
    public GameObject jumpDustVFX;
    private GameObject platformParent;
    private respawnPlayer respawn;
    public Vector2 debugVelocity;
    public float hMov;
    private float wallJumpForce=0;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        respawn=GetComponent<respawnPlayer>();
        platformParent=GameObject.FindGameObjectWithTag("PlatformParent");
    }

    // Update is called once per frame
    void LateUpdate(){
        //timers
        //for vibration
        if(vibTimer>0){vibTimer-=Time.deltaTime;}
        else{InputSystem.ResetHaptics();}
        //for walljump
        if(wallJumpTimer>0){rb.AddForce(new Vector2(wallJumpForce,0f),ForceMode2D.Impulse);wallJumpTimer-=Time.deltaTime;}
        else{wallJump=false;}
        //for wallSlide
        if(wallSlideTimer>0){wallSlideTimer-=Time.deltaTime;}
        else{wallSlide=false;}
        //for death animation
        if(deathAnimTimer>0){deathAnimTimer-=Time.deltaTime;}

        //check ground
        grounded=IsGrounded();

        //check wall
        if(IsOnWallRight()==true||IsOnWallLeft()==true){onWall=true;}
        else{onWall=false;}

        //check spike
        onSpike=IsOnSpike();

        //stop movement if touching spike
        if(onSpike==true){
            rb.velocity=new Vector2(0f,0f);
            respawn.Respawn();
        }

        //check platform
        onPlatform=IsOnPlatform();
        if(onPlatform==true){
            grounded=true;
            transform.parent=platformParent.transform;
        }
        else{
            transform.parent=null;
        }
        
        //get hoz movement input
        hMov=Gamepad.current.leftStick.x.ReadValue();

        //debug velocity
        debugVelocity=rb.velocity;
        //for wall jump
        if(wallJump==false&&Gamepad.current.leftTrigger.isPressed==true&&Gamepad.current.leftShoulder.wasPressedThisFrame&&onWall==true){
            vibTimer=0.1f;
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            if(IsOnWallLeft()==true){
                rb.AddForce(new Vector2(8f,0f),ForceMode2D.Impulse);
                wallJumpForce=8f;
                rb.velocity+=Vector2.up*jumpForce;
            }
            if(IsOnWallRight()==true){
                rb.AddForce(new Vector2(-8f,0f),ForceMode2D.Impulse);
                wallJumpForce=-8f;
                rb.velocity+=Vector2.up*jumpForce;
            }
            wallJumpTimer=0.2f;
            wallJump=true;
        }
        //for jump
        else if(Gamepad.current.leftShoulder.wasPressedThisFrame&&jumpsLeft>0){
            Instantiate(jumpDustVFX,new Vector2(transform.position.x,transform.position.y+0.2f),transform.rotation);
            vibTimer=0.1f;
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            rb.velocity=new Vector2(rb.velocity.x,jumpForce);
            grounded=false;
            //Debug.Log("jumps left:"+jumpsLeft+"\ngrounded: "+grounded+"\n\n");
            jumpsLeft--;
            //Debug.Log("jumps left:"+jumpsLeft+"\ngrounded: "+grounded+"\n\n");
        }
        //reset jumps
        if((IsGrounded()==true||IsOnPlatform()==true)&&vibTimer<=0){
            jumpsLeft=2;
        }

        //if on wall, and holding button, make player slide slowly, after 2 seconds pass
        if(wallJump==false&&onWall==true&&Gamepad.current.leftTrigger.isPressed==true){
            if(setWallSlideTimer==false){wallSlideTimer=2f;setWallSlideTimer=true;wallSlide=true;}
            if(wallSlide==false){
                rb.velocity=new Vector2(rb.velocity.x,Physics2D.gravity.y*0.07f);
            }
            else{
                rb.gravityScale=0f;
                rb.velocity=new Vector2(rb.velocity.x,Gamepad.current.leftStick.y.ReadValue()*(speed/2));
            }
        }
        else{rb.gravityScale=1f;}

        //reset: wall slide timer and setWallSlideTimer when touch ground
        if(grounded==true){wallSlide=false;setWallSlideTimer=false;}

        //make jump good
        if(rb.velocity.y<0){
            rb.velocity+=Vector2.up*Physics2D.gravity.y*(fallMult-1)*Time.deltaTime;
        }
        else if(rb.velocity.y>0&&!Gamepad.current.leftShoulder.isPressed){
            rb.velocity+=Vector2.up*Physics2D.gravity.y*(lowJumpMult-1)*Time.deltaTime;
        }

        //give animator input
        //wall climb animations
        if(IsOnWallLeft()==true&&Gamepad.current.leftTrigger.isPressed==true){
            GetComponent<SpriteRenderer>().flipX=true;
            anim.SetBool("wallHug",true);
        }
        else if(IsOnWallRight()==true&&Gamepad.current.leftTrigger.isPressed==true){
            GetComponent<SpriteRenderer>().flipX=false;
            anim.SetBool("wallHug",true);
        }
        else{
            anim.SetBool("wallHug",false);
        }
        //walking animations
        if(hMov>0){
            GetComponent<SpriteRenderer>().flipX=false;
            anim.SetBool("walking",true);
        }
        else if(hMov<0){
            GetComponent<SpriteRenderer>().flipX=true;
            anim.SetBool("walking",true);
        }
        else{
            anim.SetBool("walking",false);
        }
        //death animation
        if(IsOnSpike()==true){
            deathAnimTimer=.7f;
            anim.SetBool("death",true);
        }
        if(IsOnSpike()==false&&deathAnimTimer<=0f){
            anim.SetBool("death",false);
        }
    }
    void FixedUpdate()
    {
        //check input
        //for horizontal movement
        if(onSpike==false){
            if(hMov>0f){
                rb.AddForce(new Vector2(1f,0f),ForceMode2D.Impulse);
            }
            if(hMov<0f){
                rb.AddForce(new Vector2(-1f,0f),ForceMode2D.Impulse);
            }
            //cap max speed
            if(rb.velocity.x>3.5f){
                rb.velocity=new Vector2(3.5f,rb.velocity.y);
            }
            if(rb.velocity.x<-3.5f){
                rb.velocity=new Vector2(-3.5f,rb.velocity.y);
            }
            //slow down player
            if(rb.velocity.x>0){
                rb.velocity=new Vector2(rb.velocity.x-(Time.deltaTime*10),rb.velocity.y);
            }
            if(rb.velocity.x<0){
                rb.velocity=new Vector2(rb.velocity.x+(Time.deltaTime*10),rb.velocity.y);
            }
            //make player stop if going below certain speed
            if(rb.velocity.x>0&&rb.velocity.x<.5f){
                rb.velocity=new Vector2(0,rb.velocity.y);
            }
            if(rb.velocity.x<0&&rb.velocity.x>-.5f){
                rb.velocity=new Vector2(0,rb.velocity.y);
            }
            //make player stop instantly if not touching joystick and on ground
            if(hMov==0&&wallJumpTimer<=0){
                rb.velocity=new Vector2(0f,rb.velocity.y);
            }
            //apply final value
            rb.velocity=new Vector2(rb.velocity.x,rb.velocity.y);
        }
    }
    
    //use raycast to check if grounded
    bool IsGrounded() {
        Vector2 position = transform.position;
        position.y=position.y-0.3f;
        Vector2 direction = Vector2.down;
        //draw the ray
        //Debug.DrawRay(position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckLength, groundLayer);
        if (hit.collider != null) {return true;}
        return false;
    }
    bool IsOnPlatform() {
        Vector2 position = transform.position;
        position.y=position.y-0.3f;
        Vector2 direction = Vector2.down;
        //draw the ray
        //Debug.DrawRay(position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckLength, platFormLayer);
        if (hit.collider != null) {return true;}
        return false;
    }
    //use raycast to check if on wall right
    bool IsOnWallRight(){
        Vector2 position = transform.position;
        position.y=position.y-0.3f;
        Vector2 direction = Vector2.right;
        //draw the ray
        //Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckLength, groundLayer);
        if (hit.collider != null) {return true;}
        return false;
    }
    //use raycast to check if on wall left
    bool IsOnWallLeft(){
        Vector2 position = transform.position;
        position.y=position.y-0.3f;
        Vector2 direction = -Vector2.right;
        //draw the ray
        //Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckLength, groundLayer);
        if (hit.collider != null) {return true;}
        return false;
    }
    //use raycast to check if near spike
    bool IsOnSpike(){
        Vector2 position = transform.position;
        position.y=position.y-0.3f;

        float multNum=.07f;

        //draw the ray down left
        position.x=position.x-0.2f;
        Vector2 direction = Vector2.down;
        Debug.DrawRay(position, direction*multNum, Color.blue);
        RaycastHit2D hit1 = Physics2D.Raycast(position, direction*multNum, multNum, spikeLayer);
        position.x=position.x+0.2f;

        //draw the ray down right
        position.x=position.x+0.2f;
        direction = Vector2.down;
        Debug.DrawRay(position, direction*multNum, Color.blue);
        RaycastHit2D hit5 = Physics2D.Raycast(position, direction*multNum, multNum, spikeLayer);
        position.x=position.x-0.2f;

        position.y=position.y+0.6f;

        //draw the ray up right
        position.x=position.x+0.2f;
        direction = -Vector2.down;
        Debug.DrawRay(position, direction*multNum, Color.blue);
        RaycastHit2D hit2 = Physics2D.Raycast(position, direction*multNum, multNum, spikeLayer);
        position.x=position.x-0.2f;

        //draw the ray up left
        position.x=position.x-0.2f;
        direction = -Vector2.down;
        Debug.DrawRay(position, direction*multNum, Color.blue);
        RaycastHit2D hit6 = Physics2D.Raycast(position, direction*multNum, multNum, spikeLayer);
        position.x=position.x+0.2f;
        
        position.y=position.y-0.6f;

        //draw the ray right
        direction = Vector2.right;
        Debug.DrawRay(position, direction*0.21f, Color.blue);
        RaycastHit2D hit3 = Physics2D.Raycast(position, direction, 0.21f, spikeLayer);
        
        //draw the ray left
        direction = -Vector2.right;
        Debug.DrawRay(position, direction*0.21f, Color.blue);
        RaycastHit2D hit4 = Physics2D.Raycast(position, direction, 0.21f, spikeLayer);

        if (hit1.collider != null||hit2.collider != null||hit3.collider != null||hit4.collider != null || hit5.collider != null|| hit6.collider != null) {return true;}
        return false;
    }
    public void vibrate(float length,float intensity){
        vibTimer=length;
        Gamepad.current.SetMotorSpeeds(intensity, intensity);
    }
}
