using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField]  Enemy[] _arrPref;
    [SerializeField] Transform _playerPosition;
    [SerializeField] Transform _enemyPosition;
    [SerializeField] float smooth;
    [SerializeField] float FrameOnSwitch;
    [SerializeField] Sprite sword;
    [SerializeField] Sprite shield;
    [SerializeField] Sprite fireball;
    [SerializeField] GameObject enemyChoiseObject;
    [SerializeField] GameObject playerChoiseObject;
    [SerializeField] GameObject AnswerRound;
    [SerializeField] GameObject AnswerPlayerRound;

    Color AnswerRoundColor;
    Enemy[] curretEnemy;
    bool _allOnPlace = false;
    bool finishPrepare = false; 
    bool momentForAnswer = false;
    bool checkdAnswer = false;
    bool stopRoll = false;
    bool startShuffle = false;
    AttackType? ansPlayer = null;
    AttackType enemyAttack;

    bool playerWin = false;

    int healthEnemy=1;

    void Awake() {
        AnswerRoundColor = AnswerPlayerRound.GetComponent<SpriteRenderer>().color;
        curretEnemy = new Enemy[1];
        for(int i = 0; i < 1; i++) { 
            Debug.Log(_arrPref.Length);
            int a = Random.Range(0, _arrPref.Length);
            curretEnemy[i] = Instantiate<Enemy>(_arrPref[a]);
        }
        curretEnemy[0].transform.position = new Vector3(-_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);

    }

    void Start() {
//        Debug.Log(Manager.ScoreM.Score);
         foreach(var enemy in curretEnemy){
                enemy.StartInizialisation(1);
            }
        enemyChoiseObject.SetActive(false);
        playerChoiseObject.GetComponent<SpriteRenderer>().sprite = null;
        AnswerRound.SetActive(false);
        AnswerPlayerRound.SetActive(false);
        if(Manager.ScoreM.levelCounter < 2){
            healthEnemy = 1;
        }else if(Manager.ScoreM.levelCounter < 5){
            healthEnemy = 2;
        }else{
            healthEnemy = 3;
        }
    }

    // Update is called once per frame
    void Update() {
        if(!_allOnPlace){
            _player.transform.position = Vector2.Lerp(_player.transform.position,
                                    _playerPosition.position, smooth*Time.deltaTime);
            //Debug.Log(_player.transform.position);
            foreach(var enemy in curretEnemy){
                enemy.transform.position = Vector2.Lerp(enemy.transform.position,  
                                        _enemyPosition.position, smooth*Time.deltaTime);
            }

            if(Mathf.Abs(_player.transform.position.x) - Mathf.Abs(_playerPosition.position.x) < Mathf.Abs(0.1f)){
                _allOnPlace = true;
                finishPrepare = true;
                //Debug.Log("ok2");
                startShuffle = true;
            }
        }

        if(startShuffle) { 
            startShuffle = false;
            stopRoll = false;
            //Debug.Log("StartShuffle");
            StartCoroutine(ShuffleEnemyAnswer()); 
        }

        if(finishPrepare) { //Debug.Log("StartPreapre");
        finishPrepare = false;
        StartCoroutine(Prepare()); }

        if(momentForAnswer) { //Debug.Log("prepareAnswer"); 
        momentForAnswer = false;
        StartCoroutine(MomentForAnswer()); 
        }

        if(checkdAnswer) { //Debug.Log("CheckAnswer: " + checkdAnswer); 
        checkdAnswer = false;
        StartCoroutine(CheckWiner()); 
        }

    }

    private IEnumerator Prepare() {
        finishPrepare = false;
        yield return new WaitForSeconds(1f);
        momentForAnswer = true;
        Debug.Log("Prepare Done;");
    }

    private IEnumerator MomentForAnswer() {
        AnswerRound.SetActive(true);
        AnswerPlayerRound.SetActive(true);
        stopRoll = true;
        Debug.Log("Start Moment for Answer;");
        momentForAnswer = false;
        float duration = 0f;
        bool downBottom = false;
        while(!downBottom && duration < 1f) {
            duration += Time.deltaTime;
            downBottom = GetAnswerFromPlayer();
            yield return null;
        }
        checkdAnswer = true;
    }

    private bool GetAnswerFromPlayer(){
        if(Input.GetKeyDown(KeyCode.LeftArrow)) { 
            ansPlayer = AttackType.Sword; 
            Debug.Log("Sword"); 
            playerChoiseObject.GetComponent<SpriteRenderer>().sprite = sword;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            ansPlayer = AttackType.Shield; 
            Debug.Log("Shield"); 
            playerChoiseObject.GetComponent<SpriteRenderer>().sprite = shield;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            ansPlayer = AttackType.Fireball; 
            Debug.Log("Fireball"); 
            playerChoiseObject.GetComponent<SpriteRenderer>().sprite = fireball;
            return true;
        }
        else { ansPlayer = null; return false; }
    }

    private IEnumerator ShuffleEnemyAnswer() {
        enemyChoiseObject.SetActive(true);
        
        while(!stopRoll){
            setObjectEnemyChoise();
            yield return new WaitForSeconds(0.1f);
        }
        int answer = Random.Range(0, 10);
        if (answer%3 == 0) {
            enemyAttack = AttackType.Sword;
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = sword;
        } else if (answer%3 == 1) {
            enemyAttack = AttackType.Shield;
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = shield;
        } else {
            enemyAttack = AttackType.Fireball;
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = fireball;
        }
        yield return null;
    }

    private void setObjectEnemyChoise() {
        if (enemyChoiseObject.GetComponent<SpriteRenderer>().sprite == sword) {
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = shield;
        } else if (enemyChoiseObject.GetComponent<SpriteRenderer>().sprite == shield) {
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = fireball;
        } else {
            enemyChoiseObject.GetComponent<SpriteRenderer>().sprite = sword;
        }
    }

    private IEnumerator CheckWiner(){
        checkdAnswer = false;
        yield return new WaitForSeconds(0.3f);
        if(ansPlayer == AttackType.Sword && enemyAttack == AttackType.Fireball) {
            playerWin = true;
            AnswerPlayerRound.GetComponent<SpriteRenderer>().color = Color.green;
        } else if (ansPlayer == AttackType.Shield && enemyAttack == AttackType.Sword) {
            playerWin = true;
            AnswerPlayerRound.GetComponent<SpriteRenderer>().color = Color.green;
        } else if (ansPlayer == AttackType.Fireball && enemyAttack == AttackType.Shield) { 
            playerWin = true;
            AnswerPlayerRound.GetComponent<SpriteRenderer>().color = Color.green;
        }else{
            playerWin = false;
            AnswerPlayerRound.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if(playerWin) {
            Debug.Log("You win");
            curretEnemy[0].transform.position = new Vector3(curretEnemy[0].transform.position.x,curretEnemy[0].transform.position.y, 10f);
            StartCoroutine(MovePlayer());
            yield return new WaitForSeconds(0.3f);
            Manager.ScoreM.AddScore(20);
             --healthEnemy;
            if(healthEnemy == 0){
                curretEnemy[0].gameObject.AddComponent<Rigidbody2D>();
                curretEnemy[0].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, -4f));
                curretEnemy[0].gameObject.GetComponent<Rigidbody2D>().AddTorque(10f);
                yield return new WaitForSeconds(3f);
                Manager.TimeM.AdditionTime(4);
                Manager.ScoreM.Addlevel();
                Manager.MissionM.WalkScene();
            }else{
                Debug.Log(healthEnemy);
                //_allOnPlace = false;
                finishPrepare = false; 
                momentForAnswer = false;
                checkdAnswer = false;
                stopRoll = false;
                startShuffle = false;
                playerChoiseObject.GetComponent<SpriteRenderer>().sprite = null;
                AnswerRound.SetActive(false);
                AnswerPlayerRound.GetComponent<SpriteRenderer>().color = AnswerRoundColor;
                AnswerPlayerRound.SetActive(false);
                //Debug.Log(_allOnPlace);
            }
        }else{
            Debug.Log("You Lose");
             _player.transform.position = new Vector3(_player.transform.position.x,_player.transform.position.y, 10f);
            StartCoroutine(MoveEnemy());
            yield return new WaitForSeconds(0.3f);
            _player.AddComponent<Rigidbody2D>();
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, -4f));
            _player.GetComponent<Rigidbody2D>().AddTorque(10f);
            yield return new WaitForSeconds(2.5f);
            Manager.MissionM.ScoreScene();
        }
    }

    private IEnumerator MovePlayer() {
        float path = 0;
        Vector2 enemyPosition = new Vector2(_enemyPosition.transform.position.x-0.3f, _enemyPosition.transform.position.y);
        Vector2 oldPosition = _player.transform.position;
        while(path<0.8f) {
            _player.transform.position = Vector2.Lerp(_player.transform.position,
                                        enemyPosition, path);
            path += 1.3f*Time.deltaTime;
            yield return null;
        }
        Debug.Log("GoBack");
        path = 0;
        while(path < 1) {
            _player.transform.position = Vector2.Lerp(_player.transform.position,
                                        oldPosition, path);
            path += 1.5f*Time.deltaTime;
            yield return null;
        }
        if(healthEnemy != 0){
            _allOnPlace = false;
        }
    }

    private IEnumerator MoveEnemy() {
        float path = 0;
        Vector2 playerPosition = new Vector2(_player.transform.position.x+0.2f, _player.transform.position.y);
        Vector2 oldPosition = _enemyPosition.transform.position;
        while(path<0.8f) {
            curretEnemy[0].transform.position = Vector2.Lerp(curretEnemy[0].transform.position,
                                        playerPosition, path);
            path += 1 * Time.deltaTime;
            yield return null;
        }
       /* Debug.Log("OK");
        path = 0;
        while(!Mathf.Approximately(path,1)) {
           curretEnemy[0].transform.position = Vector2.Lerp(curretEnemy[0].transform.position,
                                        oldPosition, path);
            path += 0.003f;
            yield return null;
        }*/
    }

}
