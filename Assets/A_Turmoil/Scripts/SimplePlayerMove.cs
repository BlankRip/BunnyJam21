using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimplePlayerMove : MonoBehaviour
{

    [SerializeField] float speed;


    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            cc.Move((transform.forward * speed *  (Time.unscaledDeltaTime * (1 + (1.0f - Time.timeScale))))); 
        }

        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(-transform.right * speed * (Time.unscaledDeltaTime * (1 + (1.0f - Time.timeScale))));
        }

        if (Input.GetKey(KeyCode.S))
        {
            cc.Move(-transform.forward * speed * (Time.unscaledDeltaTime * (1 + (1.0f - Time.timeScale))));
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 finalVec = transform.right * speed * (Time.unscaledDeltaTime * (1 + (1.0f - Time.timeScale)));
            cc.Move(finalVec);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 1;
        }
    }
}
