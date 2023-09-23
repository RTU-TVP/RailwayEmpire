#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Coal_Minigame
{
    public class Egg : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _currentPosition;

        public UnityAction IsDestroyed;
        public UnityAction IsTaked;

        public float Speed
        {
            get
            {
                return _speed;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                IsTaked?.Invoke();
                Destroy(gameObject);
            }

            if (collision.tag == "Floor")
            {
                IsDestroyed?.Invoke();
                Destroy(gameObject);
            }
        }

        public void SetSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        public void SetEggPosition(Transform position)
        {
            _currentPosition = position;

            transform.position = _currentPosition.transform.position;
            transform.rotation = _currentPosition.transform.rotation;
            transform.localScale = _currentPosition.transform.localScale;
        }
    }
}
