
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStairs : MonoBehaviour
{
    [SerializeField] int N = 12;
    [SerializeField] UsualPlatform prefabUsalStep;
    [SerializeField] PlayerController _player;
    [SerializeField] FallPlatform prefabFallStep;
    [SerializeField] EnemyPlatform prefabEnemyStep;
    [SerializeField] EnemyPlatform prefabEnemyStep1;
    [SerializeField] BonusPlatfrom prefabBonusStep;
    private Vector2 startPosition;
    private int countStep;
    private StepPlatform[] _arrStep;
    private StepPlatform _currStep;
    public  const float offsetX = 0.6f;
    public  const float offsetY = 0.3f;

    public void Inizilisation(int way){
        startPosition = this.transform.position;
        _arrStep = new StepPlatform[N];
        for (int i = 0; i < _arrStep.Length; i++){
            _arrStep[(_arrStep.Length-1)-i] =  Instantiate<UsualPlatform>(prefabUsalStep);
            _arrStep[(_arrStep.Length-1)-i].transform.position = new Vector2(startPosition.x - (offsetX * i), startPosition.y - (offsetY * i));
            _arrStep[(_arrStep.Length-1)-i].name = _arrStep[(_arrStep.Length-1)-i].ToString() + ((_arrStep.Length-1)-i).ToString();
            _arrStep[(_arrStep.Length-1)-i].transform.SetParent(this.transform);
            Color random = getColorForStep();
            _arrStep[(_arrStep.Length-1)-i].GetComponent<Renderer>().material.color = random;
        }
        countStep = 0;
        _currStep = _arrStep[countStep]; 
        Generate(way);
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
    
    public void Generate(int way){
        Recharge();
        int a = (int)Random.Range(0f, 10f);
        switch(way){
            case 0:
                 a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(7, a, 6);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(10, a, 6);
                break;
            case 1:
                 a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(1, a, 6);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(4, a, 6);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(7, a, 4);
                GenerateStepAndFall(10, a, 4);
                break;
            case 2:
                 a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(2, a, 6);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(6, a, 6);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(10, a, 6);
                break;
            case 3: 
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndBonus(3, 4, 3);
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndFall(7, a, 5);
                break;
            case 4:
                a = (int)Random.Range(0f, 10f);
                GenerateStepAndDoor(3,4,3);
                GenerateStepAndDoor1(4,4,3);
                for(int i = 5; i < _arrStep.Length; ++i) { GenerateStepAndDoor1(i, 4, 3); }
            break;
        }
    }


    private void GenerateStepAndFall(int index, int way, int bound){
        Vector2 prevPosition = _arrStep[index].transform.position;
        Destroy(_arrStep[index].gameObject);
        if(way < bound){
            _arrStep[index] =  Instantiate<UsualPlatform>(prefabUsalStep);
             Color random = getColorForStep();
            _arrStep[index].GetComponent<Renderer>().material.color = random;
        }else{
            _arrStep[index] = Instantiate<FallPlatform>(prefabFallStep);
            _arrStep[index].Inizilisation(_player);
        }
        _arrStep[index].transform.position = prevPosition;
        _arrStep[index].transform.SetParent(this.transform);
    }

    private void GenerateStepAndBonus(int index, int way, int bound){
        Vector2 prevPosition = _arrStep[index].transform.position;
        Destroy(_arrStep[index].gameObject);
        if(way < bound){
            _arrStep[index] =  Instantiate<UsualPlatform>(prefabUsalStep);
             Color random = getColorForStep();
            _arrStep[index].GetComponent<Renderer>().material.color = random;
        }else{
            _arrStep[index] = Instantiate<BonusPlatfrom>(prefabBonusStep);
            _arrStep[index].Inizilisation(_player);
        }
        _arrStep[index].transform.position = prevPosition;
        _arrStep[index].transform.SetParent(this.transform);
    }

     private void GenerateStepAndDoor(int index, int way, int bound){
        Vector2 prevPosition = _arrStep[index].transform.position;
        Destroy(_arrStep[index].gameObject);
        if(way < bound){
            _arrStep[index] =  Instantiate<UsualPlatform>(prefabUsalStep);
             Color random = getColorForStep();
            _arrStep[index].GetComponent<Renderer>().material.color = random;
        }else{
            _arrStep[index] = Instantiate<EnemyPlatform>(prefabEnemyStep);
            _arrStep[index].Inizilisation(_player);
        }
        _arrStep[index].transform.position = prevPosition;
        _arrStep[index].transform.SetParent(this.transform);
    }
    private void GenerateStepAndDoor1(int index, int way, int bound){
        Vector2 prevPosition = _arrStep[index].transform.position;
        Destroy(_arrStep[index].gameObject);
        if(way < bound){
            _arrStep[index] =  Instantiate<UsualPlatform>(prefabUsalStep);
             Color random = getColorForStep();
            _arrStep[index].GetComponent<Renderer>().material.color = random;
        }else{
            _arrStep[index] = Instantiate<EnemyPlatform>(prefabEnemyStep1);
            _arrStep[index].Inizilisation(_player);
        }
        _arrStep[index].transform.position = prevPosition;
        _arrStep[index].transform.SetParent(this.transform);
    }

    private void Recharge(){
        for (int i = 0; i < _arrStep.Length; i++) {
            if(_arrStep[i] is not UsualPlatform step){
                Vector2 prevPosition = _arrStep[i].transform.position;
                Destroy(_arrStep[i].gameObject);
                _arrStep[i] =  Instantiate<UsualPlatform>(prefabUsalStep);
                Color random = getColorForStep();
                _arrStep[i].GetComponent<Renderer>().material.color = random;
                _arrStep[i].transform.position = prevPosition;
                _arrStep[i].transform.SetParent(this.transform);
            }
        }
    }

    public StepPlatform prev1Step(PartStairs ps){
        if(countStep % 12 == 0){
            return ps[ps._arrStep.Length-1];
        }else{
            return _arrStep[(countStep - 1) % 12];
        }
    }

    public StepPlatform prev2Step(PartStairs ps){
        if(countStep % 12 == 0){

            return ps[ps._arrStep.Length-2];
        }else if(countStep % 12 == 1){
            //Debug.Log("countstep == 1");
            return ps[ps._arrStep.Length-1];
        }else{
            return _arrStep[(countStep - 2) % 12];
        }
    }

    public void MoveCurrentStep(int i) => countStep += i;
    public StepPlatform this[int i] {
        get=>_arrStep[i];
    }
    public StepPlatform getCurrentStep{
        get => _arrStep[countStep  % 12];
    }

    private Color getColorForStep() { 
       return new Color(Random.Range(0.1f, 0.8f),Random.Range(0.0f,0.2f), Random.Range(0.1f,0.8f));
    }

    
}
