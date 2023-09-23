#region

using TMPro;
using Units.Minigames.Coal;
using UnityEngine;

#endregion

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _player.ScoreChanged += DisplayScore;
        }

        private void OnDisable()
        {
            _player.ScoreChanged -= DisplayScore;
        }

        private void DisplayScore(int score)
        {
            _scoreText.text = "" + score;
        }
    }
}
