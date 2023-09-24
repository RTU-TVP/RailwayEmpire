#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace UI
{
    public class VagonMenuButtons : MonoBehaviour
    {
        private UnityAction _onDoMyself;
        private UnityAction _onCallWorkers;

        public void SetActions(UnityAction onDoMyself, UnityAction onCallWorkers)
        {
            _onDoMyself = onDoMyself;
            _onCallWorkers = onCallWorkers;
        }

        public void ButtonDoMyself()
        {
            _onDoMyself?.Invoke();
        }
        public void ButtonCallWorkers()
        {
            _onCallWorkers?.Invoke();
        }
    }
}
