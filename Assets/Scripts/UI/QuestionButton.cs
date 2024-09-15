using UnityEngine;
using UnityEngine.EventSystems;

public class QuestionButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int index;
    [SerializeField] private QuestionsController _questionController;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _questionController.ChooseQuestion(index);
        
    }
    
}