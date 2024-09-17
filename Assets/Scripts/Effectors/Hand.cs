using UnityEngine;


public class Hand : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _initPosition;
    private bool _isMoving;
    public Camera _camera;

    private void Awake()
    {
        _initPosition = transform.position;
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        transform.position = _initPosition;
    }
    
    void Update()
    {
        if (!_isMoving) return;
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = _camera.transform.position.z + _camera.nearClipPlane;
        var newPos = transform.position;
        newPos.x = mousePosition.x;
        transform.position = newPos;
    }

    public void SetMoving(bool isMoving)
    {
        _isMoving = isMoving;
        if (isMoving)
        {
            _spriteRenderer.transform.localScale = 1.2f * Vector3.one;
        }
        else
        {
            _spriteRenderer.transform.localScale = 1.1f * Vector3.one;
        }
    }
}