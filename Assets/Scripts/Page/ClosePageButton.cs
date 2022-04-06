using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClosePageButton : MonoBehaviour
{
    private Button _button = default;

    [SerializeField]
    private Page _page = default;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(_page.Close);
    }

    private void OnDisable()
    {
        _button.onClick.AddListener(_page.Close);
    }
}
