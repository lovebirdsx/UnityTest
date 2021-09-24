using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindPanel : MonoBehaviour {
    public InputActionReference actionReference;
    public GameObject startRebindObject;
    public GameObject waitingForInputObject;
    public PlayerInput playerInput;
    public Text keyText;

    InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    InputAction action;

    void RefreshKeyText() {
        int bindingIndex = action.GetBindingIndexForControl(action.controls[0]);
        keyText.text = InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    void Awake() {
        action = actionReference.action;
    }

    void Start() {
        RefreshKeyText();
    }

    public void StartRebinding() {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        //playerInput.SwitchCurrentActionMap("MenuOperation");
        rebindingOperation = action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => OnRebindComplete())
            .Start();
    }

    public void ResetBinding() {
        action.RemoveAllBindingOverrides();
        RefreshKeyText();
    }

    void OnRebindComplete() {
        RefreshKeyText();

        //playerInput.SwitchCurrentActionMap("Default");
        rebindingOperation.Dispose();
        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }
}
