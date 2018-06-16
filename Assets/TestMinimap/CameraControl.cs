using UnityEngine;

public class CameraControl : MonoBehaviour {
    float smoothTime = 0.1f;
    Vector3 distance;

    public Transform target;

    Vector3 velocity = Vector3.zero;

	void Start() {
		distance = transform.position - target.position;
	}

    void LateUpdate() {
        if (target != null) {
            Vector3 targetPosition = distance + target.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}