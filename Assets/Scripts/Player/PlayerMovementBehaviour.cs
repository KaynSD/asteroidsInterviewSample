using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace kevans.asteroids
{
	public class PlayerMovementBehaviour : UpdateBehaviour
	{
		[SerializeField] float throttle = 0;
		[SerializeField] float rotate = 0;

		[SerializeField] float rotateSpeed = 100f;
		[SerializeField] float maxSpeed = 3.0f;
		[SerializeField] float forceMultiplier = 25f;

		Vector3 rotateAxis = new Vector3(0, 0, -1); 
		Vector2 moveAxis = new Vector2(0, 1); 
		Rigidbody2D localRigidbody;

		void Awake() {
			localRigidbody = GetComponent<Rigidbody2D>();
		}

		public void InputRotate(CallbackContext context) {
			switch(context.phase) {
				case UnityEngine.InputSystem.InputActionPhase.Started :
				case UnityEngine.InputSystem.InputActionPhase.Performed :
					rotate = context.ReadValue<float>();
					break;
				default :
					rotate = 0;
					break;
			}
		}

		public void InputThrottle(CallbackContext context) {
			switch(context.phase) {
				case UnityEngine.InputSystem.InputActionPhase.Canceled :
					throttle = 0;
					break;
				case UnityEngine.InputSystem.InputActionPhase.Started :
				case UnityEngine.InputSystem.InputActionPhase.Performed :
					throttle = Mathf.Clamp(context.ReadValue<float>(), 0, 1);
					break;
			}
		}

		protected override void OnUpdateExecution(float deltaTime) {
			transform.Rotate(rotateAxis * rotate * rotateSpeed * deltaTime);
			localRigidbody.AddForce(transform.up * throttle * deltaTime * forceMultiplier);
			if(localRigidbody.velocity.sqrMagnitude > maxSpeed) {
				localRigidbody.velocity *= 0.99f;
			}
		}
	}
}