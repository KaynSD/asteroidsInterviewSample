using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
namespace kevans.asteroids
{
	public class ObjectPoolController : UpdateBehaviour
	{
		[SerializeField] GameObject BulletPrefab;
		[SerializeField] GameObject AsteroidPrefab;

		LinkedPool<BulletBehaviour> bulletObjectPool;
		LinkedPool<AsteroidBehaviour> asteroidObjectPool;

		List<BulletBehaviour> activeBullets;
		List<AsteroidBehaviour> activeAsteroids;

		public BulletBehaviour[] currentBullets => activeBullets.ToArray();
		public AsteroidBehaviour[] currentAsteroids => activeAsteroids.ToArray();

		int spawnCount = 0;

		void Awake() {
			activeBullets = new List<BulletBehaviour>();
			activeAsteroids = new List<AsteroidBehaviour>();

			bulletObjectPool = new LinkedPool<BulletBehaviour>(
				() => CreateObject<BulletBehaviour>(BulletPrefab),
				(obj) => ActivateObject<BulletBehaviour>(obj, activeBullets), 
				(obj) => DeactivateBullet<BulletBehaviour>(obj, activeBullets), 
				(obj) => {},
				false, 
				20
			);
			asteroidObjectPool = new LinkedPool<AsteroidBehaviour>(
				() => CreateObject<AsteroidBehaviour>(AsteroidPrefab),
				(obj) => ActivateObject<AsteroidBehaviour>(obj, activeAsteroids), 
				(obj) => DeactivateBullet<AsteroidBehaviour>(obj, activeAsteroids), 
				(obj) => {},
				false, 
				20
			);
		}

		private T CreateObject<T>(GameObject prefab) where T : UpdateBehaviour
		{
			T behaviour = Instantiate(prefab).GetComponent<T>();
			behaviour.gameObject.name = $"{spawnCount}";
			spawnCount++;
			return behaviour;
		}

		private void ActivateObject<T>(T newObject, List<T> list) where T : UpdateBehaviour
		{
			newObject.gameObject.SetActive(true);
			list.Add(newObject);
		}

		private void DeactivateBullet<T>(T newObject, List<T> list) where T : UpdateBehaviour
		{
			newObject.gameObject.SetActive(false);
			list.Remove(newObject);
			newObject.isActive = false;
		}

		public BulletBehaviour GetBullet()
		{
			BulletBehaviour bullet = bulletObjectPool.Get();
			bullet.Spawn(bulletObjectPool);
			bullet.isActive = true;
			bullet.isDead = false;
			return bullet;
		}
		public AsteroidBehaviour GetAsteroid(int size, Vector2 bearing)
		{
			AsteroidBehaviour asteroid = asteroidObjectPool.Get();
			asteroid.Spawn(this, asteroidObjectPool, size, bearing);
			asteroid.isActive = true;
			asteroid.isDead = false;
			return asteroid;
		}

		protected override void OnUpdateExecution(float deltaTime)
		{
			BulletBehaviour[] currentBullets = activeBullets.ToArray();
			AsteroidBehaviour[] currentAsteroids = activeAsteroids.ToArray();

			foreach(BulletBehaviour bullet in currentBullets) {
				bullet.PerformUpdate(deltaTime);
			}
			foreach(AsteroidBehaviour asteroid in currentAsteroids) {
				asteroid.PerformUpdate(deltaTime);
			}
		}

		public void Cleanup()
		{
			BulletBehaviour[] currentBullets = activeBullets.Where(x => x.isDead).ToArray();
			AsteroidBehaviour[] currentAsteroids = activeAsteroids.Where(x => x.isDead).ToArray();
			foreach(AsteroidBehaviour asteroid in currentAsteroids) {
				asteroid.Remove();
			}
			foreach(BulletBehaviour bullet in currentBullets) {
				bullet.Remove();
			}
		}

		public void DestroyAll()
		{
			BulletBehaviour[] currentBullets = activeBullets.ToArray();
			AsteroidBehaviour[] currentAsteroids = activeAsteroids.ToArray();
			foreach(AsteroidBehaviour asteroid in currentAsteroids) {
				asteroid.Remove();
			}
			foreach(BulletBehaviour bullet in currentBullets) {
				bullet.Remove();
			}
		}
	}
}