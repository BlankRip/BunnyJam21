using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    TypeWriteText myDialogues;
    bool canTrigger;


    private void Start()
    {
        myDialogues = GetComponent<TypeWriteText>();
        canTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if player is triggering a conversation and repeat if the conversation is replayable
        if (canTrigger && other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.LockMovement();
            UIManager.instance.currentWriter = myDialogues;
            canTrigger = myDialogues.StartDialogue();
        }
    }
}