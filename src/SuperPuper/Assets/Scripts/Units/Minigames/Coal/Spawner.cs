#region

using System.Collections;
using UnityEngine;

#endregion

namespace Units.Minigames.Coal
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Egg _egg;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform[] _eggPath;

        [SerializeField] private Player _player;

        public void Spawn()
        {
            Egg egg = Instantiate(_egg, _spawnPoint);
            egg.IsDestroyed += EggDestroyed;
            egg.IsTaked += EggTaked;
            StartCoroutine(MoveEgg(egg));
        }

        private void EggDestroyed()
        {
            _player.ApplyDamage(1);
        }

        private void EggTaked()
        {
            _player.AddScore(1);
        }

        private IEnumerator MoveEgg(Egg egg)
        {
            for (int i = 0; i < _eggPath.Length; i++)
            {
                if (egg != null)
                {
                    egg.SetEggPosition(_eggPath[i]);
                    yield return new WaitForSeconds(egg.Speed);
                }

                else
                    break;
            }
        }
    }
}
