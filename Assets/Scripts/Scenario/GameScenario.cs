using Cysharp.Threading.Tasks;


public class GameScenario
{
    public readonly GameCommand Opening = new();
    public readonly GameCommand OpeningDialogue = new();
    public readonly GameCommand GoddessQuestion = new();
    public readonly GameCommand ManView = new();
    public readonly GameCommand ManAnswer = new();
    public readonly GameCommand Decision = new();
    public readonly GameCommand ManResult = new();
    public readonly GameCommand GoddessesComment = new();
    public readonly GameCommand ManagerCall = new();
    public readonly GameCommand ManagerDialogue = new();
    public readonly GameCommand BossCall = new();
    public readonly GameCommand FinishDialogue = new();
    public readonly GameCommand Finish = new();
    private readonly ScenarioData Data;

    public GameScenario(ScenarioData scenarioData)
    {
        Data = scenarioData;
    }
    
        
    public async UniTask Play()
    {
        
        Opening.Start();
        await Opening.CompletionTask;
        OpeningDialogue.Start();
        await OpeningDialogue.CompletionTask;
        
        for (int manIndex = 0; manIndex < Data.MenIdInOrder.Count; manIndex++)
        {
            ManView.Start();
            await ManView.CompletionTask;
            for (int goddessIndex = 0; goddessIndex < 3; goddessIndex++)
            {
                GoddessQuestion.Start();
                await GoddessQuestion.CompletionTask;
                ManAnswer.Start();
                await ManAnswer.CompletionTask;
                if (goddessIndex == 1 && manIndex != 0)
                {
                    ManagerCall.Start();
                    await ManagerCall.CompletionTask;
                    ManagerDialogue.Start();
                    await ManagerDialogue.CompletionTask;
                }
            }
            
            Decision.Start();
            await Decision.CompletionTask;
            ManResult.Start();
            await ManResult.CompletionTask;
            GoddessesComment.Start();
            await GoddessesComment.CompletionTask;
        }

        BossCall.Start();
        await BossCall.CompletionTask;
        FinishDialogue.Start();
        await FinishDialogue.CompletionTask;
        Finish.Start();
        await Finish.CompletionTask;
    }
}