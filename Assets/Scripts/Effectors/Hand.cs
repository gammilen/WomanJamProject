using UnityEngine;


public class Hand : MonoBehaviour
{
    public Camera _camera;
    void Update()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = _camera.transform.position.z + _camera.nearClipPlane;
        var newPos = transform.position;
        newPos.x = mousePosition.x;
        transform.position = newPos;
    }
}