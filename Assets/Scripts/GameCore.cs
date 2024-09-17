using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    
    public static GameCore Instance { get; private set; }
    public GlobalData Data { get; private set; }
    public GameScenario Scenario { get; private set; }
    
    public ScenarioData ScenarioData;
    public GoddessesQuestions Questions;
    public MenAnswers Answers;
    public ManagersDialogueLines ManagersLines;
    public MenData MenData;
    

    private void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(this);
        Data = new GlobalData();
        Scenario = new GameScenario(ScenarioData);
    }

    private void Start()
    {
        StartSequence().Forget(); 
    }

    private async UniTaskVoid StartSequence()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.UnscaledDeltaTime);
        Instance.Data.SetCurrentManId(ScenarioData.MenIdInOrder[0]);
        Scenario.Play().Forget();
    }
        
}