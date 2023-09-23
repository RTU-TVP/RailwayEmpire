#region

using Pathfinding;
using Units.Workers.State_Machine;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Units.Workers.States
{
    public class Move : IState
    {
        public Move(Animator animator, RichAI richAI, float speed)
        {
            _animator = animator;
            _richAI = richAI;
            _speed = speed;
        }

        private static readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly Animator _animator;
        private readonly RichAI _richAI;
        private readonly float _speed;
        private Transform _target;

        public void OnEnter()
        {
            _richAI.destination = _target.position;
            _richAI.SearchPath();
            _richAI.canMove = true;
            _richAI.maxSpeed = _speed;
            // _animator.SetBool(_IsMoving, true);
        }

        public void Tick()
        {}

        public void OnExit()
        {
            _richAI.canMove = false;
            // _animator.SetBool(_IsMoving, false);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }

}
