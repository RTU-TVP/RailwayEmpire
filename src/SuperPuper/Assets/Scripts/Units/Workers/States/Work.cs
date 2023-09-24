#region

using Units.Workers.State_Machine;
using UnityEngine;

#endregion

namespace Units.Workers.States
{
    public class Work : IState
    {
        public Work(Animator animator, float workTime)
        {
            _animator = animator;
            WorkTime = workTime;
        }

        private static readonly int _isWorking = Animator.StringToHash("is_Working");
        private static readonly int _goWorking = Animator.StringToHash("go_Working");
        public float WorkTime { get; private set; }
        private readonly Animator _animator;

        public void OnEnter()
        {
            _animator.SetBool(_isWorking, true);
            _animator.SetTrigger(_goWorking);
        }

        public void Tick()
        {
            WorkTime -= Time.deltaTime;
        }

        public void OnExit()
        {
            _animator.SetBool(_isWorking, false);
        }
    }
}
