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
    public bool inHiding;
    public int onPlayersAss;
    private int currentPickups;
    private int pickups;
    [SerializeField] AudioClip complete;
    public float delta;
    private float previous;

    private void Awake() {
        if(instance ==null) {
            instance = this;
            playerScript = FindObjectOfType<Player>();
            pickups = FindObjectsOfType<PickUp>().Length;
            Time.timeScale = 1;
            readyToLeave = false;
            inHiding = false;
            onPlayersAss = 0;
        }
    }

    private void Start() {
        UIManager.instance.totalPickups.text = pickups.ToString();
        UIManager.instance.currentPickups.text = "0";
    }

    private void Update() {
        delta = Time.unscaledTime - previous;
        previous = Time.unscaledTime;
    }

    public void PickedUp() {
        currentPickups++;
        UIManager.instance.currentPickups.text = currentPickups.ToString();
        if(currentPickups == pickups) {
            readyToLeave = true;
            GameAudio.instance.PlaySFxOneShot(complete, MixerDataBase.instance.sfxNormal);
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
