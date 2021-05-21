using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInteract : Interactable
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            OnPlayerExit();
        }
    }

    public override void OnInteraction() {
        //^ Do whatever

        base.OnInteraction();
    }
}
