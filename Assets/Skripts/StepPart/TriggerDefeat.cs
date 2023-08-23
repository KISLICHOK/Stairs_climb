using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDefeat : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null){
            Debug.Log("Defeat");
            StartCoroutine(SleepAndReload());
        }
    }

    IEnumerator SleepAndReload(){
        yield return new WaitForSeconds(1f);
        Manager.MissionM.ScoreScene();
    }
}
