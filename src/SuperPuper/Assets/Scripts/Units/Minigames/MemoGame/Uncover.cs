#region

using UnityEngine;

#endregion

namespace Minigames.MemoGame
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
