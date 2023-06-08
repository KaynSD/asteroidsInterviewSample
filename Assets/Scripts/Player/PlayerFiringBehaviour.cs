using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace kevans.asteroids
{
	public class PlayerFiringBehaviour : UpdateBehaviour
	{
		[SerializeField] bool fire = false;
		[SerializeField] float cooldown = .1f;
		float currentCooldown = 0;

		ObjectPoolController bulletPool;

		void Awake() {
			//todo; replace with a service injector
			bulletPool = FindObjectOfType<ObjectPoolController>();
		}

		public void InputFire(CallbackContext context) {
			if(!isActive) return;
			
			switch(context.phase) {
				case UnityEngine.InputSystem.InputActionPhase.Started :
				case UnityEngine.InputSystem.InputActionPhase.Performed :
					fire = true;
					break;
				default :
					fire = false;
					break;
			}
		}

		protected override void OnUpdateExecution(float deltaTime) {
			if(currentCooldown > 0) {
				currentCooldown -= deltaTime;
				if(currentCooldown <= 0) {
					currentCooldown = 0;
				} else {
					return;
				}
			}

			if(fire) {
				currentCooldown = cooldown;
				BulletBehaviour bulletBehaviour = bulletPool.GetBullet();
				bulletBehaviour.transform.position = transform.position + transform.up * 0.25f;
				bulletBehaviour.GetComponent<ProjectileMoveBehaviour>().bearing = transform.up;
			}
		}
	}
}