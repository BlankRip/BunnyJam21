using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEController : MonoBehaviour
{
    public Material PEMat;
    public float wavePowerControll;
    public float wavePowerControllLerp;
    public float t = 0;
    public bool cooldown;
    public bool startStopwatch;
    public float stopwatchTimer;

    public float max = 75, min = 5;

    public bool oneCircle = false;
    public int iteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        PEMat.SetFloat("_WaveStrength", wavePowerControll);
    }

    // Update is called once per frame
    void Update()
    {
        PEMat.SetFloat("_WaveStrength", wavePowerControllLerp);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!cooldown)
            {
                startStopwatch = true;
                cooldown = true;
                stopwatchTimer = 10f;
            }
        }

        if(cooldown == true)
        {
            stopwatchTimer -= Time.deltaTime;
            if (startStopwatch == true)
            {
                Debug.Log("FROZEN...");
            }

            if (stopwatchTimer <= 5)
            {
                startStopwatch = false;
                //begin sickness 
                if (iteration <= 1)
                {
                    wavePowerControllLerp = Mathf.Lerp(max, min, t);

                    t += (Time.deltaTime / 2.5f);
                    if (t > 1)
                    {
                        float temp = max;
                        max = min;
                        min = temp;
                        t = 0.0f;
                        iteration++;
                    }
                }
                else
                {
                    wavePowerControllLerp = 0;
                }

                if (stopwatchTimer <= 0)
                {
                    //stop sickness
                    wavePowerControllLerp = 0;
                    t = 0;
                    max = 75;
                    min = 5;
                    iteration = 0;
                    cooldown = false;
                    stopwatchTimer = 0;
                }
            }
        }


        /*
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (iteration <= 1)
            {
                if (cooldown == false)
                {
                    wavePowerControllLerp = Mathf.Lerp(max, min, t);

                    t += (Time.deltaTime / 2);

                    if (t > 1)
                    {
                        float temp = max;
                        max = min;
                        min = temp;
                        t = 0.0f;
                        iteration++;
                    }
                }
            }
            else
            {
                wavePowerControllLerp = 0;
            }
        }
        else
        {
            wavePowerControllLerp = 0;
            t = 0;
            max = 50;
            min = 5;
            iteration = 0;
        }
        */
        
    }
}
