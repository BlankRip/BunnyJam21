using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlerter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy"))
            other.GetComponent<AI>().RecievedPlayerPosition(transform.position);
    }
}
