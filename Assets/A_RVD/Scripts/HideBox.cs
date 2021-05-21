using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBox : Interactable
{
    bool hidden = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            OnPlayerEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            OnPlayerExit();
    }

    public override void OnInteraction()
    {
        switch (hidden)
        {
            case false:
                GameManager.instance.playerScript.EnterHide();
                hidden = true;
                break;
            case true:
                GameManager.instance.playerScript.ExitHide();
                hidden = false;
                break;
        }
    }
}