using System.Collections.Generic;

[System.Serializable]
public class GlobalData
{
    public int CurrentManId;
    public List<ManDecision> Decisions = new();
    public int LastQuestionId;
}