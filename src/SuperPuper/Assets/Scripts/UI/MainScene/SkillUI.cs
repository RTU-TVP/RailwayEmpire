using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        public void SetPrice(int price) => _priceText.text = price.ToString();
        public void SetLevel(int level) => _levelText.text = level.ToString();
        public void SetDescription(string description) => _descriptionText.text = description;
        public void SetIcon(Sprite icon) => _image.sprite = icon;
        public void SetButtonAction(Action action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action());
        }
    }
}
