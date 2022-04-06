using UnityEngine.Events;

public enum PageState
{
    Open,
    Close,
    InProcess
}

public interface IPage
{
    public abstract void Open();
    public abstract UnityEvent OnOpen { get; }
    public abstract UnityEvent OnOpened { get; }

    public abstract void Close();
    public abstract UnityEvent OnClose { get; }
    public abstract UnityEvent OnClosed { get; }

    public abstract PageState State { get; }
}