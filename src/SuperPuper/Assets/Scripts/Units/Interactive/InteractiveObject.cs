using UnityEngine;

namespace Interactive
{
	[RequireComponent(typeof(Collider))]
	public abstract class InteractiveObject : MonoBehaviour
	{
		protected abstract void OnMouseEnter();
		protected abstract void OnMouseExit();
		protected abstract void OnMouseUpAsButton();
	}
}