using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace kevans.asteroids
{
	[RequireComponent(typeof(ObjectPoolController))]
	public class ObjectPoolCleanupBehaviour : UpdateBehaviour
	{
		ObjectPoolController controller;
		void Start() {
			controller = GetComponent<ObjectPoolController>();
		}
		protected override void OnUpdateExecution(float deltaTime)
		{
			controller.Cleanup();
		}
	}
}