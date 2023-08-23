using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FallPlatform : StepPlatform
{
   public override void Operator(){
      if(player == null){
         Debug.Log("Non player in fall");
      }else{
         GameController.dashState = true;
         base.player.FallPlayer();
      }
   }
}
