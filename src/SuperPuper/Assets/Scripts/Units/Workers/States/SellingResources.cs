#region

using Units.Workers.State_Machine;
using UnityEngine;

#endregion

namespace Units.Workers.States
{
    public class SellingResources : IState
    {
        public SellingResources(Animator animator, float saleTime)
        {
            _animator = animator;
            SaleTime = saleTime;
        }

        private static readonly int _SellingResources = Animator.StringToHash("SellingResources");
        public float SaleTime { get; private set; }
        private readonly Animator _animator;

        public void OnEnter()
        {
            // _animator.SetBool(_SellingResources, true);
        }
        public void Tick()
        {
            SaleTime -= Time.deltaTime;
        }
        public void OnExit()
        {
            // _animator.SetBool(_SellingResources, false);
        }
    }
}
