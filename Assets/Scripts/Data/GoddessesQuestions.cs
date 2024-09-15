using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public int Id;
    public string Text;
}
    
[CreateAssetMenu(fileName = "Questions", menuName = "Data/Questions")]
public class GoddessesQuestions : ScriptableObject
{
        
    [System.Serializable]
    public class GoddessData
    {
        public int CharacterId;
        public List<QuestionData> Questions;
    }

    public List<GoddessData> Questions;
    
    

    public List<QuestionData> GetQuestions(int characterId)
    {
        var data = Questions.FirstOrDefault(x => x.CharacterId == characterId);
        return data?.Questions;
    }
}