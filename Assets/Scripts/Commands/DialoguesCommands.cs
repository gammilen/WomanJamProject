using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DialoguesCommands : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private DialogueController _dialogueController;
    private GameCommand _cmd;
    private bool _inDialogue;
    
    private void Start()
    {
        GameCore.Instance.Scenario.OpeningDialogue.StartEvent += PlayOpeningDialogue;
        GameCore.Instance.Scenario.GoddessesComment.StartEvent += PlayGoddessesComment;
        GameCore.Instance.Scenario.FinishDialogue.StartEvent += PlayFinishDialogue;
        _dialogueController.Completed += CompleteCurrent;
    }

    private void PlayOpeningDialogue()
    {
        _cmd = GameCore.Instance.Scenario.OpeningDialogue;
        _cmd.StartEvent -= PlayOpeningDialogue;
        
        PlayDialogue(GameCore.Instance.ScenarioData.OpenningDialogue);
    }

    private void PlayGoddessesComment()
    {
        _cmd = GameCore.Instance.Scenario.GoddessesComment;
        _cmd.Complete();
        // PlayDialogue(GameCore.Instance.ScenarioData.CommentDialogues[CommandHelper.GetIndexOfCurrentMan()]);
    }

    private void PlayFinishDialogue()
    {
        _cmd = GameCore.Instance.Scenario.FinishDialogue;
        _cmd.StartEvent -= PlayFinishDialogue;

        bool success = GameCore.Instance.Data.Decisions.All(x => x.IsRight);
        var dialogue = success
            ? GameCore.Instance.ScenarioData.FinalSuccessDialogue
            : GameCore.Instance.ScenarioData.FinalFailureDialogue;
        PlayDialogue(dialogue); // TODO: set normal = false
    }
    

    private void PlayDialogue(DialogueData dialogueData)
    {
        if (_inDialogue)
        {
            Debug.LogError("Already in dialogue");
            return;
        }
        _inDialogue = true;
        
        _screenViewer.SwitchView(_screenViewer.goddessesView);

        _dialogueController.SetDialogue(dialogueData.Dialogue).Forget();
    }

    private void CompleteCurrent()
    {
        Debug.Log("Finish dialogue");
        if (!_inDialogue)
        {
            Debug.Log("Dialogue finished without command");
            return;
        }

        _inDialogue = false;
        _cmd.Complete();
    }
        
}