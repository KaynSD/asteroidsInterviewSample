using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class GameStartupSequence : StartupBehaviour
	{
		[SerializeField] PlayerShipBehaviour playerShip;
		[SerializeField] ObjectPoolController objectPool;
		public override void OnStart()
		{
			Vector2 centerPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
			playerShip.transform.position = centerPoint;
			playerShip.Reset();

			float minSpawnDistance = 5;
			float maxSpawnDistance = 8;
			int minSize = 2;
			int maxSize = 8;

			float theta, distance;
			Vector2 bearing;

			for(int i = 0; i < 4; i++) {
				theta = GetRandomFloat(0, Mathf.PI * 2);
				bearing = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
				AsteroidBehaviour asteroid = objectPool.GetAsteroid(GetRandomInt(minSize, maxSize), bearing);
				
				distance = GetRandomFloat(minSpawnDistance, maxSpawnDistance);

				Vector2 position = centerPoint + distance * bearing;
				asteroid.transform.position = position;
			}
		}

		private int GetRandomInt(int min, int max)
		{
			return UnityEngine.Random.Range(min, max);
		}
		
		private float GetRandomFloat(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}
	}
}