using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public abstract class UpdateBehaviour : MonoBehaviour
	{
		public bool isActive = true;
		public void PerformUpdate(float deltaTime) {
			if(isActive) {
				OnUpdateExecution(deltaTime);
			}
		}

		abstract protected void OnUpdateExecution(float deltaTime);
	}
}