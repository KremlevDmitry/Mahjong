using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Source : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent OnStartPlaying = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnFinishPlaying = new UnityEvent();

        private AudioSource _audioSource = default;

        private IEnumerator _playing = default;


        public bool Mute
        {
            get => _audioSource.mute;
            set => _audioSource.mute = value;
        }
        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = Mathf.Clamp01(value);
        }
        public void Play(AudioClip clip, bool important = true)
        {
            if (important || !_audioSource.isPlaying)
            {
                if (_playing != null)
                {
                    StopCoroutine(_playing);
                    OnFinishPlaying.Invoke();
                }

                _audioSource.clip = clip;
                _audioSource.Play();
                StartCoroutine(_playing = Playing());
            }
        }


        private IEnumerator Playing()
        {
            OnStartPlaying.Invoke();

            yield return new WaitWhile(() => _audioSource.isPlaying);

            OnFinishPlaying.Invoke();
        }


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            OnFinishPlaying.AddListener(() => _playing = null);
        }
    }
}