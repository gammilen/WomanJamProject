using UnityEngine;

public class SentenceShowCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private ManPresentController _manPresentController;
    
    private GameCommand _cmd;

    private void Start()
    {
        GameCore.Instance.Scenario.ManResult.StartEvent += ShowSentence;
    }

    private void ShowSentence()
    {
        _cmd = GameCore.Instance.Scenario.ManResult;
        _screenViewer.SwitchView(_screenViewer.manView);

        
        _manPresentController.ShowResult(Complete).Forget();
    }

    private void Complete()
    {
        if (_cmd == null)
        {
            return;
        }
        var indexOf = CommandHelper.GetIndexOfCurrentMan();
        if (indexOf == -1)
        {
            Debug.LogError("No current man");
            GameCore.Instance.Data.CurrentManId = GameCore.Instance.ScenarioData.MenIdInOrder[0];
        }
        else
        {
            GameCore.Instance.Data.CurrentManId = GameCore.Instance.ScenarioData.MenIdInOrder[indexOf + 1];
        }
        _cmd.Complete();
        _cmd = null;
    }

}