using UnityEngine;

public class MyPlayerInput : MonoBehaviour {

    public static MyPlayerInput Instance;

    public PlayerController playerController;

    public MyInputActions InputActions => inputActions;

    MyInputActions inputActions;

    void Awake() {
        Instance = this;
        inputActions = new MyInputActions();
        inputActions.Default.move.performed += playerController.OnMove;
        inputActions.Default.move.canceled += playerController.OnMove;
        inputActions.Default.mouse.performed += playerController.OnMouse;
        inputActions.Default.click.performed += playerController.OnClick;
    }

    void OnDestroy() {
        Instance = null;
    }

    void OnEnable() {
        inputActions.Enable();
    }

    void OnDisable() {
        inputActions.Disable();
    }
}
