using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private InputActionAsset playerControls;

    private CharacterController characterController;
    private Camera mainCamera;
    private InputAction moveAction;
    private InputAction lookAction;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 currentMovement;
    private float verticalRotation;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        moveAction = playerControls.FindActionMap("Player3D").FindAction("Move");
        lookAction = playerControls.FindActionMap("Player3D").FindAction("Look");

        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
        lookAction.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => lookInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void OnEnable() {
        moveAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable() {
        moveAction.Disable();
        lookAction.Disable();
    }
    private void HandleMovement()
    {
        float verticalSpeed = moveInput.y * moveSpeed;
        float horizontalSpeed = moveInput.x * moveSpeed;
        Debug.Log($"Vertical Speed: {verticalSpeed}, Horizontal Speed: {horizontalSpeed}");

        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float mouseXRotation = lookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= lookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
