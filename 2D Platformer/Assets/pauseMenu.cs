using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject curser;
    private bool paused=false;
    public int i=0;
    private movement mov;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        mov=GameObject.FindGameObjectWithTag("Player").GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //pause and unpause
        //gamepad controls
        if(Gamepad.current!=null){
            if(Gamepad.current.startButton.wasPressedThisFrame){
                if(paused==false){
                    pauseScreen.SetActive(true);
                    paused=true;
                }
                else if(paused==true){
                    pauseScreen.SetActive(false);
                    paused=false;
                }
            }
        }
        //keyboard controls
        if(Keyboard.current.escapeKey.wasPressedThisFrame==true){
            if(paused==false){
                pauseScreen.SetActive(true);
                paused=true;
            }
            else if(paused==true){
                pauseScreen.SetActive(false);
                paused=false;
            }
        }
        //set movement to on or off based on if paused
        if(paused==true){
            mov.enabled=false;
        }
        if(paused==false){
            mov.enabled=true;
        }
        //navigate pause screen
        if(paused==true){
            //get up and down inputs
            if(Keyboard.current.wKey.wasPressedThisFrame==true){
                i--;
            }
            if(Keyboard.current.sKey.wasPressedThisFrame==true){
                i++;
            }
            //gamepad inputs
            if(Gamepad.current!=null){ 
                //get up and down inputs
                if(Gamepad.current.dpad.up.wasPressedThisFrame){
                    i--;
                }
                if(Gamepad.current.dpad.down.wasPressedThisFrame){
                    i++;
                }
            }
            //keep i in range
            if(i<0){i=1;}
            if(i>1){i=0;}

            moveCurser(i);
        }
        //get selection input
        if(Keyboard.current.enterKey.wasPressedThisFrame==true){
                selected(i);
            }
        //gamepad inputs
        if(Gamepad.current!=null){
            if(Gamepad.current.buttonSouth.wasPressedThisFrame==true){
                selected(i);
            }
        }
    }
    void moveCurser(int num){
        //resume
        if(num==0){
            curser.GetComponent<RectTransform>().anchoredPosition=new Vector2(-32f,6.5f);
        }
        //quit
        if(num==1){
            curser.GetComponent<RectTransform>().anchoredPosition=new Vector2(-16.5f,-8.5f);
        }
    }
    void selected(int num){
        if(num==0){
            pauseScreen.SetActive(false);
            paused=false;
        }
        if(num==1){
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
