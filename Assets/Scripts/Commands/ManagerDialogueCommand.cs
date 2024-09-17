using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerDialogueCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private DialogueController _dialogueController;
    [SerializeField] private PhoneController _phone;
    
    private static System.Random rng = new System.Random();  

    private GameCommand _cmd;
    private List<int> _linesPool = new List<int>();
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
        if (_linesPool.Count == 0)
        {
            _linesPool = GameCore.Instance.ManagersLines.Lines.Select((x, y) => y).ToList();
            Shuffle(_linesPool);
        }
        var line = GameCore.Instance.ManagersLines.Lines[_linesPool[0]];
        _linesPool.RemoveAt(0);
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


    public static void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
}