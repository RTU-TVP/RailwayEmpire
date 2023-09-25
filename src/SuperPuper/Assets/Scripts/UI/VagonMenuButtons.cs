#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace UI
{
    public class VagonMenuButtons : MonoBehaviour
    {
        [field: SerializeField] public GameObject ButtonDoMyselfGameObject { get; private set; }
        [field: SerializeField] public GameObject ButtonCallWorkersGameObject { get; private set; }

        private UnityAction _onDoMyself;
        private UnityAction _onCallWorkers;

        public void RegisterOnDoMyself(UnityAction onDoMyself) => _onDoMyself += onDoMyself;
        public void RegisterOnCallWorkers(UnityAction onCallWorkers) => _onCallWorkers += onCallWorkers;

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
