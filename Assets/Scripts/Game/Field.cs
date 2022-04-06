using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private Chip[] _chips = default;
    private Chip _currentChip = null;

    private Game _game = default;

    private List<Chip> _enableChips = new List<Chip>();
    private Stack<(Chip, Chip)> _prevMoves = new Stack<(Chip, Chip)>();


    public void Init(List<Sprite> sprites, Game game)
    {
        _game = game;

        int i = 0;
        Sprite currentSprite = default;

        foreach (Chip chip in _chips)
        {
            if (i == 0)
            {
                currentSprite = sprites[Random.Range(0, sprites.Count)];
                i--;
            }
            else { i++; }
            chip.Init(this, currentSprite);
        }

        Mix();
    }

    public void SelectChip(Chip chip)
    {
        if ((_currentChip != null) && (_currentChip != chip) && (_currentChip.Sprite == chip.Sprite))
        {
            _prevMoves.Push((chip, _currentChip));
            chip.Destroy();
            _currentChip.Destroy();
            _currentChip = null;

            //TODO: remove
            if (_enableChips.Count == 0)
            {
                _game.Win();
            }
            else if (!HasTurns())
            {
                _game.Lose();
            }
            //
        }
        else
        {
            if (_currentChip == chip)
            {
                _currentChip.SetSelect(false);
                _currentChip = null;
            }
            else
            {
                _currentChip?.SetSelect(false);
                chip.SetSelect(true);
                _currentChip = chip;
            }
        }
    }

    public void Mix()
    {
        if (_enableChips.Count < 2)
        {
            return;
        }

        do
        {
            for (int i = 0; i < _enableChips.Count; i++)
            {
                int rand = Random.Range(0, _enableChips.Count);
                var bufer = _enableChips[i].Sprite;

                _enableChips[i].Sprite = _enableChips[rand].Sprite;
                _enableChips[rand].Sprite = bufer;
            }
        }
        while (!HasTurns());
    }

    public (Chip, Chip)? GetHint()
    {
        var differentChips = new List<Chip>();

        foreach (Chip chip in _enableChips)
        {
            if (!chip.IsActive) { continue; }

            var containInDiffenent = false;
            foreach (Chip dChip in differentChips)
            {
                if (dChip.Sprite == chip.Sprite)
                {
                    containInDiffenent = true;
                }
            }
            if (!containInDiffenent)
            {
                differentChips.Add(chip);
            }
        }

        foreach (Chip chip in _enableChips)
        {
            if (!chip.IsActive) { continue; }

            foreach (Chip dChip in differentChips)
            {
                if ((chip != dChip) && (chip.Sprite == dChip.Sprite))
                {
                    return (chip, dChip);
                }
            }
        }

        return null;
    }

    public bool HasTurns() => GetHint() != null;

    public void Undo()
    {
        if (_prevMoves.Count == 0)
        {
            return;
        }

        var sameChips = _prevMoves.Pop();
        _enableChips.Add(sameChips.Item1);
        _enableChips.Add(sameChips.Item2);
        sameChips.Item1.Restore();
        sameChips.Item2.Restore();
    }


    private void MixChips()
    {
        for (int i = 0; i < _chips.Length; i++)
        {
            int rand = Random.Range(0, _chips.Length);
            Chip chip = _chips[i];
            _chips[i] = _chips[rand];
            _chips[rand] = chip;
        }
    }


    private void Awake()
    {
        foreach (var chip in _chips)
        {
            _enableChips.Add(chip);
        }
        Chip.OnDestroy.AddListener((chip) =>
        {
            _enableChips.Remove(chip);
        });

        MixChips();
    }

    [ContextMenu("Align Z Coordinate")]
    private void _AlignZCoordinate()
    {
        var zPosition = 0f;
        var zDelta = .0001f;

        foreach (Chip chip in _chips)
        {
            var position = chip.transform.localPosition;
            position.z = zPosition;
            chip.transform.localPosition = position;
            zPosition -= zDelta;
        }
    }

    [ContextMenu("Align XY Coordinates")]
    private void _AlignXYCoordinates()
    {
        float xDelta = 2f;
        float yDelta = 2.7f;

        //foreach (Chip chip in _chips)
        //{
        //    var position = chip.transform.localPosition;
        //    chip.transform.position = new Vector3(position.x * xDelta, position.y * yDelta, position.z);
        //}

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 position = new Vector3((i - 5 + 1f) * xDelta, (-(j - 6 + 1f) * yDelta), 0f);
                //Debug.Log((i - 5) + " " + (j - 6));
                //Debug.Log(i * 12 + j);
                //Debug.Log(position);
                _chips[j * 9 + i].transform.localPosition = position;
            }
        }
    }

    [ContextMenu("Fill Chips")]
    private void _FillChips()
    {
        _chips = GetComponentsInChildren<Chip>();
    }
}
