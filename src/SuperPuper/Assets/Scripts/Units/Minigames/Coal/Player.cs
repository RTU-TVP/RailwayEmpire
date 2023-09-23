#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Minigames.Coal
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;

        [SerializeField] private KeyCode _leftUpperKey = KeyCode.Q;
        [SerializeField] private KeyCode _leftLowerKey = KeyCode.A;
        [SerializeField] private KeyCode _rightUpperKey = KeyCode.E;
        [SerializeField] private KeyCode _rightLowerKey = KeyCode.D;

        [SerializeField] private Animator _playerAnimator;

        [SerializeField] private BoxCollider2D _upperCollider;
        [SerializeField] private BoxCollider2D _lowerCollider;

        //[SerializeField] private AudioController _audioController;

        [SerializeField] private int _healthCount = 3;
        [SerializeField] private int _score;

        private bool _canMove;

        private Transform _currentPosition;

        public UnityAction<int> HealthChanged;

        public UnityAction PlayerDead;
        public UnityAction<int> ScoreChanged;

        private void Start()
        {
            SetPosition(_positions[0]);
            _playerAnimator.SetBool("IsUpper", false);
            SetCollidersState(true);
            ScoreChanged?.Invoke(_score);
            HealthChanged?.Invoke(_healthCount);
        }

        private void Update()
        {
            MoveChecker();
        }

        public void AddScore(int count)
        {
            if (_canMove)
            {
                _score += count;
                //_audioController.PlayPointSound();
                ScoreChanged?.Invoke(_score);
            }
        }

        public void ApplyDamage(int health)
        {
            _healthCount -= health;
            //_audioController.PlayDamageSound();

            if (_healthCount <= 0)
            {
                PlayerDead?.Invoke();
                _healthCount = 0;
            }

            HealthChanged?.Invoke(_healthCount);
        }

        public void AllowMove(bool canMove)
        {
            _canMove = canMove;
        }

        private void SetPosition(Transform position)
        {
            _currentPosition = position;

            transform.position = _currentPosition.transform.position;
            transform.rotation = _currentPosition.transform.rotation;
            transform.localScale = _currentPosition.transform.localScale;
        }

        private void MoveChecker()
        {
            if (Input.GetKeyDown(_leftLowerKey))
                SetLeftLowerPosition();

            if (Input.GetKeyDown(_leftUpperKey))
                SetLeftUpperPosition();

            if (Input.GetKeyDown(_rightLowerKey))
                SetRightLowerPosition();

            if (Input.GetKeyDown(_rightUpperKey))
                SetRightUpperPosition();
        }

        private void SetCollidersState(bool isLower)
        {
            if (isLower)
            {
                _lowerCollider.enabled = true;
                _upperCollider.enabled = false;
            }
            else
            {
                _lowerCollider.enabled = false;
                _upperCollider.enabled = true;
            }
        }

        public void SetLeftLowerPosition()
        {
            if (_canMove)
            {
                SetPosition(_positions[0]);
                SetCollidersState(true);
                _playerAnimator.SetBool("IsUpper", false);
            }
        }

        public void SetLeftUpperPosition()
        {
            if (_canMove)
            {
                SetPosition(_positions[1]);
                SetCollidersState(false);
                _playerAnimator.SetBool("IsUpper", true);
            }
        }

        public void SetRightLowerPosition()
        {
            if (_canMove)
            {
                SetPosition(_positions[2]);
                SetCollidersState(true);
                _playerAnimator.SetBool("IsUpper", false);
            }
        }

        public void SetRightUpperPosition()
        {
            if (_canMove)
            {
                SetPosition(_positions[3]);
                SetCollidersState(false);
                _playerAnimator.SetBool("IsUpper", true);
            }
        }
    }
}
