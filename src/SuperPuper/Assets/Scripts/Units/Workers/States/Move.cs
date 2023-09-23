#region

using Units.Workers.State_Machine;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Units.Workers.States
{
    public class Move : IState
    {
        public Move(Animator animator, NavMeshAgent navMeshAgent, float speed)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _speed = speed;
        }

        private static readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _speed;
        private Transform _target;

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_target.position);
            _navMeshAgent.speed = _speed;
            _animator.SetBool(_IsMoving, true);
        }

        public void Tick()
        {}

        public void OnExit()
        {
            _navMeshAgent.ResetPath();
            _animator.SetBool(_IsMoving, false);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }

}
