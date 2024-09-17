using System.Collections.Generic;
using UnityEngine;

public class ManagerDialogueCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private DialogueController _dialogueController;
    [SerializeField] private PhoneController _phone;
    
    private GameCommand _cmd;
    private int _currIndex;
    private bool _inDialogue;

    private void Start()
    {
        _cmd = GameCore.Instance.Scenario.ManagerDialogue;
        _cmd.StartEvent += StartManagerDialogue;
        _dialogueController.Completed += Complete;
    }

    private void StartManagerDialogue()
    {
        if (_inDialogue)
        {
            Debug.LogError("Already in dialogue");
            return;
        }

        if (GameCore.Instance.Data.Decisions[^1].IsRight)
        {
            _cmd.Complete();
            return;
        }
        _inDialogue = true;

        
        _screenViewer.SwitchView(_screenViewer.goddessesView);
        var dialogue = new List<DialogueData.DialogueLine>();
        dialogue.Add(new DialogueData.DialogueLine()
            {CharacterId = GameCore.Instance.Data.Decisions[^1].Decision ? 
                GameCore.Instance.ScenarioData.HeavenManagerId : 
                GameCore.Instance.ScenarioData.HellManagerId,
                Text = GetLine()
            });
        _dialogueController.SetDialogue(dialogue).Forget();
    }

    private string GetLine()
    {
        var line = GameCore.Instance.ManagersLines.Lines[_currIndex];
        _currIndex++;
        return line;
    }

    private void Complete()
    {
        if (!_inDialogue)
        {
            return;
        }

        _phone.PutDown();
        _inDialogue = false;
        _cmd.Complete();
    }
}