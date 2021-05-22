using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] MeshRenderer renderer;
    [Range(0, 1)][SerializeField] float rimAmount = 0.35f;
    public virtual void OnInteraction() { 
        OnPlayerExit();
    }

    public void OnPlayerExit() {
        Debug.Log($"{this.name} is no longer the current interactable");
        GameManager.instance.playerScript.interactable = null;
        renderer.material.SetFloat("_KillMark", 0);
        UIManager.instance.DisableInteract();
    }
    public void OnPlayerEnter() {
        Debug.Log($"{this.name} is the current interactable");
        GameManager.instance.playerScript.interactable = this;
        renderer.material.SetFloat("_KillMark", rimAmount);
        UIManager.instance.EnableInteract();
    }
}
