using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseObj;
    public void PauseGame() {
        GameManager.instance.paused = true;
        Time.timeScale = 0;
        pauseObj.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseObj.SetActive(false);
        GameManager.instance.paused = false;
    }
}
