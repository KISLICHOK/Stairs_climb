using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StepPlatform : MonoBehaviour
{   
    protected PlayerController player;
    public virtual void Inizilisation(PlayerController pl){
        player = pl;
    }
    public virtual void Operator(){}
    
}
