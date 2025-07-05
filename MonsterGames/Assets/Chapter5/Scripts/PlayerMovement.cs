using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter5 : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput = Vector2.zero;
    private float _width = 0f;
    private float _height = 0f;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        animator.SetFloat("SpeedX", moveInput.x);
        animator.SetFloat("SpeedY", moveInput.y);
    }

    void Start()
    {
        _width = spriteRenderer.bounds.size.x;
        _height = spriteRenderer.bounds.size.y;
    }

    void Update() {
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + delta;
        newPos = ClampPositionToCamera(newPos);
        transform.position = newPos;
    }

    Vector3 ClampPositionToCamera(Vector3 pos) {
        Camera cam = Camera.main;
        if(cam == null)
            return pos;

        float dist = Mathf.Abs(pos.z - cam.transform.position.z);

        float left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + (_width / 2f);
        float right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - (_width / 2f);
        float bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + (_height / 2f);
        float top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - (_height / 2f);

        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);

        return pos;
    }
}
