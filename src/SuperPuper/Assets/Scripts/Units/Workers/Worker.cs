#region

using System;
using Units.Workers.State_Machine;
using Units.Workers.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

#endregion

namespace Units.Workers
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private StateMachine _stateMachine;
        private void Update()
        {
            _stateMachine?.Tick();
        }

        public void SetUp(Transform workTransform, Transform homeTransform, Transform shopTransform,
            UnityAction onWorkDone, UnityAction onSaleDone, UnityAction onHomeDone,
            float moveSpeed, float workTime, float saleTime)
        {
            bool isWorkDone = false;
            bool isSaleDone = false;

            _stateMachine = new StateMachine();

            Move move = new Move(_animator, _navMeshAgent, moveSpeed);
            move.SetTarget(workTransform);

            Work work = new Work(_animator, workTime);

            SellingResources sellingResources = new SellingResources(_animator, saleTime);

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
                if (_navMeshAgent.remainingDistance > 0.5f && isWorkDone == false && isSaleDone == false) return false;

                return true;
            });
            At(sellingResources, move, () =>
            {
                if (_navMeshAgent.remainingDistance > 0.5f && isWorkDone && isSaleDone == false) return false;

                return true;
            });
            At(move, sellingResources, () =>
            {
                if (!(sellingResources.SaleTime <= 0f)) return false;

                move.SetTarget(homeTransform);
                onSaleDone?.Invoke();
                isSaleDone = true;
                return true;
            });
            At(move, null, () =>
            {
                if (_navMeshAgent.remainingDistance > 0.5f && isWorkDone && isSaleDone) return false;

                onHomeDone?.Invoke();
                return true;
            });

            #endregion

            _stateMachine.SetState(move);
        }

        private void At(IState to, IState from, Func<bool> condition)
        {
            _stateMachine.AddTransition(to, from, condition);
        }
    }
}
