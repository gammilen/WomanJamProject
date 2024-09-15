using System.Collections.Generic;
using UnityEngine;

public class AnswersCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private DialogueController _dialogueController;
    
    private GameCommand _cmd;
    private bool _inDialogue;
    private void Start()
    {
        _cmd = GameCore.Instance.Scenario.ManAnswer;
        _cmd.StartEvent += SetAnswer;
        _dialogueController.Completed += FinishDialogue;
    }

    private void SetAnswer()
    {
        if (_inDialogue)
        {
            Debug.LogError("Already in dialogue");
        }
        _inDialogue = true;
        // TODO:
        // setup man view
        
        _screenViewer.SwitchView(_screenViewer.manView);
        
        var dialogueData = new List<DialogueData.DialogueLine>();
        dialogueData.Add(new DialogueData.DialogueLine()
        {
            CharacterId = GameCore.Instance.Data.CurrentManId,
            Text = GameCore.Instance.Answers.GetAnswer(
                GameCore.Instance.Data.CurrentManId, GameCore.Instance.Data.LastQuestionId)
        });
        _dialogueController.SetDialogue(dialogueData).Forget();

    }

    private void FinishDialogue()
    {
        if (!_inDialogue)
        {
            Debug.Log("Try to finish dialogue outside man answer");
            return;
        }

        _inDialogue = false;
        _cmd.Complete();
    }
        
}