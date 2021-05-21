using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Patrol : AI_State
{
    private Vector3 startPosition;  //Give it a startPosition so it knows where it's 'home' location is.
    private bool wandering = true;  //Set a bool or state so it knows if it's wandering or chasing a player
    private bool chasing = false;
    private GameObject target;  //The target you want it to chase
    Vector3 currentDest;
    //^When the enemy is spawned via script or if it's pre-placed in the world we want it to first
    //^Get it's location and store it so it knows where it's 'home' is
    //^We also want to set it's speed and then start wandering
    public override void InitilizeState(AI ai)
    {
        //^Get the NavMeshAgent so we can send it directions and set start position to the initial location
        Debug.Log("Entered Patrol");
        startPosition = ai.transform.position;
        currentDest = Vector3.zero;
    }

    //^When we wander we essentially want to pick a random point and then send the agent there
    void Wander(AI ai)
    {
        //^Pick a random location within wander-range of the start position and send the agent there
        Vector3 destination = startPosition + new Vector3(Random.Range(-ai.wanderRange, ai.wanderRange), 0, Random.Range(-ai.wanderRange, ai.wanderRange));
        Debug.Log(destination);
        ai.agent.SetDestination(destination);
        currentDest = destination;
    }

    public override void Exicute(AI ai)
    {
        if (currentDest == Vector3.zero)
            Wander(ai);
        else if (ai.transform.position == currentDest) {
            ai.SwitchState(connections[0]);
        }
    }
}