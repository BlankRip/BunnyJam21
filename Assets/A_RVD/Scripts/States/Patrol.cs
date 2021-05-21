using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Patrol : AI_State
{
    private GameObject target;
    private int destPoint;  //The target you want it to chase
    Vector3 currentDest;
    private bool reverse;
    private bool goIdle;
    bool goingBack;

    //^When the enemy is spawned via script or if it's pre-placed in the world we want it to first
    //^Get it's location and store it so it knows where it's 'home' is
    //^We also want to set it's speed and then start wandering
    public override void InitilizeState(AI ai)
    {
        //^Get the NavMeshAgent so we can send it directions and set start position to the initial location
        Debug.Log("Entered Patrol");
        ai.agent.speed = ai.walkSpeed;
        currentDest = Vector3.zero;
        ai.initialPos.y = ai.transform.position.y;
        goingBack = false;
        ai.myAnimator.SetBool("idle", false);
        ai.myAnimator.SetBool("walk", true);
    }

    public override void Exicute(AI ai)
    {
        //^Choose the next destination point when the agent gets
        //^close to the current one.
        if (!ai.agent.pathPending && ai.agent.remainingDistance < ai.arriveRadius)
            GotoNextPoint(ai);
    }

    void GotoNextPoint(AI ai)
    {
        //^Returns if no waypoints have been set up
        if(ai.waypoints.Length < 2) {
            if(!goingBack) {
                ai.agent.destination = ai.initialPos;
                goingBack = true;
                return;
            } else {
                ai.transform.rotation = ai.initialRotation;
                ai.SwitchState(connections[0]);
            }
        } else {
            //^Choose the next point in the array as the destination,
            //^cycling to the start if necessary.
            if (ai.loopWaypoints) {
                destPoint = (destPoint + 1) % ai.waypoints.Length;
                ai.agent.destination = ai.waypoints[destPoint].position;
                if(destPoint == 1)
                    ai.SwitchState(connections[0]);
            } else {
                if (reverse == false && destPoint == ai.waypoints.Length - 1) {
                    reverse = true;
                    goIdle = true;
                } else if (reverse == true && destPoint == 0) {
                    reverse = false;
                    goIdle = true;
                }

                if (reverse)
                    destPoint -= 1;
                else
                    destPoint += 1;

                //^Set the agent to go to the currently selected destination.
                ai.agent.destination = ai.waypoints[destPoint].position;

                if(goIdle) {
                    goIdle = false;
                    ai.SwitchState(connections[0]);
                }
            }
        }        
    }
}