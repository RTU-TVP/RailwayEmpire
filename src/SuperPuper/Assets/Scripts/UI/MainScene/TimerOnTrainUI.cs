using UnityEngine;

namespace UI.MainScene
{
    public class TimerOnTrainUI : MonoBehaviour
    {
        [SerializeField] public RectTransform _sliderMoveRectTransform;
        
        public void SetSliderValue(float value)
        {
            _sliderMoveRectTransform.anchorMax = new Vector2(value, 1);
            _sliderMoveRectTransform.offsetMax = Vector2.zero;
        }
    }
}
