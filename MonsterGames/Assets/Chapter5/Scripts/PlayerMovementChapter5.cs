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

        float left = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float bottom = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float top = cam.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);

        return pos;
    }
}
