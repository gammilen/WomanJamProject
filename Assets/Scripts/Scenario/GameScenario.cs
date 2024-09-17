using Cysharp.Threading.Tasks;
using UnityEngine;


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
        await UniTask.DelayFrame(1);
        OpeningDialogue.Start();
        await OpeningDialogue.CompletionTask;
        await UniTask.DelayFrame(1);

        for (int manIndex = 0; manIndex < Data.MenIdInOrder.Count; manIndex++)
        {
            ManView.Start();
            await ManView.CompletionTask;
            await UniTask.DelayFrame(1);

            for (int goddessIndex = 0; goddessIndex < 3; goddessIndex++)
            {
                GoddessQuestion.Start();
                await GoddessQuestion.CompletionTask;
                await UniTask.DelayFrame(1);

                ManAnswer.Start();
                await ManAnswer.CompletionTask;
                await UniTask.DelayFrame(1);

                if (goddessIndex == 0 && manIndex != 0)
                {
                    ManagerCall.Start();
                    await ManagerCall.CompletionTask;
                    await UniTask.DelayFrame(1);

                    ManagerDialogue.Start();
                    await ManagerDialogue.CompletionTask;
                    await UniTask.DelayFrame(1);
                }
            }
            
            Decision.Start();
            await Decision.CompletionTask;
            await UniTask.DelayFrame(1);

            ManResult.Start();
            await ManResult.CompletionTask;
            await UniTask.DelayFrame(1);

            GoddessesComment.Start();
            await GoddessesComment.CompletionTask;
            await UniTask.DelayFrame(1);
        }

        BossCall.Start();
        await BossCall.CompletionTask;
        await UniTask.DelayFrame(1);

        FinishDialogue.Start();
        await FinishDialogue.CompletionTask;
        await UniTask.DelayFrame(1);

        Finish.Start();
        await Finish.CompletionTask;
        await UniTask.DelayFrame(1);
        Application.Quit();
    }
}