using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hitInfo;
    MyInputActions inputActions;

    Vector3 moveDir;
    Vector2 mousePosition;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        hitInfo = new RaycastHit();
        inputActions = new MyInputActions();
        inputActions.Default.move.performed += OnMove;
        inputActions.Default.move.canceled += OnMove;
        inputActions.Default.mouse.performed += OnMouse;
        inputActions.Default.click.performed += OnClick;
    }

    void OnClick(InputAction.CallbackContext ctx) {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo)) {
            agent.destination = hitInfo.point;
        }
    }

    void OnMouse(InputAction.CallbackContext ctx) {
        mousePosition = ctx.ReadValue<Vector2>();
        // Debug.Log($"OnMouse {ctx.phase} {ctx.performed} {mousePosition}");
    }

    void OnEnable() {
        inputActions.Enable();
    }
    
    void OnDisable() {
        inputActions.Disable();
    }

    void OnMove(InputAction.CallbackContext ctx) {
        var dir = ctx.action.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y).normalized;
        Debug.Log($"OnMove {ctx.phase} {ctx.action.ReadValue<Vector2>()} moveDir = {moveDir}");
    }

    void Update() {
        if (moveDir != Vector3.zero) {
            agent.destination = agent.transform.position + moveDir * 1f;
        }
    }

    void OnPoint(InputAction.CallbackContext ctx) {
        Debug.Log($"OnPoint {ctx.phase} {ctx.performed}");
        var ray = Camera.main.ScreenPointToRay(ctx.action.ReadValue<Vector2>());
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            agent.destination = hitInfo.point;
    }
}
