using UnityEngine;

namespace Units.ScenesManagers.MainGame
{
    public class MainGameManager : MonoBehaviour
    {
        public static MainGameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
