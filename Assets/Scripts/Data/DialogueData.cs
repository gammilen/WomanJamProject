using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Data/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        [System.Serializable]
        public class DialogueLine
        {
            public int CharacterId;
            public string Text;
        }

        public List<DialogueLine> Dialogue;

        [SerializeField] private string fileName;

        #if UNITY_EDITOR
        [ContextMenu("Import dialogue")]
        private void ImportDialogue()
        {
            Dialogue = new List<DialogueLine>();
            TextAsset t = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/" + fileName + ".csv", typeof(TextAsset));
            var lines = t.text.Split("\n");
            foreach (var line in lines)
            {
                var pair = line.Split(',');
                Dialogue.Add(new DialogueLine()
                {
                    CharacterId = int.Parse(pair[0]),
                    Text = pair[1]
                });
            }
        }
#endif
    }