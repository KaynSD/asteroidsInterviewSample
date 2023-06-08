using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public abstract class TeardownBehaviour : MonoBehaviour
	{
		abstract public void OnTeardown();
	}
}