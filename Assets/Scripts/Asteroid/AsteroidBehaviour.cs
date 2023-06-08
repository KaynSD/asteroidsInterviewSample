using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace kevans.asteroids
{
	public class AsteroidBehaviour : UpdateBehaviour
	{
		[SerializeField] UpdateBehaviour[] updateBehaviours;
		ObjectPoolController controller;
		LinkedPool<AsteroidBehaviour> pool;
		public int size {get; protected set;}
		[SerializeField] int maxSpeed = 3;
		public bool isDead = false;

		void Start() {
			var list = new List<UpdateBehaviour>();
			list.Add(GetComponent<ProjectileMoveBehaviour>());
			list.Add(GetComponent<ScreenWrappingBehaviour>());
			list.Add(GetComponent<VisualiseWrapAroundBehaviour>());
			updateBehaviours = list.ToArray();
		}

		public void Spawn(ObjectPoolController controller, LinkedPool<AsteroidBehaviour> pool, int size, Vector2 bearing) {
			this.controller = controller;
			this.pool = pool;
			this.size = size;

			if(size == 0) {
				Remove();
				return;
			}

			Vector2 position = transform.position;
			Vector3 br = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
			Vector3 tl = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
			position.x = Mathf.Clamp(position.x, tl.x, br.x);
			position.y = Mathf.Clamp(position.y, tl.y, br.y);
			transform.position = position;

			GetComponent<VisualiseWrapAroundBehaviour>().Spawn();
			this.transform.localScale = Vector3.one * size * 0.5f;
			this.GetComponent<ProjectileMoveBehaviour>().bearing = bearing;
			this.GetComponent<ProjectileMoveBehaviour>().speed = (1f / (float)size) * maxSpeed;
		}

		public void Split() {
			if(size > 1) {
				for(int i = 0; i < 2; i++) {
					int newSize = size - 1;
					Vector3 bearing = new Vector2(GetRandomFloat(-1f, 1f), GetRandomFloat(-1f, 1f)).normalized;
					AsteroidBehaviour newSmallAsteroid = controller.GetAsteroid(newSize, bearing);
					newSmallAsteroid.transform.position = transform.position + (bearing * newSize * 0.25f);
				}
			}
		}
		
		protected override void OnUpdateExecution(float deltaTime)
		{
			foreach(UpdateBehaviour updateBehaviour in updateBehaviours) {
				updateBehaviour.PerformUpdate(deltaTime);
			}
		}

		public void Remove() {
			GetComponent<VisualiseWrapAroundBehaviour>().Cleanup();
			pool.Release(this);
		}
		
		private float GetRandomFloat(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		public bool Collides(Vector2 position)
		{
			if(GetComponentInChildren<Collider2D>().OverlapPoint(position)) return true;
			GameObject[] visualClones = GetComponent<VisualiseWrapAroundBehaviour>().GetVisualElements();
			foreach(GameObject clone in visualClones) {
				if(clone.GetComponentInChildren<Collider2D>().OverlapPoint(position)) return true;
			}
			return false;
		}

		public bool Collides(Collider2D playerCollider)
		{
			if(GetComponentInChildren<Collider2D>().IsTouching(playerCollider)) return true;
			GameObject[] visualClones = GetComponent<VisualiseWrapAroundBehaviour>().GetVisualElements();
			foreach(GameObject clone in visualClones) {
				if(clone.GetComponentInChildren<Collider2D>().IsTouching(playerCollider)) return true;
			}

			return false;
		}
	}
}