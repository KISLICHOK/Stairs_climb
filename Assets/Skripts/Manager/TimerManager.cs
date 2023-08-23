using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour, IGameManager
{
    public float timer { get; private set; }
    public ManagerStatus status { get; private set; }
    public void Startup(){
        status = ManagerStatus.Initializing;
        timer = 30;
        status = ManagerStatus.Started;
    }

    public void Reset() {
        timer = 30;
    }
    
    public void SubtractionTime(float dt) => timer -= dt;
    public void AdditionTime(float dt) => timer += dt;
}
