#region

using System.Collections;
using Data.Static;
using Services.Input;
using UnityEngine;

#endregion

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private InputActionsReader _inputActionsReader;
        [SerializeField] private CameraMovementRestriction _cameraMovementRestriction;
        [SerializeField] private Transform _targetTransform;
        private Coroutine _moveCoroutine;

        private Vector2 _movementInput;

        private void OnEnable()
        {
            _inputActionsReader.OnMovementAction += UpdateMovementInput;
        }

        private void OnDisable()
        {
            _inputActionsReader.OnMovementAction -= UpdateMovementInput;
        }

        private void UpdateMovementInput(Vector2 movement)
        {
            _movementInput = movement;

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            if (_movementInput != Vector2.zero)
            {
                _moveCoroutine = StartCoroutine(MoveCamera());
            }
        }

        private IEnumerator MoveCamera()
        {
            while (true)
            {
                Vector3 targetPosition = _targetTransform.position;
                Vector3 targetPositionNew = targetPosition + new Vector3(_movementInput.x, _movementInput.y, 0f);

                targetPositionNew.x = Mathf.Clamp(
                    targetPositionNew.x,
                    _cameraMovementRestriction.MinPosition.x,
                    _cameraMovementRestriction.MaxPosition.x);

                targetPositionNew.y = Mathf.Clamp(
                    targetPositionNew.y,
                    _cameraMovementRestriction.MinPosition.y,
                    _cameraMovementRestriction.MaxPosition.y);

                Vector3 smoothedPosition = Vector3.Lerp(
                    targetPosition,
                    targetPositionNew,
                    _cameraMovementRestriction.Speed * Time.deltaTime);

                _targetTransform.position = smoothedPosition;

                yield return null;
            }
        }
    }
}
