using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI moveModeText;
    [SerializeField] Button watchButton;
    [SerializeField] Button killButton;
    [SerializeField] Button interactButton;

    private void Awake() {
        if(instance ==null)
            instance = this;
    }

    public void InitilizeUI() {
        WatchReady();
        DisableKill();
        DisableInteract();
    }

    public void UpdateMoveMode(string mode) {
        moveModeText.SetText(mode);
    }

    public void WatchReady() {
        watchButton.interactable = true;
    }
    public void WatchUsed() {
        watchButton.interactable = false;
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
}
