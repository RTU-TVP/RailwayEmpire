#region

using Units.Workers.State_Machine;
using UnityEngine;

#endregion

namespace Units.Workers.States
{
    public class SellingResources : IState
    {
        public SellingResources(AnimManager_worker animManagerWorker, float saleTime)
        {
            _animManagerWorker = animManagerWorker;
            SaleTime = saleTime;
        }

        public float SaleTime { get; private set; }
        private readonly AnimManager_worker _animManagerWorker;

        public void OnEnter()
        {
            _animManagerWorker.SwitchAnimationState("Idle");
        }
        public void Tick()
        {
            SaleTime -= Time.deltaTime;
        }
        public void OnExit()
        {
        }
    }
}
