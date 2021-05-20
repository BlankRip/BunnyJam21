using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : Interactable
{
    public Transform point1, point2;
    public bool pointuno;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            OnPlayerEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            OnPlayerExit();
    }

    public override void OnInteraction()
    {
        switch (pointuno)
        {
            case true:
                GameManager.instance.playerScript.cc.enabled = false;
                GameManager.instance.playerScript.gameObject.transform.position = point2.position;
                GameManager.instance.playerScript.cc.enabled = true;
                break;
            case false:
                GameManager.instance.playerScript.cc.enabled = false;
                GameManager.instance.playerScript.gameObject.transform.position = point1.position;
                GameManager.instance.playerScript.cc.enabled = true;
                break;
        }
    }
}