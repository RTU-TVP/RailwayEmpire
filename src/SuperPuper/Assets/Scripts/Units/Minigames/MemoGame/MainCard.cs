#region

using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

#endregion

namespace Units.Minigames.MemoGame
{
    public class MainCard : MonoBehaviour
    {

        [SerializeField] private SceneController controller;
        [SerializeField] private GameObject Card_Back;
        private Vector3 _startPosition;
        public int Id { get; private set; }
        [field: SerializeField]public bool isRevealed { get; private set; }

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void Reveal()
        {
            if (Card_Back.activeSelf && controller.canReveal && !isRevealed)
            {
                StartCoroutine(On());
            }
        }
        
        IEnumerator On()
        {
            
                //yield on a new YieldInstruction that waits for 5 seconds.
                controller.CardRevealed(this);
                StartCoroutine(RotateAnim(0));
                //GetComponentInChildren<ParticleSystem>().Play();
                yield return new WaitForSeconds(0.1f);
                transform.GetChild(1).gameObject.SetActive(true);
                isRevealed = true;
            
        }
        IEnumerator Off()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.

            yield return new WaitForSeconds(0.3f);
            StartCoroutine(RotateAnim(180));            isRevealed = false;
            //GetComponentInChildren<ParticleSystem>().Play(); 
            //transform.GetChild(1).gameObject.SetActive(false);
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

            Instantiate(gameObject, transform);
            //.SetActive(false);
            //This gets the sprite renderer component and changes the property of it's sprite!
        }

        public void Unreveal()
        {
            StartCoroutine(Off());
        }

        IEnumerator RotateAnim(int side)
        {
            Vector3 _needPos = new Vector3(_startPosition.x,_startPosition.y,transform.position.z-2.0f);
            transform.DOMove(_needPos,.5f);
            yield return new WaitForSeconds(.5f);
            transform.DORotate(new Vector3(0,side,0),.5f);
            yield return new WaitForSeconds(.5f);
            transform.DOMove(_startPosition,.5f);
        }
    }
}
