using UnityEngine;

public class ScreenViewer : MonoBehaviour
{
    public Transform Camera;
    public ScreenView openingView;
    public ScreenView goddessesView;
    public ScreenView manView;
    public ScreenView decisionView;
    public ScreenView finalView;

    private ScreenView currentView;

    private void Start()
    {
        SwitchView(openingView);
        goddessesView.Deactivate();
        manView.Deactivate();
        decisionView.Deactivate();
        finalView.Deactivate();
    }

    public void SwitchView(ScreenView view)
    {
        if (currentView != null)
        {
            currentView.Deactivate();
        }

        currentView = view;
        view.Activate(Camera);
    }
}