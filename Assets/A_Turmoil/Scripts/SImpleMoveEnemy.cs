using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SImpleMoveEnemy : MonoBehaviour
{
    Vector3 dir;
    [SerializeField] GameObject player;
    [SerializeField] float speed;

    public bool freezeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        freezeEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(freezeEnemy == true)
        {

        }
        else
        {
            /*
            dir = player.transform.position - transform.position;
            dir.y = 0;
            transform.LookAt(player.transform.position * Time.timeScale);
            //transform.rotation = Quaternion.LookRotation(dir,transform.up);
            //transform.position += (dir.normalized * speed) * Time.deltaTime;
            */

            
            Vector3 lookVector = player.transform.position - transform.position;
            lookVector.y = 0;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10 * Time.deltaTime);
            transform.position += (lookVector.normalized * speed) * Time.deltaTime;
            
            Debug.DrawRay(transform.position, lookVector, Color.green);
        }
    }

    
}
