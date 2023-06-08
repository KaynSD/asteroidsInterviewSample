using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace kevans.asteroids
{
	public class TrggerGameStartController : MonoBehaviour
	{
		public GameLoopController gameLoopController;
		public void TriggerGameStart(CallbackContext context) {
			switch(context.phase) {
				case UnityEngine.InputSystem.InputActionPhase.Started :
					gameLoopController.StartGame();
					break;
			}

		}
	}
}