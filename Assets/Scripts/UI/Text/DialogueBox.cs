using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{

    public SpriteRenderer Renderer;
    public TMP_Text Text;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    public List<Sprite> _sprites;

    public List<GameObject> questions;

    public void Set(CharactersData.CharacterData charData)
    {
        SetActiveQuestions(false);
        if (_icon != null) _icon.sprite = charData.DialogueIcon;
        _name.text = charData.Name;
        Text.enabled = true;
    }

    public void SetIndex(int index)
    {
        Renderer.sprite = _sprites[index];
        
    }

    public void SetQuestions(string charName, List<string> questionsText)
    {
        _name.text = charName;

        Text.enabled = false;
        for (var i = 0; i < questions.Count; i++)
        {
            questions[i].GetComponentInChildren<TMP_Text>().text = "-" + questionsText[i];
        }

        SetActiveQuestions(true);
    }

    public void SetActiveQuestions(bool active)
    {
        foreach (var q in questions)
        {
            q.SetActive(active);
        }
    }
    
        
}