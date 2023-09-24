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
        public MoveToWork(Animator animator, RichAI richAI, float speed, Transform targetTransform)
        {
            _animator = animator;
            _richAI = richAI;
            _speed = speed;
            _targetTransform = targetTransform;
        }

        private static readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly Animator _animator;
        private readonly RichAI _richAI;
        private readonly float _speed;
        private readonly Transform _targetTransform;


        public void OnEnter()
        {
            _richAI.destination = _targetTransform.position;
            _richAI.SearchPath();
            _richAI.canMove = true;
            _richAI.maxSpeed = _speed;
            // _animator.SetBool(_IsMoving, true);
        }
        public void Tick() {}
        public void OnExit()
        {
            _richAI.canMove = false;
            // _animator.SetBool(_IsMoving, false);
        }
        
        public bool IsArrived()
        {
            return _richAI.reachedEndOfPath;
        }
    }

}
