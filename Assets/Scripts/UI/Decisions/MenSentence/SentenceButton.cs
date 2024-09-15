using UnityEngine;
using UnityEngine.EventSystems;

public class SentenceButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _toHeaven;
    [SerializeField] private ManSentenceController _sentenceController;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _sentenceController.MakeDecision(_toHeaven);
        
    }
}