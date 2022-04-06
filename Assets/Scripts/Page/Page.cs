using UnityEngine;
using UnityEngine.Events;

public abstract class Page : MonoBehaviour, IPage
{
    public abstract void Open();
    public UnityEvent OnOpen { get; private set; } = new UnityEvent();
    public UnityEvent OnOpened { get; private set; } = new UnityEvent();

    public abstract void Close();
    public UnityEvent OnClose { get; private set; } = new UnityEvent();
    public UnityEvent OnClosed { get; private set; } = new UnityEvent();

    public PageState State { get; protected set; } = PageState.Close;
}
