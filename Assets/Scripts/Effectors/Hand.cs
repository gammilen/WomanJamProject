using UnityEngine;


public class Hand : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        var newPos = transform.position;
        newPos.x = mousePosition.x;
        transform.position = newPos;
    }
}