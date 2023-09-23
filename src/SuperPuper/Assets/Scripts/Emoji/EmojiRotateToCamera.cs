using UnityEngine;

namespace Emoji
{
    public class EmojiRotateToCamera : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(UnityEngine.Camera.main.transform);
        }
    }
}
