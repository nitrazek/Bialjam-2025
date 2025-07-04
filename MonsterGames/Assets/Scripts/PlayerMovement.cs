using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 500f;

    Rigidbody2D _rb;
    Vector2 _movement;

    public InputAction playerControls;

    private void OnEnable() => playerControls.Enable();
    private void OnDisable() => playerControls.Disable();
    void Awake() => _rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate() => _rb.AddForce(_movement * moveSpeed);

    private void Update() => _movement = playerControls.ReadValue<Vector2>();
}
