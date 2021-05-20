using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public GameObject mesh;
    bool open = false;

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
        switch (open)
        {
            case true:
                mesh.SetActive(true);
                open = false;
                break;
            case false:
                mesh.SetActive(false);
                open = true;
                break;
        }
    }
}