using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : StepPlatform
{
    [SerializeField] Sprite openDoor;
    public override void Operator()
    {
        GameController.dashState = true;
        SpriteRenderer[] child = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(OpenDoorAndNextScene(child));
    }

    private IEnumerator OpenDoorAndNextScene(SpriteRenderer[] items) {
        foreach(var item in items){
            if(item.name == "CloseDoorPref") { 
                Debug.Log("find");
                item.sprite = openDoor; 
                item.transform.Translate(new Vector2(0.2f,-0.1f));
            }
        }
        yield return new WaitForSeconds(1f);
        Manager.MissionM.FightScene();
    }
}
