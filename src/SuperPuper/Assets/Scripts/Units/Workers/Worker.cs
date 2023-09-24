#region

using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Units.Workers.State_Machine;
using Units.Workers.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

#endregion

namespace Units.Workers
{
    public class Worker : MonoBehaviour
    {
        private Animator _animator;
        private RichAI _richAI;
        private StateMachine _stateMachine;

        public bool isWorkDone = false;
        public bool isSaleDone = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _richAI = GetComponent<RichAI>();
        }

        public void SetUp(Transform workTransform, Transform homeTransform, Transform shopTransform,
            UnityAction onWorkDone, UnityAction onSaleDone, UnityAction onHomeDone,
            float moveSpeed, float workTime, float saleTime)
        {

            _stateMachine = new StateMachine();

            MoveToWork moveToWork = new MoveToWork(_animator, _richAI, moveSpeed, workTransform);
            MoveToSelling moveToSelling = new MoveToSelling(_animator, _richAI, moveSpeed, shopTransform);
            MoveToHome moveToHome = new MoveToHome(_animator, _richAI, moveSpeed, homeTransform);

            Work work = new Work(_animator, workTime);

            SellingResources sellingResources = new SellingResources(_animator, saleTime);

            #region Transitions

            At(work, moveToWork, () => moveToWork.IsArrived() && !isWorkDone);
            At(moveToSelling, work, () =>
            {
                if (work.WorkTime <= 0)
                {
                    isWorkDone = true;
                    onWorkDone?.Invoke();
                    return true;
                }
                return false;
            });
            At(sellingResources, moveToSelling, () => moveToSelling.IsArrived() && !isSaleDone);
            At(moveToHome, sellingResources, () =>
            {
                if (sellingResources.SaleTime <= 0)
                {
                    isSaleDone = true;
                    onSaleDone?.Invoke();
                    return true;
                }
                return false;
            });
            At(moveToWork, moveToHome, () =>
            {
                if (moveToHome.IsArrived())
                {
                    onHomeDone?.Invoke();
                    return false;
                }
                return false;
            });

            #endregion

            _stateMachine.SetState(moveToWork);

            StartCoroutine(Updated());
        }


        private IEnumerator Updated()
        {
            while (true)
            {
                _stateMachine.Tick();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void At(IState to, IState from, Func<bool> condition)
        {
            _stateMachine.AddTransition(to, from, condition);
        }
    }
}
