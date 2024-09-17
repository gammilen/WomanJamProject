using System.Collections.Generic;

[System.Serializable]
public class GlobalData
{
    public int CurrentManId { get; private set; }
    public int LastManId { get; private set; } = -1;
    public List<ManDecision> Decisions = new();
    public int LastQuestionId;

    public void SetCurrentManId(int id)
    {
        LastManId = CurrentManId;
        CurrentManId = id;
    }
}