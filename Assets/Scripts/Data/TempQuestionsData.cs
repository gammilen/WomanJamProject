using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "TempQuestions", menuName = "Data/TempQuestions")]
    public class TempQuestionsData : ScriptableObject
    {
        public List<QuestionData> Questions;
        public List<GoddessQuestions> IdList;

        [System.Serializable]
        public class GoddessQuestions
        {
            public int CharacterId;
            public List<int> QuestionsIdList;
        }
    }
}