using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RecordManager))]
[RequireComponent(typeof(TimerManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(AudioManager))]
public class Manager : MonoBehaviour
{   
    public static RecordManager ScoreM { get; private set; }
    public static TimerManager TimeM { get; private set; }
    public static MissionManager MissionM { get; private set; }
    public static AudioManager AudioM { get; private set; }
    public static bool IsReady { get; private set; }
    private List<IGameManager> _startSequence;
    
    void Awake() {
        DontDestroyOnLoad(gameObject);
        ScoreM = GetComponent<RecordManager>();
        TimeM = GetComponent<TimerManager>();
        MissionM = GetComponent<MissionManager>();
        AudioM = GetComponent<AudioManager>();
        _startSequence = new List<IGameManager>();
 
        _startSequence.Add(ScoreM);
        _startSequence.Add(TimeM);
        _startSequence.Add(MissionM);
        _startSequence.Add(AudioM);

        StartCoroutine(StartupManagers());
    }
    
    private IEnumerator StartupManagers() {
        foreach (IGameManager manager in _startSequence){
            manager.Startup();
        }
        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while(numReady < numModules){
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence){
                if(manager.status == ManagerStatus.Started){
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");
                //Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            yield return null;
        }
        IsReady = true;
        Debug.Log("All Managers started up");
        //Messenger.Broadcast();
    }
}
