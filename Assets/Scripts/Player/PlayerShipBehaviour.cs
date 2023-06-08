using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class PlayerShipBehaviour : MonoBehaviour
	{
		public Collider2D collider2D {get; private set;}
		public bool isDead = false;

		void Start() {
			collider2D = GetComponentInChildren<Collider2D>();
		}
		public void Reset() {
			isDead = false;
			GetComponent<PlayerFiringBehaviour>().isActive = true;
			GetComponent<VisualiseWrapAroundBehaviour>().Spawn();
			GetComponentInChildren<SpriteRenderer>().enabled = true;

			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		public void Die() {
			isDead = true;
			GetComponent<PlayerFiringBehaviour>().isActive = false;
			GetComponent<VisualiseWrapAroundBehaviour>().Cleanup();
			GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}
}