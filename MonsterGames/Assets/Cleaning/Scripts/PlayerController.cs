using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private float width = 100f;
    [SerializeField] private float height = 100f;
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private BoxCollider floor;

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
        EnforceFloorBoundary();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"PlayerController: OnTriggerEnter with {other.gameObject.name}");
        if(!other.CompareTag("Splatter") && !other.CompareTag("TrashBag")) return;
        Destroy(other.gameObject);
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

        currentMovement.x = Mathf.Clamp(horizontalMovement.x, -width / 2f, width / 2f);
        currentMovement.z = Mathf.Clamp(horizontalMovement.z, -width / 2f, width / 2f);
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

    private void EnforceFloorBoundary()
    {
        if (floor == null) return;

        float floorTopZ = floor.bounds.max.z;
        float floorBottomZ = floor.bounds.min.z;
        float floorLeftX = floor.bounds.min.x;
        float floorRightX = floor.bounds.max.x;

        Vector3 pos = transform.position;

        if (pos.z > floorTopZ)
        {
            pos.z = floorTopZ;
            characterController.enabled = false;
            transform.position = pos;
            characterController.enabled = true;
        }

        if (pos.z < floorBottomZ)
        {
            pos.z = floorBottomZ;
            characterController.enabled = false;
            transform.position = pos;
            characterController.enabled = true;
        }

        if (pos.x < floorLeftX)
        {
            pos.x = floorLeftX;
            characterController.enabled = false;
            transform.position = pos;
            characterController.enabled = true;
        }

        if (pos.x > floorRightX)
        {
            pos.x = floorRightX;
            characterController.enabled = false;
            transform.position = pos;
            characterController.enabled = true;
        }
    }
}
