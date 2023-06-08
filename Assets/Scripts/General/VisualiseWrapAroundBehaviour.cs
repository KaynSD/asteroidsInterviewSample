using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kevans.asteroids
{
	public class VisualiseWrapAroundBehaviour : UpdateBehaviour
	{
		[SerializeField] GameObject selfVisuals;
		float screenWidth, screenHeight;
		GameObject horizontalClone, verticalClone, diagonalClone;

		private Vector3 offsetX, offsetY, centerPoint;

		void Awake() {
			Vector3 br = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
			Vector3 tl = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

			centerPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));

			screenWidth = br.x - tl.x;
			screenHeight = br.y - tl.y;

			Transform parentObj = transform.parent;
			horizontalClone = Instantiate(selfVisuals, parentObj);
			verticalClone = Instantiate(selfVisuals, parentObj);
			diagonalClone = Instantiate(selfVisuals, parentObj);

			horizontalClone.name = $"{gameObject.name} (Horizontal Mirror)";
			verticalClone.name = $"{gameObject.name} (Vertical Mirror)";
			diagonalClone.name = $"{gameObject.name} (Diagonal Mirror)";

			offsetX = new Vector3(screenWidth, 0);
			offsetY = new Vector3(0, screenHeight);
		}

		protected override void OnUpdateExecution(float deltaTime) {
			Vector2 position = transform.position;
			Vector3 diagonalOffset = new Vector3();
			if(position.x > centerPoint.x) {
				horizontalClone.transform.position = transform.position - offsetX;
				diagonalOffset += offsetX;
			} else {
				horizontalClone.transform.position = transform.position + offsetX;
				diagonalOffset -= offsetX;
			}

			if(position.y > centerPoint.y) {
				verticalClone.transform.position = transform.position - offsetY;
				diagonalOffset += offsetY;
			} else {
				verticalClone.transform.position = transform.position + offsetY;
				diagonalOffset -= offsetY;
			}

			diagonalClone.transform.position = diagonalOffset;

			horizontalClone.transform.rotation = transform.rotation;
			verticalClone.transform.rotation = transform.rotation;
			diagonalClone.transform.rotation = transform.rotation;

			horizontalClone.transform.localScale = transform.localScale;
			verticalClone.transform.localScale = transform.localScale;
			diagonalClone.transform.localScale = transform.localScale;

			
		}

		public void Spawn() {
			horizontalClone.SetActive(true);
			verticalClone.SetActive(true);
			diagonalClone.SetActive(true);
		}

		public void Cleanup()
		{
			horizontalClone.SetActive(false);
			verticalClone.SetActive(false);
			diagonalClone.SetActive(false);
		}

		public GameObject[] GetVisualElements()
		{
			return new []{horizontalClone, verticalClone, diagonalClone};
		}
	}
}