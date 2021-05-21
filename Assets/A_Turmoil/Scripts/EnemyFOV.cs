using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float fieldOfViewAngle;
    [SerializeField] float fieldOfViewRange;

    Vector3 playerDir;
    RaycastHit hit;
    float angle;

    [SerializeField] bool playerCaught;
    [SerializeField] bool inFOV;
    [SerializeField] float chaseTime;
    [SerializeField] float chaseTimer;

    // Start is called before the first frame update
    void Start()
    {
        //DEFAULT VALUES
        chaseTime = 3;
        fieldOfViewAngle = 90;
        fieldOfViewRange = 10;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyFOVAndChaseTimer();
    }

    public void enemyFOVAndChaseTimer()
    {
        playerDir = player.transform.position - transform.position;
        angle = Vector3.Angle(playerDir.normalized, transform.forward);

        Vector3 vectorLeft = Quaternion.AngleAxis(fieldOfViewAngle/2, Vector3.up) * transform.forward;
        Vector3 vectorRight = Quaternion.AngleAxis(-(fieldOfViewAngle / 2), Vector3.up) * transform.forward;

        Debug.DrawRay(transform.position, vectorRight.normalized * fieldOfViewRange, Color.white);
        Debug.DrawRay(transform.position, transform.forward * fieldOfViewRange, Color.white);
        Debug.DrawRay(transform.position, vectorLeft.normalized * fieldOfViewRange, Color.white);

        if (angle <= fieldOfViewAngle * 0.5f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= fieldOfViewRange)
            {
                if (Physics.Raycast(transform.position, playerDir.normalized, out hit, fieldOfViewRange))
                {
                    Debug.Log("Player within range!");
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player Caught!");
                        Debug.DrawRay(transform.position, playerDir.normalized * hit.distance, Color.red);
                        playerCaught = true;
                        inFOV = true;
                    }
                    else
                    {
                        Debug.Log("Hit a obstacle!");
                        Debug.DrawRay(transform.position, playerDir.normalized * hit.distance, Color.black);
                        inFOV = false;
                    }
                }
            }
            else
            {
                Debug.Log("Out of range!");
                Debug.DrawRay(transform.position, playerDir, Color.white);
                inFOV = false;
            }
        }
        else
        {
            Debug.Log("Did not hit anything!");
            Debug.DrawRay(transform.position, playerDir, Color.white);
            inFOV = false;
        }

        if (inFOV == true)
        {
            //START CHASE STATE
            chaseTimer = 0;
        }
        else if(playerCaught && !inFOV)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseTime)
            {
                //STOP CHASE STATE
                playerCaught = false;
                chaseTimer = 0;
            }
        }
    }
}
