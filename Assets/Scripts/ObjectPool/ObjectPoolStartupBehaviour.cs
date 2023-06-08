using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	[RequireComponent(typeof(ObjectPoolController))]
	public class ObjectPoolStartupBehaviour : StartupBehaviour
	{
		ObjectPoolController controller;
		void Start() {
			controller = GetComponent<ObjectPoolController>();
		}

		public override void OnStart()
		{

		}
	}
}