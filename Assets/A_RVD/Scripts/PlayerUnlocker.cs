using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlocker : MonoBehaviour
{
    public Player player;
    public void Unlock()
    {
        player.UnlockMovement();
    }
}