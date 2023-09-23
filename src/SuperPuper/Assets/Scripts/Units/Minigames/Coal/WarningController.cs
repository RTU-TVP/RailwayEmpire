using System.Collections;
using UnityEngine;

namespace Units.Minigames.Coal
{
    public class WarningController : MonoBehaviour 
    {
        [SerializeField] private GameObject WTL;
        [SerializeField] private GameObject WTR;
        [SerializeField] private GameObject WBL;
        [SerializeField] private GameObject WBR;
    
        public void ShowSec(int posNum)
        {
            switch (posNum)
            {
                case 0:
                    WTL.SetActive(true);
                    StartCoroutine(Delay(WTL));
                    break;
                case 1:
                    WTR.SetActive(true);
                    StartCoroutine(Delay(WTR));
                    break;
                case 2:
                    WBL.SetActive(true);
                    StartCoroutine(Delay(WBL));
                    break;
                case 3:
                    WBR.SetActive(true);
                    StartCoroutine(Delay(WBR));
                    break;
            }
        }

        IEnumerator Delay(GameObject sign)
        {
            yield return new WaitForSeconds(.5f);
            sign.SetActive(false);
        }
    }
}
