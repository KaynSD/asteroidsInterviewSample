using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	[RequireComponent(typeof(ObjectPoolController))]
	public class ObjectPoolTeardownBehaviour : TeardownBehaviour
	{
		ObjectPoolController controller;
		void Start() {
			controller = GetComponent<ObjectPoolController>();
		}

		public override void OnTeardown()
		{
			controller.DestroyAll();
		}
	}
}