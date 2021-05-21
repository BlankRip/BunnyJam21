using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player playerScript;
    public bool paused;

    
    public float delta;
    private float previous;

    private void Awake() {
        if(instance ==null)
            instance = this;
    }

    private void Start() {
        playerScript = FindObjectOfType<Player>();
        Time.timeScale = 1;
    }

    private void Update() {
        delta = Time.unscaledTime - previous;
        previous = Time.unscaledTime;
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
