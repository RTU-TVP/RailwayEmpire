#region

using System.Collections;
using System.Linq;
using UnityEngine;

#endregion

namespace Coal_Minigame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Spawner[] _spawnerList;

        [SerializeField] private Player _player;

        //[SerializeField] private AudioController _audioController;

        [SerializeField] private float _spawnTime = 1f;

        private bool _isStarted;

        private void OnEnable()
        {
            _player.PlayerDead += StopGame;
        }

        private void OnDisable()
        {
            _player.PlayerDead -= StopGame;
        }

        public void StartGame()
        {
            //_audioController.PlayMainMusic(true);
            _isStarted = true;
            _player.AllowMove(true);
            StartCoroutine(SpawnCoroutine());
        }

        private void StopGame()
        {
            //_audioController.PlayMainMusic(false);
            _isStarted = false;
            _player.AllowMove(false);
        }

        private void Spawn()
        {
            if (_isStarted)
            {
                _spawnerList[ChooseSpawnerNumber()].Spawn();
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            while (_isStarted)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnTime);
            }
        }

        private int ChooseSpawnerNumber()
        {
            return Random.Range(0, _spawnerList.Count());
        }
    }
}
