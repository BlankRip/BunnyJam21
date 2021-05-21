using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    public Animator myAnimator;
    public float arriveRadius = 0.2f;
    public float idleTime = 3;
    public float searchTime = 2;
    public NavMeshAgent agent;
    public float walkSpeed = 3.5f;
    public float chaseSpeed = 5;
    public Transform[] waypoints;
    protected AI_State currentState;
    public bool loopWaypoints;

    [Space]
    [Header("FOV Stuff")]
    private Transform player;
    [SerializeField] float fieldOfViewAngle = 90;
    [SerializeField] float fieldOfViewRange = 10;
    [SerializeField] LayerMask playerAndObstacles;

    [SerializeField] bool inFOV;
    Vector3 playerDir;
    RaycastHit hit;
    float angle;

    [HideInInspector] public bool chasing;
    [HideInInspector] public bool inChaseState;
    [HideInInspector] public Vector3 pointNoted;
    [HideInInspector] public Vector3 initialPos;
    [HideInInspector] public Quaternion initialRotation;
    [HideInInspector] protected bool dead;
    protected Chase chaseState;

    protected void AiInitilize() {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        initialRotation = transform.rotation;

        //chaseTime = 3;
        player = GameManager.instance.playerScript.transform;
    }

    protected void StandardUpdates() {
        ViewCone();
    }

    public virtual void RecievedPlayerPosition(Vector3 pos, bool isPlayer) { 
        pointNoted.x = pos.x;
        pointNoted.y = transform.position.y;
        pointNoted.z = pos.z;
    }

    public void ViewCone() 
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
                if (Physics.Raycast(transform.position, playerDir.normalized, out hit, fieldOfViewRange, playerAndObstacles))
                {
                    Debug.Log("Player within range!");
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player Caught!");
                        Debug.DrawRay(transform.position, playerDir.normalized * hit.distance, Color.red);
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

        if (!chasing && inFOV == true)
        {
            //START CHASE STATE
            chasing = true;
            if(!inChaseState)
                SwitchState(chaseState);
        }
        else if(chasing && !inFOV)
        {
            chasing = false;
        }
    }

    public void Death() {
        UIManager.instance.DisableKill();
        dead = true;
        StartCoroutine(Dieing());
    }

    IEnumerator Dieing() {
        yield return new WaitForSeconds(1);
        UIManager.instance.WatchReady();
        Destroy(this.gameObject);
    }

    public void SwitchState(AI_State state) {
        currentState.ExitState(this);
        currentState = state;
        currentState.InitilizeState(this);
    }
}
