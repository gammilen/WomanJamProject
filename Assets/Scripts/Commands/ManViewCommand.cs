using UnityEngine;

public class ManViewCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private ManPresentController _presentController;
    
    private GameCommand _cmd;

    private void Start()
    {
        GameCore.Instance.Scenario.ManView.StartEvent += ShowMan;
        _presentController.MoveNextEvent += Complete;
    }

    private void ShowMan()
    {
        Debug.Log("Man view");
        _cmd = GameCore.Instance.Scenario.ManView;
        _screenViewer.SwitchView(_screenViewer.manView);
        _presentController.Present(GameCore.Instance.MenData.GetData(GameCore.Instance.Data.CurrentManId)).Forget();
    }

    private void Complete()
    {
        _cmd.Complete();
        _cmd = null;
    }

}