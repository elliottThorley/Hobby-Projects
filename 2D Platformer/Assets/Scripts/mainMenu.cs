using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public int i;
    private float vibTimer=0;
    public bool hasSave=false;
    public GameObject continueButton;
    public GameObject[] defaultMenuButtons;
    public GameObject[] optionMenuButtons;
    public GameObject[] confirmMenuButtons;
    private bool inOptions=false;
    private bool inConfirmation=false;
    private bool inNewGameConfirm=false;
    // Start is called before the first frame update
    void Start()
    {
        //get showcutscene value from save data, default is 0
        if(PlayerPrefs.GetInt("showCutscene",0)==0){
            hasSave=false;
        }
        else{
            hasSave=true;
        }
        if(hasSave==false){
            continueButton.SetActive(false);
        }
        if(hasSave==false){i=1;}
        if(hasSave==true){i=0;}

        optionButtons(false);
        confirmButtons(false);
    }

    // Update is called once per frame
    void Update()
    {
        //for vibration
        if(vibTimer>0){vibTimer-=Time.deltaTime;}
        else{InputSystem.ResetHaptics();}
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
                vibrate(.1f,.2f);
            }
            if(Gamepad.current.dpad.down.wasPressedThisFrame){
                i++;
                vibrate(.1f,.2f);
            }
        }
        //make i stay in range
        //if not in options menu
        if(inOptions==false){
            if(i<0){i=3;}
            if(i==4){
                if(hasSave==false){i=1;}
                else{i=0;}
            }
            if(hasSave==false&&i==0){i=3;}
        }
        //if in options menu or confirmation menu or in newgameconfirm
        if(inOptions==true||inConfirmation==true||inNewGameConfirm==true){
            if(i<1){i=2;}
            if(i>2){i=1;}
        }

        //move the curser
        moveCurser(i);
        //get selection input
        if(Keyboard.current.enterKey.wasPressedThisFrame==true){
                selected(i);
            }
        //gamepad inputs
        if(Gamepad.current!=null){
            if(Gamepad.current.buttonSouth.wasPressedThisFrame==true){
                vibrate(.1f,.2f);
                selected(i);
            }
        }
    }
    void moveCurser(int num){
        //continue
        if(num==0){
            gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(-110f,-164f);
        }
        //new game
        if(num==1){
            gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(-130f,-220f);
        }
        //options
        if(num==2){
            gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(-90f,-273f);
        }
        //quit
        if(num==3){
            gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(-50f,-329f);
        }

    }
    void selected(int num){
        //continue
        if(num==0){
            SceneManager.LoadScene("CoreScene", LoadSceneMode.Single);
        }
        //new game/delete data/yes
        if(num==1){
            //new game
            if(inOptions==false&&inConfirmation==false&&inNewGameConfirm==false){
                //hide default buttons
                defaultButtons(false);
                //show confirm buttons
                confirmButtons(true);
                //set variables
                inNewGameConfirm=true;
            }
            //delete data
            else if(inOptions==true){
                //hide option buttons
                optionButtons(false);
                //show confirm buttons
                confirmButtons(true);
                //set variables
                inConfirmation=true;
                inOptions=false;
            }
            //yes-delete data
            else if(inConfirmation==true){
                //deletes all data
                PlayerPrefs.DeleteAll();
                //hide confirm buttons
                confirmButtons(false);
                //show option buttons
                optionButtons(true);
                //set variables
                inConfirmation=false;
                inOptions=true;
            }
            //yes-new game
            else if(inNewGameConfirm==true){
                //start new game
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("showCutscene",1);
                SceneManager.LoadScene("introCutscene", LoadSceneMode.Single);
            }
            else{}
        }
        //options/back/no
        if(num==2){
            //options
            if(inOptions==false&&inConfirmation==false&&inNewGameConfirm==false){
                //hide default buttons
                defaultButtons(false);
                //show option buttons
                optionButtons(true);
                //set variables
                i=1;
                inOptions=true;
            }
            //back
            else if(inOptions==true){
                //hide option buttons
                optionButtons(false);
                //show default buttons
                defaultButtons(true);
                //set variables
                i=0;
                inOptions=false;
            }
            //no-delete data
            else if(inConfirmation==true){
                //hide confirm buttons
                confirmButtons(false);
                //show option buttons
                optionButtons(true);
                //set variables
                inOptions=true;
                i=1;
                inConfirmation=false;
            }
            //no-new game confirm
            else if(inNewGameConfirm==true){
                //hide confirm buttons
                confirmButtons(false);
                //show default buttons
                defaultButtons(true);
                //set variables
                i=0;
                inNewGameConfirm=false;
            }
            else{}
        }
        //quit
        if(num==3){
            //make vibration stop before quit
            vibTimer=0;
            InputSystem.ResetHaptics();
            //quit
            Application.Quit();
        }
    }
    public void vibrate(float length,float intensity){
        vibTimer=length;
        Gamepad.current.SetMotorSpeeds(intensity, intensity);
    }
    void confirmButtons(bool status){
        for(int j=0;j<confirmMenuButtons.Length;j++){
            confirmMenuButtons[j].SetActive(status);
        }
    }
    void optionButtons(bool status){
        for(int j=0;j<optionMenuButtons.Length;j++){
            optionMenuButtons[j].SetActive(status);
        }
    }
    void defaultButtons(bool status){
        for(int j=0;j<defaultMenuButtons.Length;j++){
            defaultMenuButtons[j].SetActive(status);
        }
        //check prefs
        if(PlayerPrefs.GetInt("showCutscene",0)==0){hasSave=false;}
        else{hasSave=true;}
        //set the continue button to show or not
        if(hasSave==false){
            continueButton.SetActive(false);
        }
    }

}
