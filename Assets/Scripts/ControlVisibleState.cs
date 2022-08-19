using System;
using Interfaces;
using UnityEngine;


public class ControlVisibleState : MonoBehaviour
{
	[SerializeField] private Ground refObject;

	private IObject IObject => (IObject)refObject;

	private void OnBecameInvisible()
	{
		print("OnBecameInvisible");
		IObject.IsActive = false;
	}

	private void OnBecameVisible()
	{
		print("OnBecameVisible");
		IObject.IsActive = true;
	}
}
