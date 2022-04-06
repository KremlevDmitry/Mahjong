using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SuitSet : ScriptableObject
{
    [SerializeField]
    private List<Sprite> _sprites = default;
    public IList<Sprite> Sprites => _sprites.AsReadOnly();
}
