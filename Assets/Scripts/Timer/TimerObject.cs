using UnityEngine;

namespace Timer
{
    public class TimerObject : MonoBehaviour
    {
        public Timer Timer { get; private set; } = default;


        private void Awake()
        {
            Timer = new Timer(this);
        }
    }
}