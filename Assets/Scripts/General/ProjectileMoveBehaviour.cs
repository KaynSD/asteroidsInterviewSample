using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
namespace kevans.asteroids
{
	public class ProjectileMoveBehaviour : UpdateBehaviour
	{
		public float speed;
		public Vector3 bearing;

		protected override void OnUpdateExecution(float deltaTime)
		{
			if(bearing.magnitude != 1) {
				bearing = bearing.normalized;
			}
			transform.Translate(bearing * speed * deltaTime);
		}

	}
}