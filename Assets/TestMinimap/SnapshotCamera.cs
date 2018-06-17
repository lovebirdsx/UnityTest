using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour {
	
	void Start() {
		StartCoroutine(_WaitAndDestory());
	}

	IEnumerator _WaitAndDestory() {
		for (int i = 0; i < 3; i++) {
			yield return new WaitForFixedUpdate();
		}
		Camera camera = GetComponent<Camera>();
		camera.targetTexture = null;
		Destroy(gameObject);
	}
}
