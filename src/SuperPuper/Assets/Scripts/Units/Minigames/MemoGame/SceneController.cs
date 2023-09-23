#region

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class SceneController : MonoBehaviour
{

    public const int gridRows = 3;
    public const int gridCols = 4;
    [SerializeField] private float offsetX = 4f;
    [SerializeField] private float offsetY = 5f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private GameObject[] childObjects;

    //-------------------------------------------------------------------------------------------------------------------------------------------

    private MainCard _firstRevealed;

    private int _score;
    private MainCard _secondRevealed;
    private readonly int _targetScore = 6;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers); //This is a function we will create in a minute!

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard);
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, childObjects[id]);

                float posX = offsetX * i + startPos.x;
                float posY = offsetY * j + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score++;
            if (_score == _targetScore)
            {
                Debug.Log("Win");
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_001");
    }
}
