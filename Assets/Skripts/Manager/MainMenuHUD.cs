using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHUD : MonoBehaviour
{
    public void LoadGame(){
        if(Manager.IsReady){
            Manager.MissionM.WalkScene();
        }
    }

    public void CloseGame(){
        if(Manager.IsReady){
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
