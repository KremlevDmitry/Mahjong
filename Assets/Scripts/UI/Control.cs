using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField]
    private Game _game = default;

    [SerializeField]
    private Page _settings = default;
    [SerializeField]
    private Page _menu = default;
    [SerializeField]
    private Page _daily = default;

    [SerializeField]
    private Button _settingsButton = default;
    [SerializeField]
    private Button _dailyButton = default;
    [SerializeField]
    private Button _menuButton = default;
    [SerializeField]
    private Button _restartButton = default;
    [SerializeField]
    private Button _hintButton = default;
    [SerializeField]
    private Button _mixButton = default;
    [SerializeField]
    private Button _undoButton = default;


    private void Awake()
    {
        _settingsButton.onClick.AddListener(_settings.Open);
        _dailyButton.onClick.AddListener(_daily.Open);
        _menuButton.onClick.AddListener(_menu.Open);
        _restartButton.onClick.AddListener(_game.Restart);
        _hintButton.onClick.AddListener(_game.UseHint);
        _mixButton.onClick.AddListener(_game.Mix);
        _undoButton.onClick.AddListener(_game.Undo);
    }
}
