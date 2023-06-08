using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace kevans.asteroids
{
	public class CollisionLogicController : UpdateBehaviour
	{
		[SerializeField] ObjectPoolController objectPool;
		[SerializeField] PlayerShipBehaviour playerShip;
		protected override void OnUpdateExecution(float deltaTime)
		{
			BulletBehaviour[] bullets = objectPool.currentBullets.Where(x => !x.isDead).ToArray();
			AsteroidBehaviour[] asteroids = objectPool.currentAsteroids.Where(x => !x.isDead).ToArray();

			foreach(AsteroidBehaviour asteroidBehaviour in asteroids) {
				bool asteroidShot = false;
				foreach(BulletBehaviour bulletBehaviour in bullets) {
					Vector2 position = bulletBehaviour.transform.position;
					if(asteroidBehaviour.Collides(position)) {
						asteroidBehaviour.isDead = true;
						bulletBehaviour.isDead = true;

						asteroidBehaviour.Split();
						asteroidShot = true;
						break;
					}

					if(asteroidShot) {
						break;
					}
				}

				if(asteroidShot) {
					break;
				}

				if(playerShip.isDead) {
					continue;
				}

				if(asteroidBehaviour.Collides(playerShip.collider2D)) {
					playerShip.Die();
					asteroidBehaviour.Split();
					asteroidBehaviour.isDead = true;
				}
			}
		}
	}
}