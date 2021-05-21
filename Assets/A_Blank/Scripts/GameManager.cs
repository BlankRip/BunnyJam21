using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player playerScript;
    public bool paused;

    private void Awake() {
        if(instance ==null)
            instance = this;
    }

    private void Start() {
        playerScript = FindObjectOfType<Player>();
        Time.timeScale = 1;
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }
}
