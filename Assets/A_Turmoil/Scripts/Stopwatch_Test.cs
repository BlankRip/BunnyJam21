using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch_Test : MonoBehaviour
{
    bool isFreezing;

    [SerializeField] float freezeTimer;
    [SerializeField] float freezeCooldown;

    [SerializeField] float freezeCooldownTime;
    [SerializeField] float freezeTime;

    GameObject[] enemies;

    bool onerun = false;

    // Start is called before the first frame update
    void Start()
    {
        isFreezing = false;
        freezeCooldown = 0;
        freezeTimer = 0;
        freezeCooldownTime = 10;
        freezeTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopWatchFreeze();
        }

        if (freezeCooldown >= 0)
        {
            freezeCooldown -= Time.deltaTime;
            if (freezeCooldown <= 0)
            {
                freezeCooldown = 0;
            }
        }

        if (isFreezing == true)
        {
            if (onerun == false)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<SImpleMoveEnemy>().freezeEnemy = true;
                }
                freezeTimer = freezeTime;
                onerun = true;
            }

            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<SImpleMoveEnemy>().freezeEnemy = false;
                }
                isFreezing = false;
                onerun = false;
                freezeTimer = freezeTime;
            }
            else
            {

            }

        }
    }

    public void StopWatchFreeze()
    {
        if (freezeCooldown <= 0)
        {
            freezeCooldown = freezeCooldownTime;
            isFreezing = true;
        }
    }
}
