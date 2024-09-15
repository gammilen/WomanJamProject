public static class CommandHelper
{
    public static int GetIndexOfCurrentMan()
    {
        return GameCore.Instance.ScenarioData.MenIdInOrder.IndexOf(
            GameCore.Instance.Data.CurrentManId);
    }
    
}