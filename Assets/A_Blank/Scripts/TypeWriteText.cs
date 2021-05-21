using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TypeWriteText : MonoBehaviour
{
    [TextArea(5, 20)] [SerializeField] string[] myDialogs;                          // Strings of the dialogs to be displayed
    [SerializeField] [Range(0, 1)] float typeSpeed = 0.023f;               // The speed at which the text will be displayed letter by letter
    [SerializeField] bool replayable;                             // Make it true if the conversation is replayable
    [SerializeField] UnityEvent endConvoEvent;                    // Event triggered when conversation ends.
    TextMeshProUGUI text;                                   // The text component in the pannel
    GameObject dialoguePanel;
    int dialogIndex;                                              // Keeping track of which dialog is being displayed
    //[SerializeField] List<AudioClip> dialogues;
    //AudioSource audioSource;

    private void Start()
    {
        text = UIManager.instance.dialogueTextSpace;
        dialoguePanel = UIManager.instance.dialoguePannel;
        // audioSource = gameObject.GetComponent<AudioSource>();
    }


    public bool TextInteraction()
    {
        //If all the text of the dialogue is displayed then move to the next dialogue 
        //else snap to show all text of current dialogue
        if(text.text.Length == myDialogs[dialogIndex].Length) {
            StopAllCoroutines();
            dialogIndex++;
            //audioSource.Stop();
            // If the last dialog was displayed then turn off the pannal else move to displaying the next dialog
            if (dialogIndex >= myDialogs.Length) {
                endConvoEvent?.Invoke();
                GameManager.instance.playerScript.UnlockMovement();
                dialoguePanel.SetActive(false);
                return false;
            } else
                StartCoroutine(TypeWriteEffect());
        } else {
            StopAllCoroutines();
            text.text = myDialogs[dialogIndex];
        }

        return true;
    }

    //Resets the converation to the first dialogue, can also be called start conversation function
    public bool StartDialogue() {
        dialogIndex = 0;
        // if (!audioSource.isPlaying)
        // {
        //     audioSource.Stop();
        //     audioSource.PlayOneShot(dialogues[dialogIndex]);
        // }
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeWriteEffect());                        // Displaying the first dialog when the pannel appears

        if(replayable)
            return true;
        else
            return false;
    }

    
    //Coroutin to display the current dialog letter by letter
    IEnumerator TypeWriteEffect()
    {
        text.text = "";
        int index = 0;                                          //Keeps track of which letter of the string to display next and it is on at the movement
        while(true)
        {
            //If the last letter was displayed then break from the loop else continue to display the next letters
            if (index >= myDialogs[dialogIndex].Length)
                break;
            text.text += myDialogs[dialogIndex][index].ToString();
            // if (!audioSource.isPlaying)
            // {
            //     audioSource.Stop();
            //     audioSource.PlayOneShot(dialogues[dialogIndex]);
            // }
            index++;
            yield return new WaitForSeconds(typeSpeed);         //The time gap between the display of each letter
        }
    }
}
