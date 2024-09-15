using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Data/Character", order = 0)]
public class CharactersData : ScriptableObject
{
    [System.Serializable]
    public class CharacterData
    {
        public int Id;
        public CharacterType Type;
        public Sprite DialogueIcon;
        public string Name;
        public AudioClip Voice;
    }
    

    public List<CharacterData> Data;

    public CharacterData GetById(int id)
    {
        return Data.FirstOrDefault(x => x.Id == id);
    }
}

public enum CharacterType
{
    Goddess,
    Man,
    Manager,
    Boss,
    Familiar
}
