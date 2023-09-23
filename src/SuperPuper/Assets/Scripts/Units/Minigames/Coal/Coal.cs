#region

using UnityEngine;

#endregion

namespace Units.Minigames.Coal
{
    public class Coal : MonoBehaviour
    {
        public float _speed;
        private PlayerController _player;
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                _player.AddLostScore(1);
                Destroy(gameObject);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                _player.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
