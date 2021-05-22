using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Button watchButton;
    [SerializeField] Image watchFill;
    [SerializeField] Button killButton;
    [SerializeField] Button interactButton;
    [SerializeField] Image modeButton;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] AudioClip unfreeze;
    int currmode = 0;
    public GameObject dialoguePannel;
    public TextMeshProUGUI dialogueTextSpace;
    public TypeWriteText currentWriter;
    public Sprite[] modes;
    public TextMeshProUGUI currentPickups;
    public TextMeshProUGUI totalPickups;

    private void Awake() {
        if(instance ==null)
            instance = this;
        
        modeButton.sprite = modes[currmode];
    }

    public void InitilizeUI() {
        WatchReady();
        DisableKill();
        DisableInteract();
    }

    public void UpdateMoveMode(int mode) {
        modeButton.sprite = modes[mode];
    }

    public void WatchReady() {
        watchButton.interactable = true;
        GameAudio.instance.PlaySFxOneShot(unfreeze, MixerDataBase.instance.sfxNormal);
    }
    public void WatchUsed() {
        watchButton.interactable = false;
    }
    public void WatchRecovering(float max, float current) {
        watchFill.fillAmount = Mathf.InverseLerp(max, 0, current);
    }

    public void EnableKill() {
        killButton.interactable = true;
    }
    public void DisableKill() {
        killButton.interactable = false;
    }

    public void EnableInteract() {
        interactButton.interactable = true;
    }
    public void DisableInteract() {
        interactButton.interactable = false;
    }

    public void ShowEnd() {
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }
    public void ShowVictory() {
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
    }

    public void NextDialogue() {
        currentWriter.TextInteraction();
    }
}
