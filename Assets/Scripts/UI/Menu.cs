using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Page))]
public class Menu : MonoBehaviour
{
    [SerializeField]
    private Game _game = default;
    private Page _page = default;

    [SerializeField]
    private Page _levels = default;

    [SerializeField]
    private Button _newGameButton = default;
    [SerializeField]
    private Button _levelsButton = default;
    [SerializeField]
    private Button _restartButton = default;


    private void Awake()
    {
        _page = GetComponent<Page>();

        _newGameButton.onClick.AddListener(() =>
        {
            _page.Close();

            //TODO: move to Game.cs
            Levels.Instance.ResetProgress();
            _game.NewGame(Levels.Instance.Current);
            //
        });
        _levelsButton.onClick.AddListener(_levels.Open);
        _restartButton.onClick.AddListener(() =>
        {
            _page.Close();

            _game.Restart();
        });
    }
}
