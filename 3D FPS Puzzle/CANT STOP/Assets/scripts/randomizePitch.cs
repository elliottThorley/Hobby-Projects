using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizePitch : MonoBehaviour
{
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.pitch = Random.Range(.7f, 1.5f);
        sound.Play();
    }
}