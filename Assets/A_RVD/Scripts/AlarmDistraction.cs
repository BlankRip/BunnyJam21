using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmDistraction : MonoBehaviour
{
    [SerializeField] float alarmTime = 2;
    private bool ringing;
    private float ringingFor;

    private void Update() {
        if(ringing) {
            ringingFor += Time.deltaTime;
            if(ringingFor >= alarmTime) {
                ringingFor = 0;
                ringing = false;
                Debug.LogError("THE ALARM IS Stopped");
            }
        }
    }

    public void PlayEffect() {
        Debug.LogError("THE ALARM IS RINGING");
        ringing = true;
    }
}
