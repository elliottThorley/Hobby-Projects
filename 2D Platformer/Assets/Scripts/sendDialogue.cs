using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class sendDialogue : MonoBehaviour
{
    private float distance;
    private GameObject player;
    private bool done=false;
    public string named;
    public string[] message;
    private string[] messageToSend={"temp"};
    private diologue dio;
    private int counter=0;
    private TextMeshProUGUI textBox;
    private float waitBeforeNextPress=0f;
    private bool canPress=true;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        dio=GameObject.FindGameObjectWithTag("GameController").GetComponent<diologue>();
        textBox=GameObject.FindGameObjectWithTag("TextBox").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        if(waitBeforeNextPress>0){canPress=false;waitBeforeNextPress-=Time.deltaTime;}
        if(waitBeforeNextPress<=0){canPress=true;}

        distance = Vector3.Distance (transform.position, player.transform.position);
        if(Gamepad.current.buttonSouth.wasPressedThisFrame&&distance<2&&done==false&&counter<message.Length&&canPress==true){
            if(counter>0){
                textBox.text=null;
            }
            messageToSend[0]=message[counter];
            dio.Go(named,messageToSend);
            waitBeforeNextPress=3f;
            counter++;
        }
        if(counter==message.Length){
            done=true;
        }
    }
}
