using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : AI_State
{
    private float catchDistance = 1;
    private float distance;
    private float searchingFor;
    private Vector3 chasePos;
    private bool caughtPlayer;


    public override void InitilizeState(AI ai) {
        Debug.Log("Entered Chase");
        ai.inChaseState = true;
        chasePos = GameManager.instance.playerScript.transform.position;
        chasePos.y = ai.transform.position.y;
        ai.agent.speed = ai.chaseSpeed;
        ai.agent.SetDestination(chasePos);
        searchingFor = 0;
        caughtPlayer = false;
        ai.myAnimator.SetBool("idle", false);
        ai.myAnimator.SetBool("walk", false);
        ai.myAnimator.SetBool("run", true);
    }

    public override void Exicute(AI ai) {
        if(!caughtPlayer) {
            Vector3 mag = (GameManager.instance.playerScript.transform.position - ai.transform.position);
            Debug.LogError(mag);
            distance = (GameManager.instance.playerScript.transform.position - ai.transform.position).sqrMagnitude;
            if(distance <= catchDistance * catchDistance) {
                GameManager.instance.playerScript.LockMovement();
                GameManager.instance.playerScript.Zap();
                if(mag.z < 0)
                {
                    ai.agent.enabled = false;
                    Vector3 pos = new Vector3(GameManager.instance.playerScript.gameObject.transform.position.x, 
                    ai.gameObject.transform.position.y, GameManager.instance.playerScript.gameObject.transform.position.z + 0.5f);
                    ai.transform.LookAt(new Vector3(GameManager.instance.playerScript.gameObject.transform.position.x, ai.gameObject.transform.rotation.y, GameManager.instance.playerScript.gameObject.transform.position.z));
                    ai.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    ai.gameObject.transform.position = pos;
                    GameManager.instance.playerScript.PositionMeAlt(ai, 0);
                }
                else if(mag.z > 0)
                {
                    ai.agent.enabled = false;
                    Vector3 pos = new Vector3(GameManager.instance.playerScript.gameObject.transform.position.x, 
                    ai.gameObject.transform.position.y, GameManager.instance.playerScript.gameObject.transform.position.z - 0.5f);
                    ai.transform.LookAt(new Vector3(GameManager.instance.playerScript.gameObject.transform.position.x, ai.gameObject.transform.rotation.y, GameManager.instance.playerScript.gameObject.transform.position.z));
                    ai.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    ai.gameObject.transform.position = pos;
                    GameManager.instance.playerScript.PositionMeAlt(ai, 180);
                }
                ai.myAnimator.SetTrigger("attack");
                caughtPlayer = true;
                return;
            }

            chasePos = GameManager.instance.playerScript.transform.position;
            chasePos.y = ai.transform.position.y;
            ai.agent.SetDestination(chasePos);

            if(!ai.chasing) {
                if(!GameManager.instance.paused)
                    searchingFor += GameManager.instance.delta;
                if(searchingFor >= ai.searchTime) {
                    searchingFor = 0;
                    ai.SwitchState(connections[0]);
                }
            } else if(searchingFor != 0)
                searchingFor = 0;
        }
    }

    public override void ExitState(AI ai) {
        ai.inChaseState = false;
        //ai.chasing = false;
    }
}