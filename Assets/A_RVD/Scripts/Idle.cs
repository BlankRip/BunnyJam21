using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AI_State
{
    public override void InitilizeState(AI ai)
    {
        ai.SwitchState(connections[0]);
    }

    public override void Exicute(AI ai)
    {
        ai.myAnimator.SetBool("idle", true);
    }
}