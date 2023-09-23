using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Workers.State_Machine;
using Workers.States;

namespace Workers
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private StateMachine _stateMachine;

        public void SetUp(Transform workTransform, Transform homeTransform, Transform shopTransform,
            UnityAction onWorkDone, float moveSpeed, float workTime, float saleTime)
        {
            var isWorkDone = false;
            var isSaleDone = false;

            _stateMachine = new StateMachine();

            var move = new Move(_animator, _navMeshAgent, moveSpeed);
            move.SetTarget(workTransform);

            var work = new Work(_animator, workTime);

            var sellingResources = new SellingResources(_animator, saleTime);

            #region Transitions

            At(move, work, () =>
            {
                if (!(work.WorkTime <= 0f)) return false;

                move.SetTarget(shopTransform);
                onWorkDone?.Invoke();
                isWorkDone = true;
                return true;
            });
            At(work, move, () =>
            {
                if (_navMeshAgent.remainingDistance > 0.5f || isWorkDone == true) return false;

                return true;
            });
            At(sellingResources, move, () =>
            {
                if (_navMeshAgent.remainingDistance > 0.5f || isWorkDone == false) return false;

                return true;
            });
            At(move, sellingResources, () =>
            {
                if (!(sellingResources.SaleTime <= 0f)) return false;

                move.SetTarget(homeTransform);
                isSaleDone = true;
                return true;
            });

            #endregion

            _stateMachine.SetState(move);
        }

        private void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        private void Update() => _stateMachine?.Tick();
    }
}
