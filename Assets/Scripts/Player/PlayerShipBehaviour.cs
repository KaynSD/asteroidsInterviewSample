using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class PlayerShipBehaviour : MonoBehaviour
	{
		public Collider2D collider2D {get; private set;}

		void Start() {
			collider2D = GetComponentInChildren<Collider2D>();
		}
		public void Reset() {
			GetComponent<PlayerFiringBehaviour>().isActive = true;
			GetComponent<VisualiseWrapAroundBehaviour>().Spawn();
			GetComponentInChildren<SpriteRenderer>().enabled = true;
		}

		public void Die() {
			GetComponent<PlayerFiringBehaviour>().isActive = false;
			GetComponent<VisualiseWrapAroundBehaviour>().Cleanup();
			GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}
}