using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class AnimManager_worker : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public readonly string[] AnimStates = new string[4] { "Idle", "Working", "Moving", "Happy" };

    private void Start()
    {
        SwitchAnimationState(AnimStates[0]);
        StartCoroutine(TestCaroutine());
    }

    public void SwitchAnimationState(string toState)
    {
        foreach (string stateName in AnimStates)
        {
            if (stateName == toState)
            {
                switch (stateName)
                {
                    case "Idle":
                        _animator.SetBool("is_" + stateName, true);
                        _animator.SetFloat("Idle_Variant", Random.Range(0, 3));
                        break;
                    case "Working":
                        _animator.SetBool("is_" + stateName, true);
                        _animator.SetFloat("Working_Variant", Random.Range(0, 1));
                        break;
                    default:
                        _animator.SetBool("is_" + stateName, true);
                        break;
                }
            }
            else
            {
                _animator.SetBool("is_" + stateName, false);
            }
        }
        _animator.SetTrigger("go_" + toState);
    }

    public void SetMovingBlend(float blendFactor)
    {
        if (0 <= blendFactor && blendFactor <= 1)
        {
            _animator.SetFloat("Moving_Blend", blendFactor);
        }
    }

    private IEnumerator TestCaroutine()
    {
        yield return new WaitForSeconds(10f);
        SwitchAnimationState(AnimStates[1]);
    }
}
