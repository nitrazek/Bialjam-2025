using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter3 : MonoBehaviour
{

    public BoxCollider2D m_Collider;

    enum PlayerState
    {
        Idle,
        Moving,
        Rolling
    }

    PlayerState currentState = PlayerState.Idle;

    Vector2 moveInput = Vector2.zero;
    private Vector2 currentVelocity = Vector2.zero;
    Camera cam;

    const float DEACCEL_SPEED = 0.1f;
    const float X_SPEED = 2f;
    const float Y_SPEED = 0.05f;

    private void OnEnable()
    {
        cam = Camera.main;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>() * X_SPEED;
        moveInput.y = 0;
    }

    void FixedUpdate()
    {
        float step = Time.deltaTime / DEACCEL_SPEED;
        currentVelocity = Vector2.MoveTowards(currentVelocity, moveInput, step);

        Vector3 delta = new Vector3(currentVelocity.x, currentVelocity.y, 0f) * X_SPEED * Time.deltaTime;
        Vector3 newPos = transform.position + delta;

        newPos = ClampPositionToCamera(newPos);

        transform.position = newPos;
           
        if (currentState == PlayerState.Idle)
        {
            StartRolling();
        }
    }
    Vector3 ClampPositionToCamera(Vector3 pos)
    {
        if (cam == null)
            return pos;

        float dist = Mathf.Abs(pos.z - cam.transform.position.z);

        float left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);

        return pos;
    }

    void StartRolling()
    {
        Vector3 roll_vec = new Vector3(0f, Y_SPEED, 0f);

        float colliderTopY = m_Collider.bounds.max.y - 0.1f;

        float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);
        float viewportTopY = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //Debug.Log($"colliderTopY: {colliderTopY}, viewportTopY: {viewportTopY}");

        if (viewportTopY < colliderTopY)
        {
            cam.transform.Translate(roll_vec);
        }

        transform.Translate(roll_vec);
    }
}
