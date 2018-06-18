using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour {

	public Light mainLight;

	void EnableLightShadow(bool isEnable) {		
		mainLight.shadows = isEnable ? LightShadows.Soft : LightShadows.None;
	}

	void RenderToImage() {
		var camera = GetComponent<Camera>();
		camera.Render();
		camera.targetTexture = null;
	}

	void Start() {
		EnableLightShadow(false);
		RenderToImage();
		EnableLightShadow(true);
		Destroy(gameObject);
	}
}
