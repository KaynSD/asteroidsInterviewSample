using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class GameLoopController : MonoBehaviour
	{
		[SerializeField] StartupBehaviour[] startupBehaviours;
		[SerializeField] UpdateBehaviour[] fixedUpdateBehaviours; 
		[SerializeField] UpdateBehaviour[] updateBehaviours;
		[SerializeField] TeardownBehaviour[] teardownBehaviours;

		public bool isRunning = false;

		public void StartGame() {
			if(isRunning) {
				EndGame();
			}
			foreach(StartupBehaviour sBehaviour in startupBehaviours) {
				sBehaviour.OnStart();
			}
			isRunning = true;
		}

		void Update()
		{
			if(!isRunning) {
				return;
			}
			float t = Time.deltaTime;
			foreach(UpdateBehaviour uBehaviour in updateBehaviours) {
				uBehaviour.PerformUpdate(t);
			}
		}

		void FixedUpdate() {
			if(!isRunning) {
				return;
			}
			float t = Time.fixedDeltaTime;
			foreach(UpdateBehaviour uBehaviour in fixedUpdateBehaviours) {
				uBehaviour.PerformUpdate(t);
			}
		}
		public void EndGame() {
			foreach(TeardownBehaviour tBehaviour in teardownBehaviours) {
				tBehaviour.OnTeardown();
			}
			isRunning = false;
		}
	}
}