using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player playerScript;
    public bool paused;
    public bool readyToLeave;
    private int currentPickups;
    private int pickups;

    
    public float delta;
    private float previous;

    private void Awake() {
        if(instance ==null) {
            instance = this;
            playerScript = FindObjectOfType<Player>();
            pickups = FindObjectsOfType<PickUp>().Length;
            Time.timeScale = 1;
            readyToLeave = false;
        }
    }

    private void Update() {
        delta = Time.unscaledTime - previous;
        previous = Time.unscaledTime;
    }

    public void PickedUp() {
        currentPickups++;
        if(currentPickups == pickups) {
            readyToLeave = true;
            Debug.Log("Player Ready to exit sound effect");
        }
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
