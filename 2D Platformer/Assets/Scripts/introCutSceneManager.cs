using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class introCutSceneManager : MonoBehaviour
{
    private float totalTime=8f;
    // Start is called before the first frame update
    void Start()
    {
        if(Gamepad.current!=null){
            InputSystem.ResetHaptics();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(totalTime>0){totalTime-=Time.deltaTime;}
        if(totalTime<=0){SceneManager.LoadScene("CoreScene", LoadSceneMode.Single);}
    }
}
