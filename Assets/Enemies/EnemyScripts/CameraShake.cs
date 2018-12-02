using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	private float duration = 1.0f;
	private float magnitude = 0.05f;
	[SerializeField] GameObject m_camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator ShakeCamera() {
		Vector3 originalPosition = m_camera.transform.localPosition;
		float timeElapsed = 0.0f;
		float xShake = 0.0f;
		float yShake = 0.0f;

		while (timeElapsed < duration) {
			xShake = Random.Range (-1f, 1f) * magnitude;
			yShake = Random.Range (-1f, 1f) * magnitude;

			m_camera.transform.localPosition = new Vector3 (xShake, yShake, originalPosition.z);

			timeElapsed += Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();
		}

		m_camera.transform.localPosition = originalPosition; //Resets camera position 
	}
}
