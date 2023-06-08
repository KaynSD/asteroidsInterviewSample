using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace kevans.asteroids
{
	public class BulletBehaviour : UpdateBehaviour
	{
		[SerializeField] float lifetime = 2.0f;
		[SerializeField] float currentLifetime = 2.0f;
		LinkedPool<BulletBehaviour> pool;

		[SerializeField] UpdateBehaviour[] updateBehaviours;
		public bool isDead = false;
		
		void Start() {
			var list = new List<UpdateBehaviour>();
			list.Add(GetComponent<ProjectileMoveBehaviour>());
			list.Add(GetComponent<ScreenWrappingBehaviour>());
			updateBehaviours = list.ToArray();
		}

		public void Spawn(LinkedPool<BulletBehaviour> pool) {
			this.pool = pool;
			currentLifetime = lifetime;
			isDead = false;
		}
		protected override void OnUpdateExecution(float deltaTime)
		{
			foreach(UpdateBehaviour updateBehaviour in updateBehaviours) {
				updateBehaviour.PerformUpdate(deltaTime);
			}

			currentLifetime -= deltaTime;
			if(currentLifetime <= 0) {
				isDead = true;
			}
		}

		public void Remove() {
			pool.Release(this);
		}
	}
}