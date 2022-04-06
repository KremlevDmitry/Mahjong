using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;

public class Chip : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private readonly Vector3 SHIFT_VECTOR = new Vector3(.05f, -.05f, 0f);

    [HideInInspector]
    public static UnityEvent<Chip> OnDestroy = new UnityEvent<Chip>();
    [HideInInspector]
    public static UnityEvent<Chip> OnRestore = new UnityEvent<Chip>();

    public bool IsDestroyed { get; private set; } = false;

    public bool IsActive { get; private set; } = default;

    private Field _field = default;

    //TODO: сделать нормальную проверку
    private SpriteRenderer spriteRenderer = default;
    private SpriteRenderer _spriteRenderer => spriteRenderer ??= GetComponent<SpriteRenderer>();
    public Sprite Sprite
    {
        get => _spriteRenderer.sprite;
        set => _spriteRenderer.sprite = value;
    }
    //

    [SerializeField]
    private Chip[] _leftChips = default;
    [SerializeField]
    private Chip[] _rightChips = default;
    [SerializeField]
    private Chip[] _topChips = default;


    public void Init(Field field, Sprite sprite)
    {
        _field = field;
        _spriteRenderer.sprite = sprite;

        SetActive(IsEnable());
        OnDestroy.AddListener((chip) =>
        {
            if (_topChips.Contains(chip) || _leftChips.Contains(chip) || _rightChips.Contains(chip))
            {
                SetActive(IsEnable());
            }
        });
        OnRestore.AddListener((chip) =>
        {
            SetActive(IsEnable());
        });
    }

    public void SetSelect(bool isSelect)
    {
        _spriteRenderer.color = isSelect ? new Color(1f, .5f, .5f, 1f) : new Color(1f, 1f, 1f, 1f);
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
        _spriteRenderer.color = !isActive ? new Color(.5f, .5f, .5f, 1f) : new Color(1f, 1f, 1f, 1f);
    }

    public void Highlight()
    {
        void DeHighlight(Chip _)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            OnDestroy.RemoveListener(DeHighlight);
        }

        _spriteRenderer.color = new Color(1f, 1f, 0f, 1f);
        OnDestroy.AddListener(DeHighlight);
    }

    public void DeHighlight()
    {
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public bool IsEnable()
    {
        foreach (var chip in _topChips)
        {
            if (!(chip?.IsDestroyed ?? false)) { return false; }
        }

        bool isEnable = true;
        foreach (var chip in _leftChips)
        {
            if (!(chip?.IsDestroyed ?? true)) { isEnable = false; }
        }
        if (isEnable) { return true; }

        isEnable = true;
        foreach (var chip in _rightChips)
        {
            if (!(chip?.IsDestroyed ?? true)) { isEnable = false; }
        }
        if (isEnable) { return true; }
        return false;
    }

    public void Destroy()
    {
        SetSelect(false);

        IsDestroyed = true;
        OnDestroy.Invoke(this);

        gameObject.SetActive(false);
    }

    public void Restore()
    {
        IsDestroyed = false;
        OnRestore.Invoke(this);

        gameObject.SetActive(true);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsActive) { return; }
        _field.SelectChip(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsActive) { return; }
        transform.position += SHIFT_VECTOR;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsActive) { return; }
        transform.position -= SHIFT_VECTOR;
    }
}
