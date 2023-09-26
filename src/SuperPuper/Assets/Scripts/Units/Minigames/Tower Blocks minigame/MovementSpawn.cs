using System;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class MovementSpawn : MonoBehaviour
{
    private bool conditionMovementHorizontal = true;
    public CinemachineVirtualCamera cameraPrincipal;
    private int _score;
    public GameObject CubePrefab;
    private bool _canSpawn = true;
    private float _spawnTimer = 0;
    public bool _hasStarted;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private List<GameObject> _cubes = new();

    public event Action OnGameCompleted;
    public event Action OnGameLost;

    private void Start()
    {
        OnGameCompleted += DestroyCubes;
        OnGameLost += DestroyCubes;
    }
    private void DestroyCubes()
    {
        foreach (var cubeDestroy in _cubes)
        {
            Destroy(cubeDestroy);
        }
    }

    void Update()
    {
        if (_hasStarted)
        {
            Movement();
            UpdateTime();
        }
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canSpawn)
        {
            SpawnCube();
            transform.Translate(new Vector3(0, 1.9f, 0));
            cameraPrincipal.transform.Translate(new Vector3(0, 1.9f, 0));
        }
        if (transform.localPosition.x <= 6 && conditionMovementHorizontal)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 5));
            if (transform.localPosition.x >= 6)
            {
                conditionMovementHorizontal = false;
            }
        }
        if (!conditionMovementHorizontal)
        {
            transform.Translate(-Vector3.right * (Time.deltaTime * 5));
            if (transform.localPosition.x <= -6)
            {
                conditionMovementHorizontal = true;
            }
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _scoreText.text = "Score: " + _score;

        if (_score == 10)
        {
            print("Winner Winner Chicken Dinner");

            OnGameCompleted?.Invoke();
        }
    }

    private void UpdateTime()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= 1.0f)
        {
            _spawnTimer = 0;
            _canSpawn = true;
        }
    }

    private void SpawnCube()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canSpawn)
        {
            var cube = Instantiate(CubePrefab, transform.position, Quaternion.Euler(-90, 0, 90));
            _cubes.Add(cube);

            if (cube.TryGetComponent(out DefeatCondition defeatCondition))
            {
                defeatCondition.OnCubeFall += () =>
                {
                    print("Loose");
                    OnGameLost?.Invoke();
                };
            }

            _canSpawn = false;
        }
    }
}
