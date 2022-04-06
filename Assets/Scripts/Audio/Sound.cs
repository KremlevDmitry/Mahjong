using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class Sound : Audio<Sound>
    {
        protected override bool _defaultImportant => true;
    }
}