using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RyanNielson.InputBinder;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class DialogueController : MonoBehaviour
    {
        [SerializeField] private InputBinder _binder;
        [SerializeField] private TextWriter _writer;
        [SerializeField] private DialogueBox _normalDialogueBox;
        
        [SerializeField] private CharactersData _charactersData;
        [SerializeField] private float _timePerChar = 0.3f;
        [SerializeField] private SoundPlayer _soundPlayer;
        

        public event Action Completed;
        private List<DialogueData.DialogueLine> _currentDialogue;
        private int _currentLineIndex;

        private void Start()
        {
            
            _binder.BindKey(KeyCode.Mouse0, InputEvent.Pressed, HandleClick);
        }

        public async UniTaskVoid SetDialogue(List<DialogueData.DialogueLine> dialogue)
        {
            if (_currentDialogue != null)
            {
                Debug.LogError("Already has dialogue");
                return;
            }
            await UniTask.DelayFrame(1);

            _currentDialogue = dialogue;
            _currentLineIndex = 0;
            _normalDialogueBox.gameObject.SetActive(true);

            UpdateDialogueLine();
        }


        private void HandleClick()
        {
            if (_writer.IsProcessing())
            {
                _soundPlayer.StopFX();
                _writer.ForceEnd();
                return;
            }
            if (_currentDialogue != null)
            {
                if (_currentDialogue.Count == _currentLineIndex + 1)
                {
                    CloseDialogue();
                }
                else
                {
                    _currentLineIndex++;
                    UpdateDialogueLine();
                }

            }
        }

        private void CloseDialogue()
        {
            _currentDialogue = null;
            _currentLineIndex = 0;
            Completed?.Invoke();
            _normalDialogueBox.gameObject.SetActive(false);
        }

        private void UpdateDialogueLine()
        {
            var character = _charactersData.GetById(
                _currentDialogue[_currentLineIndex].CharacterId);
            if (character.Type == CharacterType.Goddess || character.Type == CharacterType.Man)
            {
                var index = 1;
                if (character.Type == CharacterType.Goddess)
                {
                    index = GameCore.Instance.ScenarioData.GoddessesIdInOrder.IndexOf(
                        character.Id);
                }
                _normalDialogueBox.SetIndex(index);
            }
            else
            {
                _normalDialogueBox.SetIndex(2);
            }
            
            // SetDialogueBox(character.Type != CharacterType.Boss && character.Type != CharacterType.Manager);
            _normalDialogueBox.Set(character);
            _writer.AddWriter(
                _normalDialogueBox.Text,
                _currentDialogue[_currentLineIndex].Text,
                _timePerChar,
                false);
            _soundPlayer.PlayFX(character.Voice);
        }
        
        
    }