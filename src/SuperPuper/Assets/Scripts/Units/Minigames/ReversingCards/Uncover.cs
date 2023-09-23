#region

using UnityEngine;

#endregion

public class Uncover : MonoBehaviour
{
    [SerializeField] private MainCard _mainCard;
    private void OnMouseDown()
    {
        _mainCard.Reveal();
    }
}
