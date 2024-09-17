using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class QuestionsCommand : MonoBehaviour
{
    [SerializeField] private ScreenViewer _screenViewer;
    [SerializeField] private QuestionsController _questionsController;
    
    private GameCommand _cmd;
    private int _currentGoddessIndex;
    
    private void Start()
    {
        GameCore.Instance.Scenario.GoddessQuestion.StartEvent += SetQuestions;
        _questionsController.QuestionChooseEvent += Complete;
    }

    private void SetQuestions()
    {
        Debug.Log("questions");
        _cmd = GameCore.Instance.Scenario.GoddessQuestion;
        
        var goddessId = GameCore.Instance.ScenarioData.GoddessesIdInOrder[_currentGoddessIndex];
        var questions =
            GameCore.Instance.Questions.GetQuestions(goddessId);
        _currentGoddessIndex++;
        if (_currentGoddessIndex >= GameCore.Instance.ScenarioData.GoddessesIdInOrder.Count)
        {
            _currentGoddessIndex = 0;
        }
        
        _screenViewer.SwitchView(_screenViewer.goddessesView);
        _questionsController.Question(goddessId, questions).Forget();
        
        // TODO:
        // start dialogue ??? (question)
    }

    private void Complete()
    {
        if (_cmd != null)
        {
            CompleteAsync().Forget();
        }
        
    }

    private async UniTaskVoid CompleteAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        _cmd.Complete();
        _cmd = null;

    }
        
}