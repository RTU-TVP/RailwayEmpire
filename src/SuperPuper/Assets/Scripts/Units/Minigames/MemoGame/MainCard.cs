#region

using UnityEngine;

#endregion

namespace Minigames.MemoGame
{
    public class MainCard : MonoBehaviour
    {

        [SerializeField] private SceneController controller;
        [SerializeField] private GameObject Card_Back;

        public int Id { get; private set; }

        public void Reveal()
        {
            if (Card_Back.activeSelf && controller.canReveal)
            {
                Card_Back.SetActive(false);
                controller.CardRevealed(this);
            }
        }


        public void ChangeSprite(int id, GameObject gameObject)
        {
            Id = id;

            int i = 1;
            while (i < transform.childCount)
            {

                i++;
                Destroy(transform.GetChild(1).gameObject);
            }
            Instantiate(gameObject, transform); //This gets the sprite renderer component and changes the property of it's sprite!
        }

        public void Unreveal()
        {
            Card_Back.SetActive(true);
        }
    }
}
