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
        Debug.Log($"{this.name} is no longer the current interactable");
        GameManager.instance.playerScript.interactable = null;
        UIManager.instance.DisableInteract();
    }
    public void OnPlayerEnter() {
        Debug.Log($"{this.name} is the current interactable");
        GameManager.instance.playerScript.interactable = this;
        UIManager.instance.EnableInteract();
    }
}
