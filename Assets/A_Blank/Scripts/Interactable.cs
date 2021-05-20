using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public virtual void OnInteraction() { 
        OnPlayerExit();
    }

    public void OnPlayerExit() {
        GameManager.instance.playerScript.interactable = null;
        UIManager.instance.DisableInteract();
    }
    public void OnPlayerEnter() {
        GameManager.instance.playerScript.interactable = this;
        UIManager.instance.EnableInteract();
    }
}
