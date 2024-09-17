using UnityEngine;

public class PhoneCallCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private PhoneController _phoneController;
    
    private GameCommand _cmd;

    private void Start()
    {
        GameCore.Instance.Scenario.ManagerCall.StartEvent += TryStartCall;
        GameCore.Instance.Scenario.BossCall.StartEvent += StartBossCall;
        _phoneController.MoveNextEvent += Complete;
        // TODO: phone controller . picked up += cmd.Complete 
    }

    private void StartBossCall()
    {
        _cmd = GameCore.Instance.Scenario.BossCall;
        StartCall();
    }

    private void TryStartCall()
    {
        _cmd = GameCore.Instance.Scenario.ManagerCall;
        if (GameCore.Instance.Data.Decisions[^1].IsRight)
        {
            _cmd.Complete();
            return;
        }
        StartCall();
    }

    private void StartCall()
    {
        _screenViewer.SwitchView(_screenViewer.goddessesView);
        _phoneController.StartRinging();
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