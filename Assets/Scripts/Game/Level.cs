using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Level : ScriptableObject
{
    [SerializeField]
    private Field _field = default;
    public Field Field => _field;

    [SerializeField]
    private List<SuitSet> _suitSets = default;
    public IList<SuitSet> SuitSets => _suitSets.AsReadOnly();
}
