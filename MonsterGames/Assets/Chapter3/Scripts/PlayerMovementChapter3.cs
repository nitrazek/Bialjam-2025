using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter3 : MonoBehaviour
{
    public float speed = 2f;
    public float decelerationTime = 0.3f;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentVelocity = Vector2.zero;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log($"Player MoveInput: {moveInput}");
    }

    void Update()
    {
        float step = Time.deltaTime / decelerationTime;
        currentVelocity = Vector2.MoveTowards(currentVelocity, moveInput, step);

        Vector3 delta = new Vector3(currentVelocity.x, currentVelocity.y, 0f) * speed * Time.deltaTime;
        transform.position += delta;
    }
}
