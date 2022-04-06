using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class Music : Audio<Music>
    {
        protected override bool _defaultImportant => false;
    }
}