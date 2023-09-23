#region

using UnityEngine;

#endregion

namespace Units.Minigames.Coal
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomGravity : MonoBehaviour
    {

        // Global Gravity doesn't appear in the inspector. Modify it here in the code
        // (or via scripting) to define a different default gravity for all objects.

        static public float globalGravity = -9.81f;
        // Gravity Scale editable on the inspector
        // providing a gravity scale per object

        public float gravityScale = 1.0f;

        private Rigidbody m_rb;

        private void FixedUpdate()
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            m_rb.AddForce(gravity, ForceMode.Acceleration);
        }

        private void OnEnable()
        {
            m_rb = GetComponent<Rigidbody>();
            m_rb.useGravity = false;
        }
    }
}
