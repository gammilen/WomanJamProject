using RyanNielson.InputBinder;
using UnityEngine;

public class FinalCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private InputBinder _binder;
    
    private GameCommand _cmd;

    private void Start()
    {
        GameCore.Instance.Scenario.Finish.StartEvent += Finish;
        _binder.BindKey(KeyCode.Mouse0, InputEvent.Pressed, Complete);

        // TODO: final effector . Completed += Complete
    }

    private void Finish()
    {
        _cmd = GameCore.Instance.Scenario.Finish;
        _cmd.StartEvent -= Finish;
        
        _screenViewer.SwitchView(_screenViewer.finalView);
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