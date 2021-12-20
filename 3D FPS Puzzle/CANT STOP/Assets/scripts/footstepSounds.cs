using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSounds : MonoBehaviour
{
    private PlayerMovement pm;
    private Rigidbody rb;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.grounded == true && rb.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {
            sound.pitch = Random.Range(.7f, 1.5f);
            sound.Play();
        }
    }
}
