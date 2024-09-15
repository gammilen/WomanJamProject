using UnityEngine;

public class DecisionCommand : MonoBehaviour
{
    [SerializeField] private ManSentenceController _sentenceController;
    [SerializeField] private ScreenViewer _screenViewer;
    
    
    private GameCommand _cmd;
    private void Start()
    {
        GameCore.Instance.Scenario.Decision.StartEvent += DoDecision;
        _sentenceController.DecisionMade += Complete;
    }

    private void DoDecision()
    {
        _cmd = GameCore.Instance.Scenario.Decision;

        _screenViewer.SwitchView(_screenViewer.decisionView);
        _sentenceController.PrepareToMakeDecision();
    }

    private void Complete()
    {
        if (_cmd == null)
        {
            return;
        }
        _cmd.Complete();
        _cmd = null;
    }

}