using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Patrol : AI_State
{
    private GameObject target;
    private int destPoint = 0;  //The target you want it to chase
    Vector3 currentDest;
    private bool reverse;

    //^When the enemy is spawned via script or if it's pre-placed in the world we want it to first
    //^Get it's location and store it so it knows where it's 'home' is
    //^We also want to set it's speed and then start wandering
    public override void InitilizeState(AI ai)
    {
        //^Get the NavMeshAgent so we can send it directions and set start position to the initial location
        Debug.Log("Entered Patrol");
        currentDest = Vector3.zero;
        reverse = false;
    }

    public override void Exicute(AI ai)
    {
        //^Choose the next destination point when the agent gets
        //^close to the current one.
        if (!ai.agent.pathPending && ai.agent.remainingDistance < 0.5f)
            GotoNextPoint(ai);
        //else if (ai.transform.position == currentDest) {
        //     ai.SwitchState(connections[0]);
        // }
    }

    void GotoNextPoint(AI ai)
    {
        //^Returns if no waypoints have been set up
        if (ai.waypoints.Length == 0)
            return;
        //^Set the agent to go to the currently selected destination.
        ai.agent.destination = ai.waypoints[destPoint].position;
        //^Choose the next point in the array as the destination,
        //^cycling to the start if necessary.
        if (ai.loop)
            destPoint = (destPoint + 1) % ai.waypoints.Length;
        else
        {
            if (reverse == false && destPoint == ai.waypoints.Length - 1 && destPoint > 0)
                reverse = true;
            else if (reverse == true && destPoint == 0)
                reverse = false;

            if (reverse)
                destPoint -= 1;
            else
                destPoint += 1;
        }
    }
}