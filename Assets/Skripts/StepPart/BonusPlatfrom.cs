using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlatfrom : StepPlatform
{
    public override void Operator()
    {
        Manager.TimeM.AdditionTime(15);
    }
}
