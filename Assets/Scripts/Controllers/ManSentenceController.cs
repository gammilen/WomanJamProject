using System;
using Cysharp.Threading.Tasks;
using RyanNielson.InputBinder;
using UnityEngine;

public class ManSentenceController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputBinder _binder;
    
    [SerializeField] private AudioClip _btnClick;
    [SerializeField] private GameObject _heavenNormal;
    [SerializeField] private GameObject _heavenPressed;
    [SerializeField] private GameObject _hellNormal;
    [SerializeField] private GameObject _hellPressed;
    [SerializeField] private SoundPlayer _soundPlayer;
    
    
    
    [SerializeField] private Hand _hand;
    public event Action DecisionMade;
    private bool _deciding;
    
    private void Start()
    {
        _binder.BindKey(KeyCode.Mouse0, InputEvent.Pressed, HandleClick);
    }
    
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
        _hand.SetMoving(false);
        SetButtonPressed(toHeaven);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        
        _hand.gameObject.SetActive(false);
        DecisionMade?.Invoke();
        
    }

    private void SetButtonPressed(bool toHeaven)
    {
        _hellNormal.SetActive(toHeaven);
        _heavenNormal.SetActive(!toHeaven);
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
        _hand.SetMoving(true);
        _hand.ResetPosition();
        ResetButtons();
    }

    private void HandleClick()
    {
        if (!_deciding)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject == _heavenNormal.gameObject)
            {
                MakeDecision(true).Forget();
            }
            else if (hit.collider.gameObject == _hellNormal.gameObject)
            {
                MakeDecision(false).Forget();
            }
        }
    }
}