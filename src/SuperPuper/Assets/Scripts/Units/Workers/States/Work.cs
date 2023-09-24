#region

using Units.Workers.State_Machine;
using UnityEngine;

#endregion

namespace Units.Workers.States
{
    public class Work : IState
    {
        public Work(AnimManager_worker animManagerWorker, float workTime)
        {
            _animManagerWorker = animManagerWorker;
            WorkTime = workTime;
        }

        public float WorkTime { get; private set; }
        private readonly AnimManager_worker _animManagerWorker;

        public void OnEnter()
        {
            _animManagerWorker.SwitchAnimationState("Working");
        }

        public void Tick()
        {
            WorkTime -= Time.deltaTime;
        }

        public void OnExit()
        {}
    }
}
