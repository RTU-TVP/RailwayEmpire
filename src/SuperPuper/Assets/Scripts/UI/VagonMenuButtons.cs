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
            Debug.Log("Я сам разгружу вагон!");
        }
        public void ButtonCallWorkers()
        {
            _onCallWorkers?.Invoke();
            Debug.Log("Я позову рабочих!");
        }
    }
}
