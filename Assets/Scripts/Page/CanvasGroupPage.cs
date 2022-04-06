using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupPage : Page
{
    private const float OPEN_CLOSE_TIME = .3f;

    private CanvasGroup _canvasGroup = default;


    public override void Open()
    {
        if (State != PageState.Close) { return; }
        StartCoroutine(Opening());
    }

    public override void Close()
    {
        if (State != PageState.Open) { return; }
        StartCoroutine(Closing());
    }


    private IEnumerator Opening()
    {
        OnOpen.Invoke();
        State = PageState.InProcess;

        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = true;
        for (float t = 0; t < OPEN_CLOSE_TIME; t += Time.deltaTime)
        {
            _canvasGroup.alpha = t / OPEN_CLOSE_TIME;
            yield return null;
        }
        _canvasGroup.alpha = 1f;

        State = PageState.Open;
        OnOpened.Invoke();
    }

    private IEnumerator Closing()
    {
        OnClose.Invoke();
        State = PageState.InProcess;

        _canvasGroup.alpha = 1f;
        for (float t = OPEN_CLOSE_TIME; t > 0; t -= Time.deltaTime)
        {
            _canvasGroup.alpha = t / OPEN_CLOSE_TIME;
            yield return null;
        }
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0f;

        State = PageState.Close;
        OnClosed.Invoke();
    }


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
}
