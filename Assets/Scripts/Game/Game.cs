using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Field _currentField = default;

    [SerializeField]
    private Page _winPage = default;
    [SerializeField]
    private Page _losePage = default;


    public void Win()
    {
        void NewGameOnClose()
        {
            NewGame(Levels.Instance.Current);
            _winPage.OnClose.RemoveListener(NewGameOnClose);
        }

        _winPage.Open();
        Levels.Instance.IncreaseProgress();
        _winPage.OnClose.AddListener(NewGameOnClose);
    }

    public void Lose()
    {
        void RestartOnClose()
        {
            Restart();
            _losePage.OnClose.RemoveListener(RestartOnClose);
        }

        _losePage.Open();
        _losePage.OnClose.AddListener(RestartOnClose);
    }
    
    public void NewGame(Level level)
    {
        DestroyLevel();
        CreateLevel(level);
    }

    public void Restart()
    {
        NewGame(Levels.Instance.Current);
    }

    public void UseHint()
    {
        var sameChips = _currentField?.GetHint();

        sameChips?.Item1.Highlight();
        sameChips?.Item2.Highlight();
    }

    public void Undo()
    {
        _currentField.Undo();
    }


    private void CreateLevel(Level level)
    {
        Field field = level.Field;
        IList<SuitSet> suitSets = level.SuitSets;
        List<Sprite> sprites = new List<Sprite>();

        _currentField = Instantiate(field, transform);
        foreach (SuitSet set in suitSets)
        {
            sprites.AddRange(set.Sprites);
        }
        _currentField.Init(sprites, this);
    }

    private void DestroyLevel()
    {
        if (_currentField != null)
        {
            Destroy(_currentField.gameObject);
        }
    }

    public void Mix()
    {
        _currentField?.Mix();
    }


    [SerializeField]
    private Field level = default;
    private void Start()
    {
        NewGame(Levels.Instance.Current);
        //CreateLevel(Levels.Instance.Current, level);

    }

    private void CreateLevel(Level level, Field _field)
    {
        Field field = _field;
        IList<SuitSet> suitSets = level.SuitSets;
        List<Sprite> sprites = new List<Sprite>();

        _currentField = Instantiate(field, transform);
        foreach (SuitSet set in suitSets)
        {
            sprites.AddRange(set.Sprites);
        }
        _currentField.Init(sprites, this);
    }
}
