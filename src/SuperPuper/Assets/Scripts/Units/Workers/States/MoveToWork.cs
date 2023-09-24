#region

using Pathfinding;
using Units.Workers.State_Machine;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Units.Workers.States
{
    public class MoveToWork : IState
    {
        public MoveToWork(AnimManager_worker animManagerWorker, RichAI richAI, float speed, Transform targetTransform)
        {
            _animManagerWorker = animManagerWorker;
            _richAI = richAI;
            _speed = speed;
            _targetTransform = targetTransform;
        }

        private readonly AnimManager_worker _animManagerWorker;
        private readonly RichAI _richAI;
        private readonly float _speed;
        private readonly Transform _targetTransform;


        public void OnEnter()
        {
            _richAI.destination = _targetTransform.position;
            _richAI.SearchPath();
            _richAI.canMove = true;
            _richAI.maxSpeed = _speed;
            _animManagerWorker.SwitchAnimationState("Moving");
        }
        public void Tick() {}
        public void OnExit()
        {
            _richAI.canMove = false;
        }

        public bool IsArrived()
        {
            return _richAI.reachedEndOfPath;
        }
    }

}
