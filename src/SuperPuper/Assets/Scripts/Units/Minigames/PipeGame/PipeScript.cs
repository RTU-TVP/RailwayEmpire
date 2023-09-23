#region

using UnityEngine;

#endregion

public class PipeScript : MonoBehaviour
{
    [SerializeField] private PipesGameManager _pipesGameManager;
    [SerializeField] private int[] _correctRotation;
    [SerializeField] private int _currentRotation;
    [field: SerializeField] public bool IsPlaced { get; private set; }
    private readonly float[] rotations = { 0, 90, 180, 270 };

    private void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, rotations[rand], 0);
        _currentRotation = rand;

        foreach (float rot in _correctRotation)
        {
            if (_currentRotation == rot)
            {
                IsPlaced = true;
                _pipesGameManager.CorrectMove();
            }

        }

    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        _currentRotation += 1;
        if (_currentRotation > 3) _currentRotation = 0;
        foreach (float rot in _correctRotation)
        {
            if (_currentRotation == rot)
            {
                IsPlaced = true;
                _pipesGameManager.CorrectMove();
                break;
            }
            if (IsPlaced)
            {
                IsPlaced = false;
                _pipesGameManager.WrongMove();
            }
        }
    }
}
