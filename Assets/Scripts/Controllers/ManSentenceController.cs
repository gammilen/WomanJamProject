using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ManSentenceController : MonoBehaviour
{
    [SerializeField] private AudioClip _btnClick;
    [SerializeField] private GameObject _heavenNormal;
    [SerializeField] private GameObject _heavenPressed;
    [SerializeField] private GameObject _hellNormal;
    [SerializeField] private GameObject _hellPressed;
    [SerializeField] private SoundPlayer _soundPlayer;
    
    
    
    [SerializeField] private Hand _hand;
    public event Action DecisionMade;
    private bool _deciding;
    public async UniTaskVoid MakeDecision(bool toHeaven)
    {
        if (!_deciding)
        {
            Debug.LogError("Can't decide now");
            return;
        }

        _deciding = false;
        int index = CommandHelper.GetIndexOfCurrentMan();
        GameCore.Instance.Data.Decisions.Add(
            new ManDecision()
            {
                CharacterId = GameCore.Instance.Data.CurrentManId, 
                Decision = toHeaven,
                IsRight =
                    index + 1 == GameCore.Instance.ScenarioData.MenIdInOrder.Count ||
                    GameCore.Instance.ScenarioData.RightAnswers[index] == toHeaven
            });
        _soundPlayer.PlayFX(_btnClick);
        SetButtonPressed(toHeaven);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        
        _hand.gameObject.SetActive(false);
        DecisionMade?.Invoke();
        
    }

    private void SetButtonPressed(bool toHeaven)
    {
        _hellNormal.SetActive(false);
        _heavenNormal.SetActive(false);
        _hellPressed.SetActive(!toHeaven);
        _heavenPressed.SetActive(toHeaven);
    }

    private void ResetButtons()
    {
        _hellNormal.SetActive(true);
        _heavenNormal.SetActive(true);
        _hellPressed.SetActive(false);
        _heavenPressed.SetActive(false);
    }

    public void PrepareToMakeDecision()
    {
        _deciding = true;
        _hand.gameObject.SetActive(true);
        ResetButtons();
    }
}