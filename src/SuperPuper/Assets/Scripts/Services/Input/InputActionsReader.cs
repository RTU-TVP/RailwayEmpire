using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Services.Input
{
	[CreateAssetMenu(fileName = "InputActionsReader", menuName = "Services/Input/InputActionsReader")]
	public class InputActionsReader : ScriptableObject, InputActions.IDefaultInputActionsActions
	{
		private InputActions _inputActions;

		public UnityAction<Vector2> OnMovementAction;

		private void OnEnable()
		{
			if (_inputActions != null) return;

			_inputActions = new InputActions();
			_inputActions.DefaultInputActions.SetCallbacks(this);
			_inputActions.DefaultInputActions.Enable();
		}

		public void OnMovement(InputAction.CallbackContext context)
		{
			OnMovementAction?.Invoke(context.ReadValue<Vector2>());
		}
	}
}