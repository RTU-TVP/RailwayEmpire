#region

using UnityEngine;

#endregion

namespace Units.Minigames.MemoGame
{
    public class Uncover : MonoBehaviour
    {
        [SerializeField] private MainCard _mainCard;
        private void OnMouseDown()
        {
            _mainCard.Reveal();
        }
    }
}
