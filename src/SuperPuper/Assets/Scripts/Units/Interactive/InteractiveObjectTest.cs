using UnityEngine;

namespace Interactive
{
	public class InteractiveObjectTest : InteractiveObject
	{
		protected override void OnMouseEnter()
		{
			Debug.Log("OnMouseEnter" + name);
		}

		protected override void OnMouseExit()
		{
			Debug.Log("OnMouseExit" + name);
		}
		
		protected override void OnMouseUpAsButton()
		{
			Debug.Log("OnMouseUpAsButton" + name);
		}
	}
}