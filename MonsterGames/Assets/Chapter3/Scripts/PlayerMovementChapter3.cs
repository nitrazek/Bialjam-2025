using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter3 : MonoBehaviour
{

    public BoxCollider2D bgCollider;
    public Animator animator;

    enum PlayerState
    {
        Idle,
        Rolling,
        Hit,
    }

    PlayerState currentState = PlayerState.Idle;

    Vector2 moveInput = Vector2.zero;
    Vector2 hitTarget = Vector2.zero;
    private Vector2 currentVelocity = Vector2.zero;
    Camera cam;

    const float DEACCEL_SPEED = 2f;
    const float X_SPEED = 2f;
    const float Y_SPEED = 0.04f;

    private void OnEnable()
    {
        cam = Camera.main;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>() * X_SPEED;
        moveInput.y = 0;
    }
    void OnSpace(InputValue value)
    {
        if (currentState == PlayerState.Idle)
        {
            currentState = PlayerState.Rolling;
        }
    }

    void FixedUpdate()
    {
        Vector2 moveTowardsVec;
        if (currentState == PlayerState.Hit && hitTarget != Vector2.zero)
        {
            Vector2 playerPos2D = new Vector2(transform.position.x, 0f);
            moveTowardsVec = hitTarget - playerPos2D;
        }
        else
        {
            moveTowardsVec = moveInput;
        }

        float step = Time.deltaTime / DEACCEL_SPEED;
        currentVelocity = Vector2.MoveTowards(currentVelocity, moveTowardsVec, step);

        Vector3 delta = new Vector3(currentVelocity.x, currentVelocity.y, 0f) * X_SPEED * Time.deltaTime;
        Vector3 newPos = transform.position + delta;

        //newPos = ClampPositionToCamera(newPos);

        transform.position = newPos;
        
        if (currentState == PlayerState.Idle)
        {
            animator.speed = 0f;
        }
        else if (currentState == PlayerState.Hit)
        {
            animator.speed = .5f;
            HandleRolling();
        }
        else if (currentState == PlayerState.Rolling)
        {
            animator.speed = 1f;
            HandleRolling();
        }
    }

    Vector3 ClampPositionToCamera(Vector3 pos)
    {
        float dist = Mathf.Abs(pos.z - cam.transform.position.z);

        float left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);

        return pos;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name.Contains("Gutter"))
        {
            currentVelocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.name.Contains("Wall"))
        {
            currentVelocity = Vector2.zero;

            Bounds gutterBounds = col.bounds;
            Bounds playerBounds = GetComponent<Collider2D>().bounds;

            float pushX = 0f;
            if (playerBounds.center.x < gutterBounds.center.x)
            {
                pushX = gutterBounds.min.x - playerBounds.extents.x - .02f;
            }
            else
            {
                pushX = gutterBounds.max.x + playerBounds.extents.x + .02f;
            }

            transform.position = new Vector3(pushX, transform.position.y, transform.position.z);
            return;
        }
        else if (col.name.Contains("Gutter"))
        {
            if (currentState == PlayerState.Hit)
            {
                return;
            }

            currentState = PlayerState.Hit;

            Bounds gutterBounds = col.bounds;

            if (col.name.StartsWith("r"))
            {
                hitTarget = new Vector2(gutterBounds.min.x + .1f, 0);
            }
            else if (col.name.StartsWith("l"))
            {
                hitTarget = new Vector2(gutterBounds.max.x - .1f, 0);
            }
            return;
        }
    }
    void HandleRolling()
    {
        Vector3 roll_vec = new Vector3(0f, Y_SPEED, 0f);

        float colliderTopY = bgCollider.bounds.max.y - 0.1f;

        float dist = Mathf.Abs(transform.position.z - cam.transform.position.z);
        float viewportTopY = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        if (viewportTopY < colliderTopY)
        {
            cam.transform.Translate(roll_vec);
        }

        transform.Translate(roll_vec);
    }
}
