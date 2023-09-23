using UnityEngine;
using Workers.State_Machine;

namespace Workers.States
{
    public class SellingResources : IState
    {
        public SellingResources(Animator animator, float saleTime)
        {
            _animator = animator;
            SaleTime = saleTime;
        }

        private static readonly int _SellingResources = Animator.StringToHash("SellingResources");
        private readonly Animator _animator;
        public float SaleTime { get; private set; }

        public void OnEnter()
        {
            _animator.SetBool(_SellingResources, true);
        }
        public void Tick()
        {
            SaleTime -= Time.deltaTime;
        }
        public void OnExit()
        {
            _animator.SetBool(_SellingResources, false);
        }
    }
}
