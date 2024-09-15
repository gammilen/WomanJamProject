using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class QuestionsController : MonoBehaviour
{
    [SerializeField] private DialogueBox dialogue;
    [SerializeField] private CharactersData _charactersData;
    [SerializeField] private AudioClip _click;
    [SerializeField] private SoundPlayer _soundPlayer;
    

    
    public event Action QuestionChooseEvent;
    private List<QuestionData> _currQuestions;
    private bool _choosing;

    public async UniTaskVoid Question(int charId, List<QuestionData> questions)
    {
        await UniTask.DelayFrame(1);
        _choosing = true;
        
        var index = GameCore.Instance.ScenarioData.GoddessesIdInOrder.IndexOf(
            charId);
        var character = _charactersData.GetById(charId);
        dialogue.SetIndex(index);
        dialogue.SetQuestions(character.Name, questions.Select(x => x.Text).ToList());
        dialogue.gameObject.SetActive(true);
        _currQuestions = questions;
    }

    public void ChooseQuestion(int index)
    {
        if (!_choosing)
        {
            Debug.LogError("Can't choose question when not choosing");
        }
        _soundPlayer.PlayFX(_click);

        GameCore.Instance.Data.LastQuestionId = _currQuestions[index].Id;
        QuestionChooseEvent?.Invoke();
        dialogue.gameObject.SetActive(false);
    }
    
        
}