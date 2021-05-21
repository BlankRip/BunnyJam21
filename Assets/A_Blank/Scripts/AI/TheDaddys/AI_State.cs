using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State : MonoBehaviour
{
    public List<AI_State> connections;

    public AI_State() {
        connections = new List<AI_State>();
    }

    public virtual void InitilizeState(AI ai) { }

    public virtual void Exicute(AI ai) { }

    public virtual void ExitState(AI ai) { }
}
