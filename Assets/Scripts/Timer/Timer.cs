using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Timer
{
    public class Timer
    {
        public UnityEvent<Time> OnTimeSet = new UnityEvent<Time>();

        private MonoBehaviour _timerObject = default;

        private IEnumerator _timerCorouine = default;
        private Time _time = default;
        public Time Time => _time;

        public bool IsPlaing { get; private set; } = false;


        public Timer(MonoBehaviour timerObject)
        {
            _timerObject = timerObject;
            _timerCorouine = TimerCoroutine();
        }


        public void Start()
        {
            _timerObject.StartCoroutine(_timerCorouine);
            IsPlaing = true;
        }

        public void Pause()
        {
            _timerObject.StopCoroutine(_timerCorouine);
            IsPlaing = false;
        }

        public void Stop()
        {
            Pause();
            _timerCorouine = TimerCoroutine();
        }


        private IEnumerator TimerCoroutine()
        {
            var waitForSecond = new WaitForSeconds(1f);
            _time = Time.Zero;

            for (; ; )
            {
                OnTimeSet.Invoke(_time);
                yield return waitForSecond;

                _time.AddSecond();
            }
        }
    }
}