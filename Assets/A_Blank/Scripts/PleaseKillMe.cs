using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseKillMe : MonoBehaviour
{
    [SerializeField] AI myAi;

    private void Start() {
        if(myAi == null)
            myAi = GetComponentInParent<AI>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            GameManager.instance.playerScript.aiReadyToDie = myAi;
            UIManager.instance.EnableKill();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            GameManager.instance.playerScript.aiReadyToDie = null;
            UIManager.instance.DisableKill();
        }
    }
}