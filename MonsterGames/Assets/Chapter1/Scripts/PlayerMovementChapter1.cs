using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementChapter1 : MonoBehaviour
{
    public float speed = 2f;
    public Animator animator;
    public BoxCollider2D[] colliderObjects;
    private Vector2 moveInput = Vector2.zero;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        animator.SetFloat("SpeedX", moveInput.x);
        animator.SetFloat("SpeedY", moveInput.y);
    }

    void Update()
    {
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + delta;

        newPos = ClampPositionToCamera(newPos);

        newPos = ClampPositionToObject(newPos);

        transform.position = newPos;
    }

    Vector3 ClampPositionToCamera(Vector3 pos)
    {
        Camera cam = Camera.main;
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

    Vector3 ClampPositionToObject(Vector3 pos)
    {
        if (colliderObjects == null)
            return pos;

        foreach (var col in colliderObjects)
        {
            if (col == null)
                continue;

            float closestY = Mathf.Abs(col.bounds.min.y) <= Mathf.Abs(col.bounds.max.y) ? col.bounds.min.y : col.bounds.max.y;
            Debug.Log($"closestY {closestY}");
            Debug.Log($"pos.y {pos.y}");

            if((closestY < 0 && closestY > pos.y) || (closestY > 0 && closestY < pos.y))
                pos.y = Mathf.Abs(pos.y) <= Mathf.Abs(closestY) ? pos.y : closestY;
            Debug.Log($"pos.y new {pos.y}");
        }

        return pos;
    }
}
