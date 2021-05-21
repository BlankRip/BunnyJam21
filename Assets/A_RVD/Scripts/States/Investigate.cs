using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : AI_State
{
    private float investigatedFor;

    private float distance;
    private bool atPoint;
    public override void InitilizeState(AI ai) {
        ai.agent.speed = ai.walkSpeed;
        ai.agent.SetDestination(ai.pointNoted);
        ai.myAnimator.SetBool("idle", false);
        ai.myAnimator.SetBool("run", false);
        ai.myAnimator.SetBool("walk", true);
        atPoint = false;
        investigatedFor = 0;
    }

    public override void Exicute(AI ai) {
        if(!atPoint) {
            distance = (ai.pointNoted - ai.transform.position).sqrMagnitude;
            if(distance < ai.arriveRadius * ai.arriveRadius)
                atPoint = true;
        } else {
            investigatedFor += Time.deltaTime;
            if(investigatedFor >= ai.investigateTime)
                ai.SwitchState(connections[0]);
        }
    }
}
