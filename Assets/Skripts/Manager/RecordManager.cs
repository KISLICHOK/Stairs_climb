using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour, IGameManager
{
    public int Score { get; private set; }
    public int levelCounter { get; private set; }
    public ManagerStatus status { get; private set; }
    public void Startup() {
        status = ManagerStatus.Initializing;
        Score = 0;
        levelCounter = 0;
        Debug.Log("Data manager starting...");
        status = ManagerStatus.Started;
    }

    public void AddScore(int summand) { 
        if(summand >= 0) { Score += summand; } 
    }

    public void Addlevel() => ++levelCounter;

    public void Reset() { 
        Score = 0;
    }
    

}
