#region

using TMPro;
using UnityEngine;

#endregion

namespace Units.Minigames.TowerBlocks
{
    public class MovementSpawn : MonoBehaviour
    {
        public UnityEngine.Camera camaraPrincipal;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _score;

        private bool conditionMovementHorizontal = true;
        private bool conditionMovementVertical = true;

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space) && conditionMovementVertical)
            {
                transform.Translate(new Vector3(0, 1.9f, 0));
                camaraPrincipal.transform.Translate(new Vector3(0, 1.9f, 0));
                conditionMovementVertical = false;
            }
            if (transform.position.x <= 6 && conditionMovementHorizontal)
            {
                transform.Translate(Vector3.right * (Time.deltaTime * 5));
                if (transform.position.x >= 6)
                {
                    conditionMovementHorizontal = false;
                }
            }
            if (!conditionMovementHorizontal)
            {
                transform.Translate(-Vector3.right * (Time.deltaTime * 5));
                if (transform.position.x <= -6)
                {
                    conditionMovementHorizontal = true;
                }
            }
            conditionMovementVertical = true;
        }

        public void AddScore(int points)
        {
            _score += points;
            _scoreText.text = "Score: " + _score;
            if (_score == 10)
            {
                print("Winner Winner Chicken Dinner");
            }
        }
    }
}
