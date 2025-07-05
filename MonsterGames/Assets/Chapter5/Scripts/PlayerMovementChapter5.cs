using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter5 : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 moveInput = Vector2.zero;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log($"Player MoveInput: {moveInput}");
    }

    void Update() {
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + delta;
        transform.position = newPos;
    }
}
