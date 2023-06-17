using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour {

    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;
    private bool showTrigger;

    public string Name;

    [TextArea(5, 500)]
    public string[] sentences;

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        showTrigger = false;
    }

    private void Update()
    {
        if(showTrigger == true && Input.GetKeyDown(KeyCode.F)){
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }

    public void OnTriggerStay(Collider other)
    {
       
        if ((other.gameObject.tag == "Player"))
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
            showTrigger = true;
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
        showTrigger = false;
    }
}

