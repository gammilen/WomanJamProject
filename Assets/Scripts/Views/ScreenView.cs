using UnityEngine;

public class ScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    
    public void Activate(Transform cameraObj)
    {
        cameraObj.SetParent(transform, false);
        cameraObj.localPosition = Vector3.zero;
        _view.SetActive(true);
    }

    public void Deactivate()
    {
        _view.SetActive(false);
    }
        
}