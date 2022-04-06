using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chapter
{
    [SerializeField]
    private List<Level> _levels = default;
    public IList<Level> Levels => _levels.AsReadOnly();
}
