using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AI_State
{
    float idelingFor;
    public override void InitilizeState(AI ai) {
        if(ai.waypoints.Length > 1) {
            if(Random.Range(0, 101) > 85) {
                ai.myAnimator.SetBool("idle", true);
            ai.myAnimator.SetBool("walk", false);
            ai.myAnimator.SetBool("run", true);
                idelingFor = 0;
            }
            else
                ai.SwitchState(connections[0]);
        } else {
            ai.myAnimator.SetBool("idle", true);
            ai.myAnimator.SetBool("walk", false);
            ai.myAnimator.SetBool("run", true);
        }
    }

    public override void Exicute(AI ai) {
        if(ai.waypoints.Length > 1) {
            idelingFor += Time.deltaTime;
            if(idelingFor >= ai.idleTime) {
                ai.myAnimator.SetBool("idle", false);
                ai.myAnimator.SetBool("walk", false);
                ai.myAnimator.SetBool("run", true);
                ai.SwitchState(connections[0]);
            }
        }
    }
}