#region

using System.Collections;
using UnityEngine;

#endregion

namespace Units.Minigames.MemoGame
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
                GetComponentInChildren<ParticleSystem>().Play();
                StartCoroutine(On());
                controller.CardRevealed(this);
            }
        }
        
        IEnumerator On()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(0.3f);               
            transform.GetChild(1).gameObject.SetActive(true);
        }
        IEnumerator Off()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(0.5f);           
            GetComponentInChildren<ParticleSystem>().Play(); 
            yield return new WaitForSeconds(0.3f);     
            transform.GetChild(1).gameObject.SetActive(false);
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
            Instantiate(gameObject, transform).SetActive(false);
            //This gets the sprite renderer component and changes the property of it's sprite!
        }

        public void Unreveal()
        {
            StartCoroutine(Off());
        }
    }
}
