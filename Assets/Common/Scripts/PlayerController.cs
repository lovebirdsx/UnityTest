using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    NavMeshAgent agent;
    RaycastHit hitInfo;

    Vector3 moveDir;
    Vector2 mousePosition;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        hitInfo = new RaycastHit();
    }

    public void OnClick(InputAction.CallbackContext ctx) {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo)) {
            agent.destination = hitInfo.point;
        }
        Debug.Log($"OnClick {ctx.phase} {ctx.performed} mouse pos = {mousePosition}");
    }

    public void OnMouse(InputAction.CallbackContext ctx) {
        if (ctx.phase == InputActionPhase.Canceled) {
            return;
        }

        mousePosition = ctx.ReadValue<Vector2>();
        Debug.Log($"OnMouse {ctx.phase} {ctx.performed} mouse pos = {mousePosition}");
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        var dir = ctx.action.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y).normalized;
        // Debug.Log($"OnMove {ctx.phase} {ctx.action.ReadValue<Vector2>()} moveDir = {moveDir}");
    }

    void Update() {
        if (moveDir != Vector3.zero) {
            agent.destination = agent.transform.position + moveDir * 1f;
        }
    }
}
