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
    
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        _questionController.ChooseQuestion(index);

    }
}