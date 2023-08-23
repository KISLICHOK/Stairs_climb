using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISample : MonoBehaviour
{
    public void GoToMainMenu(){
        Manager.MissionM.MainMenu();
    }

     public void OnSoundToggle() {
        Manager.AudioM.soundMute = !Manager.AudioM.soundMute;
     }
}
