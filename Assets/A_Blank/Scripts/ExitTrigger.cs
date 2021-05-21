using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(GameManager.instance.readyToLeave && other.CompareTag("Player")) {
            GameManager.instance.playerScript.LockMovement();
            UIManager.instance.ShowVictory();
        }
    }
}
