using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    [RequireComponent(typeof(Text))]
    public class ShowTimer : MonoBehaviour
    {
        private Text _text = default;
        [SerializeField]
        private TimerObject _timer = default;


        private void SetText(Time time)
        {
            _text.text = time.ToString();
        }


        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            SetText(_timer.Timer.Time);
            _timer.Timer.OnTimeSet.AddListener(SetText);
        }
    }
}