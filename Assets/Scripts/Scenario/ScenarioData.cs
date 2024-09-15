using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "ScenarioData", menuName = "Data/Scenario")]
    public class ScenarioData : ScriptableObject
    {
        public List<int> MenIdInOrder;
        public List<int> GoddessesIdInOrder;
        public DialogueData OpenningDialogue;
        public DialogueData[] CommentDialogues; // after each man
        public bool[] RightAnswers; // for each man
        public int HeavenManagerId;
        public int HellManagerId;
        public DialogueData FinalSuccessDialogue;
        public DialogueData FinalFailureDialogue;
    }
