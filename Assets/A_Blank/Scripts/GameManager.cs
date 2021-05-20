using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player playerScript;

    private void Awake() {
        if(instance ==null)
            instance = this;
    }

    private void Start() {
        playerScript = FindObjectOfType<Player>();
    }
}
