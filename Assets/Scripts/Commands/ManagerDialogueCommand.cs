using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerDialogueCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private DialogueController _dialogueController;
    
    private GameCommand _cmd;
    private List<int> _linesPool;
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

        _inDialogue = true;
        if (GameCore.Instance.Data.Decisions[^1].IsRight)
        {
            _cmd.Complete();
            return;
        }
        
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
        if (_linesPool.Count == 0)
        {
            _linesPool = GameCore.Instance.ManagersLines.Lines.Select((x, y) => y).ToList();
        }

        var index = Random.Range(0, _linesPool.Count - 1);
        var line = GameCore.Instance.ManagersLines.Lines[_linesPool[index]];
        _linesPool.RemoveAt(index);
        return line;
    }

    private void Complete()
    {
        if (!_inDialogue)
        {
            return;
        }
        _cmd.Complete();
    }

}