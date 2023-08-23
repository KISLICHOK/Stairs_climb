using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChabgeScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null){
            Debug.Log("Defeat");
            StartCoroutine(SleepAndGoWalk());
        }
    }

    IEnumerator SleepAndGoWalk(){
        yield return new WaitForSeconds(1.2f);
        Manager.MissionM.WalkScene();
    }
}
