using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class MissionManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set;}
    
    public void Startup() {
        Debug.Log("Mission manager starting...");
        status = ManagerStatus.Started;
    } 
   

    public void FightScene() {
        SceneManager.LoadScene("FightScene");
    }

    public void WalkScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu() {
        gameObject.name = "Destroy";
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ScoreScene() {
        Debug.Log("ScoreScrenScene");
        SceneManager.LoadScene("ScoreScrenScene");
    }
}
