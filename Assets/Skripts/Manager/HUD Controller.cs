using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreLabel;
    [SerializeField] TMP_Text TimerLabel;
    [SerializeField] GameObject popup;

    void Start(){
       // Debug.Log(Manager.ScoreM.Score);
       popup.SetActive(false);
        ScoreLabel.text = Manager.ScoreM.Score.ToString();
        TimerLabel.text = ((int) Manager.TimeM.timer).ToString();
    }

    void Update(){
        //Debug.Log(Manager.ScoreM.Score);

        ScoreLabel.text = Manager.ScoreM.Score.ToString();
        TimerLabel.text = ((int) Manager.TimeM.timer).ToString();
        if(Input.GetKeyDown(KeyCode.Escape) && !popup.activeSelf){
            popup.SetActive(true);
            GameController.dashState = true;
            GameController.scaleTime = 0;
        }else if(Input.GetKeyDown(KeyCode.Escape) && popup.activeSelf){
            popup.SetActive(false);
            GameController.dashState = false;
            GameController.scaleTime = 1;
        }

    }

}
