using UnityEngine;

namespace UI.MainScene
{
    public class TimerOnTrainUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _sliderMoveRectTransform;
        [SerializeField] private GameObject _timerOnTrainPanel;

        public void SetSliderValue(float value)
        {
            _sliderMoveRectTransform.anchorMax = new Vector2(value, 1);
            _sliderMoveRectTransform.offsetMax = Vector2.zero;
        }

        public void SetActivePanel(bool value) => _timerOnTrainPanel.SetActive(value);
    }
}
