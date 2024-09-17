using UnityEngine;
using UnityEngine.EventSystems;

public class SentenceButton : MonoBehaviour
{
    [SerializeField] private bool _toHeaven;
    [SerializeField] private ManSentenceController _sentenceController;
    
    public void OnMouseDown()
    {
        _sentenceController.MakeDecision(_toHeaven).Forget();
        
    }
}