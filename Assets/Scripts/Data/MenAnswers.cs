using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Answers", menuName = "Data/Answers")]
    public class MenAnswers : ScriptableObject
    {
        [System.Serializable]
        public class AnswerData
        {
            public int QuestionId;
            public string Answer;
        }
        [System.Serializable]
        public class ManAnswers
        {
            public int CharacterId;
            public List<AnswerData> Answers = new List<AnswerData>();
        }

        public List<ManAnswers> Answers;

        public string GetAnswer(int charId, int questionId)
        {
            var man = Answers.FirstOrDefault(x => x.CharacterId == charId);
            if (man == null)
            {
                return string.Empty;
            }
            var answer = man.Answers.FirstOrDefault(x => x.QuestionId == questionId);
            return answer == null ? string.Empty : answer.Answer;
        }
    }
}