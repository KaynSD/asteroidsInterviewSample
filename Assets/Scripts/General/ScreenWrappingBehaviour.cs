using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class ScreenWrappingBehaviour : UpdateBehaviour
	{
		float capLX, capLY, capRX, capRY;

		void Awake() {
			Vector2 caps = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
			capRX = caps.x;
			capRY = caps.y;

			caps = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
			capLX = caps.x;
			capLY = caps.y;
		}
		
		protected override void OnUpdateExecution(float deltaTime)
		{
			Vector3 newLocation = transform.position;
			bool performMove = false;
			if(newLocation.x < capLX) {
				newLocation.x = capRX + (newLocation.x - capLX);
				performMove = true;
			} else if (newLocation.x >= capRX) {
				newLocation.x = capLX + (newLocation.x - capRX);
				performMove = true;
			}

			if(newLocation.y < capLY) {
				newLocation.y = capRY + (newLocation.y - capLY);
				performMove = true;
			} else if (newLocation.y >= capRY) {
				newLocation.y = capLY + (newLocation.y - capRY);
				performMove = true;
			}

			if(performMove) {
				transform.position = newLocation;
			}
		}
	}
}