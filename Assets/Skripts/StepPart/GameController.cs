using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int N = 12;
    [SerializeField] PartStairs _originalPart;
    [SerializeField] GameObject floor;
    [SerializeField] Decorate[] arrDecorate;
    [SerializeField] GameObject sign;
    public PlayerController playerContrl;
    private PartStairs[] _arrPart;
    public const float offsetX = 0.6f;
    public const float offsetY = 0.3f;
    Vector2 startPosition;
    Vector2 position4;
    Vector2 position3;
    //StepPlatform currentPlatform;
    PartStairs _currentPart;
    public  static bool dashState = false;
    public static int scaleTime = 1;
    int indexGenerate = 1;
    
    public StepPlatform GetCurrentPlatform {
        get => _arrPart[1].getCurrentStep;
    }
    public StepPlatform GetPrev1Platfrom {
        get => _arrPart[1].prev1Step(_arrPart[0]);
    }
    public StepPlatform GetPrev2Platfrom {
        get => _arrPart[1].prev2Step(_arrPart[0]);
    }

    void Awake(){
        dashState = false;
    }
    void Start()
    {
        //Player = _player;
        startPosition = _originalPart.transform.position;
        _arrPart = new PartStairs[4];
        _arrPart[0] = Instantiate<PartStairs>(_originalPart);
        _arrPart[0].transform.position = startPosition - new Vector2(offsetX*N, offsetY*N);
        _arrPart[0].name = _arrPart[0].ToString() + (0).ToString();
        _arrPart[1] = _originalPart;
        _arrPart[1].name = _arrPart[1].ToString() + (1).ToString();
        _arrPart[2] = Instantiate<PartStairs>(_originalPart);
        _arrPart[2].transform.position = startPosition + new Vector2(offsetX*N, offsetY*N);
        _arrPart[2].name = _arrPart[2].ToString() + (2).ToString();
        _arrPart[3] = Instantiate<PartStairs>(_originalPart);
        _arrPart[3].transform.position = startPosition + new Vector2(offsetX*N, offsetY*N)*2;
        _arrPart[3].name = _arrPart[3].ToString() + (3).ToString();
        _arrPart[0].Inizilisation(3);
        _arrPart[1].Inizilisation(0);
        _arrPart[2].Inizilisation(1);
        _arrPart[3].Inizilisation(2);
        position3 = _arrPart[2].transform.position;
        position4 = _arrPart[3].transform.position;
        dashState = false;
        sign.SetActive(false);
    }


    void Update()
    {
        if(Manager.TimeM.timer < 0.8f) {
            Manager.MissionM.ScoreScene();
        }
        Manager.TimeM.SubtractionTime(Time.deltaTime*scaleTime);
        if(!dashState) {
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                playerContrl.MakeStepSound();
                playerContrl.ChangeSpriteWalk();
                GetCurrentPlatform.Operator();
                foreach(var part in _arrPart){
                    part.moveStep(offsetX, offsetY);
                    part.MoveCurrentStep(1);
                }
                foreach(var decor in arrDecorate){
                    decor.moveStep(offsetX, offsetY);
                }
                floor.transform.Translate(new Vector2(-offsetX, -offsetY*1.2f));
                Manager.ScoreM.AddScore(1);
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                if(GetCurrentPlatform is FallPlatform) {
                    Manager.ScoreM.AddScore(2);
                    foreach(var part in _arrPart){
                        part.MoveCurrentStep(3);
                    }
                    StartCoroutine(MoveDashAll());
                } else {
                    StartCoroutine(Stun());
                }
            }
       }
       if(_arrPart[3].transform.position.x < position3.x) {
            Refresh();
            _arrPart[3].Generate(indexGenerate);
            if(indexGenerate > 4) {
                indexGenerate = 1;
            } else { 
                ++indexGenerate; 
            }
       }
      // Debug.DrawLine(GetCurrentPlatform.transform.position, GetCurrentPlatform.transform.position - new Vector3(0,-2), Color.red);
       //Debug.DrawLine(GetPrev1Platfrom.transform.position, GetPrev1Platfrom.transform.position - new Vector3(0,-2), Color.red);
       //Debug.DrawLine(GetPrev2Platfrom.transform.position, GetPrev2Platfrom.transform.position - new Vector3(0,-2), Color.red);
    }


    private IEnumerator MoveDashAll() {
        dashState = true;
        float progress = 0;
        while(progress < 0.5){
            progress += Time.deltaTime;
                    foreach(var part in _arrPart) {
                        part.moveDash(offsetX*6, offsetY*6);
                    }
                    foreach(var decor in arrDecorate){
                        decor.moveDash(offsetX*6, offsetY*6);
                    }
            yield return null;
        }
        GetPrev2Platfrom.Operator();
        GetPrev1Platfrom.Operator();
        dashState = false;
    }

   
    private void Refresh() {
        _arrPart[0].transform.position = position4;
        PartStairs temp = _arrPart[0];
        for(int i = 0; i < _arrPart.Length-1; i++) {
            _arrPart[i] = _arrPart[i+1];
        }
        _arrPart[_arrPart.Length-1] = temp;
    }

    private IEnumerator Stun() {
        dashState = true;
        sign.SetActive(true);
        float progress = 0;
        while(progress < 0.7f){
            progress += Time.deltaTime;
            yield return null;
        }
        sign.SetActive(false);
        dashState = false;
    }

}
