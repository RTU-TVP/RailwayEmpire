using UnityEngine;
using Workers.State_Machine;

namespace Workers.States
{
    public class Work : IState
    {
        public Work(Animator animator, float workTime)
        {
            _animator = animator;
            WorkTime = workTime;
        }

        private static readonly int _IsWorking = Animator.StringToHash("isWorking");
        private readonly Animator _animator;
        public float WorkTime { get; private set; }

        public void OnEnter()
        {
            _animator.SetBool(_IsWorking, true);
        }

        public void Tick()
        {
            WorkTime -= Time.deltaTime;
        }

        public void OnExit()
        {
            _animator.SetBool(_IsWorking, false);
        }
    }
}