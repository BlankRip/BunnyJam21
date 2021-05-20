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
            dir = player.transform.position - transform.position;
            transform.LookAt(player.transform.position * Time.deltaTime);
            transform.position += (transform.forward * speed) * Time.deltaTime;
        }
    }

    
}
