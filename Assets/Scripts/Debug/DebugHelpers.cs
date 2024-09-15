using UnityEngine;
public class DebugHelpers : MonoBehaviour
{
    [ContextMenu("GlobalData dump")]
    private void DumpGlobalData()
    {
        Debug.Log(JsonUtility.ToJson(GameCore.Instance.Data));
    }
        
}