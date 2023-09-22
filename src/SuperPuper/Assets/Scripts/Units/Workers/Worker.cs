using System;
using Data.Static.Workers;
using UnityEngine;
using UnityEngine.AI;
using Workers.State_Machine;
using Workers.States;

namespace Workers
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private WorkersConfiguration _workersConfiguration;
        private StateMachine _stateMachine;

        public void SetUp(Transform target, Transform home)
        {
            _stateMachine = new StateMachine();

            var move = new Move(_animator, _navMeshAgent, _workersConfiguration.MoveSpeedDefault);
            move.SetTarget(target);

            var work = new Work(_animator, _workersConfiguration.WorkTimeDefault);

            At(move, work, () =>
            {
                if (!(work.WorkTime <= 0f)) return false;

                move.SetTarget(home);
                return true;
            });
            At(work, move, () =>
            {
                if (!(_navMeshAgent.remainingDistance <= 0.5f)) return false;

                return true;

            });

            _stateMachine.SetState(move);
        }

        private void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        private void Update() => _stateMachine?.Tick();
    }
}
