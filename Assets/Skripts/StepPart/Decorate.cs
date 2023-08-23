using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void moveDash(float dX, float dY)
    {
        transform.Translate(new Vector2(-dX*Time.deltaTime, -dY*Time.deltaTime));
    }

    public  void moveStep(float dX, float dY)
    {
        transform.Translate(new Vector2(-dX, -dY));
        //++countStep;
        //_currStep = _arrStep[countStep]; 
    }
}
