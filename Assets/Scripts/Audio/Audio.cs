using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public abstract class Audio<T> : MonoBehaviour where T : Audio<T>
    {
        private static T _instance = null;

        [SerializeField]
        private List<Source> sources = new List<Source>();
        protected IList<Source> _sources => sources.AsReadOnly();

        protected Queue<Source> _sourcesQueue = new Queue<Source>();

        protected abstract bool _defaultImportant { get; }


        public static void Play(AudioClip clip, bool? important = null)
        {
            important ??= _instance._defaultImportant;

            
        }


        protected virtual void Awake()
        {
            _instance = (T)this;
        }

        protected virtual void Start()
        {
            foreach (Source source in sources)
                _sourcesQueue.Enqueue(source);
        }
    }
}
