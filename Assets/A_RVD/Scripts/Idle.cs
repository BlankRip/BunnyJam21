using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AI_State
{
    public override void InitilizeState(AI ai)
    {
        Patrol idleState = new Patrol();
        Chase chaseState = new Chase();
        connections.Add(idleState);
        connections.Add(chaseState);
    }

    public override void Exicute(AI ai)
    {
        ai.myAnimator.SetBool("idle", true);
    }
}