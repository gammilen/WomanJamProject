using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ManagersDialoguesLines", menuName = "Data/Managers Lines")]
    public class ManagersDialogueLines : ScriptableObject
    {
        public List<string> Lines;
    }
}