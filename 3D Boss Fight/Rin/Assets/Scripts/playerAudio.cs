using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{
    public playerMovement mov;
    public AudioSource footsteps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.movement.x != 0 || mov.movement.z != 0&&mov.characterController.isGrounded==true)
        {
            if (footsteps.isPlaying == false) {
                footsteps.pitch = Random.Range(1f, 1.2f);
                footsteps.Play();
            }
        }
        else
        {
            footsteps.Pause();
        }
    }
}
