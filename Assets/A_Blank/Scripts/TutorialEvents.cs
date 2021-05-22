using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public void ChargeClock() {
        UIManager.instance.WatchReady();
    }
}
