using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MenData", menuName = "Data/Men")]
public class MenData : ScriptableObject
{
    [System.Serializable]
    public class ManData
    {
        public int CharacterId;
        public string Bio;
        public Sprite View;
    }

    public List<ManData> Data;

    public ManData GetData(int charId)
    {
        return Data.FirstOrDefault(x => x.CharacterId == charId);
    }
}