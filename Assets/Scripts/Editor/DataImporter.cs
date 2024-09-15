using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DataImporter
    {
        [MenuItem("Tools/ImportQuestionsAnswers")]
        public static void ImportQuestionsAnswers()
        {
            TextAsset t = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/answers.csv", typeof(TextAsset));

            var questions = new Dictionary<int, string>();
            var menData = new List<MenAnswers.ManAnswers>();

            int manIndex = 0;
            int lastQuestionId = -1;
            var lines = t.text.Split("\n");
            foreach (var line in lines)
            {
                var lineData = line.Split("\t");
                var questionId = int.Parse(lineData[0]);
                if (!questions.ContainsKey(questionId))
                {
                    questions[questionId] = lineData[1];
                }
                if (lastQuestionId != questionId)
                {
                    lastQuestionId = questionId;
                    manIndex = 0;
                }

                if (menData.Count <= manIndex)
                {
                    menData.Add(new MenAnswers.ManAnswers());
                }

                menData[manIndex].Answers.Add(new MenAnswers.AnswerData()
                {
                    QuestionId = questionId,
                    Answer = lineData[2]
                });
                manIndex++;

            }
            
            var dataId = AssetDatabase.FindAssets("t:ScenarioData")[0];
            var data = AssetDatabase.LoadAssetAtPath<ScenarioData>(AssetDatabase.GUIDToAssetPath(dataId));

            Debug.Log(menData.Count);
            for (var index = 0; index < data.MenIdInOrder.Count; index++)
            {
                menData[index].CharacterId = data.MenIdInOrder[index];
            }
            
            dataId = AssetDatabase.FindAssets("t:MenAnswers")[0];
            var menSO = AssetDatabase.LoadAssetAtPath<MenAnswers>(AssetDatabase.GUIDToAssetPath(dataId));
            menSO.Answers = menData;

            dataId = AssetDatabase.FindAssets("t:TempQuestionsData")[0];
            var questionsData = AssetDatabase.LoadAssetAtPath<TempQuestionsData>(AssetDatabase.GUIDToAssetPath(dataId));
            questionsData.Questions = questions.Select(x => new QuestionData() {Id = x.Key, Text = x.Value}).ToList();

            AssetDatabase.SaveAssets();
        }

        [MenuItem("Tools/ConvertGoddessesQuestion")]
        public static void TemptToRealGoddessesQuestions()
        {
            var dataId = AssetDatabase.FindAssets("t:TempQuestionsData")[0];
            var data = AssetDatabase.LoadAssetAtPath<TempQuestionsData>(AssetDatabase.GUIDToAssetPath(dataId));
            
            dataId = AssetDatabase.FindAssets("t:GoddessesQuestions")[0];
            var questionsData = AssetDatabase.LoadAssetAtPath<GoddessesQuestions>(AssetDatabase.GUIDToAssetPath(dataId));

            var goddesses = new List<GoddessesQuestions.GoddessData>();
            foreach (var goddessData in data.IdList)
            {
                goddesses.Add(new GoddessesQuestions.GoddessData()
                {
                    CharacterId = goddessData.CharacterId,
                    Questions = data.Questions.Where(x => goddessData.QuestionsIdList.Contains(x.Id)).ToList()
                });
            }

            questionsData.Questions = goddesses;
            AssetDatabase.SaveAssets();

        }
        
        
    }
}