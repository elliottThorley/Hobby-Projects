using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class diologue : MonoBehaviour
{
    public int dialogueNum=0;
    private float distance;
    public bool made=false;
    private GameObject player;
    public string[] lines;
    private GameObject UI;
    private TextMeshProUGUI nameBox;
    private TextMeshProUGUI textBox;
    private bool skipChar=false;
    private string skippedWord;
    public Animator UIanim;
    private float waitToDeactivate=0;
    // Start is called before the first frame update
    void Start()
    {
        UI=GameObject.FindGameObjectWithTag("Dialogue");
        player=GameObject.FindGameObjectWithTag("Player");
        UIanim=GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Animator>();
        nameBox=GameObject.FindGameObjectWithTag("NameBox").GetComponent<TextMeshProUGUI>();
        textBox=GameObject.FindGameObjectWithTag("TextBox").GetComponent<TextMeshProUGUI>();
        //UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance (transform.position, player.transform.position);
        UIanim.SetBool("down",made);
        if(made==true&&(Gamepad.current.buttonSouth.wasPressedThisFrame)&&waitToDeactivate<=0){
            waitToDeactivate=2f;
            made=false;
        }
        if(waitToDeactivate<=0f&&made==false){
            textBox.text=null;
            nameBox.text=null;
           // UI.SetActive(false);
        }
        if(waitToDeactivate>0){
            waitToDeactivate-=Time.deltaTime;
        }

        
    }
    public void Go(string name,string[] message){
            textBox.text=null;
            nameBox.text=null;
        waitToDeactivate=3f;
        dialogueNum=0;
        made=true;
        UI.SetActive(false);
        nameBox.text=name;
        lines=message;
        UI.SetActive(true);
        StartCoroutine(typeLine());
        dialogueNum++;
    }
    IEnumerator typeLine(){
        foreach (char c in lines[dialogueNum]){
            if(c=='<'){skipChar=true;}
            if(c=='>'){skippedWord+=c;skipChar=false;}

            if(skipChar==true){skippedWord+=c;yield return new WaitForSeconds(.0f);}
            else{
                if(skippedWord!=null){textBox.text+=skippedWord;skippedWord=null;}
                else{textBox.text+=c;}
                yield return new WaitForSeconds(.035f);
                }
        }
    }
}
