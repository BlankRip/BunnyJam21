using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add this project so that in case any debug lienes are left they will be ignored by compiler when built
public class DebugPerformanceProtection : MonoBehaviour
{
     private void Awake() {
#if UNITY_EDITOR
    Debug.unityLogger.logEnabled = true;
#else
    Debug.unityLogger.logEnabled = false;
#endif    
    }
}
