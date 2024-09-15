using UnityEngine;

public class FinalCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    private GameCommand _cmd;

    private void Start()
    {
        GameCore.Instance.Scenario.Finish.StartEvent += Finish;
        // TODO: final effector . Completed += Complete
    }

    private void Finish()
    {
        _cmd = GameCore.Instance.Scenario.Finish;
        _cmd.StartEvent -= Finish;
        
        _screenViewer.SwitchView(_screenViewer.finalView);
        
        // TODO:
        // start photo effect

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