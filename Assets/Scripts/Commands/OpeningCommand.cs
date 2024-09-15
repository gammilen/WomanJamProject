using UnityEngine;

public class OpeningCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private OpeningEffector _effector;
    private GameCommand _cmd;
    
    private void Start()
    {
        if (GameCore.Instance.Scenario.Opening.Started)
        {
            Play();
        }
        else
        {
            GameCore.Instance.Scenario.Opening.StartEvent += Play;
        }
    }

    private void Play()
    {
        _cmd = GameCore.Instance.Scenario.Opening;
        
        _cmd.StartEvent -= Play;
        _screenViewer.SwitchView(_screenViewer.openingView);
        _effector.PlayEffect(Complete).Forget();
    }

    private void Complete()
    {
        _cmd.Complete();
        _cmd = null;
    }
        
}