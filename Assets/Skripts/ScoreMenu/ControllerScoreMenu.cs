using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ControllerScoreMenu : MonoBehaviour
{

    //[SerializeField] Button Menu;
    //[SerializeField] Button Restart;

    [SerializeField] TMP_Text ScoreLabel;
    float count;

    void Start() {
        ScoreLabel.text = 0.ToString();
    }
    
    void Update(){

       StartCoroutine(UpdateScore());
    }

    public void LoadMenu() {
        Manager.MissionM.MainMenu();
    }

    public void RestartGame() {
        Manager.TimeM.Reset();
        Manager.ScoreM.Reset();
        Manager.MissionM.WalkScene();
    }

    IEnumerator UpdateScore(){
        while (count < Manager.ScoreM.Score ) {
            count += 10*Time.deltaTime;
            ScoreLabel.text = ((int) count).ToString();
            if(count + 10 > Manager.ScoreM.Score) {
                count += 3;
                ScoreLabel.text = ((int)Manager.ScoreM.Score).ToString();
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
