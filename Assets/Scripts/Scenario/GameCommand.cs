using System;
using Cysharp.Threading.Tasks;

public class GameCommand
{
    public bool Started { get; private set; }
    public  UniTaskCompletionSource CompletionSource { get; private set; } 
    public event Action StartEvent;

    public UniTask CompletionTask => CompletionSource.Task;

    public void Complete()
    {
        CompletionSource.TrySetResult();
    }

    public void Start()
    {
        Started = true;
        CompletionSource = new UniTaskCompletionSource();
        StartEvent?.Invoke();
    }

}