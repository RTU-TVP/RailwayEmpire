#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Interactive
{
    [RequireComponent(typeof(Collider))]
    public class InteractiveObject : MonoBehaviour
    {
        private UnityAction _onMouseEnter;
        private UnityAction _onMouseExit;
        private UnityAction _onMouseUpAsButton;

        private void OnMouseEnter()
        {
            _onMouseEnter?.Invoke();
        }
        private void OnMouseExit()
        {
            _onMouseExit?.Invoke();
        }
        private void OnMouseUpAsButton()
        {
            _onMouseUpAsButton?.Invoke();
        }

        public void RegisterMouseEnter(UnityAction action)
        {
            _onMouseEnter += action;
        }
        public void UnregisterMouseEnter(UnityAction action)
        {
            _onMouseEnter -= action;
        }

        public void RegisterMouseExit(UnityAction action)
        {
            _onMouseExit += action;
        }
        public void UnregisterMouseExit(UnityAction action)
        {
            _onMouseExit -= action;
        }

        public void RegisterMouseUpAsButton(UnityAction action)
        {
            _onMouseUpAsButton += action;
        }
        public void UnregisterMouseUpAsButton(UnityAction action)
        {
            _onMouseUpAsButton -= action;
        }
    }
}
