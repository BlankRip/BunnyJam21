using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeDistraction : Interactable
{
    [SerializeField] GameObject distraction;
    [SerializeField] UnityEvent effectEvent;
    private bool activated;
    private void OnTriggerEnter(Collider other) {
        if(!activated && other.CompareTag("Player")) {
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            OnPlayerExit();
        }
    }

    public override void OnInteraction() {
        activated = true;
        effectEvent.Invoke();
        distraction.SetActive(true);
        OnPlayerExit();
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff() {
        yield return new WaitForSeconds(2);
        distraction.SetActive(false);
    }
}
